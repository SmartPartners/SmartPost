using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPost.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debt",
                table: "PartnerProducts");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PartnerProducts");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PartnerProducts",
                newName: "TransNo");

            migrationBuilder.AddColumn<decimal>(
                name: "Debt",
                table: "Partners",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "Partners",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debt",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "TransNo",
                table: "PartnerProducts",
                newName: "Status");

            migrationBuilder.AddColumn<decimal>(
                name: "Debt",
                table: "PartnerProducts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "PartnerProducts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
