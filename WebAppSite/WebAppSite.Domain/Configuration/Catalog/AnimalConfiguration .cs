using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppSite.Domain.Entities.Catalog;

namespace WebAppSite.Domain.Configuration.Catalog
{
    public class AnimalConfiguration : BaseConfiguration<Animal>
    {
        

        public override void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.ToTable("tblAnimals");//

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Image)
                .HasMaxLength(255)
                .IsRequired(false);
        }
    }
}
