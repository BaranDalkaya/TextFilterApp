using Application.Services.Interfaces;

namespace Application.Services
{
    public class TextFilterService : ITextFilterService
    {
        private readonly char[] symbols = ".,!?():;<>{}[]|#@-_=+^*£$%~`'\" ".ToCharArray();

        /// <summary>
        /// Takes a string and filters out all symbols returning a list of words
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IEnumerable<string> FilterOutSymbols(string text)
        {
            ValidateParameters(text);
            var words = text.Split(symbols);
            return words.Where(x => x != string.Empty).ToList();
        }

        /// <summary>
        /// Takes a list of strings and an array of chars, checks if the middle of the strings matches any char in the array and returns all that have a match
        /// For evenly lengthed strings it will check either of the middle two characters
        /// </summary>
        /// <param name="words"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAllContainingAnyFromCollectionInMiddle(IEnumerable<string> words, char[] collection)
        {
            ValidateParameters(words, collection);

            var filteredWords = new List<string>();

            foreach (var word in words)
            {
                var match = false;
                var wordLower = word.ToLower();
                var length = word.Length;
                var indexMiddle = length % 2.0 == 0 ? length / 2 - 1 : (int)Math.Floor(length / 2.0);

                if (length % 2 == 0) // Length is even, check middle two letters
                {
                    var middleLetters = wordLower.Substring(indexMiddle, 2);
                    match = middleLetters.IndexOfAny(collection) != -1;
                }
                else // Length is odd, check middle letter
                {
                    var middleLetter = wordLower[indexMiddle];
                    match = collection.Contains(middleLetter);
                }

                if (match) filteredWords.Add(word);
            }
            return filteredWords;
        }

        /// <summary>
        /// Returns all strings that have a length lower than the value. Default value = 4
        /// </summary>
        /// <param name="words"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAllShorterThan(IEnumerable<string> words, int value = 4)
        {
            ValidateParameters(words);
            return words.Where(x => x.Length < value);
        }

        /// <summary>
        /// Returns all strings that contain char value. Default value = 't'
        /// </summary>
        /// <param name="words"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAllContainingChar(IEnumerable<string> words, char value = 't')
        {
            ValidateParameters(words);
            return words.Where(x => x.Contains(value));
        }

        /// <summary>
        /// Throws a argumentNullException if any parameter is null
        /// </summary>
        /// <param name="list"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ValidateParameters(params object[] list)
        {
            foreach(var item in list)
            {
                if (item == null) throw new ArgumentNullException();
            }
        }
    }
}