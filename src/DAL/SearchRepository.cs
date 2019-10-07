using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
    public class SearchRepository: ISearchRepository
    {
        private readonly IFactoryConnection _factoryConnection;

        public SearchRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<string[]> FindWordsAsync(string word)
        {
            using (var conn = _factoryConnection.GetDbConnection())
            {
                try
                {
                    await conn.OpenAsync();

                    const string query = @" SELECT result.word as Words
                                            FROM (
	                                            SELECT 	word, 
			                                            similarity(word, @Word) AS sml
	                                            FROM	word_rus
	                                            WHERE	word ~ @Word
	                                            ORDER BY sml DESC	
                                            ) result
                                            LIMIT 10";

                    var result = await conn.QueryAsync<string>(query, new
                    {
                        Word = word.Insert(0, "^")
                    });

                    return result?.ToArray();
                }
                catch (DbException exception)
                {
                    throw exception;
                }
            }
        }
    }
}