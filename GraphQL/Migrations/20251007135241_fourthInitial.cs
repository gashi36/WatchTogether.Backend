using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQL.Migrations
{
    /// <inheritdoc />
    public partial class fourthInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Rooms_RoomId",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_RoomId",
                table: "Guests",
                newName: "IX_Guests_RoomId");

            migrationBuilder.AddColumn<double>(
                name: "CurrentTime",
                table: "Rooms",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlaying",
                table: "Rooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "Messages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GuestId",
                table: "Messages",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Rooms_RoomId",
                table: "Guests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Guests_GuestId",
                table: "Messages",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "GuestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Rooms_RoomId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Guests_GuestId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_GuestId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CurrentTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsPlaying",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_RoomId",
                table: "Guest",
                newName: "IX_Guest_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Rooms_RoomId",
                table: "Guest",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
