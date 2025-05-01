using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class updateids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_CreatedBy",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_DeletedBy",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_e.UpdatedBy",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_DeletedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_e.UpdatedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId",
                table: "ResumePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_CreatedBy",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_DeletedBy",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_e.UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_CreatedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_DeletedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_e.UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.DropIndex(
                name: "IX_ResumePosts_ResumeCompanyEntryId",
                table: "ResumePosts");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCompanyEntries_ResumeId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DeletedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_e.UpdatedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CreatedBy",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_DeletedBy",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_e.UpdatedBy",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "e.UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "e.UpdatedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "e.UpdatedBy",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "Resumes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedBy",
                table: "Resumes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Resumes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Resumes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ResumeSkills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId1",
                table: "ResumeSkills",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ResumePosts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ResumeCompanyEntryId1",
                table: "ResumePosts",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeEducations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ResumeEducations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeCourses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ResumeCourses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ResumeCompanyEntries",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId1",
                table: "ResumeCompanyEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "Projects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedBy",
                table: "Projects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Projects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Projects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedBy",
                table: "Articles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Articles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedBy",
                table: "Articles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Articles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Articles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId1",
                table: "ResumeSkills",
                column: "ResumeId1");

            migrationBuilder.CreateIndex(
                name: "IX_ResumePosts_ResumeCompanyEntryId1",
                table: "ResumePosts",
                column: "ResumeCompanyEntryId1");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCompanyEntries_ResumeId1",
                table: "ResumeCompanyEntries",
                column: "ResumeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId1",
                table: "ResumeCompanyEntries",
                column: "ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId1",
                table: "ResumePosts",
                column: "ResumeCompanyEntryId1",
                principalTable: "ResumeCompanyEntries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId1",
                table: "ResumeSkills",
                column: "ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCompanyEntries_Resumes_ResumeId1",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumePosts_ResumeCompanyEntries_ResumeCompanyEntryId1",
                table: "ResumePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId1",
                table: "ResumeSkills");

            migrationBuilder.DropIndex(
                name: "IX_ResumeSkills_ResumeId1",
                table: "ResumeSkills");

            migrationBuilder.DropIndex(
                name: "IX_ResumePosts_ResumeCompanyEntryId1",
                table: "ResumePosts");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCompanyEntries_ResumeId1",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropColumn(
                name: "ResumeId1",
                table: "ResumeSkills");

            migrationBuilder.DropColumn(
                name: "ResumeCompanyEntryId1",
                table: "ResumePosts");

            migrationBuilder.DropColumn(
                name: "ResumeId1",
                table: "ResumeCompanyEntries");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Resumes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DeletedBy",
                table: "Resumes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Resumes",
                type: "bigint",
                nullable: false,
                defaultValue: 1L,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Resumes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "e.UpdatedBy",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ResumeSkills",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ResumePosts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "ResumeId",
                table: "ResumeEducations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ResumeEducations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "ResumeId",
                table: "ResumeCourses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ResumeCourses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ResumeCompanyEntries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DeletedBy",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 1L,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Projects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "e.UpdatedBy",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatedBy",
                table: "Articles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectId",
                table: "Articles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DeletedBy",
                table: "Articles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "Articles",
                type: "bigint",
                nullable: false,
                defaultValue: 1L,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Articles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "e.UpdatedBy",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_CreatedBy",
                table: "Resumes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_DeletedBy",
                table: "Resumes",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_e.UpdatedBy",
                table: "Resumes",
                column: "e.UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumePosts_ResumeCompanyEntryId",
                table: "ResumePosts",
                column: "ResumeCompanyEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCompanyEntries_ResumeId",
                table: "ResumeCompanyEntries",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeletedBy",
                table: "Projects",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_e.UpdatedBy",
                table: "Projects",
                column: "e.UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CreatedBy",
                table: "Articles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_DeletedBy",
                table: "Articles",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_e.UpdatedBy",
                table: "Articles",
                column: "e.UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_CreatedBy",
                table: "Articles",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_DeletedBy",
                table: "Articles",
                column: "DeletedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_e.UpdatedBy",
                table: "Articles",
                column: "e.UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatedBy",
                table: "Projects",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_DeletedBy",
                table: "Projects",
                column: "DeletedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_e.UpdatedBy",
                table: "Projects",
                column: "e.UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_CreatedBy",
                table: "Resumes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_DeletedBy",
                table: "Resumes",
                column: "DeletedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_e.UpdatedBy",
                table: "Resumes",
                column: "e.UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
