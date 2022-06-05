using TextFilterApp.Services.Interfaces;

namespace TextFilterApp
{
    public class TextFilter
    {
        private ITextFilterService _textFilterService { get; set; }

        private readonly char[] vowels = "aeiou".ToCharArray();

        public TextFilter(ITextFilterService textFilterService)
        {
            _textFilterService = textFilterService;
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
        public List<string> FilterText(string text)
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
