using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slice.Persistence.Data.Migrations
{
    public partial class UpdatedColumnNameAndAddedPaymentIntenetIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "OrderHeaders",
                newName: "SessionId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "OrderHeaders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "OrderHeaders",
                newName: "TransactionId");
        }
    }
}
