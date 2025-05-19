using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenanceApp.Migrations
{
    /// <inheritdoc />
    public partial class SmallChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRecords_UserAccounts_ApplicationUserId",
                table: "ServiceRecords");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRecords_ApplicationUserId",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ServiceRecords");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ServiceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_UserId",
                table: "ServiceRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRecords_UserAccounts_UserId",
                table: "ServiceRecords",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
