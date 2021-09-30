using CazareCamine.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Context.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder
                .HasKey(u => u.UserId);

            builder
                .Property(u => u.Email)
                .IsRequired();

            builder
                .Property(u => u.Password)
                .IsRequired();

            builder
                .Property(u => u.LastName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(u => u.FirstName)
                .HasColumnType("varchar(50)")
                .IsRequired();

        }
    }
}
