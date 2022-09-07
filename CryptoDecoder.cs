
using System.Text;

namespace Cryptography
{
    public class CryptoDecoder
    {
        private bool _haveKey { get; set; }
        private Dictionary<char, char> _decoder { get; set; }
        private Language _language { get; }

        public CryptoDecoder(Language language)
        {
            _haveKey = false;
            _language = language;
            _decoder = new Dictionary<char, char>((int)_language);
        }

        public CryptoDecoder(string key, Language language)
        {
            _haveKey = true;
            _language = language;
            _decoder = new();
            SetKey(key);
        }
        public string TextDecoder(string Data)
        {
            if (!_haveKey)
            {
                throw new Exception("There is no key");
            }
            StringBuilder builder = new StringBuilder();
            builder.Capacity = Data.Length;
            foreach (char ch in Data)
            {
                builder.Append(_decoder[ch]);
            }
            return builder.ToString();
        }
        public void DecodeFile(string encodedFile, string decodedFile)
        {
            var encodedData = FileManager.Read(encodedFile);
            var decodedData = TextDecoder(encodedData);
            FileManager.Write(decodedFile, decodedData);
        }

        public void SetKey(string key)
        {
            //Console.WriteLine(key);
            var keys = key.ToList<char>();
            if (keys.Count != (int)_language)
            {
                throw new ArgumentException("Key is invalid");
            }
            var values = KeyGenerator.KeyGeneratorForLanguage(_language).Alphabet();
            _decoder = keys
                .Zip(values, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);
            _haveKey = true;
        }

        public static string FindKey()
        {
            throw new NotImplementedException(); // TO DO
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
