using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    /// <inheritdoc />
    public partial class createprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexContents");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserCreateId = table.Column<long>(type: "bigint", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserUpdateId = table.Column<long>(type: "bigint", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserDeleteId = table.Column<long>(type: "bigint", nullable: true),
                    DateDelete = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserDeleteId",
                        column: x => x.UserDeleteId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserUpdateId",
                        column: x => x.UserUpdateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserCreateId = table.Column<long>(type: "bigint", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserUpdateId = table.Column<long>(type: "bigint", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserDeleteId = table.Column<long>(type: "bigint", nullable: true),
                    DateDelete = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserDeleteId",
                        column: x => x.UserDeleteId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserUpdateId",
                        column: x => x.UserUpdateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ProjectId",
                table: "Articles",
                column: "ProjectId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.CreateTable(
                name: "IndexContents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserCreateId = table.Column<long>(type: "bigint", nullable: true),
                    UserDeleteId = table.Column<long>(type: "bigint", nullable: true),
                    UserUpdateId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateDelete = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndexContents_Users_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IndexContents_Users_UserDeleteId",
                        column: x => x.UserDeleteId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IndexContents_Users_UserUpdateId",
                        column: x => x.UserUpdateId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndexContents_UserCreateId",
                table: "IndexContents",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexContents_UserDeleteId",
                table: "IndexContents",
                column: "UserDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexContents_UserUpdateId",
                table: "IndexContents",
                column: "UserUpdateId");
        }
    }
}
