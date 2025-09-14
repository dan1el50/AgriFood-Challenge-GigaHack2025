using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaHack2025.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmerProfileWithNullableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FarmerProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LegalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyIdno = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RepFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RepLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RepDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RepGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepIdNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CropCultivation = table.Column<bool>(type: "bit", nullable: true),
                    AnimalHusbandry = table.Column<bool>(type: "bit", nullable: true),
                    MixedFarming = table.Column<bool>(type: "bit", nullable: true),
                    ProcessingActivities = table.Column<bool>(type: "bit", nullable: true),
                    TotalFarmland = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Arable = table.Column<bool>(type: "bit", nullable: true),
                    Orchards = table.Column<bool>(type: "bit", nullable: true),
                    Vineyards = table.Column<bool>(type: "bit", nullable: true),
                    Pastures = table.Column<bool>(type: "bit", nullable: true),
                    Hayfields = table.Column<bool>(type: "bit", nullable: true),
                    Greenhouses = table.Column<bool>(type: "bit", nullable: true),
                    Cattle = table.Column<int>(type: "int", nullable: true),
                    Pigs = table.Column<int>(type: "int", nullable: true),
                    Sheep = table.Column<int>(type: "int", nullable: true),
                    Goats = table.Column<int>(type: "int", nullable: true),
                    Horses = table.Column<int>(type: "int", nullable: true),
                    Poultry = table.Column<int>(type: "int", nullable: true),
                    TotalEmployees = table.Column<int>(type: "int", nullable: true),
                    FamilyMembers = table.Column<int>(type: "int", nullable: true),
                    SeasonalWorkers = table.Column<int>(type: "int", nullable: true),
                    AnnualIncome = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: true),
                    AnnualExpenses = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: true),
                    SubsidiesReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ConsentDataSharing = table.Column<bool>(type: "bit", nullable: true),
                    AcceptPrivacyPolicy = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmerProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FarmerProfiles_UserId",
                table: "FarmerProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmerProfiles");
        }
    }
}
