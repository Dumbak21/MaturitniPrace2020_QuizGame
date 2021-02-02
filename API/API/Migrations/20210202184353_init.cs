using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QA", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "QA",
                columns: new[] { "Id", "Answer", "Answers", "Area", "Question", "Type" },
                values: new object[] { new Guid("c82bc5e0-2aa5-4044-b501-f9187b141ea3"), null, "1°2°3°4", 4, "How much is 2+2", 0 });

            migrationBuilder.InsertData(
                table: "QA",
                columns: new[] { "Id", "Answer", "Answers", "Area", "Question", "Type" },
                values: new object[] { new Guid("6c9b473e-e27a-4c47-8b6b-1378a476edeb"), "1021", null, 4, "How much is 1000+21", 1 });

            migrationBuilder.InsertData(
                table: "QA",
                columns: new[] { "Id", "Answer", "Answers", "Area", "Question", "Type" },
                values: new object[] { new Guid("ead3d6f3-4634-4410-b7dd-9d69cd09093b"), null, "c°d°3°z", 4, "Last letter in alphabet", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QA");
        }
    }
}
