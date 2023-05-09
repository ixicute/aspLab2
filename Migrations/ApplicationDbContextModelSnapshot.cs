﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkolprojektLab2.Data;

#nullable disable

namespace SkolprojektLab2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkolprojektLab2.Models.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassId"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClassId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.RelationshipTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FK_ClassId")
                        .HasColumnType("int");

                    b.Property<int>("FK_CourseId")
                        .HasColumnType("int");

                    b.Property<int>("FK_StudentId")
                        .HasColumnType("int");

                    b.Property<int>("FK_TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FK_ClassId");

                    b.HasIndex("FK_CourseId");

                    b.HasIndex("FK_StudentId");

                    b.HasIndex("FK_TeacherId");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.RelationshipTable", b =>
                {
                    b.HasOne("SkolprojektLab2.Models.Class", "Classes")
                        .WithMany("Relationships")
                        .HasForeignKey("FK_ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkolprojektLab2.Models.Course", "Courses")
                        .WithMany("Relationships")
                        .HasForeignKey("FK_CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkolprojektLab2.Models.Student", "Students")
                        .WithMany("Relationships")
                        .HasForeignKey("FK_StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkolprojektLab2.Models.Teacher", "Teachers")
                        .WithMany("Relationships")
                        .HasForeignKey("FK_TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classes");

                    b.Navigation("Courses");

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Class", b =>
                {
                    b.Navigation("Relationships");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Course", b =>
                {
                    b.Navigation("Relationships");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Student", b =>
                {
                    b.Navigation("Relationships");
                });

            modelBuilder.Entity("SkolprojektLab2.Models.Teacher", b =>
                {
                    b.Navigation("Relationships");
                });
#pragma warning restore 612, 618
        }
    }
}