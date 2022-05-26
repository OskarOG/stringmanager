using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StringManager.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessGroup_AccessGroup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AccessGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folder_Folder_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Folder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderAccessGroupRight",
                columns: table => new
                {
                    FolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessRights = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderAccessGroupRight", x => new { x.FolderId, x.AccessGroupId });
                    table.ForeignKey(
                        name: "FK_FolderAccessGroupRight_AccessGroup_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalTable: "AccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderAccessGroupRight_Folder_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroupUser",
                columns: table => new
                {
                    AccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroupUser", x => new { x.AccessId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AccessGroupUser_AccessGroup_AccessId",
                        column: x => x.AccessId,
                        principalTable: "AccessGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessGroupUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroup_ParentId",
                table: "AccessGroup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroupUser_UsersId",
                table: "AccessGroupUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Folder_ParentId",
                table: "Folder",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderAccessGroupRight_AccessGroupId",
                table: "FolderAccessGroupRight",
                column: "AccessGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessGroupUser");

            migrationBuilder.DropTable(
                name: "FolderAccessGroupRight");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AccessGroup");

            migrationBuilder.DropTable(
                name: "Folder");
        }
    }
}
