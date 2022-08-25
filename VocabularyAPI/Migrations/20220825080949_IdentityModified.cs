using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabularyAPI.Migrations
{
    public partial class IdentityModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Synonyms",
                columns: table => new
                {
                    SynonymId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SynonymName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Definition1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sentence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonyms", x => x.SynonymId);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Definition1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sentence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Synonyms");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
