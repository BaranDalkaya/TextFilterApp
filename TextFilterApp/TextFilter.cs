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

        public List<string> FilterText(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();

            var words = _textFilterService.FilterOutWords(text);
            var filter1 = _textFilterService.GetAllContainingAnyFromCollectionInMiddle(words, vowels);
            var filter2 = _textFilterService.GetAllShorterThan(filter1);
            var filter3 = _textFilterService.GetAllContainingChar(filter2);
            return filter3;
        }
    }
}
