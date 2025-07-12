using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
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
                name: "FK_Articles_Users_UpdatedBy",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_DeletedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UpdatedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId1",
                table: "ResumeSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_CreatedBy",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_DeletedBy",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_CreatedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_DeletedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_IsDeleted",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_ResumeSkills_ResumeId1",
                table: "ResumeSkills");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DeletedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_IsDeleted",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UpdatedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CreatedBy",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_DeletedBy",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_IsDeleted",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UpdatedBy",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ResumeId1",
                table: "ResumeSkills");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Resumes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeSkills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Projects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Articles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId");

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

            migrationBuilder.DropIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Resumes",
                type: "integer",
                nullable: false,
                defaultValue: 1,
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
                name: "ResumeId",
                table: "ResumeSkills",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId1",
                table: "ResumeSkills",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 1,
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

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Articles",
                type: "integer",
                nullable: false,
                defaultValue: 1,
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

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_CreatedBy",
                table: "Resumes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_DeletedBy",
                table: "Resumes",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_IsDeleted",
                table: "Resumes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UpdatedBy",
                table: "Resumes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId1",
                table: "ResumeSkills",
                column: "ResumeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeletedBy",
                table: "Projects",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IsDeleted",
                table: "Projects",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UpdatedBy",
                table: "Projects",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CreatedBy",
                table: "Articles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_DeletedBy",
                table: "Articles",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IsDeleted",
                table: "Articles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UpdatedBy",
                table: "Articles",
                column: "UpdatedBy");

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
                name: "FK_Articles_Users_UpdatedBy",
                table: "Articles",
                column: "UpdatedBy",
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
                name: "FK_Projects_Users_UpdatedBy",
                table: "Projects",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkills_Resumes_ResumeId1",
                table: "ResumeSkills",
                column: "ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");

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
                name: "FK_Resumes_Users_UpdatedBy",
                table: "Resumes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
