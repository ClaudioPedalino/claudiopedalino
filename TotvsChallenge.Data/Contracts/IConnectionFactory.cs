using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TotvsChallenge.Data.Contracts
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection { get; }
    }
}
