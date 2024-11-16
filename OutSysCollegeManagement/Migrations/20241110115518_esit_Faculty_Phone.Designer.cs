﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OutSysCollegeManagement;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    [DbContext(typeof(CollegeDbContext))]
    [Migration("20241110115518_esit_Faculty_Phone")]
    partial class esit_Faculty_Phone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesCourse_id")
                        .HasColumnType("int");

                    b.Property<int>("StudentsSID")
                        .HasColumnType("int");

                    b.HasKey("CoursesCourse_id", "StudentsSID");

                    b.HasIndex("StudentsSID");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Course", b =>
                {
                    b.Property<int>("Course_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Course_id"));

                    b.Property<string>("Course_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Department_id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("FacultyFid")
                        .HasColumnType("int");

                    b.HasKey("Course_id");

                    b.HasIndex("Department_id");

                    b.HasIndex("FacultyFid");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Department", b =>
                {
                    b.Property<int>("Department_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Department_id"));

                    b.Property<string>("D_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Department_id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Exams", b =>
                {
                    b.Property<int>("Exam_code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Exam_code"));

                    b.Property<string>("D_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Department_id")
                        .HasColumnType("int");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Exam_code");

                    b.HasIndex("Department_id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Faculty", b =>
                {
                    b.Property<int>("Fid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Fid"));

                    b.Property<int?>("Department_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Fid");

                    b.HasIndex("Department_id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Faculty_Phone", b =>
                {
                    b.Property<int>("Fid")
                        .HasColumnType("int");

                    b.Property<string>("Phone_no")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Fid", "Phone_no");

                    b.ToTable("Faculty_Phone");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Hostel", b =>
                {
                    b.Property<int>("Hostel_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Hostel_id"));

                    b.Property<string>("Hostel_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("No_of_seats")
                        .HasColumnType("int");

                    b.HasKey("Hostel_id");

                    b.ToTable("Hostels");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Student", b =>
                {
                    b.Property<int>("SID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SID"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("F_id")
                        .HasColumnType("int");

                    b.Property<int?>("Hostel_id")
                        .HasColumnType("int");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("SID");

                    b.HasIndex("F_id");

                    b.HasIndex("Hostel_id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Subject", b =>
                {
                    b.Property<int>("Subject_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Subject_id"));

                    b.Property<int?>("F_id")
                        .HasColumnType("int");

                    b.Property<string>("Subject_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Subject_id");

                    b.HasIndex("F_id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.student_Phone", b =>
                {
                    b.Property<int>("SID")
                        .HasColumnType("int");

                    b.Property<string>("Phone_no")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SID", "Phone_no");

                    b.ToTable("Student_Phones");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourse_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutSysCollegeManagement.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsSID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Course", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutSysCollegeManagement.Models.Faculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyFid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Exams", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_id");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Faculty", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_id");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Faculty_Phone", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Faculty", "Faculty")
                        .WithMany("Faculty_Phones")
                        .HasForeignKey("Fid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Student", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("F_id");

                    b.HasOne("OutSysCollegeManagement.Models.Hostel", "Hostel")
                        .WithMany("Students")
                        .HasForeignKey("Hostel_id");

                    b.Navigation("Faculty");

                    b.Navigation("Hostel");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Subject", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Faculty", "Faculty")
                        .WithMany("Subjects")
                        .HasForeignKey("F_id");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.student_Phone", b =>
                {
                    b.HasOne("OutSysCollegeManagement.Models.Student", "Student")
                        .WithMany("StudentPhones")
                        .HasForeignKey("SID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Faculty", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Faculty_Phones");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Hostel", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("OutSysCollegeManagement.Models.Student", b =>
                {
                    b.Navigation("StudentPhones");
                });
#pragma warning restore 612, 618
        }
    }
}
