﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bArtTestTask.WebAPI.Migrations
{
    public partial class FixAutogeneratedValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Incidents",
                type: "NVARCHAR(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(36)",
                oldDefaultValueSql: "CAST(NewId() as varchar(36))");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Incidents",
                type: "NVARCHAR(36)",
                nullable: false,
                defaultValueSql: "CAST(NewId() as varchar(36))",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}