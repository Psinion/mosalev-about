using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class fixresumerelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResumeId",
                table: "ResumeSkills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResumeId",
                table: "ResumeEducations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResumeId",
                table: "ResumeCourses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResumeId",
                table: "ResumeCompanyEntries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeEducations_ResumeId",
                table: "ResumeEducations",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCourses_ResumeId",
                table: "ResumeCourses",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCompanyEntries_ResumeId",
                table: "ResumeCompanyEntries",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCourses_Resumes_ResumeId",
                table: "ResumeCourses",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeEducations_Resumes_ResumeId",
                table: "ResumeEducations",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCourses_Resumes_ResumeId",
                table: "ResumeCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeEducations_Resumes_ResumeId",
                table: "ResumeEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.DropIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.DropIndex(
                name: "IX_ResumeEducations_ResumeId",
                table: "ResumeEducations");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCourses_ResumeId",
                table: "ResumeCourses");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCompanyEntries_ResumeId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ResumeSkills");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ResumeEducations");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ResumeCourses");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ResumeCompanyEntries");
        }
    }
}
