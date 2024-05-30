using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirstApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Pharmacy",
                table: "Prescription",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Pharmacy",
                table: "Prescription");
        }
    }
}
