
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Cryptography;

namespace Cryptography
{
    public class CryptoEncoder
    {
        private Dictionary<char, char> Encoder { get; set; }

        public CryptoEncoder(string keyFile)
        {
            using (var streamReader = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(streamReader, Encoding.UTF8))
            {
                List<char> russianAlphabet = KeyGenerator.RussianAlphabet();
                List<char> key = reader.ReadToEnd().ToList();
                Encoder = new Dictionary<char, char>(russianAlphabet.Count);
                for (int i = 0; i < russianAlphabet.Count; i++)
                {
                    Encoder.Add(russianAlphabet[i], key[i]);
                }
            }
        }
        public void TextFormater(string inputFile, string outputFile)
        {
            var rgx1 = new Regex(@"(\W|\d)");

            using (var streamWriter = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(streamWriter, Encoding.UTF8))
            {
                using (var streamReader = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(streamReader, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        string result = rgx1.Replace(line, String.Empty);
                        result = result.ToLower();
                        //var t = Encoder.Where()
                        writer.Write(result);
                    }
                }
            }

 
        }

        public void TextEncoder(string inputFile, string outputFile)
        {
            using (var streamWriter = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(streamWriter, Encoding.UTF8))
            {
                using (var streamReader = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(streamReader, Encoding.UTF8))
                {
                    string text = reader.ReadToEnd();
                    StringBuilder sb = new StringBuilder();
                    sb.Capacity = text.Length;
                    foreach (char ch in text)
                    {
                        sb.Append(Encoder[ch]);
                    }
                    writer.Write(sb.ToString());
                }
            }
        }

    }
}
