using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace th_poker_api.Migrations
{
    public partial class purchaseData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MDPaymentItem",
                columns: new[] { "IdPymItem", "CreatedBy", "CreatedOn", "Desc", "Price", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { "749be3b1-566b-4eb8-8e3b-8ac5e443df57", "System", new DateTime(2023, 6, 14, 16, 3, 30, 925, DateTimeKind.Local).AddTicks(3236), "40B", 67000f, null, null, 4E+10f },
                    { "995a034d-801c-49b1-a06e-c39fca2abb0d", "System", new DateTime(2023, 6, 14, 16, 3, 30, 925, DateTimeKind.Local).AddTicks(3249), "750B", 519000f, null, null, 7.5E+11f },
                    { "9b90f32b-6fe8-4c08-936f-f0962c33f5cc", "System", new DateTime(2023, 6, 14, 16, 3, 30, 925, DateTimeKind.Local).AddTicks(3264), "6T", 1590000f, null, null, 6E+12f },
                    { "c43fa0ad-5fd3-477a-9797-57f7a4b178de", "System", new DateTime(2023, 6, 14, 16, 3, 30, 925, DateTimeKind.Local).AddTicks(3246), "250B", 259000f, null, null, 2.5E+11f },
                    { "c67a7ab1-e264-4b67-a4df-674f300e31cd", "System", new DateTime(2023, 6, 14, 16, 3, 30, 925, DateTimeKind.Local).AddTicks(3251), "1T", 719000f, null, null, 1E+12f },
                    { "fe8c521c-b1ac-4140-97ca-c85945a07774", "System", new DateTime(2023, 6, 14, 16, 3, 30, 925, DateTimeKind.Local).AddTicks(3261), "3T", 1099000f, null, null, 3E+12f }
                });
        }
    }
}
