
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Cryptography
{
    public class CryptoEncoder
    {
        public static void TextFormater(string inputFile, string outputFile)
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
                        var line = reader.ReadToEnd();
                        string result = rgx1.Replace(line, String.Empty);
                        result = result.ToLower();
                        writer.Write(result);
                    }
                }
            }

 
        }
    }
}
