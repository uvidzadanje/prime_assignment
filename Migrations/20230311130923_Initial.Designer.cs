// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace prime_assignment.Migrations
{
    [DbContext(typeof(PrimeContext))]
    [Migration("20230311130923_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("MonthSalary")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TeamID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Models.Task", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AssigneeID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<int>("teamID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AssigneeID");

                    b.HasIndex("teamID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("LeaderID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("LeaderID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Models.Employee", b =>
                {
                    b.HasOne("Models.Team", null)
                        .WithMany("Employees")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("Models.Task", b =>
                {
                    b.HasOne("Models.Employee", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeID");

                    b.HasOne("Models.Team", "team")
                        .WithMany("Tasks")
                        .HasForeignKey("teamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");

                    b.Navigation("team");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.HasOne("Models.Employee", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderID");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
