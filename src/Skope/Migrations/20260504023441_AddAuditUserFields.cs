using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skope.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dashboards_Slug",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "Configuration",
                table: "Widgets");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Widgets",
                newName: "SourceConfig");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "Widgets",
                newName: "WidgetType");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Widgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataSource",
                table: "Widgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayConfig",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Widgets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Widgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Widgets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Widgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PlanningCenterId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DashboardShares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Dashboards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Dashboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Dashboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningCenterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BillingContactId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_BillingContactId",
                        column: x => x.BillingContactId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Widgets_CreatedById",
                table: "Widgets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Widgets_UpdatedById",
                table: "Widgets",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlanningCenterId",
                table: "Users",
                column: "PlanningCenterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_OrganizationId_Slug",
                table: "Dashboards",
                columns: new[] { "OrganizationId", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UpdatedById",
                table: "Dashboards",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_BillingContactId",
                table: "Organizations",
                column: "BillingContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_PlanningCenterId",
                table: "Organizations",
                column: "PlanningCenterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Slug",
                table: "Organizations",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Organizations_OrganizationId",
                table: "Dashboards",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Users_UpdatedById",
                table: "Dashboards",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Widgets_Users_CreatedById",
                table: "Widgets",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Widgets_Users_UpdatedById",
                table: "Widgets",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Organizations_OrganizationId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Users_UpdatedById",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Widgets_Users_CreatedById",
                table: "Widgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Widgets_Users_UpdatedById",
                table: "Widgets");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Widgets_CreatedById",
                table: "Widgets");

            migrationBuilder.DropIndex(
                name: "IX_Widgets_UpdatedById",
                table: "Widgets");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlanningCenterId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_OrganizationId_Slug",
                table: "Dashboards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_UpdatedById",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "DisplayConfig",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DashboardShares");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Dashboards");

            migrationBuilder.RenameColumn(
                name: "WidgetType",
                table: "Widgets",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "SourceConfig",
                table: "Widgets",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Configuration",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlanningCenterId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_Slug",
                table: "Dashboards",
                column: "Slug",
                unique: true);
        }
    }
}
