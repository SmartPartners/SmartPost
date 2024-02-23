using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPost.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "PercentageSalePrice",
                table: "StokProducts",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "StokProducts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PercentageSalePrice",
                table: "Products",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PercentageSalePrice",
                table: "Cards",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Cards",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageSalePrice",
                table: "StokProducts");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "StokProducts");

            migrationBuilder.DropColumn(
                name: "PercentageSalePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PercentageSalePrice",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Cards");
        }
    }
}
