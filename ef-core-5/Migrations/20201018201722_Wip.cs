﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ef_core_5.Migrations
{
    public partial class Wip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSectionStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "CourseSections",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_StudentId",
                table: "CourseSections",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Students_StudentId",
                table: "CourseSections",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Students_StudentId",
                table: "CourseSections");

            migrationBuilder.DropIndex(
                name: "IX_CourseSections_StudentId",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CourseSections");

            migrationBuilder.CreateTable(
                name: "CourseSectionStudent",
                columns: table => new
                {
                    CourseSectionsCourseSectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentsStudentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSectionStudent", x => new { x.CourseSectionsCourseSectionId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_CourseSectionStudent_CourseSections_CourseSectionsCourseSectionId",
                        column: x => x.CourseSectionsCourseSectionId,
                        principalTable: "CourseSections",
                        principalColumn: "CourseSectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSectionStudent_Students_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSectionStudent_StudentsStudentId",
                table: "CourseSectionStudent",
                column: "StudentsStudentId");
        }
    }
}
