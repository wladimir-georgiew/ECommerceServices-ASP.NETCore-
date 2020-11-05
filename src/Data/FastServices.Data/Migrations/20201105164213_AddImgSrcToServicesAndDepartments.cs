using Microsoft.EntityFrameworkCore.Migrations;

namespace FastServices.Data.Migrations
{
    public partial class AddImgSrcToServicesAndDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardImgSrc",
                table: "Services",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImgSrc",
                table: "Departments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardImgSrc",
                table: "Departments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardImgSrc",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BackgroundImgSrc",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CardImgSrc",
                table: "Departments");
        }
    }
}
