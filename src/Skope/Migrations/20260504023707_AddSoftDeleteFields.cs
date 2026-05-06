using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skope.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Widgets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Widgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Dashboards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Dashboards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widgets_DeletedById",
                table: "Widgets",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_DeletedById",
                table: "Dashboards",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Users_DeletedById",
                table: "Dashboards",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Widgets_Users_DeletedById",
                table: "Widgets",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Users_DeletedById",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Widgets_Users_DeletedById",
                table: "Widgets");

            migrationBuilder.DropIndex(
                name: "IX_Widgets_DeletedById",
                table: "Widgets");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_DeletedById",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Dashboards");
        }
    }
}
