using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Services
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public  DbSet<Label> Labels { get; set; }
        public  DbSet<NoteLabel> NoteLabels { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                UserModelID = 1,
                FirstName = "Niha",
                LastName = "Jain",
                Email = "niha@gmail.com",
                Password = "Pass@123"
            }, new UserModel
            {
                UserModelID = 2,
                FirstName = "Janvi",
                LastName = "Kirsten",
                Email = "janvi@gmail.com",
                Password = "Pass@123"
            });*/
            modelBuilder.Entity<Note>(entity =>
            {

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Reminder)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");


                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserModelID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("NotesFK_UserID");

                
            });

            modelBuilder.Entity<Label>(entity =>
            {

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Labels)
                    .HasForeignKey(d => d.UserModelID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LabelsFK_UserID");
            });

           /* modelBuilder.Entity<NoteLabel>(entity =>
            {
                entity.ToTable("NoteLabel");

                entity.Property(e => e.NoteLabelId).HasColumnName("NoteLabelID");

                entity.Property(e => e.LabelId).HasColumnName("LabelId").IsRequired();

                entity.Property(e => e.NotesId).HasColumnName("NotesID").IsRequired();

                entity.Property(e => e.UserModelID).HasColumnName("UserModelID").IsRequired();

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.NoteLabels)
                    .HasForeignKey(d => d.LabelId)
                    .HasConstraintName("NoteLabelFK_LabelID");

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NoteLabels)
                    .HasForeignKey(d => d.NotesId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("NoteLabelFK_NotesID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NoteLabels)
                    .HasForeignKey(d => d.UserModelID)
                    .HasConstraintName("NoteLabelFK_UserID");
            });*/
        }
    }
}
