using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiqueAuction.Infrastructure.Migrations
{
    public partial class UpdateAutoBid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncrementPerUnit",
                table: "AutoBids");

            migrationBuilder.DropColumn(
                name: "MaxBidAmount",
                table: "AutoBids");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "IncrementPerUnit",
                table: "AutoBids",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<double>(
                name: "MaxBidAmount",
                table: "AutoBids",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
