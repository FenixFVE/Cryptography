
using System.Text;

namespace Cryptography
{
    public class FrequencyAnalyzer
    {
        private const int _size = 1;
        public List<string> Count(string text)
        {
            var frequency = new Dictionary<string, int>(text.Length);
            for (int i = 0; i < text.Length - _size + 1; i++)
            {
                var key = text.Substring(i, _size);
                if (frequency.ContainsKey(key))
                {
                    frequency[key]++;
                }
                else
                {
                    frequency.Add(key, 1);
                }
            }
            var triplets = from entry in frequency orderby entry.Value descending select entry.Key;
            return triplets.ToList();
        }
        public List<string> ReadSample(string dataFile) =>
            FileManager
                .Read(dataFile)
                .Split("\n")
                .ToList();

        public void CreatSample(string textFile, string dataFile)
        {
            var rawText = FileManager.Read(textFile);
            var formatedText = CryptoEncoder.TextFormater(rawText);
            var data = Count(formatedText);
            using (var streamWriter = new FileStream(dataFile, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(streamWriter, Encoding.UTF8))
            {
                foreach (string str in data)
                {
                    writer.WriteLine(str);
                }
            }
        }

        public string GetKey(List<string> sample, List<string> encrypted, Language language)
        {
            var alphabet = KeyGenerator.KeyGeneratorForLanguage(language).Alphabet();
            var keys = new List<char>((int)language);
            int j = _size - 1;
            foreach (char letter in alphabet)
            {
                for (int i = 0; i < sample.Count && i < encrypted.Count; i++)
                {
                    if (sample[i][j] == letter)
                    {
                        keys.Add(encrypted[i][j]);
                        break;
                    }
                }
            }
            return new string(keys.ToArray());
        }

        public string TryToDecode(string sampleFile, string encodedText, Language language)
        {
            var sample = ReadSample(sampleFile);
            var encodedSample = Count(encodedText);
            var key = GetKey(sample, encodedSample, language);
            var decoder = new CryptoDecoder(key, language);
            var decodedText = decoder.TextDecoder(encodedText);
            return decodedText;
        }
    }
}
