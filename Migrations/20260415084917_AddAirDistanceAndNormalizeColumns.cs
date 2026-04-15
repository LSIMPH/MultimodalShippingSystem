using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultimodalShippingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAirDistanceAndNormalizeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "RoadShipments");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "RoadShipments");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "AirShipments");

            migrationBuilder.DropColumn(
                name: "IsPriority",
                table: "AirShipments");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AirShipments");

            migrationBuilder.RenameColumn(
                name: "senderName",
                table: "Shipments",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "receiverName",
                table: "Shipments",
                newName: "ReceiverName");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "RoadShipments",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "distance",
                table: "RoadShipments",
                newName: "Distance");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "AirShipments",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "isPriority",
                table: "AirShipments",
                newName: "IsPriority");

            migrationBuilder.RenameColumn(
                name: "distance",
                table: "AirShipments",
                newName: "Distance");

            migrationBuilder.AlterColumn<string>(
                name: "SenderName",
                table: "Shipments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverName",
                table: "Shipments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Shipments",
                newName: "senderName");

            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                table: "Shipments",
                newName: "receiverName");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "RoadShipments",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "RoadShipments",
                newName: "distance");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "AirShipments",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "IsPriority",
                table: "AirShipments",
                newName: "isPriority");

            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "AirShipments",
                newName: "distance");

            migrationBuilder.AlterColumn<string>(
                name: "senderName",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "receiverName",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Shipments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Shipments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Distance",
                table: "RoadShipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "RoadShipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Distance",
                table: "AirShipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPriority",
                table: "AirShipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "AirShipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
