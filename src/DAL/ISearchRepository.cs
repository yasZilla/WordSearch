using System.Threading.Tasks;

namespace DAL
{
    public interface ISearchRepository
    {
        Task<string[]> FindWordsAsync(string word);
    }
}