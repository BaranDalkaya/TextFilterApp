namespace TextFilterApp.Services.Interfaces
{
    public interface ITextFilterService
    {
        List<string> FilterOutSymbols(string text);
        List<string> GetAllContainingAnyFromCollectionInMiddle(List<string> words, char[] collection);
        List<string> GetAllShorterThan(List<string> words, int value = 4);
        List<string> GetAllContainingChar(List<string> words, char value = 't');
    }
}
