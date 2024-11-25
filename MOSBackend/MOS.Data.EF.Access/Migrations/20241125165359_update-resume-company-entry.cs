using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class updateresumecompanyentry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCompanyEntries_Companies_CompanyId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCompanyEntries_CompanyId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "ResumeCompanyEntries");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "ResumeCompanyEntries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebSiteUrl",
                table: "ResumeCompanyEntries",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "ResumeCompanyEntries");

            migrationBuilder.DropColumn(
                name: "WebSiteUrl",
                table: "ResumeCompanyEntries");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "ResumeCompanyEntries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "ResumeCompanyEntries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "ResumeCompanyEntries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserCreateId = table.Column<long>(type: "bigint", nullable: true),
                    UserDeleteId = table.Column<long>(type: "bigint", nullable: true),
                    UserUpdateId = table.Column<long>(type: "bigint", nullable: true),
                    Commentary = table.Column<string>(type: "text", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateDelete = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Users_UserDeleteId",
                        column: x => x.UserDeleteId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Users_UserUpdateId",
                        column: x => x.UserUpdateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCompanyEntries_CompanyId",
                table: "ResumeCompanyEntries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserCreateId",
                table: "Companies",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserDeleteId",
                table: "Companies",
                column: "UserDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserUpdateId",
                table: "Companies",
                column: "UserUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCompanyEntries_Companies_CompanyId",
                table: "ResumeCompanyEntries",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
