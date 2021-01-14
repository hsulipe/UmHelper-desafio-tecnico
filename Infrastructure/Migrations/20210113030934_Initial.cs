using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 64, nullable: false),
                    CpfNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", maxLength: 64, nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", maxLength: 8, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    InitialBalance = table.Column<double>(type: "double precision", nullable: false),
                    CurrentBalance = table.Column<double>(type: "double precision", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 64, nullable: false),
                    From = table.Column<Guid>(type: "uuid", maxLength: 64, nullable: false),
                    To = table.Column<Guid>(type: "uuid", maxLength: 64, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_user_account_From",
                        column: x => x.From,
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transaction_user_account_To",
                        column: x => x.To,
                        principalTable: "user_account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_From",
                table: "transaction",
                column: "From");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_To",
                table: "transaction",
                column: "To");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "user_account");
        }
    }
}
