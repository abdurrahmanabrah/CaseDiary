using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseDiary.Migrations
{
    /// <inheritdoc />
    public partial class njhhc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseDetails_CAseMaster_CaseId",
                table: "CaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Adalot_AdalotId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Badi_BadiId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_CaseSource_CaseSourceId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Complainant_ComplainantId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Court_CourtId",
                table: "CAseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CAseMaster_Section_SectionId",
                table: "CAseMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAseMaster",
                table: "CAseMaster");

            migrationBuilder.RenameTable(
                name: "CAseMaster",
                newName: "CaseMaster");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_SectionId",
                table: "CaseMaster",
                newName: "IX_CaseMaster_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_CourtId",
                table: "CaseMaster",
                newName: "IX_CaseMaster_CourtId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_ComplainantId",
                table: "CaseMaster",
                newName: "IX_CaseMaster_ComplainantId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_CaseSourceId",
                table: "CaseMaster",
                newName: "IX_CaseMaster_CaseSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_BadiId",
                table: "CaseMaster",
                newName: "IX_CaseMaster_BadiId");

            migrationBuilder.RenameIndex(
                name: "IX_CAseMaster_AdalotId",
                table: "CaseMaster",
                newName: "IX_CaseMaster_AdalotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseMaster",
                table: "CaseMaster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseDetails_CaseMaster_CaseId",
                table: "CaseDetails",
                column: "CaseId",
                principalTable: "CaseMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseMaster_Adalot_AdalotId",
                table: "CaseMaster",
                column: "AdalotId",
                principalTable: "Adalot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseMaster_Badi_BadiId",
                table: "CaseMaster",
                column: "BadiId",
                principalTable: "Badi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseMaster_CaseSource_CaseSourceId",
                table: "CaseMaster",
                column: "CaseSourceId",
                principalTable: "CaseSource",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseMaster_Complainant_ComplainantId",
                table: "CaseMaster",
                column: "ComplainantId",
                principalTable: "Complainant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseMaster_Court_CourtId",
                table: "CaseMaster",
                column: "CourtId",
                principalTable: "Court",
                principalColumn: "CourtId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseMaster_Section_SectionId",
                table: "CaseMaster",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseDetails_CaseMaster_CaseId",
                table: "CaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseMaster_Adalot_AdalotId",
                table: "CaseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseMaster_Badi_BadiId",
                table: "CaseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseMaster_CaseSource_CaseSourceId",
                table: "CaseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseMaster_Complainant_ComplainantId",
                table: "CaseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseMaster_Court_CourtId",
                table: "CaseMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseMaster_Section_SectionId",
                table: "CaseMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseMaster",
                table: "CaseMaster");

            migrationBuilder.RenameTable(
                name: "CaseMaster",
                newName: "CAseMaster");

            migrationBuilder.RenameIndex(
                name: "IX_CaseMaster_SectionId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseMaster_CourtId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_CourtId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseMaster_ComplainantId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_ComplainantId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseMaster_CaseSourceId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_CaseSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseMaster_BadiId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_BadiId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseMaster_AdalotId",
                table: "CAseMaster",
                newName: "IX_CAseMaster_AdalotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAseMaster",
                table: "CAseMaster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseDetails_CAseMaster_CaseId",
                table: "CaseDetails",
                column: "CaseId",
                principalTable: "CAseMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_Adalot_AdalotId",
                table: "CAseMaster",
                column: "AdalotId",
                principalTable: "Adalot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_Badi_BadiId",
                table: "CAseMaster",
                column: "BadiId",
                principalTable: "Badi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_CaseSource_CaseSourceId",
                table: "CAseMaster",
                column: "CaseSourceId",
                principalTable: "CaseSource",
                principalColumn: "ID",
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

            migrationBuilder.AddForeignKey(
                name: "FK_CAseMaster_Section_SectionId",
                table: "CAseMaster",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
