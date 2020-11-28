using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastServices.Data.Migrations
{
    public partial class UpdateComplaintEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Complaints",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Complaints",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Complaints",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_IsDeleted",
                table: "Complaints",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Complaints_IsDeleted",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Complaints");
        }
    }
}
