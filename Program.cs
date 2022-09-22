
using Cryptography;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var language = Language.Russian;
// encode file
if (false)
{
    var keyGenerator = new RussianKeyGenerator();
    keyGenerator.CreateKey("key.txt");
    
    var key = FileManager.Read("key.txt");
    
    var encoder = new CryptoEncoder(key, language);
    encoder.EncodeFile("text.txt", "encoded_text.txt");
    
    var decoder = new CryptoDecoder(language);
    decoder.SetKey(key);
    decoder.DecodeFile("encoded_text.txt", "decoded_text.txt");
}

// try to decode big file
if (true)
{
    var analyzer = new FrequencyAnalyzer();
    analyzer.CreatSample("book2.txt", "book_data.txt");

    var key = FileManager.Read("key.txt");
    var encoder = new CryptoEncoder(key, language);
    encoder.EncodeFile("book.txt", "encoded_book.txt");

    var encodedText = FileManager.Read("encoded_book.txt");
    var decoded = analyzer.TryToDecode("book_data.txt", encodedText, language);
    FileManager.Write("decoded_book.txt", decoded);
}

