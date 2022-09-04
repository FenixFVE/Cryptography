
using System;
using System.Text;

namespace Cryptography
{
    public class KeyGenerator
    {
        
        public static string RandomRussianKey()
        {
            List<char> let = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToList();
            let.Insert(6, 'ё');
            Random random = new Random();
            return new string(let.OrderBy(x => random.Next()).ToArray());
        }

        public static void CreateRussianKey(string fileName)
        {
            using (var sw = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate), Encoding.UTF8))
            {
                string russianKey = RandomRussianKey();
                sw.WriteLine(russianKey);
            }
        }
    }
}
