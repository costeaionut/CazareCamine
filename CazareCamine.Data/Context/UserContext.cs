using CazareCamine.Data.Context.Configurations;
using CazareCamine.Data.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<UserModel>());
        }
    }
}
