using Application.Services.Interfaces;

namespace Application
{
    public class TextFilter
    {
        private ITextFilterService _textFilterService { get; set; }
        private IAssemblyLoader _assemblyLoader { get; set; }

        private readonly char[] vowels = "aeiou".ToCharArray();

        public TextFilter(ITextFilterService textFilterService, IAssemblyLoader assemblyLoader)
        {
            _textFilterService = textFilterService;
            _assemblyLoader = assemblyLoader;
        }


        /// <summary>
        /// Applies FilterText Method to the loaded file line by line, writing the filtered words to the console.
        /// </summary>
        /// <param name="resourceName"></param>
        public void ApplyFiltersToFile(string resourceName)
        {
            var stream = _assemblyLoader.LoadEmbeddedResource(resourceName);
            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var filteredWords = FilterText(line);

                        foreach (var word in filteredWords)
                        {
                            Console.WriteLine(word);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Failed loading resource: {resourceName}");
            }
        }

        /// <summary>
        /// Takes a input text and applies three text filters to it returning a list of strings.
        /// Initially the text is split into words by filtering out all symbols and returning a list of words.
        /// Filter 1: Gets all words that contain a vowel in the middle.
        /// Filter 2: Gets all words shorter than 4.
        /// Filter 3: Gets all words containing 't'.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IEnumerable<string> FilterText(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();

            var words = _textFilterService.FilterOutSymbols(text);
            var filter1 = _textFilterService.GetAllContainingAnyFromCollectionInMiddle(words, vowels);
            var filter2 = _textFilterService.GetAllShorterThan(filter1);
            var filter3 = _textFilterService.GetAllContainingChar(filter2);
            return filter3;
        }
    }
}
