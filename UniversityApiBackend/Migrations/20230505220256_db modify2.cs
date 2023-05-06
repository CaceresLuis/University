using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApiBackend.Migrations
{
    public partial class dbmodify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategory_Categories_CategoryId",
                table: "CourseCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategory_Courses_CourseId",
                table: "CourseCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCategory",
                table: "CourseCategory");

            migrationBuilder.RenameTable(
                name: "CourseCategory",
                newName: "CourseCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategory_CourseId",
                table: "CourseCategories",
                newName: "IX_CourseCategories_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategory_CategoryId",
                table: "CourseCategories",
                newName: "IX_CourseCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCategories",
                table: "CourseCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_Categories_CategoryId",
                table: "CourseCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_Courses_CourseId",
                table: "CourseCategories",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_Categories_CategoryId",
                table: "CourseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_Courses_CourseId",
                table: "CourseCategories");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCategories",
                table: "CourseCategories");

            migrationBuilder.RenameTable(
                name: "CourseCategories",
                newName: "CourseCategory");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategories_CourseId",
                table: "CourseCategory",
                newName: "IX_CourseCategory_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategories_CategoryId",
                table: "CourseCategory",
                newName: "IX_CourseCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCategory",
                table: "CourseCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategory_Categories_CategoryId",
                table: "CourseCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategory_Courses_CourseId",
                table: "CourseCategory",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
