using Cryptography;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

//KeyGenerator.CreateRussianKey("test.txt");

var encoder = new CryptoEncoder("test.txt");

//encoder.TextFormater("input.txt", "formated.txt");
//encoder.TextEncoder("formated.txt", "output.txt");


CryptoEncoder.TextFormater("book.txt", "formated_book.txt");
var decoder = new CryptoDecoder();
decoder.CountFrequency("formated_book.txt");

