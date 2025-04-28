using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _241125_Speisekarte.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speisen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titel = table.Column<string>(type: "TEXT", nullable: false),
                    Notiz = table.Column<string>(type: "TEXT", nullable: true),
                    Bewertung = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speisen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zutaten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Beschreibung = table.Column<string>(type: "TEXT", nullable: false),
                    Menge = table.Column<int>(type: "INTEGER", nullable: false),
                    Einheit = table.Column<string>(type: "TEXT", nullable: false),
                    SpeiseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zutaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zutaten_Speisen_SpeiseId",
                        column: x => x.SpeiseId,
                        principalTable: "Speisen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zutaten_SpeiseId",
                table: "Zutaten",
                column: "SpeiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zutaten");

            migrationBuilder.DropTable(
                name: "Speisen");
        }
    }
}
