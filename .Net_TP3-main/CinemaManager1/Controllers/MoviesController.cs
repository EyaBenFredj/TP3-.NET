﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManager1.Models.Cinema;

namespace CinemaManager1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaDbContext _context;

        public MoviesController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Movies.Include(m => m.Producer);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,ProducerId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id", movie.ProducerId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,ProducerId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        public async Task<IActionResult> MoviesAndTheirProds()
        {
            var moviesWithProducers = await _context.Movies
        .Include(m => m.Producer)
        .ToListAsync();

            return View(moviesWithProducers);
        }

        public async Task<IActionResult> MoviesAndTheirProds_UsingModels()
        {
            var moviesWithProducers = await (from m in _context.Movies
                                             join p in _context.Producers
                                             on m.ProducerId equals p.Id
                                             select new ProdMovie
                                             {
                                                 mTitle = m.Title,
                                                 mGenre = m.Genre,
                                                 pName = p.Name,
                                                 pNat = p.Nationality
                                             }).ToListAsync();

            return View(moviesWithProducers);
        }

        public IActionResult SearchByTitle(string critere)
        {
          
            var movies = string.IsNullOrEmpty(critere)
                         ? _context.Movies.Include(m => m.Producer).ToList()
                         : _context.Movies
                             .Where(m => m.Title.Contains(critere))
                             .Include(m => m.Producer)
                             .ToList();

            return View(movies);
        }
      
        public IActionResult SearchByGenre(string genre)
        {
           
            var movies = string.IsNullOrEmpty(genre)
                         ? _context.Movies.Include(m => m.Producer).ToList()
                         : _context.Movies
                             .Where(m => m.Genre.Contains(genre))
                             .Include(m => m.Producer)
                             .ToList();

           
            ViewBag.Genres = _context.Movies
                                      .Select(m => m.Genre)
                                      .Distinct()
                                      .OrderBy(g => g)
                                      .ToList()
                                      .Select(g => new { Value = g, Text = g })
                                      .ToList();

            return View(movies);
        }
        public IActionResult SearchBy2(string genre, string title)
        {
            var genres = _context.Movies
                                 .Select(m => m.Genre)
                                 .Distinct()
                                 .OrderBy(g => g)
                                 .ToList();
            genres.Insert(0, "All");
            var movies = _context.Movies.Include(m => m.Producer).AsQueryable();
            if (!string.IsNullOrEmpty(genre) && genre != "All")
            {
                movies = movies.Where(m => m.Genre == genre);
            }
            if (!string.IsNullOrEmpty(title))
            {
                movies = movies.Where(m => m.Title.Contains(title));
            }
            var filteredMovies = movies.ToList();

            ViewBag.Genres = new SelectList(genres);
            return View(filteredMovies);
        }

    }
}
