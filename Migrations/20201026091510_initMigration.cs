using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;
using System;

namespace TestOcbc.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomersId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomersId);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransaksis",
                columns: table => new
                {
                    MasterTransaksiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TransactionCode = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: true),
                    TransactionName = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransaksis", x => x.MasterTransaksiId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CustomersId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    point = table.Column<int>(nullable: false),
                    TransDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "CustomersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MasterTransaksis",
                columns: new[] { "MasterTransaksiId", "TransactionCode", "TransactionName" },
                values: new object[] { 1, "bp", "Beli Pulsa" });

            migrationBuilder.InsertData(
                table: "MasterTransaksis",
                columns: new[] { "MasterTransaksiId", "TransactionCode", "TransactionName" },
                values: new object[] { 2, "bt", "Beli Listrik" });

            migrationBuilder.InsertData(
                table: "MasterTransaksis",
                columns: new[] { "MasterTransaksiId", "TransactionCode", "TransactionName" },
                values: new object[] { 3, "st", "Setor Tunai" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomersId",
                table: "Transactions",
                column: "CustomersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterTransaksis");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
