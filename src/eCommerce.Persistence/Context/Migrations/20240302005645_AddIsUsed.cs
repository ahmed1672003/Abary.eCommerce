using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddIsUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "Identity",
                table: "UserToken",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "Identity",
                table: "UserToken");
        }
    }
}
