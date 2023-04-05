using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bArtTestTask.WebAPI.Migrations
{
    public partial class ChangeFieldToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Incidents_incident_name",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "incident_name",
                table: "Accounts",
                type: "NVARCHAR(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Incidents_incident_name",
                table: "Accounts",
                column: "incident_name",
                principalTable: "Incidents",
                principalColumn: "name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Incidents_incident_name",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "incident_name",
                table: "Accounts",
                type: "NVARCHAR(36)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Incidents_incident_name",
                table: "Accounts",
                column: "incident_name",
                principalTable: "Incidents",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
