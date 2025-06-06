﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.Lookups
{
    public class State
    {
        public long Id { get; private set; }

        public string Abbreviation { get; private set; }

        public string Name { get; private set; }
    }

    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Id)
                .ValueGeneratedNever();

            entity.Property(x => x.Abbreviation)
                .HasMaxLength(2)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            //Table & Columns
            entity.ToTable("States", "Lookup");
        }
    }
}