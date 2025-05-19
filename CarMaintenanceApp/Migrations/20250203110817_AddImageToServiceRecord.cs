using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMaintenanceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToServiceRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ServiceRecords",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ServiceRecords");
        }
    }
}
