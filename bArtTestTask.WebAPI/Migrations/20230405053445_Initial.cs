using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bArtTestTask.WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    name = table.Column<string>(type: "NVARCHAR(36)", nullable: false, defaultValueSql: "CAST(NewId() as varchar(36))"),
                    description = table.Column<string>(type: "NVARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    name = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    incident_name = table.Column<string>(type: "NVARCHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accounts_Incidents_incident_name",
                        column: x => x.incident_name,
                        principalTable: "Incidents",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    email = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    first_name = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    last_name = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contacts_Accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "Accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_incident_name",
                table: "Accounts",
                column: "incident_name");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_name",
                table: "Accounts",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_account_id",
                table: "Contacts",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_email",
                table: "Contacts",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Incidents");
        }
    }
}
