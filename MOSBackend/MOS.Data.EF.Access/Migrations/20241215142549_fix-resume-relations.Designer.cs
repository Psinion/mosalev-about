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
    [Migration("20241215142549_fix-resume-relations")]
    partial class fixresumerelations
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

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.Resume", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<int>("CurrencyType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PinnedToLocale")
                        .HasColumnType("integer");

                    b.Property<int>("Salary")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

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

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeCompanyEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long?>("ResumeId")
                        .HasColumnType("bigint");

                    b.Property<string>("WebSiteUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeCompanyEntries");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateEnd")
                        .HasColumnType("date");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ResumeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeCourses");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeEducation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("DateEnd")
                        .HasColumnType("date");

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ResumeId")
                        .HasColumnType("bigint");

                    b.Property<string>("SpecializationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeEducations");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumePost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly?>("DateEnd")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DateStart")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ResumeCompanyEntryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResumeCompanyEntryId");

                    b.ToTable("ResumePosts");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeSkill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ResumeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeSkills");
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

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.Resume", b =>
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

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeCompanyEntry", b =>
                {
                    b.HasOne("MOS.Domain.Entities.Resumes.Resume", null)
                        .WithMany("CompanyEntries")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeCourse", b =>
                {
                    b.HasOne("MOS.Domain.Entities.Resumes.Resume", null)
                        .WithMany("Courses")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeEducation", b =>
                {
                    b.HasOne("MOS.Domain.Entities.Resumes.Resume", null)
                        .WithMany("Educations")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumePost", b =>
                {
                    b.HasOne("MOS.Domain.Entities.Resumes.ResumeCompanyEntry", null)
                        .WithMany("ResumePosts")
                        .HasForeignKey("ResumeCompanyEntryId");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeSkill", b =>
                {
                    b.HasOne("MOS.Domain.Entities.Resumes.Resume", null)
                        .WithMany("Skills")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.Resume", b =>
                {
                    b.Navigation("CompanyEntries");

                    b.Navigation("Courses");

                    b.Navigation("Educations");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("MOS.Domain.Entities.Resumes.ResumeCompanyEntry", b =>
                {
                    b.Navigation("ResumePosts");
                });
#pragma warning restore 612, 618
        }
    }
}