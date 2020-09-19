using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_ktr_msc.Data.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Libraries_LibraryId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_OwnerId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_OwnerId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Profiles");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "Profiles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    EmailAdress = table.Column<string>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_OwnerId",
                table: "UserProfiles",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Libraries_LibraryId",
                table: "Profiles",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Libraries_LibraryId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "Profiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_OwnerId",
                table: "Profiles",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Libraries_LibraryId",
                table: "Profiles",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_OwnerId",
                table: "Profiles",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
