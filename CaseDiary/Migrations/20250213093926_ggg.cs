using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseDiary.Migrations
{
    /// <inheritdoc />
    public partial class ggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_complainants_ComplainantId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_court_CourtId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_criminals_CriminalId",
                table: "CAseMaster");

            migrationBuilder.DropTable(
                name: "criminals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_court",
                table: "court");

            migrationBuilder.DropPrimaryKey(
                name: "PK_complainants",
                table: "complainants");

            migrationBuilder.RenameTable(
                name: "court",
                newName: "Court");

            migrationBuilder.RenameTable(
                name: "complainants",
                newName: "Complainant");

            migrationBuilder.RenameColumn(
                name: "CriminalId",
                table: "CAseMaster",
                newName: "BadiId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_CriminalId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_BadiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Court",
                table: "Court",
                column: "CourtId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complainant",
                table: "Complainant",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Badi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BadiName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Badi", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_Badi_BadiId",
                table: "CAseMaster",
                column: "BadiId",
                principalTable: "Badi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_Complainant_ComplainantId",
                table: "CAseMaster",
                column: "ComplainantId",
                principalTable: "Complainant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_Court_CourtId",
                table: "CAseMaster",
                column: "CourtId",
                principalTable: "Court",
                principalColumn: "CourtId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Badi_BadiId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Complainant_ComplainantId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Court_CourtId",
                table: "CAseMaster");

            migrationBuilder.DropTable(
                name: "Badi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Court",
                table: "Court");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complainant",
                table: "Complainant");

            migrationBuilder.RenameTable(
                name: "Court",
                newName: "court");

            migrationBuilder.RenameTable(
                name: "Complainant",
                newName: "complainants");

            migrationBuilder.RenameColumn(
                name: "BadiId",
                table: "CAseMaster",
                newName: "CriminalId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_BadiId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_CriminalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_court",
                table: "court",
                column: "CourtId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_complainants",
                table: "complainants",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "criminals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConvictionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrimeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriminalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_criminals", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_complainants_ComplainantId",
                table: "CAseMaster",
                column: "ComplainantId",
                principalTable: "complainants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_court_CourtId",
                table: "CAseMaster",
                column: "CourtId",
                principalTable: "court",
                principalColumn: "CourtId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_criminals_CriminalId",
                table: "CAseMaster",
                column: "CriminalId",
                principalTable: "criminals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
