
using System;
using System.Text;

namespace Cryptography
{
    public class KeyGenerator
    {
        public static List<char> RussianAlphabet()
        {
            List<char> alphabet = Enumerable.Range(0, 32).Select((i, x) => (char)('а' + i)).ToList();
            alphabet.Insert(6, 'ё');
            return alphabet;
        }
        public static string RandomRussianKey()
        {
            var alphabet  = RussianAlphabet();
            var random = new Random();
            var shuffledAlphabet = alphabet.OrderBy(x => random.Next()).ToArray();
            return new string(shuffledAlphabet);
        }
        public static void CreateRussianKey(string fileName)
        {
            var stream = File.Open(fileName, FileMode.OpenOrCreate);
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                string russianKey = RandomRussianKey();
                writer.WriteLine(russianKey);
            }
        }
    }
}
