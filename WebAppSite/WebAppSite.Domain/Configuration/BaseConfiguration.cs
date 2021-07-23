using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppSite.Domain.Configuration
{
    //гонфигурация для BaseEntity
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
        public void CreateDefaultConfiguration(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey("Id");

            builder.Property("DateCreate")
                .IsRequired();
        }
    }
}
