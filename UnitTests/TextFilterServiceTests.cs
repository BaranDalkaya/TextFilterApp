using TextFilterApp.Services;

namespace UnitTests
{
    [TestClass]
    public class TextFilterServiceTests
    {
        private TextFilterService _service;
        private List<string> words;

        #region TestInitialize
        [TestInitialize]
        public void Initialize()
        {
            _service = new TextFilterService();
            words = new List<string>()
            {
                "I",
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine",
                "Ten",
                "Test",
                "TEST",
                "Testing"
            };
        }
        #endregion

        #region FilterOutWords
        [TestMethod]
        public void FilterOutWords_Success()
        {
            var text = "One,;Two! \"Three? :Four `Five Six).Seven";
            var expexted = new List<string>() { "One", "Two", "Three", "Four", "Five", "Six", "Seven" };

            var result = _service.FilterOutWords(text);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void FilterOutWords_EmptyInput_Success()
        {
            var expexted = new List<string>();

            var result = _service.FilterOutWords(string.Empty);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FilterOutWords_Error_NullInput()
        {
            _service.FilterOutWords(null);
        }
        #endregion

        #region GetAllContainingAnyFromCollectionInMiddleTests
        [TestMethod]
        public void GetAllContainingAnyFromCollectionInTheMiddle_Success()
        {
            var collection = "aeiou".ToCharArray();
            var expexted = new List<string>() { "I", "Four", "Five", "Six", "Nine", "Ten", "Test", "TEST" };
            
            var result = _service.GetAllContainingAnyFromCollectionInMiddle(words, collection);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GetAllContainingAnyFromCollectionInTheMiddle_EmptyWordsInput_Success()
        {
            var collection = "aeiou".ToCharArray();
            var expexted = new List<string>();

            var result = _service.GetAllContainingAnyFromCollectionInMiddle(new List<string>(), collection);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GetAllContainingAnyFromCollectionInTheMiddle_EmptyCollectionInput_Success()
        {
            var expexted = new List<string>();

            var result = _service.GetAllContainingAnyFromCollectionInMiddle(words, "".ToCharArray());

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllContainingAnyFromCollectionInTheMiddle_NullWordsInput_Error()
        {
            _service.GetAllContainingAnyFromCollectionInMiddle(null, "".ToCharArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllContainingAnyFromCollectionInTheMiddle_NulllCollectionInput_Success()
        {
            _service.GetAllContainingAnyFromCollectionInMiddle(words, null);
        }
        #endregion

        #region GetAllShorterThanTests
        [TestMethod]
        public void GetAllShorterThan_DefaultValue_Success()
        {
            var expexted = new List<string>() { "I", "One", "Two", "Six", "Ten" };

            var result = _service.GetAllShorterThan(words);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GetAllShorterThan_Success()
        {
            var expexted = new List<string>() { "I" };

            var result = _service.GetAllShorterThan(words, 3);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GetAllShorterThan_EmptyInput_Success()
        {
            var expexted = new List<string>();

            var result = _service.GetAllShorterThan(new List<string>());

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllShorterThan_NullInput_Error()
        {
            _service.GetAllShorterThan(null);

        }
        #endregion

        #region GetAllContainingCharTests
        [TestMethod]
        public void GetAllContainingChar_DefaultValue_Success()
        {
            var expexted = new List<string>() { "Eight", "Test", "Testing" };

            var result = _service.GetAllContainingChar(words);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GetAllContainingChar_Success()
        {
            var expexted = new List<string>() { "Two", "Four" };

            var result = _service.GetAllContainingChar(words, 'o');

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GetAllContainingChar_EmptyInput_Success()
        {
            var expexted = new List<string>();

            var result = _service.GetAllContainingChar(new List<string>());

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllContainingChar_NullInput_Error()
        {
            _service.GetAllContainingChar(null);

        }
        #endregion
    }
}