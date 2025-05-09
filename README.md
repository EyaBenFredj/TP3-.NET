
Overview
This project is a Java-based practical work (Travaux Pratiques - TP3) that focuses on object-oriented programming and string manipulation. It includes two main exercises:

- Exercise 1: File Handling and String Manipulation
- Exercise 2: XOR Encryption and Decryption

Each exercise is implemented in its own Java class for modularity and clarity.

📁 Exercise 1: FileHandler Class
This exercise involves creating a FileHandler class to read and manipulate text files. It provides various string processing utilities including line pattern matching, string comparison, and character manipulation.

Class: FileHandler

Attributes:

- filePath (String): Path to the input file.
- fileSizeInKb (Integer): Size of the file in kilobytes.
- numberOfLines (Integer): Number of lines in the file.
- fileContent (ArrayList<String>): Stores file content line by line.

Constructor:

- Accepts a file path and validates its existence.
- Initializes all attributes if the path is valid.
- Displays an error message if the file does not exist.

Methods:

- readFile(): Reads file content line by line and stores it in fileContent.
- findLinesWithPattern(String pattern): Returns all lines containing a specific substring.
- compareStrings1(String str1, String str2): Compares strings using equals().
- compareStrings2(String str1, String str2): Compares strings using == and != operators.
- compareStrings3(String str1, String str2): Compares strings using equalsIgnoreCase().
- reverseString(int lineIndex): Reverses the characters of a specific line.
- removeDuplicates(int lineIndex): Removes duplicate characters from a specific line while preserving order.

Comparison Insights:

- equals(): Compares content of strings (case-sensitive).
- equalsIgnoreCase(): Compares content ignoring case.
- == and !=: Compare object references (not content).

Sample Usage in Main Method:

public class TP3 {
    public static void main(String[] args) {
        FileHandler fileHandler = new FileHandler("file.log");
        fileHandler.reverseString(1);
        fileHandler.removeDuplicates(2);

        ArrayList<String> linesWithPattern = fileHandler.findLinesWithPattern("epoch");

        System.out.println("Comparaison avec equals : " + fileHandler.compareStrings1("hello", "Hello"));
        System.out.println("Comparaison avec equalsIgnoreCase : " + fileHandler.compareStrings3("hello", "HELLO"));
        System.out.println("Comparaison avec == : " + fileHandler.compareStrings2("hello", "hello"));
        System.out.println("Comparaison avec == : " + fileHandler.compareStrings2(new String("hello"), "hello"));
        System.out.println("Comparaison avec == : " + fileHandler.compareStrings2(new String("hello"), new String("hello")));

        System.out.println("Ligne contenant le motif 'epoch' : " + linesWithPattern.get(0));
    }
}

📁 Exercise 2: XORCipher Class
This exercise introduces a basic encryption technique using the XOR bitwise operator to encrypt and decrypt strings.

Class: XORCipher

Attributes:

- key (String): The encryption key used for both encryption and decryption.

Constructor:

- Initializes the encryption key.

Methods:

- encrypt(String plaintext): Encrypts the plaintext using XOR with the key.
- decrypt(String ciphertext): Decrypts the ciphertext using XOR with the key.

How XOR Works:

- Each character in the plaintext is XORed with a character from the key (looped as necessary).
- XORing again with the same key decrypts the message.

Sample Usage in Main Method:

public class TP3 {
    public static void main(String[] args) {
        String key = "secret";
        String plaintext = "Hello, world";

        XORCipher xorCipher = new XORCipher(key);

        String encryptedText = xorCipher.encrypt(plaintext);
        System.out.println("Texte chiffré : " + encryptedText);

        String decryptedText = xorCipher.decrypt(encryptedText);
        System.out.println("Texte déchiffré : " + decryptedText);
    }
}

📦 Project Structure

TP3/
│
├── FileHandler.java
├── XORCipher.java
├── TP3.java
├── README.md
└── file.log (Sample input file for testing)

🛠 Requirements

- Java JDK 8 or higher
- Any standard IDE (Eclipse, IntelliJ IDEA, NetBeans)
- Sample input files for testing (e.g., file.log)

✅ How to Run

1. Compile all Java files:
   javac *.java

2. Run the TP3 main class:
   java TP3

📚 Notes

- Ensure that the input files (like file.log) exist in the correct directory before running.
- The XORCipher class can be extended for more advanced encryption schemes.
- String comparison techniques are essential to understand nuances in Java object handling.

📖 License

This project is intended for educational use only as part of coursework for Programmation Orientée Objet et Java (2023/2024).

Let me know if you'd like this README saved as a Markdown (.md) file!
