using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPost.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "PartnerProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerProducts_UserId",
                table: "PartnerProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerProducts_Users_UserId",
                table: "PartnerProducts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartnerProducts_Users_UserId",
                table: "PartnerProducts");

            migrationBuilder.DropIndex(
                name: "IX_PartnerProducts_UserId",
                table: "PartnerProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PartnerProducts");
        }
    }
}
