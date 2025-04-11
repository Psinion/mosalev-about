using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class removeoldaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_UserCreateId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_UserDeleteId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_UserUpdateId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserCreateId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserDeleteId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserUpdateId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_UserCreateId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_UserDeleteId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_UserUpdateId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_UserCreateId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_UserDeleteId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_UserUpdateId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserCreateId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserDeleteId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserUpdateId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserCreateId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserDeleteId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserUpdateId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "UserDeleteId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "UserUpdateId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserDeleteId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserUpdateId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UserDeleteId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UserUpdateId",
                table: "Articles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Resumes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreateId",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserDeleteId",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserUpdateId",
                table: "Resumes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreateId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserDeleteId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserUpdateId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreateId",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserDeleteId",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserUpdateId",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserCreateId",
                table: "Resumes",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserDeleteId",
                table: "Resumes",
                column: "UserDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserUpdateId",
                table: "Resumes",
                column: "UserUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserCreateId",
                table: "Projects",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserDeleteId",
                table: "Projects",
                column: "UserDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserUpdateId",
                table: "Projects",
                column: "UserUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserCreateId",
                table: "Articles",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserDeleteId",
                table: "Articles",
                column: "UserDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserUpdateId",
                table: "Articles",
                column: "UserUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_UserCreateId",
                table: "Articles",
                column: "UserCreateId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_UserDeleteId",
                table: "Articles",
                column: "UserDeleteId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_UserUpdateId",
                table: "Articles",
                column: "UserUpdateId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserCreateId",
                table: "Projects",
                column: "UserCreateId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserDeleteId",
                table: "Projects",
                column: "UserDeleteId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserUpdateId",
                table: "Projects",
                column: "UserUpdateId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_UserCreateId",
                table: "Resumes",
                column: "UserCreateId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_UserDeleteId",
                table: "Resumes",
                column: "UserDeleteId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_UserUpdateId",
                table: "Resumes",
                column: "UserUpdateId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
