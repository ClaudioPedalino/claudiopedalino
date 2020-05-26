using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.DataAccess.Repositories
{

    public class ReportRepository : IReportRepository
    {
        #region firstTry
        //private string connectionString;

        //public DapperRepository()
        //{
        //    connectionString = "Server=.;Database=test02;Trusted_Connection=True;";
        //}

        //public IDbConnection Connection
        //{
        //    get
        //    {
        //        return new SqlConnection(connectionString);
        //    }
        //}

        //public object FindClientInfoById(Guid id)
        //{
        //    using (IDbConnection dbConnection = Connection)
        //    {
        //        string query = @"SELECT
        //                         (C.FirstName + ' ' + c.LastName) as Cliente,
        //                         SUM(O.TotalAmount) as TotalSpend,
        //                         (SELECT COUNT(*) FROM PaymentTypes WHERE P.Id = 1) as Efectivo,
        //                         (SELECT COUNT(*) FROM PaymentTypes WHERE P.Id = 2) as Debito,
        //                         (SELECT COUNT(*) FROM PaymentTypes WHERE P.Id = 3) as Credito
        //                        FROM Operations O
        //                        INNER JOIN Clients C ON C.Id = O.ClientId
        //                        INNER JOIN PaymentTypes P ON P.Id = O.PaymentTypeId
        //                        WHERE C.ID = @id
        //                        GROUP BY p.Id , C.FirstName, c.LastName";
        //        dbConnection.Open();
        //        return dbConnection.Query<object>(query, new { id });

        //    }
        //}

        //public int FindOperationsByClientId(Guid id)
        //{
        //    using (IDbConnection dbConnection = Connection)
        //    {
        //        string query = @"SELECT COUNT(*) FROM Operations WHERE ClientId = @id";
        //        dbConnection.Open();
        //        return dbConnection.QuerySingle<int>(query, new { id });

        //    }
        //}
        #endregion

        #region secondTry
        private readonly IBaseRepository baseRepository;
        public ReportRepository(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        #region Works withoutAsync
        //public int DummyQuery(string id)
        //{
        //    var query = "SELECT COUNT(*) FROM Operations WHERE ClientId = '3FA85F64-5717-4562-B3FC-2C963F66AFA5'";
        //    return baseRepository.WithConnection(q =>
        //    {
        //        var result = q.QuerySingle<int>(query);
        //        return result;
        //    });
        //}
        #endregion


        public async Task<ClientInfoModelResponse> FindClientInfoById(string @ClientId)
        {
            return await baseRepository.WithConnection(async q =>
            {
                string query = @"SELECT 
	                                (C.FirstName + ' ' +C.LastName) as ClientFullName,
	                                SUM(O.TotalAmount) as TotalSpend,
	                                (SELECT COUNT(*) FROM Operations WHERE PaymentTypeId = 1 and ClientId = @ClientId) AS CashTimes,
	                                (SELECT COUNT(*) FROM Operations WHERE PaymentTypeId = 2 and ClientId = @ClientId) AS DebitTimes,
	                                (SELECT COUNT(*) FROM Operations WHERE PaymentTypeId = 3 and ClientId = @ClientId) AS CreditTimes
                                FROM Operations o
	                                INNER JOIN Clients C ON C.Id = O.ClientId
                                WHERE o.ClientId = @ClientId
                                    AND PaymentTypeId in (1,2,3)
                                GROUP BY c.id, C.FirstName, C.LastName
                                ";

                var param = new { ClientId };

                var result = await q.QuerySingleAsync<ClientInfoModelResponse>(query, param).ConfigureAwait(false);
                return result;
            });
        }

        public async Task<OperationInfoModelResponse> FindOperationInfoById(string OperationId)
        {
            return await baseRepository.WithConnection(async q =>
            {
                #region QueryOfProcedure
                //@"SELECT
                // CONVERT(VARCHAR,O.DateCreated,103) AS DateCreated,
                // O.TotalAmount AS TotalAmount,
                // P.Description AS PaymentType,
                // CH.AmountToReturn AS ChangeReturned,
                // CL.FirstName + ' ' + Cl.LastName AS ClientFullName
                //FROM Operations O
                //INNER JOIN Clients CL ON CL.Id = O.ClientId
                //LEFT JOIN Change CH ON CH.Id = O.ChangeId
                //INNER JOIN PaymentTypes P ON P.Id = O.PaymentTypeId
                //WHERE O.Id = @id";
                #endregion

                var param = new { OperationId };
                var result = await q.QuerySingleAsync<OperationInfoModelResponse>
                    ("sp_FindOperationsByClientId", param,
                    commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

                return result;
            });
        }
        #endregion
    }

}
