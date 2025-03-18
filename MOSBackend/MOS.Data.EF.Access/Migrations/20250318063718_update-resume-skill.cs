using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class updateresumeskill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId",
                table: "ResumePosts");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "ResumeSkills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "ResumeCompanyEntryId",
                table: "ResumePosts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ResumeId",
                table: "ResumeCompanyEntries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId",
                table: "ResumePosts",
                column: "ResumeCompanyEntryId",
                principalTable: "ResumeCompanyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId",
                table: "ResumePosts");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "ResumeSkills");

            migrationBuilder.AlterColumn<long>(
                name: "ResumeCompanyEntryId",
                table: "ResumePosts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ResumeId",
                table: "ResumeCompanyEntries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId",
                table: "ResumePosts",
                column: "ResumeCompanyEntryId",
                principalTable: "ResumeCompanyEntries",
                principalColumn: "Id");
        }
    }
}
