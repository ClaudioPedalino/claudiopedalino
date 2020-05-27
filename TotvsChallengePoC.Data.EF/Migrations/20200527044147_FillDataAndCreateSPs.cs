using Microsoft.EntityFrameworkCore.Migrations;

namespace TotvsChallengePoC.Data.EF.Migrations
{
    public partial class FillDataAndCreateSPs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(InitialScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private const string InitialScript = @"
            INSERT [dbo].[Clients] ([Id], [FirstName], [LastName]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa1', N'TestClient01', N'LastName01')
            INSERT [dbo].[Clients] ([Id], [FirstName], [LastName]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa2', N'TestClient02', N'LastName02')
            INSERT [dbo].[Clients] ([Id], [FirstName], [LastName]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa3', N'TestClient03', N'LastName03')
            INSERT [dbo].[Clients] ([Id], [FirstName], [LastName]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa4', N'TestClient04', N'LastName04')
            INSERT [dbo].[Clients] ([Id], [FirstName], [LastName]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa5', N'TestClient05', N'LastName05')
            SET IDENTITY_INSERT [dbo].[PaymentTypes] ON 

            INSERT [dbo].[PaymentTypes] ([Id], [Description]) VALUES (1, N'Cash')
            INSERT [dbo].[PaymentTypes] ([Id], [Description]) VALUES (2, N'Debit Card')
            INSERT [dbo].[PaymentTypes] ([Id], [Description]) VALUES (3, N'Credit Card')
            SET IDENTITY_INSERT [dbo].[PaymentTypes] OFF

            /****** Object:  StoredProcedure [dbo].[sp_FindClientInfoById]  ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            CREATE PROCEDURE [dbo].[sp_FindClientInfoById] @ClientId uniqueidentifier
            AS
            SELECT 
	            (C.FirstName + ' ' +C.LastName) as Client,
	            SUM(O.TotalAmount) as TotalSpend,
	            (SELECT COUNT(*) FROM Operations WHERE PaymentTypeId = 1 and ClientId = @ClientId) AS CashTimes,
	            (SELECT COUNT(*) FROM Operations WHERE PaymentTypeId = 2 and ClientId = @ClientId) AS DebitTimes,
	            (SELECT COUNT(*) FROM Operations WHERE PaymentTypeId = 3 and ClientId = @ClientId) AS CreditTimes
            FROM Operations o
	            INNER JOIN Clients C ON C.Id = O.ClientId
            WHERE o.ClientId = @ClientId and PaymentTypeId in (1,2,3)
            GROUP BY c.id, C.FirstName, C.LastName
            GO
            /****** Object:  StoredProcedure [dbo].[sp_FindOperationsByClientId]   ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            CREATE PROCEDURE [dbo].[sp_FindOperationsByClientId] @OperationId uniqueidentifier
            AS
            SELECT
	            CONVERT(VARCHAR,O.DateCreated,103) AS DateCreated,
	            O.TotalAmount AS TotalAmount,
	            P.Description AS PaymentType,
	            CH.AmountToReturn AS ChangeReturned,
	            CL.FirstName + ' ' + Cl.LastName AS ClientFullName
            FROM Operations O
            INNER JOIN Clients CL ON CL.Id = O.ClientId
            LEFT JOIN Change CH ON CH.Id = O.ChangeId
            INNER JOIN PaymentTypes P ON P.Id = O.PaymentTypeId
            WHERE O.Id = @OperationId
            GO
        ";
    }
}
