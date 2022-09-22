
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
            var memory = new Dictionary<char, char>((int)language);
            var keys = new List<char>((int)language);
            int j = _size - 1;
            foreach (char letter in alphabet)
            {
                for (int i = 0; i < sample.Count && i < encrypted.Count; i++)
                {
                    if (sample[i][j] == letter && !keys.Contains(encrypted[i][j]))
                    {
                        bool flag = true;
                        for (int k = 0; k < _size; k++)
                        {
                            if (memory.ContainsKey(sample[i][k]))
                            {
                                if (memory[sample[i][k]] != encrypted[i][k])
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (flag)
                        {
                            if (!memory.ContainsKey(sample[i][j]))
                            {
                                memory.Add(sample[i][j], letter);
                            }
                            keys.Add(encrypted[i][j]);
                            break;
                        }
                    }
                }
            }
            return new string(keys.ToArray());
        }

        public string GetKey2(List<string> sample, List<string> encrypted, Language language)
        {
            var alphabet = KeyGenerator.KeyGeneratorForLanguage(language).Alphabet();
            var memory = new Dictionary<char, char>((int)language);
            var keys = new List<char>((int)language);
            int max = Math.Min(sample.Count, encrypted.Count);
            foreach (char letter in alphabet)
            {
                for (int line = 0; line < max; line++)
                {
                    for (int letter_index = 0; letter_index < _size; letter_index++)
                    {
                        if (sample[line][letter_index] == letter && !keys.Contains(encrypted[line][letter_index]))
                        {
                            bool flag = true;
                            for (int letter_index_2 = 0; letter_index_2 < _size; letter_index_2++)
                            {
                                if (letter_index != letter_index_2 && memory.ContainsKey(sample[line][letter_index_2]) && memory[sample[line][letter_index_2]] != encrypted[line][letter_index_2])
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag) 
                            {
                                if (!memory.ContainsKey(sample[line][letter_index]))
                                {
                                    memory.Add(sample[line][letter_index], letter);
                                }
                                keys.Add(encrypted[line][letter_index]);
                                goto GetKeyExit;
                            }
                        }
                    }
                }
                Console.WriteLine($"didn't found {letter}");
                GetKeyExit: continue;
            }

             return new string (keys.ToArray());
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
            FileManager.Write("attempted_data.txt", stream.ToString());
            var key = GetKey2(sample, encodedSample, language);
            FileManager.Write("attempted_key.txt", key);
            var decoder = new CryptoDecoder(key, language);
            var decodedText = decoder.TextDecoder(encodedText);
            return decodedText;
        }
    }
}
