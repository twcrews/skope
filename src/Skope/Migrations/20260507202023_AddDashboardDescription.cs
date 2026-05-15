using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skope.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dashboards",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dashboards");
        }
    }
}
