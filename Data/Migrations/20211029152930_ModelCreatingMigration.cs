using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ModelCreatingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPerson");

            migrationBuilder.CreateTable(
                name: "GroupDAOPersonDAO",
                columns: table => new
                {
                    GroupsId = table.Column<long>(type: "bigint", nullable: false),
                    PersonsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDAOPersonDAO", x => new { x.GroupsId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_GroupDAOPersonDAO_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDAOPersonDAO_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupDAOPersonDAO_PersonsId",
                table: "GroupDAOPersonDAO",
                column: "PersonsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupDAOPersonDAO");

            migrationBuilder.CreateTable(
                name: "GroupPerson",
                columns: table => new
                {
                    GroupsId = table.Column<long>(type: "bigint", nullable: false),
                    PersonsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPerson", x => new { x.GroupsId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_GroupPerson_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPerson_PersonsId",
                table: "GroupPerson",
                column: "PersonsId");
        }
    }
}
