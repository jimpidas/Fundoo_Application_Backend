﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Services;

namespace Repository.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20210714152212_Repository.Services.NotesContext")]
    partial class RepositoryServicesNotesContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonLayer.DatabaseModel.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReminderOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserModelID")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("UserModelID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("CommonLayer.DatabaseModel.UserModel", b =>
                {
                    b.Property<int>("UserModelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserModelID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserModelID = 1,
                            Email = "niha@gmail.com",
                            FirstName = "Niha",
                            LastName = "Jain",
                            Password = "Pass@123"
                        },
                        new
                        {
                            UserModelID = 2,
                            Email = "janvi@gmail.com",
                            FirstName = "Janvi",
                            LastName = "Kirsten",
                            Password = "Pass@123"
                        });
                });

            modelBuilder.Entity("CommonLayer.DatabaseModel.Note", b =>
                {
                    b.HasOne("CommonLayer.DatabaseModel.UserModel", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserModelID")
                        .HasConstraintName("NotesFK_UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommonLayer.DatabaseModel.UserModel", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}