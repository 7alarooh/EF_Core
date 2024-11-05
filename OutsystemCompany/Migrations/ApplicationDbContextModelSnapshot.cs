﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OutsystemCompany;

#nullable disable

namespace OutsystemCompany.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OutsystemCompany.Models.Department", b =>
                {
                    b.Property<int>("Dnumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Dnumber"));

                    b.Property<string>("Dname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MgrSsn")
                        .HasColumnType("int");

                    b.Property<DateTime>("MgrStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Dnumber");

                    b.HasIndex("MgrSsn");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Dependent", b =>
                {
                    b.Property<int>("Essn")
                        .HasColumnType("int");

                    b.Property<string>("DependentName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Bdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Essn", "DependentName");

                    b.ToTable("Dependents");
                });

            modelBuilder.Entity("OutsystemCompany.Models.DeptLocation", b =>
                {
                    b.Property<int>("Dnumber")
                        .HasColumnType("int");

                    b.Property<string>("Dlocation")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Dnumber", "Dlocation");

                    b.ToTable("DeptLocations");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Employee", b =>
                {
                    b.Property<int>("Ssn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ssn"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Bdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Dno")
                        .HasColumnType("int");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Minit")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int?>("Super_Ssn")
                        .HasColumnType("int");

                    b.HasKey("Ssn");

                    b.HasIndex("Dno");

                    b.HasIndex("Super_Ssn");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Project", b =>
                {
                    b.Property<int>("Pnumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pnumber"));

                    b.Property<int>("Dnum")
                        .HasColumnType("int");

                    b.Property<string>("Plocation")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Pname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Pnumber");

                    b.HasIndex("Dnum");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Works_On", b =>
                {
                    b.Property<int>("Essn")
                        .HasColumnType("int");

                    b.Property<int>("Pno")
                        .HasColumnType("int");

                    b.Property<decimal>("Hours")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Essn", "Pno");

                    b.HasIndex("Pno");

                    b.ToTable("WorksOn");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Department", b =>
                {
                    b.HasOne("OutsystemCompany.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("MgrSsn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Dependent", b =>
                {
                    b.HasOne("OutsystemCompany.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("Essn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OutsystemCompany.Models.DeptLocation", b =>
                {
                    b.HasOne("OutsystemCompany.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Dnumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Employee", b =>
                {
                    b.HasOne("OutsystemCompany.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("Dno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutsystemCompany.Models.Employee", "Supervisor")
                        .WithMany("Supervisees")
                        .HasForeignKey("Super_Ssn");

                    b.Navigation("Department");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Project", b =>
                {
                    b.HasOne("OutsystemCompany.Models.Department", "Department")
                        .WithMany("Projects")
                        .HasForeignKey("Dnum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Works_On", b =>
                {
                    b.HasOne("OutsystemCompany.Models.Employee", "Employee")
                        .WithMany("WorksOnProjects")
                        .HasForeignKey("Essn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutsystemCompany.Models.Project", "Project")
                        .WithMany("WorksOnEmployees")
                        .HasForeignKey("Pno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Employee", b =>
                {
                    b.Navigation("Supervisees");

                    b.Navigation("WorksOnProjects");
                });

            modelBuilder.Entity("OutsystemCompany.Models.Project", b =>
                {
                    b.Navigation("WorksOnEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
