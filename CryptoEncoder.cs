
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Cryptography;

namespace Cryptography
{
    public class CryptoEncoder
    {
        private Dictionary<char, char> Encoder { get; }

        public CryptoEncoder(string keyFile)
        {
            using (var streamReader = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(streamReader, Encoding.UTF8))
            {
                var values = reader.ReadToEnd().ToList();
                if (values is null || values.Count != 33)
                {
                    throw new ArgumentException("key is invalid");
                }
                var keys = KeyGenerator.RussianAlphabet();
                Encoder = keys
                    .Zip(values, (k, v) => new { k, v })
                    .ToDictionary(x => x.k, x => x.v);
            }
        }
        public static void TextFormater(string rawFile, string formatedFile)
        {
            var rgx1 = new Regex(@"(\W|\d)");

            using (var streamWriter = new FileStream(formatedFile, FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(streamWriter, Encoding.UTF8))
            {
                using (var streamReader = new FileStream(rawFile, FileMode.Open, FileAccess.Read))
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

        public void TextEncoder(string formatedFile, string encodedFile)
        {
            using (var streamWriter = new FileStream(encodedFile, FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(streamWriter, Encoding.UTF8))
            {
                using (var streamReader = new FileStream(formatedFile, FileMode.Open, FileAccess.Read))
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
