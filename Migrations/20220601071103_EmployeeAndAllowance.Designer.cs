// <auto-generated />
using System;
using CRUDWEPAPI_EF.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDWEPAPI_EF.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20220601071103_EmployeeAndAllowance")]
    partial class EmployeeAndAllowance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.EmployeeModel.Allowances", b =>
                {
                    b.Property<int>("employeeAllowancesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("allowanceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("employeeID")
                        .HasColumnType("int");

                    b.HasKey("employeeAllowancesID");

                    b.HasIndex("employeeID");

                    b.ToTable("Allowance");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.EmployeeModel.Employee", b =>
                {
                    b.Property<int>("employeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("employeeDept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employeeEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("promotedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("employeeID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.EmployeeModel.Allowances", b =>
                {
                    b.HasOne("CRUDWEPAPI_EF.Model.EmployeeModel.Employee", "Employee")
                        .WithMany("Allowances")
                        .HasForeignKey("employeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.Order", b =>
                {
                    b.HasOne("CRUDWEPAPI_EF.Model.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CRUDWEPAPI_EF.Model.EmployeeModel.Employee", b =>
                {
                    b.Navigation("Allowances");
                });
#pragma warning restore 612, 618
        }
    }
}
