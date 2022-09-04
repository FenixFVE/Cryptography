using Cryptography;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

//KeyGenerator.CreateRussianKey("test.txt");

var encoder = new CryptoEncoder("test.txt");

encoder.TextFormater("input.txt", "formated.txt");
encoder.TextEncoder("formated.txt", "output.txt");

