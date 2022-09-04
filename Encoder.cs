
using System;
using System.IO;
using System.Text;

namespace Cryptography
{
    public class Encoder
    {
        public static void TextFormater(string inputFile, string outputFile)
        {
            using (var reader = new StreamReader(inputFile, Encoding.UTF8))
            {
                var stream = File.Open(outputFile, FileMode.OpenOrCreate);
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    // todo
                }
            }
        }
    }
}
