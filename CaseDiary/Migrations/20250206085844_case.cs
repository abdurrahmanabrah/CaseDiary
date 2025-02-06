using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseDiary.Migrations
{
    /// <inheritdoc />
    public partial class @case : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adalot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdalotName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adalot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseSource",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseSource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "complainants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complainants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "court",
                columns: table => new
                {
                    CourtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_court", x => x.CourtId);
                });

            migrationBuilder.CreateTable(
                name: "criminals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriminalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CrimeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConvictionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_criminals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SectionNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAseMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    CriminalId = table.Column<int>(type: "int", nullable: false),
                    ComplainantId = table.Column<int>(type: "int", nullable: false),
                    CaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseSourceId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    AdalotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAseMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAseMaster_Adalot_AdalotId",
                        column: x => x.AdalotId,
                        principalTable: "Adalot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAseMaster_CaseSource_CaseSourceId",
                        column: x => x.CaseSourceId,
                        principalTable: "CaseSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAseMaster_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAseMaster_complainants_ComplainantId",
                        column: x => x.ComplainantId,
                        principalTable: "complainants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAseMaster_court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "court",
                        principalColumn: "CourtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAseMaster_criminals_CriminalId",
                        column: x => x.CriminalId,
                        principalTable: "criminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentHearingDate = table.Column<DateTime>(type: "date", nullable: false),
                    NextHearingDate = table.Column<DateTime>(type: "date", nullable: false),
                    Hiring = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseDetails_CAseMaster_CaseId",
                        column: x => x.CaseId,
                        principalTable: "CAseMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseDetails_CaseId",
                table: "CaseDetails",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CAseMaster_AdalotId",
                table: "CAseMaster",
                column: "AdalotId");

            migrationBuilder.CreateIndex(
                name: "IX_CAseMaster_CaseSourceId",
                table: "CAseMaster",
                column: "CaseSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CAseMaster_ComplainantId",
                table: "CAseMaster",
                column: "ComplainantId");

            migrationBuilder.CreateIndex(
                name: "IX_CAseMaster_CourtId",
                table: "CAseMaster",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CAseMaster_CriminalId",
                table: "CAseMaster",
                column: "CriminalId");

            migrationBuilder.CreateIndex(
                name: "IX_CAseMaster_SectionId",
                table: "CAseMaster",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseDetails");

            migrationBuilder.DropTable(
                name: "CAseMaster");

            migrationBuilder.DropTable(
                name: "Adalot");

            migrationBuilder.DropTable(
                name: "CaseSource");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "complainants");

            migrationBuilder.DropTable(
                name: "court");

            migrationBuilder.DropTable(
                name: "criminals");
        }
    }
}
