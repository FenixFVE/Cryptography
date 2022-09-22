
using System.Text;

namespace Cryptography
{
    public class FrequencyAnalyzer
    {
        private const int _size = 2;
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

        public string GetKey2(List<string> sample, List<string> encryted, Language language)
        {

            var indexes = KeyGenerator
                .KeyGeneratorForLanguage(language)
                .Alphabet()
                .Zip(
                    Enumerable
                        .Range(0, (int)language)
                        .ToList(), 
                    (k, v) => new { k, v }
                )
                .ToDictionary(x => x.k, x => x.v);

            var keys = Enumerable.Repeat('\0', (int)language).ToList();

            for (int i = 0; i < sample.Count && i < encryted.Count; i++)
            {
                bool isFit = true;
                for (int j = 0; j < _size; j++)
                {
                    char ch = keys[indexes[sample[i][j]]];
                    if (ch != '\0' && ch != encryted[i][j])
                    {
                        isFit = false;
                        break;
                    }
                }
                if (isFit)
                {
                    for (int j = 0; j < _size; j++)
                    {
                        keys[indexes[sample[i][j]]] = encryted[i][j];
                    }
                }
                if (!keys.Contains('\0')) break;
            }

            return new string(keys.ToArray());
        }

        public string TryToDecode(string sampleFile, string encodedText, Language language)
        {
            var sample = ReadSample(sampleFile);
            var encodedSample = Count(encodedText);
            var stream = new StringBuilder();
            foreach (var encoded in encodedSample)
            {
                stream.Append(encoded + '\n');
            }
            FileManager.Write("attemptedSample.txt", stream.ToString());
            var key = GetKey(sample, encodedSample, language);
            FileManager.Write("attempted_key.txt", key);
            var decoder = new CryptoDecoder(key, language);
            var decodedText = decoder.TextDecoder(encodedText);
            return decodedText;
        }
    }
}
