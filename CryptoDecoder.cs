
using System;
using System.Text;
using Cryptography;

namespace Cryptography
{
    
    public class CryptoDecoder
    {
        private Dictionary<char, char> Decoder { get; }

        public CryptoDecoder() 
        {
            Decoder = new Dictionary<char, char>(33);
        }

        public CryptoDecoder(string keyFile)
        {
            using (var streamReader = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(streamReader, Encoding.UTF8))
            {
                var keys = reader.ReadToEnd().ToList<char>();
                if (keys is null || keys.Count != 33)
                {
                    throw new ArgumentException("key is invalid");
                }
                var values = KeyGenerator.RussianAlphabet();
                Decoder = keys
                    .Zip(values, (k, v) => new {k, v})
                    .ToDictionary(x => x.k, x => x.v);
            }
        }

        public void TextDecoder(string encodedFile, string decodedFile)
        {
            if (Decoder.Count != 33)
            {
                throw new Exception("There is no valid key");
            }
            // TO DO
        }
        /*
        public void CountFrequency(string sampleText)
        {

            using (var streamReader = new FileStream(sampleText, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(streamReader, Encoding.UTF8))
            {
                string text = reader.ReadToEnd();
                var frequency = text
                    .GroupBy(x => x)
                    .ToDictionary(g => g.Key, g => g.Count())
                    .OrderBy(x => x.Value)
                    .ToList();
            }
            foreach (var (a, b) in _frequency)
            {
                Console.WriteLine($"{a}: {b}");
            }

        }
        */
    }
}
