
using Cryptography;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var language = Language.Russian;

//var keyGenerator = new RussianKeyGenerator();
//keyGenerator.CreateKey("key.txt");

var key = FileManager.Read("key.txt");

var encoder = new CryptoEncoder(key, language);
encoder.EncodeFile("text.txt", "encoded_text.txt");

var decoder = new CryptoDecoder(language);
decoder.SetKey(key);
decoder.DecodeFile("encoded_text.txt", "decoded_text.txt");