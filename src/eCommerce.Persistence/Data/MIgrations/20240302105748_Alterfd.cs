using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alterfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogedout",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndLogin",
                schema: "Identity",
                table: "UserLogin",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndLogin",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.AddColumn<bool>(
                name: "IsLogedout",
                schema: "Identity",
                table: "UserLogin",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
