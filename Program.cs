Console.WriteLine("Glosprogram");

/*
List<string> words = [
  "hus", "house",     // jämna index => svenskt uppslag, udda => engelsk översättning
  "hem", "home",
  "stor", "big",      // synonymer får hanteras i en loop
  "stor", "large"
];
*/

List<Word> words = [];

// fyll listan med ord från disk (wordlists)
foreach (string line in File.ReadAllLines("./wordlists/swedish-english.csv"))
{
  string[] wordPair = line.Split(",");
  words.Add(new Word(wordPair[0], wordPair[1], "swedish", "english"));
}

// referera till ett ord ur vår array (hem på engelska):
//Console.WriteLine(words[1].WordOut);

// Dictionary

Dictionary<string, List<Word>> swedishToEnglish = words
.GroupBy(word => word.WordIn, StringComparer.OrdinalIgnoreCase) // skapar lista baserat på gemensam nyckel (i e stor => big, large)
.ToDictionary(
  group => group.Key, // nyckeln
  group => group.ToList(),          // värdet, typiskt hela objektet (referensen)
  StringComparer.OrdinalIgnoreCase
);

// referera till ett ord ur vår dictionary
//Console.WriteLine(swedishToEnglish["hem"][0].WordOut);

while (true)
{

  Console.WriteLine("Ange vilket ord du vill översätta");
  string? wordToTranslate = Console.ReadLine();

  // IF IT contains the key
  if (swedishToEnglish.ContainsKey(wordToTranslate!))
  {
    // loopa ut synonymer
    foreach (var word in swedishToEnglish[wordToTranslate!])
    {
      Console.WriteLine(word.WordOut);
    }
  }
  else
  {
    Console.WriteLine("This word does not exist in this dictionary");
  }

}






class Word(string wordIn, string wordOut, string languageIn, string languageOut)
{
  public string WordIn { get; } = wordIn;
  public string WordOut { get; } = wordOut;
  public string LanguageIn { get; } = languageIn;
  public string LanguageOut { get; } = languageOut;
}