using System.Threading.Tasks;
using Model;

namespace Domain
{
    public interface ISearchService
    {
        Task<SearchResult> SearchAsync(string word);
    }
}