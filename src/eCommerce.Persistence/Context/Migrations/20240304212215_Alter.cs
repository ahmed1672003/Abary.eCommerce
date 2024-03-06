using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class Alter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                schema: "Identity",
                table: "UserLogin",
                columns: new[] { "LoginProvider", "ProviderKey", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                schema: "Identity",
                table: "UserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });
        }
    }
}
