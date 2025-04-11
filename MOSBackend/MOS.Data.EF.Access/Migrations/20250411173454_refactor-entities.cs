using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class refactorentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Resumes",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Resumes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "e.UpdatedBy",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "e.UpdatedBy",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Articles",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Articles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Articles",
                type: "bigint",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CreatedAt",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "e.UpdatedBy",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "e.UpdatedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "e.UpdatedBy",
                table: "Articles");
        }
    }
}
