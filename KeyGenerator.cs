
namespace Cryptography
{
    public enum Language : int
    {
        English = 26,
        Russian = 33,
    }

    public abstract class KeyGenerator
    {
        public abstract Language language { get; }
        public abstract List<char> Alphabet();
        public string RandomKey()
        {
            var alphabet = Alphabet();
            var random = new Random();
            var shuffledAlphabet = alphabet.OrderBy(x => random.Next()).ToArray();
            return new string(shuffledAlphabet);
        }
        public void CreateKey(string fileName)
        {
            string russianKey = RandomKey();
            FileManager.Write(fileName, russianKey);
        }
        public static KeyGenerator KeyGeneratorForLanguage(Language language) =>
            language switch
            {
                Language.Russian => new RussianKeyGenerator(),
                Language.English => new EnglishKeyGenerator(),
                _ => throw new Exception($"KeyGenerator for {Enum.GetName(typeof(Language), language)} language is not supported"),
            };
    }
}
