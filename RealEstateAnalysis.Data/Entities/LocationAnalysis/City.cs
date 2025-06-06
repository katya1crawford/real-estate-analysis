﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.LocationAnalysis
{
    public class City : BaseEntity
    {
        #region Constructors

        internal City()
        {
        }

        public City(string cityName,
            State state,
            int populationInYearStart,
            int populationInYearEnd,
            decimal medianHouseholdIncomeInYearStart,
            decimal medianHouseholdIncomeInYearEnd,
            decimal medianHouseOrCondoValueInYearStart,
            decimal medianHouseOrCondoValueInYearEnd,
            decimal crimeIndexInYearStart,
            decimal crimeIndexInYearEnd,
            decimal recentYearJobsGrowthRate,
            int populationYearStart,
            int populationYearEnd,
            int medianHouseholdIncomeYearStart,
            int medianHouseholdIncomeYearEnd,
            int medianHouseOrCondoValueYearStart,
            int medianHouseOrCondoValueYearEnd,
            int numberOfJobsAdded,
            int crimeIndexYearStart,
            int crimeIndexYearEnd,
            DateTime? autoGeneratedDate,
            string userId)
        {
            UserId = userId;

            Update(cityName: cityName,
                state: state,
                populationInYearStart: populationInYearStart,
                populationInYearEnd: populationInYearEnd,
                medianHouseholdIncomeInYearStart: medianHouseholdIncomeInYearStart,
                medianHouseholdIncomeInYearEnd: medianHouseholdIncomeInYearEnd,
                medianHouseOrCondoValueInYearStart: medianHouseOrCondoValueInYearStart,
                medianHouseOrCondoValueInYearEnd: medianHouseOrCondoValueInYearEnd,
                crimeIndexInYearStart: crimeIndexInYearStart,
                crimeIndexInYearEnd: crimeIndexInYearEnd,
                recentYearJobsGrowthRate: recentYearJobsGrowthRate,
                populationYearStart: populationYearStart,
                populationYearEnd: populationYearEnd,
                medianHouseholdIncomeYearStart: medianHouseholdIncomeYearStart,
                medianHouseholdIncomeYearEnd: medianHouseholdIncomeYearEnd,
                medianHouseOrCondoValueYearStart: medianHouseOrCondoValueYearStart,
                medianHouseOrCondoValueYearEnd: medianHouseOrCondoValueYearEnd,
                numberOfJobsAdded: numberOfJobsAdded,
                crimeIndexYearStart,
                crimeIndexYearEnd,
                autoGeneratedDate: autoGeneratedDate);
        }

        #endregion Constructors

        #region Properties

        public string CityName { get; private set; }

        public int PopulationInYearStart { get; private set; }

        public int PopulationInYearEnd { get; private set; }

        public int PopulationYearStart { get; private set; }

        public int PopulationYearEnd { get; private set; }

        public decimal MedianHouseholdIncomeInYearStart { get; private set; }

        public decimal MedianHouseholdIncomeInYearEnd { get; private set; }

        public int MedianHouseholdIncomeYearStart { get; private set; }

        public int MedianHouseholdIncomeYearEnd { get; private set; }

        public decimal MedianHouseOrCondoValueInYearStart { get; private set; }

        public decimal MedianHouseOrCondoValueInYearEnd { get; private set; }

        public int MedianHouseOrCondoValueYearStart { get; private set; }

        public int MedianHouseOrCondoValueYearEnd { get; private set; }

        public decimal CrimeIndexInYearStart { get; private set; }

        public decimal CrimeIndexInYearEnd { get; private set; }

        public int CrimeIndexYearStart { get; private set; }

        public int CrimeIndexYearEnd { get; private set; }

        public decimal RecentYearJobsGrowthRate { get; private set; }

        public int NumberOfJobsAdded { get; private set; }

        public bool IsFavorite { get; private set; }

        internal string UserId { get; private set; }

        internal User User { get; private set; }

        internal long StateId { get; private set; }

        public State State { get; private set; }

        public DateTime? AutoGeneratedDate { get; private set; }

        #endregion Properties

        #region Commands

        public void Update(string cityName,
            State state,
            int populationInYearStart,
            int populationInYearEnd,
            decimal medianHouseholdIncomeInYearStart,
            decimal medianHouseholdIncomeInYearEnd,
            decimal medianHouseOrCondoValueInYearStart,
            decimal medianHouseOrCondoValueInYearEnd,
            decimal crimeIndexInYearStart,
            decimal crimeIndexInYearEnd,
            decimal recentYearJobsGrowthRate,
            int populationYearStart,
            int populationYearEnd,
            int medianHouseholdIncomeYearStart,
            int medianHouseholdIncomeYearEnd,
            int medianHouseOrCondoValueYearStart,
            int medianHouseOrCondoValueYearEnd,
            int numberOfJobsAdded,
            int crimeIndexYearStart,
            int crimeIndexYearEnd,
            DateTime? autoGeneratedDate = null)
        {
            CityName = cityName;
            State = state;
            PopulationInYearStart = populationInYearStart;
            PopulationInYearEnd = populationInYearEnd;
            MedianHouseholdIncomeInYearStart = medianHouseholdIncomeInYearStart;
            MedianHouseholdIncomeInYearEnd = medianHouseholdIncomeInYearEnd;
            MedianHouseOrCondoValueInYearStart = medianHouseOrCondoValueInYearStart;
            MedianHouseOrCondoValueInYearEnd = medianHouseOrCondoValueInYearEnd;
            CrimeIndexInYearStart = crimeIndexInYearStart;
            CrimeIndexInYearEnd = crimeIndexInYearEnd;
            RecentYearJobsGrowthRate = recentYearJobsGrowthRate;
            NumberOfJobsAdded = numberOfJobsAdded;
            PopulationYearEnd = populationYearEnd;
            MedianHouseholdIncomeYearEnd = medianHouseholdIncomeYearEnd;
            MedianHouseOrCondoValueYearEnd = medianHouseOrCondoValueYearEnd;
            CrimeIndexYearStart = crimeIndexYearStart;
            CrimeIndexYearEnd = crimeIndexYearEnd;
            PopulationYearStart = populationYearStart;
            MedianHouseholdIncomeYearStart = medianHouseholdIncomeYearStart;
            MedianHouseOrCondoValueYearStart = medianHouseOrCondoValueYearStart;
            AutoGeneratedDate = autoGeneratedDate;
        }

        public void ToggleIsFavorite()
        {
            IsFavorite = !IsFavorite;
        }

        #endregion Commands
    }

    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.CityName)
                .HasMaxLength(500)
                .IsRequired();

            //Relationships
            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            entity.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("Cities", "LocationAnalysis");
        }
    }
}