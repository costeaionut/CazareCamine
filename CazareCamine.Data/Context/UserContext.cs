using CazareCamine.Data.Context.Configurations;
using CazareCamine.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<Roles> Role { get; set; }
        public DbSet<User_Roles> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Roles table configuration
            modelBuilder.Entity<Roles>()
                .HasKey(r => r.Id)
                .HasName("PrimaryKey_RolesId");

            modelBuilder.Entity<Roles>(eb =>
            {
                eb.Property(r => r.Role).IsRequired();
            });

            //User_Roles table configuration
            modelBuilder.Entity<User_Roles>()
                .HasOne<Roles>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<User_Roles>()
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<User_Roles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId })
                .HasName("ComposedPrimaryKey_UserId_RoleId");

            modelBuilder.Entity<User_Roles>(eb => {
                eb.Property(ur => ur.UserId).HasColumnType("nvarchar(450)");
            });
        }
    }
}
