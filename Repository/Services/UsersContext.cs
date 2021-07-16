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

        }
    }
}
