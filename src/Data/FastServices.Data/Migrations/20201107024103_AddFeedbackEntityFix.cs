using Microsoft.EntityFrameworkCore.Migrations;

namespace FastServices.Data.Migrations
{
    public partial class AddFeedbackEntityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Services_ServiceId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_ServiceId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Feedback");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Feedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Departments_DepartmentId",
                table: "Feedback",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Departments_DepartmentId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Feedback");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Feedback",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ServiceId",
                table: "Feedback",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Services_ServiceId",
                table: "Feedback",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
