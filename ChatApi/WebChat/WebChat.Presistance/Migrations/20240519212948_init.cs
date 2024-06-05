using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebChat.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedGroupBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    groupimgurl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "FriendShip",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestFriendConfirm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendShip", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_FriendShip_AppUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendShip_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleteBySender = table.Column<int>(type: "int", nullable: false),
                    IsDeleteByReciver = table.Column<int>(type: "int", nullable: false),
                    MessageTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_GroupMessages_AppUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessages_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupUserId });
                    table.ForeignKey(
                        name: "FK_UserGroups_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupUserId",
                        column: x => x.GroupUserId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    FriendshipId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleteBySender = table.Column<int>(type: "int", nullable: false),
                    IsDeleteByReciver = table.Column<int>(type: "int", nullable: false),
                    MessageTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_FriendMessages_FriendShip_FriendshipId",
                        column: x => x.FriendshipId,
                        principalTable: "FriendShip",
                        principalColumn: "FriendshipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequest",
                columns: table => new
                {
                    FrindRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendShipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequest", x => x.FrindRequestId);
                    table.ForeignKey(
                        name: "FK_FriendRequest_FriendShip_FriendShipId",
                        column: x => x.FriendShipId,
                        principalTable: "FriendShip",
                        principalColumn: "FriendshipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeletedMessages",
                columns: table => new
                {
                    GroupMessagesMessageId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedMessages", x => new { x.GroupMessagesMessageId, x.Id });
                    table.ForeignKey(
                        name: "FK_DeletedMessages_GroupMessages_GroupMessagesMessageId",
                        column: x => x.GroupMessagesMessageId,
                        principalTable: "GroupMessages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppUserId",
                table: "AppUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendMessages_FriendshipId",
                table: "FriendMessages",
                column: "FriendshipId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_FriendShipId",
                table: "FriendRequest",
                column: "FriendShipId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendShip_FriendId",
                table: "FriendShip",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendShip_UserId",
                table: "FriendShip",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_GroupId",
                table: "GroupMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_SenderId",
                table: "GroupMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupUserId",
                table: "UserGroups",
                column: "GroupUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedMessages");

            migrationBuilder.DropTable(
                name: "FriendMessages");

            migrationBuilder.DropTable(
                name: "FriendRequest");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "GroupMessages");

            migrationBuilder.DropTable(
                name: "FriendShip");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
