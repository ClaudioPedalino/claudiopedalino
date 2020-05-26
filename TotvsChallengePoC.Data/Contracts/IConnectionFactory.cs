using System.Data.SqlClient;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection { get; }
    }
}
