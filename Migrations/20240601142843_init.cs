using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace kebab.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => new { x.BlockId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "TransactionInputs",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "integer", nullable: false),
                    txid = table.Column<string>(type: "text", nullable: false),
                    OutputIndex = table.Column<int>(type: "integer", nullable: false),
                    Signature = table.Column<byte[]>(type: "bytea", nullable: false),
                    TransactionBlockId = table.Column<int>(type: "integer", nullable: true),
                    TransactionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionInputs", x => new { x.BlockId, x.txid, x.OutputIndex });
                    table.ForeignKey(
                        name: "FK_TransactionInputs_Transactions_TransactionBlockId_Transacti~",
                        columns: x => new { x.TransactionBlockId, x.TransactionId },
                        principalTable: "Transactions",
                        principalColumns: new[] { "BlockId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "TransactionOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    PublicKey = table.Column<string>(type: "text", nullable: false),
                    Nonce = table.Column<int>(type: "integer", nullable: false),
                    TransactionBlockId = table.Column<int>(type: "integer", nullable: true),
                    TransactionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionOutputs_Transactions_TransactionBlockId_Transact~",
                        columns: x => new { x.TransactionBlockId, x.TransactionId },
                        principalTable: "Transactions",
                        principalColumns: new[] { "BlockId", "Id" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionInputs_TransactionBlockId_TransactionId",
                table: "TransactionInputs",
                columns: new[] { "TransactionBlockId", "TransactionId" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOutputs_TransactionBlockId_TransactionId",
                table: "TransactionOutputs",
                columns: new[] { "TransactionBlockId", "TransactionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionInputs");

            migrationBuilder.DropTable(
                name: "TransactionOutputs");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
