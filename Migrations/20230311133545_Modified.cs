using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prime_assignment.Migrations
{
    /// <inheritdoc />
    public partial class Modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_teamID",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "teamID",
                table: "Tasks",
                newName: "TeamID");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_teamID",
                table: "Tasks",
                newName: "IX_Tasks_TeamID");

            migrationBuilder.AlterColumn<int>(
                name: "TeamID",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamID",
                table: "Tasks",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamID",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TeamID",
                table: "Tasks",
                newName: "teamID");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TeamID",
                table: "Tasks",
                newName: "IX_Tasks_teamID");

            migrationBuilder.AlterColumn<int>(
                name: "teamID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_teamID",
                table: "Tasks",
                column: "teamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
