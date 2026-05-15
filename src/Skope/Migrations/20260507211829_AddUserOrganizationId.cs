using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skope.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOrganizationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dashboards_OwnerId_Slug",
                table: "Dashboards");

            migrationBuilder.AddColumn<string>(
                name: "PlanningCenterOrganizationId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlanningCenterOrganizationId",
                table: "Users",
                column: "PlanningCenterOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_OwnerId",
                table: "Dashboards",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_Slug",
                table: "Dashboards",
                column: "Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PlanningCenterOrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_OwnerId",
                table: "Dashboards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_Slug",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "PlanningCenterOrganizationId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_OwnerId_Slug",
                table: "Dashboards",
                columns: new[] { "OwnerId", "Slug" },
                unique: true);
        }
    }
}
