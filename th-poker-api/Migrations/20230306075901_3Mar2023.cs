using System;
using Microsoft.EntityFrameworkCore.Migrations;
using th_poker_api.Model.Game;

#nullable disable

namespace th_poker_api.Migrations
{
    public partial class _3Mar2023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameplayHeader",
                columns: table => new
                {
                    GameplayHeaderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdTSRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Card1 = table.Column<int>(type: "int", nullable: false),
                    Card2 = table.Column<int>(type: "int", nullable: false),
                    Card3 = table.Column<int>(type: "int", nullable: false),
                    Card4 = table.Column<int>(type: "int", nullable: false),
                    Card5 = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameplayHeader", x => x.GameplayHeaderID);
                });

            migrationBuilder.CreateTable(
                name: "MDCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndexCard = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MDGames",
                columns: table => new
                {
                    IdMDGames = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyInMin = table.Column<float>(type: "real", nullable: false),
                    BuyInMax = table.Column<float>(type: "real", nullable: false),
                    StakesMin = table.Column<float>(type: "real", nullable: false),
                    StakesMax = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDGames", x => x.IdMDGames);
                });

            migrationBuilder.CreateTable(
                name: "MDPaymentItem",
                columns: table => new
                {
                    IdPymItem = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDPaymentItem", x => x.IdPymItem);
                });

            migrationBuilder.CreateTable(
                name: "MDRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MDStatuses",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDStatuses", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "MDUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ReferalJoin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    LoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    profilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                columns: table => new
                {
                    IDPayment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDPurchaseItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDPaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.IDPayment);
                });

            migrationBuilder.CreateTable(
                name: "paymentMethods",
                columns: table => new
                {
                    IDPaymentMethod = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentMethods", x => x.IDPaymentMethod);
                });

            migrationBuilder.CreateTable(
                name: "RolesDashboard",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesDashboard", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    IDStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.IDStatus);
                });

            migrationBuilder.CreateTable(
                name: "tableBets",
                columns: table => new
                {
                    idTableBet = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idTSRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amt = table.Column<float>(type: "real", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tableBets", x => x.idTableBet);
                });

            migrationBuilder.CreateTable(
                name: "TSPurchases",
                columns: table => new
                {
                    IdPurchase = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdPymItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Amount_a = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSPurchases", x => x.IdPurchase);
                });

            migrationBuilder.CreateTable(
                name: "UserAmount",
                columns: table => new
                {
                    AmountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FreeSpin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdsCount = table.Column<int>(type: "int", nullable: true),
                    TimeForAds = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAmount", x => x.AmountID);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginDashboard",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginDashboard", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "WinType",
                columns: table => new
                {
                    IdWinType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinType", x => x.IdWinType);
                });

            migrationBuilder.CreateTable(
                name: "MDGameType",
                columns: table => new
                {
                    IdMDGameType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MDGamesIdMDGames = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDGameType", x => x.IdMDGameType);
                    table.ForeignKey(
                        name: "FK_MDGameType_MDGames_MDGamesIdMDGames",
                        column: x => x.MDGamesIdMDGames,
                        principalTable: "MDGames",
                        principalColumn: "IdMDGames");
                });

            migrationBuilder.CreateTable(
                name: "MDRoomList",
                columns: table => new
                {
                    IdTSRoom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<bool>(type: "bit", nullable: false),
                    MaxPlayer = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MDGamesIDIdMDGames = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDRoomList", x => x.IdTSRoom);
                    table.ForeignKey(
                        name: "FK_MDRoomList_MDGames_MDGamesIDIdMDGames",
                        column: x => x.MDGamesIDIdMDGames,
                        principalTable: "MDGames",
                        principalColumn: "IdMDGames");
                });

            migrationBuilder.CreateTable(
                name: "MDFriends",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userCreateID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDFriends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MDFriends_MDUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MDFriendship",
                columns: table => new
                {
                    RequesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddresseeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDFriendship", x => new { x.RequesterId, x.AddresseeId });
                    table.ForeignKey(
                        name: "FriendshipToAddressee_FK",
                        column: x => x.AddresseeId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FriendshipToRequester_FK",
                        column: x => x.RequesterId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UsersLogin",
                columns: table => new
                {
                    IdUsersLogin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUserUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogin", x => x.IdUsersLogin);
                    table.ForeignKey(
                        name: "FK_UsersLogin_MDUsers_IdUserUserId",
                        column: x => x.IdUserUserId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UsersProfile",
                columns: table => new
                {
                    IDProfile = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUserUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Profile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProfile", x => x.IDProfile);
                    table.ForeignKey(
                        name: "FK_UsersProfile_MDUsers_IdUserUserId",
                        column: x => x.IdUserUserId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UsersReferal",
                columns: table => new
                {
                    ReferalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersIdUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReferalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersReferal", x => x.ReferalId);
                    table.ForeignKey(
                        name: "FK_UsersReferal_MDUsers_UsersIdUserId",
                        column: x => x.UsersIdUserId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UsersToken",
                columns: table => new
                {
                    UserTokenID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUserUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTokenGoogle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTokenFacebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToken", x => x.UserTokenID);
                    table.ForeignKey(
                        name: "FK_UsersToken_MDUsers_IdUserUserId",
                        column: x => x.IdUserUserId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "GameplayDetail",
                columns: table => new
                {
                    GameplayDetailID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GMHeaderIdGameplayHeaderID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdUserUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDTSRooms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardUser1 = table.Column<int>(type: "int", nullable: true),
                    CardUser2 = table.Column<int>(type: "int", nullable: true),
                    Card1 = table.Column<int>(type: "int", nullable: true),
                    Card2 = table.Column<int>(type: "int", nullable: true),
                    Card3 = table.Column<int>(type: "int", nullable: true),
                    Card4 = table.Column<int>(type: "int", nullable: true),
                    Card5 = table.Column<int>(type: "int", nullable: true),
                    PrevBal = table.Column<float>(type: "real", nullable: true),
                    Balance = table.Column<float>(type: "real", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinTypeIdIdWinType = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameplayDetail", x => x.GameplayDetailID);
                    table.ForeignKey(
                        name: "FK_GameplayDetail_GameplayHeader_GMHeaderIdGameplayHeaderID",
                        column: x => x.GMHeaderIdGameplayHeaderID,
                        principalTable: "GameplayHeader",
                        principalColumn: "GameplayHeaderID");
                    table.ForeignKey(
                        name: "FK_GameplayDetail_MDUsers_IdUserUserId",
                        column: x => x.IdUserUserId,
                        principalTable: "MDUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameplayDetail_WinType_WinTypeIdIdWinType",
                        column: x => x.WinTypeIdIdWinType,
                        principalTable: "WinType",
                        principalColumn: "IdWinType");
                });

            migrationBuilder.InsertData(
                table: "MDCards",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Desc", "IndexCard", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3277), "Clover 2", 0, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3278) },
                    { 2, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3279), "Diamond 2", 1, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3279) },
                    { 3, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3280), "Heart 2", 2, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3280) },
                    { 4, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3281), "Spade 2", 3, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3282) },
                    { 5, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3282), "Clover 3", 4, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3283) },
                    { 6, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3283), "Diamond 3", 5, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3284) },
                    { 7, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3284), "Heart 3", 6, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3285) },
                    { 8, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3285), "Spade 3", 7, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3286) },
                    { 9, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3287), "Clover 4", 8, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3287) },
                    { 10, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3288), "Diamond 4", 9, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3289) },
                    { 11, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3289), "Heart 4", 10, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3290) },
                    { 12, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3291), "Spade 4", 11, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3291) },
                    { 13, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3292), "Clover 5", 12, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3292) },
                    { 14, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3293), "Diamond 5", 13, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3293) },
                    { 15, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3294), "Heart 5", 14, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3294) },
                    { 16, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3295), "Spade 5", 15, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3295) },
                    { 17, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3296), "Clover 6", 16, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3296) },
                    { 18, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3297), "Diamond 6", 17, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3297) },
                    { 19, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3298), "Heart 6", 18, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3298) },
                    { 20, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3299), "Spade 6", 19, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3300) },
                    { 21, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3300), "Clover 7", 20, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3301) },
                    { 22, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3301), "Diamond 7", 21, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3302) },
                    { 23, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3302), "Heart 7", 22, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3303) },
                    { 24, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3303), "Spade 7", 23, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3304) },
                    { 25, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3304), "Clover 8", 24, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3305) },
                    { 26, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3305), "Diamond 8", 25, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3306) },
                    { 27, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3306), "Heart 8", 26, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3307) },
                    { 28, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3308), "Spade 8", 27, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3308) },
                    { 29, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3309), "Clover 9", 28, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3309) },
                    { 30, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3310), "Diamond 9", 29, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3310) },
                    { 31, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3311), "Heart 9", 30, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3311) },
                    { 32, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3312), "Spade 9", 31, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3313) },
                    { 33, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3313), "Clover 10", 32, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3314) },
                    { 34, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3314), "Diamond 10", 33, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3315) },
                    { 35, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3315), "Heart 10", 34, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3316) },
                    { 36, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3317), "Spade 10", 35, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3317) },
                    { 37, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3318), "Clover A", 36, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3318) },
                    { 38, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3319), "Diamond A", 37, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3319) },
                    { 39, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3320), "Heart A", 38, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3320) },
                    { 40, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3321), "Spade A", 39, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3321) },
                    { 41, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3322), "Clover J", 40, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3322) },
                    { 42, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3323), "Diamond J", 41, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3323) }
                });

            migrationBuilder.InsertData(
                table: "MDCards",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Desc", "IndexCard", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 43, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3324), "Heart J", 42, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3324) },
                    { 44, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3325), "Spade J", 43, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3325) },
                    { 45, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3326), "Clover Q", 44, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3326) },
                    { 46, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3327), "Diamond Q", 45, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3328) },
                    { 47, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3328), "Heart Q", 46, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3329) },
                    { 48, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3329), "Spade Q", 47, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3330) },
                    { 49, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3330), "Clover K", 48, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3331) },
                    { 50, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3332), "Diamond K", 49, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3332) },
                    { 51, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3333), "Heart K", 50, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3333) },
                    { 52, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3334), "Spade K", 51, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3334) }
                });

            migrationBuilder.InsertData(
                table: "MDGameType",
                columns: new[] { "IdMDGameType", "CreatedBy", "CreatedOn", "GameDesc", "MDGamesIdMDGames", "UpdatedBy", "UpdatedOn", "isActive" },
                values: new object[] { "1", null, null, "Poker", null, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3191), false });

            migrationBuilder.InsertData(
                table: "MDGames",
                columns: new[] { "IdMDGames", "BuyInMax", "BuyInMin", "CreatedBy", "CreatedOn", "GameDesc", "GameTitle", "StakesMax", "StakesMin", "UpdatedBy", "UpdatedOn", "isActive" },
                values: new object[,]
                {
                    { "ac660c4d-ecdb-4e9b-a1eb-7720eea21461", 10000f, 2000f, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3241), "Casino \n Singapore", "Marina Bay sands", 1000f, 500f, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3240), true },
                    { "ae1cb439-3328-40c3-812d-9bd684e8881b", 30000f, 10000f, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3269), "Las Vegas \n Nevada", "", 3000f, 1500f, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3268), true },
                    { "b463af03-9a95-4a46-855e-b1f1e07a0b8b", 20000f, 5000f, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3265), "Casino \n Lisboa", "", 1500f, 800f, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3265), true }
                });

            migrationBuilder.InsertData(
                table: "MDPaymentItem",
                columns: new[] { "IdPymItem", "CreatedBy", "CreatedOn", "Desc", "Price", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { "0b3605c3-80fc-4410-92b1-669bcf41d1e9", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3360), "Bronze", 50f, null, null, 10f },
                    { "0f2b4216-eb7f-456a-8839-de9e62427d56", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3363), "Silver", 100f, null, null, 20f },
                    { "1def6a4a-51e1-4ec8-9707-acdc6b4ad4aa", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3374), "Mythic", 500f, null, null, 100f },
                    { "499834a8-a42e-4970-ae08-995c6ee1cff4", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3366), "Gold", 150f, null, null, 30f },
                    { "a1c74d01-b9c3-4b11-a235-a371268e424a", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3370), "Platinum", 250f, null, null, 50f },
                    { "a398ae24-a61a-4465-912b-0c8757b32958", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3368), "Diamond", 200f, null, null, 40f },
                    { "d683af6f-46af-4ff4-af4f-bee062cd692e", "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3372), "Legend", 300f, null, null, 60f }
                });

            migrationBuilder.InsertData(
                table: "MDRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Desc", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3158), "Player", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3159) },
                    { 2, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3160), "Admin", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3161) },
                    { 3, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3162), "User", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3162) }
                });

            migrationBuilder.InsertData(
                table: "MDStatuses",
                columns: new[] { "IdStatus", "CreatedBy", "CreatedOn", "Desc", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3173), "Active", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3173) },
                    { 2, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3174), "Banned", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3175) },
                    { 3, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3176), "PAID", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3176) },
                    { 4, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3177), "UNPAID", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3177) },
                    { 5, "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3178), "HOLD", "SYSTEM", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3178) }
                });

            migrationBuilder.InsertData(
                table: "WinType",
                columns: new[] { "IdWinType", "CreatedBy", "CreatedOn", "Desc", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3075), "High Card", null, null },
                    { 2, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3084), "Pair", null, null },
                    { 3, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3085), "Two pair", null, null },
                    { 4, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3086), "Three of a kind", null, null },
                    { 5, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3087), "Straight", null, null },
                    { 6, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3087), "Flush", null, null },
                    { 7, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3088), "Full House", null, null },
                    { 8, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3088), "Four of a Kind", null, null },
                    { 9, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3089), "Straight Flush", null, null },
                    { 10, "System", new DateTime(2023, 3, 6, 14, 59, 1, 783, DateTimeKind.Local).AddTicks(3090), "Royal Flush", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameplayDetail_GMHeaderIdGameplayHeaderID",
                table: "GameplayDetail",
                column: "GMHeaderIdGameplayHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_GameplayDetail_IdUserUserId",
                table: "GameplayDetail",
                column: "IdUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameplayDetail_WinTypeIdIdWinType",
                table: "GameplayDetail",
                column: "WinTypeIdIdWinType");

            migrationBuilder.CreateIndex(
                name: "IX_MDFriends_UserId",
                table: "MDFriends",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MDFriendship_AddresseeId",
                table: "MDFriendship",
                column: "AddresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_MDGameType_MDGamesIdMDGames",
                table: "MDGameType",
                column: "MDGamesIdMDGames");

            migrationBuilder.CreateIndex(
                name: "IX_MDRoomList_MDGamesIDIdMDGames",
                table: "MDRoomList",
                column: "MDGamesIDIdMDGames");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLogin_IdUserUserId",
                table: "UsersLogin",
                column: "IdUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersProfile_IdUserUserId",
                table: "UsersProfile",
                column: "IdUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReferal_UsersIdUserId",
                table: "UsersReferal",
                column: "UsersIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToken_IdUserUserId",
                table: "UsersToken",
                column: "IdUserUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameplayDetail");

            migrationBuilder.DropTable(
                name: "MDCards");

            migrationBuilder.DropTable(
                name: "MDFriends");

            migrationBuilder.DropTable(
                name: "MDFriendship");

            migrationBuilder.DropTable(
                name: "MDGameType");

            migrationBuilder.DropTable(
                name: "MDPaymentItem");

            migrationBuilder.DropTable(
                name: "MDRoles");

            migrationBuilder.DropTable(
                name: "MDRoomList");

            migrationBuilder.DropTable(
                name: "MDStatuses");

            migrationBuilder.DropTable(
                name: "PaymentHistory");

            migrationBuilder.DropTable(
                name: "paymentMethods");

            migrationBuilder.DropTable(
                name: "RolesDashboard");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "tableBets");

            migrationBuilder.DropTable(
                name: "TSPurchases");

            migrationBuilder.DropTable(
                name: "UserAmount");

            migrationBuilder.DropTable(
                name: "UserLoginDashboard");

            migrationBuilder.DropTable(
                name: "UsersLogin");

            migrationBuilder.DropTable(
                name: "UsersProfile");

            migrationBuilder.DropTable(
                name: "UsersReferal");

            migrationBuilder.DropTable(
                name: "UsersToken");

            migrationBuilder.DropTable(
                name: "GameplayHeader");

            migrationBuilder.DropTable(
                name: "WinType");

            migrationBuilder.DropTable(
                name: "MDGames");

            migrationBuilder.DropTable(
                name: "MDUsers");
        }
    }
}
