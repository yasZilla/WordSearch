using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{word}")]
        public async Task<ActionResult<SearchResult>> Get(string word)
        {
            return Ok(await _searchService.SearchAsync(word));
        }
    }
}