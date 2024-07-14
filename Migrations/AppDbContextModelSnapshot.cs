﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace kebab.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kebab.Models.Block", b =>
                {
                    b.Property<int>("BlockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BlockId"));

                    b.Property<byte[]>("BlockHash")
                        .HasColumnType("bytea");

                    b.Property<string>("Nonce")
                        .HasColumnType("text");

                    b.Property<byte[]>("PreviousHash")
                        .HasColumnType("bytea");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("BlockId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("Kebab.Models.Transaction", b =>
                {
                    b.Property<int>("BlockId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("BlockId", "Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Kebab.Models.TransactionInput", b =>
                {
                    b.Property<int>("BlockId")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionId")
                        .HasColumnType("integer");

                    b.Property<int>("OutputIndex")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Signature")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int?>("TransactionBlockId")
                        .HasColumnType("integer");

                    b.HasKey("BlockId", "TransactionId", "OutputIndex");

                    b.HasIndex("TransactionBlockId", "TransactionId");

                    b.ToTable("TransactionInputs");
                });

            modelBuilder.Entity("Kebab.Models.TransactionOutput", b =>
                {
                    b.Property<int>("BlockId")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionId")
                        .HasColumnType("integer");

                    b.Property<int>("OutputIndex")
                        .HasColumnType("integer");

                    b.Property<int>("Nonce")
                        .HasColumnType("integer");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TransactionBlockId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("BlockId", "TransactionId", "OutputIndex");

                    b.HasIndex("TransactionBlockId", "TransactionId");

                    b.ToTable("TransactionOutputs");
                });

            modelBuilder.Entity("Kebab.Models.Transaction", b =>
                {
                    b.HasOne("Kebab.Models.Block", "block")
                        .WithMany("Transactions")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("block");
                });

            modelBuilder.Entity("Kebab.Models.TransactionInput", b =>
                {
                    b.HasOne("Kebab.Models.Transaction", "Transaction")
                        .WithMany("Inputs")
                        .HasForeignKey("TransactionBlockId", "TransactionId");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Kebab.Models.TransactionOutput", b =>
                {
                    b.HasOne("Kebab.Models.Transaction", "Transaction")
                        .WithMany("Outputs")
                        .HasForeignKey("TransactionBlockId", "TransactionId");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Kebab.Models.Block", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Kebab.Models.Transaction", b =>
                {
                    b.Navigation("Inputs");

                    b.Navigation("Outputs");
                });
#pragma warning restore 612, 618
        }
    }
}
