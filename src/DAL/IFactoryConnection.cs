using System.Data.Common;

namespace DAL
{
    public interface IFactoryConnection
    {
        DbConnection GetDbConnection();
    }
}