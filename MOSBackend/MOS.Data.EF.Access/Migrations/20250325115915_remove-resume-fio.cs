using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class removeresumefio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Resumes");

            migrationBuilder.AlterColumn<long>(
                name: "ResumeId",
                table: "ResumeSkills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Resumes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Resumes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Resumes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ResumeId",
                table: "ResumeSkills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");
        }
    }
}
