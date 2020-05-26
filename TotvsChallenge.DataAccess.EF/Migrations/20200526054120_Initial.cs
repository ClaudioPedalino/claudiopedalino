using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TotvsChallenge.DataAccess.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Change",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AmountToReturn = table.Column<decimal>(nullable: false),
                    B100 = table.Column<int>(nullable: true),
                    B50 = table.Column<int>(nullable: true),
                    B20 = table.Column<int>(nullable: true),
                    B10 = table.Column<int>(nullable: true),
                    C50 = table.Column<int>(nullable: true),
                    C10 = table.Column<int>(nullable: true),
                    C05 = table.Column<int>(nullable: true),
                    C01 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Change", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    ClientPaymentAmount = table.Column<decimal>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    ChangeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Change_ChangeId",
                        column: x => x.ChangeId,
                        principalTable: "Change",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ChangeId",
                table: "Operations",
                column: "ChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ClientId",
                table: "Operations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PaymentTypeId",
                table: "Operations",
                column: "PaymentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Change");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "PaymentTypes");
        }
    }
}
