using System.Data.SqlClient;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IConnectionFactory
    {
        /// <summary>
        /// Create SQL connection to database
        /// </summary>
        SqlConnection CreateConnection { get; }
    }
}
