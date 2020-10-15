using Microsoft.EntityFrameworkCore.Migrations;

namespace ef_core_5.Migrations
{
    public partial class UpdateCourseSectionEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseSectionEnrollments",
                newName: "CourseSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseSectionId",
                table: "CourseSectionEnrollments",
                newName: "CourseId");
        }
    }
}
