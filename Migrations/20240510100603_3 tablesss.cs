using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb4.Migrations
{
    /// <inheritdoc />
    public partial class _3tablesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Book_FK_BookId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Customers_FK_CustomerId",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loan",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "Loan",
                newName: "Loans");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_FK_CustomerId",
                table: "Loans",
                newName: "IX_Loans_FK_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_FK_BookId",
                table: "Loans",
                newName: "IX_Loans_FK_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "LoanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_FK_BookId",
                table: "Loans",
                column: "FK_BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_FK_CustomerId",
                table: "Loans",
                column: "FK_CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_FK_BookId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_FK_CustomerId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "Loan");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_FK_CustomerId",
                table: "Loan",
                newName: "IX_Loan_FK_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_FK_BookId",
                table: "Loan",
                newName: "IX_Loan_FK_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loan",
                table: "Loan",
                column: "LoanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Book_FK_BookId",
                table: "Loan",
                column: "FK_BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Customers_FK_CustomerId",
                table: "Loan",
                column: "FK_CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
