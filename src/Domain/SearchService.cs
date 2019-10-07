using System.Threading.Tasks;
using DAL;
using Model;

namespace Domain
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _repository;

        public SearchService(ISearchRepository repository)
        {
            _repository = repository;
        }

        public async Task<SearchResult> SearchAsync(string word)
        {
            return new SearchResult
            {
                Words = string.IsNullOrEmpty(word) ? null : await _repository.FindWordsAsync(word.ToLower())
            };
        }
    }
}