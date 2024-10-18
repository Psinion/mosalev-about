﻿// <auto-generated />
using System;
using MOS.Data.EF.Access.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOS.Data.EF.Access.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20241018175950_create-index-content")]
    partial class createindexcontent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MOS.Domain.Entities.Index.IndexContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UserCreateId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserDeleteId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserUpdateId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("UserDeleteId");

                    b.HasIndex("UserUpdateId");

                    b.ToTable("IndexContents");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Index.IndexContent", b =>
                {
                    b.HasOne("MOS.Domain.Entities.Users.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId");

                    b.HasOne("MOS.Domain.Entities.Users.User", "UserDelete")
                        .WithMany()
                        .HasForeignKey("UserDeleteId");

                    b.HasOne("MOS.Domain.Entities.Users.User", "UserUpdate")
                        .WithMany()
                        .HasForeignKey("UserUpdateId");

                    b.Navigation("UserCreate");

                    b.Navigation("UserDelete");

                    b.Navigation("UserUpdate");
                });
#pragma warning restore 612, 618
        }
    }
}
