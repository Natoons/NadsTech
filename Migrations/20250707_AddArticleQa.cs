using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NadsTech.Migrations
{
    public partial class AddArticleQa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleQas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleQas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleQas");
        }
    }
} 