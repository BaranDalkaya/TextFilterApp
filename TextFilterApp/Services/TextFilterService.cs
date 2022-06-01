using TextFilterApp.Services.Interfaces;

namespace TextFilterApp.Services
{
    public class TextFilterService : ITextFilterService
    {
        private readonly char[] symbols = ".,!?():;`'\" ".ToCharArray();

        /// <summary>
        /// Takes a string and filters out word returning a list of strings
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> FilterOutWords(string text)
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
        public List<string> GetAllContainingAnyFromCollectionInMiddle(List<string> words, char[] collection)
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
        public List<string> GetAllShorterThan(List<string> words, int value = 4)
        {
            ValidateParameters(words);
            return words.Where(x => x.Length < value).ToList();
        }

        /// <summary>
        /// Returns all strings that contain char value. Default value = 't'
        /// </summary>
        /// <param name="words"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<string> GetAllContainingChar(List<string> words, char value = 't')
        {
            ValidateParameters(words);
            return words.Where(x => x.Contains(value)).ToList();
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