namespace Application.Services.Interfaces
{
    public interface ITextFilterService
    {
        IEnumerable<string> FilterOutSymbols(string text);
        IEnumerable<string> GetAllContainingAnyFromCollectionInMiddle(IEnumerable<string> words, char[] collection);
        IEnumerable<string> GetAllShorterThan(IEnumerable<string> words, int value = 4);
        IEnumerable<string> GetAllContainingChar(IEnumerable<string> words, char value = 't');
    }
}
