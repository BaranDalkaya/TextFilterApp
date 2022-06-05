using TextFilterApp;
using TextFilterApp.Services.Interfaces;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class TextFilterTests
    {
        private MockRepository _mock;
        private Mock<ITextFilterService> _textFilterService;
        private TextFilter _textFilter;

        [TestInitialize]
        public void Initialize()
        {
            _mock = new MockRepository(MockBehavior.Strict);
            _textFilterService = _mock.Create<ITextFilterService>();
            _textFilter = new TextFilter(_textFilterService.Object);
        }


        [TestMethod]
        public void FilterOutWords_Success()
        {
            var text = "Testing";
            var words = new List<string>() { "words" };
            var filter1 = new List<string>() { "filter1" };
            var filter2 = new List<string>() { "filter2" };
            var filter3 = new List<string>() { "filter3" };

            _textFilterService.Setup(x => x.FilterOutSymbols(text)).Returns(words);
            _textFilterService.Setup(x => x.GetAllContainingAnyFromCollectionInMiddle(words, It.IsAny<char[]>())).Returns(filter1);
            _textFilterService.Setup(x => x.GetAllShorterThan(filter1, It.IsAny<int>())).Returns(filter2);
            _textFilterService.Setup(x => x.GetAllContainingChar(filter2, It.IsAny<char>())).Returns(filter3);

            var result = _textFilter.FilterText(text);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(filter3, result);
        }

        [TestMethod]
        public void FilterOutWords_EmptyText_Success()
        {
            var result = _textFilter.FilterText(string.Empty);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<string>(), result);
        }

        [TestMethod]
        public void FilterOutWords_NullText_Success()
        {
            var result = _textFilter.FilterText(null);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<string>(), result);
        }
    }
}