using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomaciM3T2.Migrations
{
    public partial class Inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Festivals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumOdrzavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    MaksimumPosetilaca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festivals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipKartes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipKartes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kartas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    DatumKupovine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kupac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preuzeta = table.Column<bool>(type: "bit", nullable: false),
                    TipKarteId = table.Column<int>(type: "int", nullable: false),
                    FestivalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kartas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kartas_Festivals_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kartas_TipKartes_TipKarteId",
                        column: x => x.TipKarteId,
                        principalTable: "TipKartes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Festivals",
                columns: new[] { "Id", "DatumOdrzavanja", "MaksimumPosetilaca", "Mesto", "Naziv", "Ocena" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000, "Mokrin", "Svetsko prvenstvo u nadmetanju guskova", 4 },
                    { 2, new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 150000, "Nis", "Nisville", 5 },
                    { 3, new DateTime(2023, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 100000, "Beograd", "Beer Fest", 4 }
                });

            migrationBuilder.InsertData(
                table: "TipKartes",
                columns: new[] { "Id", "Naziv" },
                values: new object[,]
                {
                    { 1, "Redovna karta" },
                    { 2, "VIP karta" }
                });

            migrationBuilder.InsertData(
                table: "Kartas",
                columns: new[] { "Id", "Cena", "DatumKupovine", "FestivalId", "Kupac", "Preuzeta", "TipKarteId" },
                values: new object[,]
                {
                    { 1, 1000.0, new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pera Peric", true, 1 },
                    { 3, 1800.0, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Marko Markovic", false, 1 },
                    { 5, 1560.0, new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Mika Mikic", true, 1 },
                    { 2, 2000.0, new DateTime(2023, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Djoka Djokic", false, 2 },
                    { 4, 2600.0, new DateTime(2023, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Lara Laric", true, 2 },
                    { 6, 2820.0, new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ana Ancic", false, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kartas_FestivalId",
                table: "Kartas",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_Kartas_TipKarteId",
                table: "Kartas",
                column: "TipKarteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kartas");

            migrationBuilder.DropTable(
                name: "Festivals");

            migrationBuilder.DropTable(
                name: "TipKartes");
        }
    }
}
