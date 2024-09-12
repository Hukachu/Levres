using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Levres.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobil",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    model = table.Column<int>(type: "int", nullable: false),
                    godinaProizvodnje = table.Column<DateOnly>(type: "date", nullable: false),
                    gorivo = table.Column<int>(type: "int", nullable: false),
                    transmisija = table.Column<int>(type: "int", nullable: false),
                    brojVrata = table.Column<int>(type: "int", nullable: false),
                    boja = table.Column<int>(type: "int", nullable: false),
                    pogon = table.Column<int>(type: "int", nullable: false),
                    felge = table.Column<int>(type: "int", nullable: false),
                    emisioniStandard = table.Column<int>(type: "int", nullable: false),
                    sjedecaMjesta = table.Column<int>(type: "int", nullable: false),
                    masaTezina = table.Column<int>(type: "int", nullable: false),
                    vrstaInterijera = table.Column<int>(type: "int", nullable: false),
                    svjetla = table.Column<int>(type: "int", nullable: false),
                    cijena = table.Column<double>(type: "float", nullable: false),
                    slike = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobil", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Konfiguracija",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    model = table.Column<int>(type: "int", nullable: false),
                    boja = table.Column<int>(type: "int", nullable: false),
                    felge = table.Column<int>(type: "int", nullable: false),
                    vrsta_interijera = table.Column<int>(type: "int", nullable: false),
                    svjetla = table.Column<int>(type: "int", nullable: false),
                    motor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konfiguracija", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    automobilID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kupacID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    datum = table.Column<DateOnly>(type: "date", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.id);
                    table.ForeignKey(
                     name: "FK_Narudzba_Automobil_automobilID",
                     column: x => x.automobilID,
                     principalTable: "Automobil",
                     principalColumn: "id",
                     onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzba_Korisnik_kupacID",
                        column: x => x.kupacID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oprema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazivDijela = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<int>(type: "int", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    cijena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oprema", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Servis",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kupacID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    model = table.Column<int>(type: "int", nullable: false),
                    registracijskeTablice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servis", x => x.id);
                    table.ForeignKey(
                        name: "FK_Servis_Korisnik",
                        column: x => x.kupacID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                        );

                });

            migrationBuilder.CreateTable(
                name: "NoviAutomobil",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    garancija = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoviAutomobil", x => x.id);
                    table.ForeignKey(
                        name: "FK_NoviAutomobil_Automobil_id",
                        column: x => x.id,
                        principalTable: "Automobil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolovniAutomobil",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kilometraza = table.Column<int>(type: "int", nullable: false),
                    stete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojVlasnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolovniAutomobil", x => x.id);
                    table.ForeignKey(
                        name: "FK_PolovniAutomobil_Automobil_id",
                        column: x => x.id,
                        principalTable: "Automobil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konfiguracija");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "NoviAutomobil");

            migrationBuilder.DropTable(
                name: "Oprema");

            migrationBuilder.DropTable(
                name: "PolovniAutomobil");

            migrationBuilder.DropTable(
                name: "Servis");

            migrationBuilder.DropTable(
                name: "Automobil");
        }
    }
}
