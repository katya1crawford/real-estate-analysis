using RealEstateAnalysis.Data.Entities.RentalProperty;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadRentRollSummary
    {
        private List<ReadFloorPanGroup> floorPanGroups;

        public ReadRentRollSummary(List<RentRollItem> rentRollItems, Property property)
        {
            RentRollItems = rentRollItems.Select(x => new ReadRentRollItem(x)).ToList();
            TotalSquareFootage = RentRollItems.Sum(x => x.SquareFootage);
            NumberOfVacantUnits = RentRollItems.Count(x => x.IsVacant);
            NumberOfRenovatedUnits = RentRollItems.Count(x => x.IsRenovated);
            TotalContractRent = RentRollItems.Sum(x => x.ContractRent ?? 0);
            TotalOtherIncome = RentRollItems.Sum(x => x.OtherIncome ?? 0);
            TotalMarketRent = RentRollItems.Sum(x => x.MarketRent ?? 0);
            NumberOfMonthToMonthUnits = RentRollItems.Count(x => x.IsMonthToMonth);
            TotalNumberOfUnits = rentRollItems.Count();

            floorPanGroups = GetFloorPanGroups(RentRollItems);
            FloorPlanAverageContractRentByMonthSummary = new ReadFloorPlanAverageContractRentByMonthSummary(GetFloorPlanAverageContractRentByYearMonthGroups(rentRollItems));
            AverageContractRentByMonth = GetReadAverageContractRentByYearMonth(rentRollItems, FloorPlanAverageContractRentByMonthSummary);
            NewLeasesCountByMonth = GetNewLeaseCountByMonth(rentRollItems);
            LeasesExpireCountByMonth = GetLeaseExpireCountByMonth(rentRollItems);
            RentsAllOccupiedUnits = BuildRentsAllOccupiedUnits(floorPanGroups);
            RentsNonRenovatedOccupiedUnits = BuildRentsNonRenovatedOccupiedUnits(floorPanGroups);
            RentsRenovatedOccupiedUnits = BuildRentsRenovatedOccupiedUnits(floorPanGroups);
            OccupancyAllUnits = BuildOccupancyAllUnits(floorPanGroups);
            OccupancyNonRenovatedUnits = BuildOccupancyNonRenovatedUnits(floorPanGroups);
            OccupancyRenovatedUnits = BuildOccupancyRenovatedUnits(floorPanGroups);
            FloorPlansSummary = new ReadFloorPlansSummary(BuildFloorPlans(floorPanGroups));

            NumberOfFloorPlans = floorPanGroups.Count;
            VacancyRate = Math.Round(Divide(rentRollItems.Count(x => x.IsVacant), rentRollItems.Count) * 100, 2);
            AverageContractRent = Math.Round(Divide(rentRollItems.Sum(x => x.ContractRent ?? 0), rentRollItems.Count(x => x.ContractRent > 0)), 0);
            AverageMarketRent = Math.Round(Divide(rentRollItems.Sum(x => x.MarketRent ?? 0), rentRollItems.Count(x => x.MarketRent > 0)), 0);
            TotalActualMonthlyIncome = rentRollItems.Sum(x => x.ContractRent ?? 0 + x.OtherIncome ?? 0);
            AverageSquareFootage = Math.Round(Divide(rentRollItems.Sum(x => x.SquareFootage), rentRollItems.Count(x => x.SquareFootage > 0)), 0);
            AverageOtherIncome = Math.Round(Divide(rentRollItems.Sum(x => x.OtherIncome ?? 0), rentRollItems.Count(x => x.OtherIncome > 0)), 0);
            AveragePercentOfMarketRent = Math.Round(Divide(RentRollItems.Sum(x => x.PercentOfMarketRent), RentRollItems.Count(x => x.PercentOfMarketRent > 0)), 2);

            Address = new ReadAddress(property);
        }

        public List<ReadRentRollItem> RentRollItems { get; }

        public ReadFloorPlansSummary FloorPlansSummary { get; }

        public List<ReadAverageContractRentByMonth> AverageContractRentByMonth { get; }

        public ReadFloorPlanAverageContractRentByMonthSummary FloorPlanAverageContractRentByMonthSummary { get; }

        public List<ReadLeasesCountByMonth> NewLeasesCountByMonth { get; }

        public List<ReadLeasesCountByMonth> LeasesExpireCountByMonth { get; }

        public ReadRents RentsAllOccupiedUnits { get; }

        public ReadRents RentsNonRenovatedOccupiedUnits { get; }

        public ReadRents RentsRenovatedOccupiedUnits { get; }

        public ReadOccupancy OccupancyAllUnits { get; }

        public ReadOccupancy OccupancyNonRenovatedUnits { get; }

        public ReadOccupancy OccupancyRenovatedUnits { get; }

        public int TotalSquareFootage { get; }

        public int NumberOfVacantUnits { get; }

        public int NumberOfRenovatedUnits { get; }

        public decimal TotalContractRent { get; }

        public decimal TotalOtherIncome { get; }

        public decimal TotalMarketRent { get; }

        public int NumberOfMonthToMonthUnits { get; }

        public int TotalNumberOfUnits { get; }

        public int NumberOfFloorPlans { get; }

        public decimal VacancyRate { get; }

        public decimal AverageContractRent { get; }

        public decimal AverageMarketRent { get; }

        public decimal TotalActualMonthlyIncome { get; }

        public decimal AverageSquareFootage { get; }

        public decimal AverageOtherIncome { get; }

        public decimal AveragePercentOfMarketRent { get; }

        public ReadAddress Address { get; }

        private List<ReadLeasesCountByMonth> GetLeaseExpireCountByMonth(List<RentRollItem> rentRollItems) =>
            rentRollItems
                .Where(x => x.LeaseEndDate.HasValue)
                .OrderBy(x => x.LeaseEndDate)
                .GroupBy(x => new { x.LeaseEndDate.Value.Year, x.LeaseEndDate.Value.Month })
                .Select(x => new ReadLeasesCountByMonth(count: x.Count(), date: x.First().LeaseEndDate.Value))
                .ToList();

        private List<ReadLeasesCountByMonth> GetNewLeaseCountByMonth(List<RentRollItem> rentRollItems) =>
            rentRollItems
                .Where(x => x.LeaseStartDate.HasValue)
                .OrderBy(x => x.LeaseStartDate)
                .GroupBy(x => new { x.LeaseStartDate.Value.Year, x.LeaseStartDate.Value.Month })
                .Select(x => new ReadLeasesCountByMonth(count: x.Count(), date: x.First().LeaseStartDate.Value))
                .ToList();

        private List<ReadFloorPlan> BuildFloorPlans(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadFloorPlan>();

            foreach (var floorPlanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPlanGroup.RentRollItems.Where(x => x.IsVacant == false).ToList();
                var averageContractRent = Math.Round(Divide(occupiedUnits.Sum(x => x.ContractRent.Value), occupiedUnits.Count), 0);
                var minimumContractRent = occupiedUnits.OrderByDescending(x => x.ContractRent).LastOrDefault()?.ContractRent ?? 0;
                var maximumContractRent = occupiedUnits.OrderByDescending(x => x.ContractRent).FirstOrDefault()?.ContractRent ?? 0;

                var rentRollsWithMarketRent = floorPlanGroup.RentRollItems.Where(x => x.MarketRent > 0).ToList();
                var averageMarketRent = Math.Round(Divide(rentRollsWithMarketRent.Sum(x => x.MarketRent.Value), rentRollsWithMarketRent.Count), 0);
                var minimumMarketRent = averageMarketRent > 0
                    ? rentRollsWithMarketRent.Min(x => x.MarketRent.Value)
                    : 0;
                var maximumMarketRent = averageMarketRent > 0
                    ? rentRollsWithMarketRent.Max(x => x.MarketRent.Value)
                    : 0;

                floorPlans.Add(new ReadFloorPlan(floorPlan: floorPlanGroup.FloorPlan,
                    units: floorPlanGroup.RentRollItems.Count,
                    bedrooms: floorPlanGroup.RentRollItems.First().Bedrooms,
                    bathrooms: floorPlanGroup.RentRollItems.First().Bathrooms,
                    averageSquareFootage: (int)Math.Round(Divide(floorPlanGroup.RentRollItems.Sum(x => x.SquareFootage), floorPlanGroup.RentRollItems.Count), 0),
                    averageContractRent: averageContractRent,
                    minimumContractRent: minimumContractRent,
                    maximumContractRent: maximumContractRent,
                    averageMarketRent: averageMarketRent,
                    minimumMarketRent: minimumMarketRent,
                    maximumMarketRent: maximumMarketRent));
            }

            return floorPlans;
        }

        private List<ReadFloorPanGroup> GetFloorPanGroups(List<ReadRentRollItem> rentRollItems)
        {
            var results = new List<ReadFloorPanGroup>();
            var rentRollItemsGroups = rentRollItems.GroupBy(x => new { x.SquareFootage, x.Bedrooms, x.Bathrooms });

            foreach (var rentRollItemGroup in rentRollItemsGroups)
            {
                results.Add(new ReadFloorPanGroup(floorPlan: GetFloorPlan(rentRollItemGroup.Key.Bedrooms, rentRollItemGroup.Key.Bathrooms, rentRollItemGroup.Key.SquareFootage),
                    rentRollItems: rentRollItemGroup.ToList()));
            }

            return results.ToList();
        }

        private ReadRents BuildRentsAllOccupiedUnits(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadRentsFloorPlan>();

            foreach (var floorPanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant == false).ToList();

                if (occupiedUnits.Any())
                {
                    floorPlans.Add(new ReadRentsFloorPlan(floorPlan: floorPanGroup.FloorPlan,
                        units: occupiedUnits.Count,
                        averageContractRent: Math.Round(Divide(occupiedUnits.Sum(x => x.ContractRent.Value), occupiedUnits.Count), 0),
                        averageMarketRent: Math.Round(Divide(occupiedUnits.Sum(x => x.MarketRent ?? 0), occupiedUnits.Count(x => x.MarketRent > 0)), 0)));
                }
            }

            return new ReadRents(floorPlans);
        }

        private ReadRents BuildRentsNonRenovatedOccupiedUnits(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadRentsFloorPlan>();

            foreach (var floorPanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant == false && x.IsRenovated == false).ToList();

                if (occupiedUnits.Any())
                {
                    floorPlans.Add(new ReadRentsFloorPlan(floorPlan: floorPanGroup.FloorPlan,
                        units: occupiedUnits.Count,
                        averageContractRent: Math.Round(Divide(occupiedUnits.Sum(x => x.ContractRent.Value), occupiedUnits.Count), 0),
                        averageMarketRent: Math.Round(Divide(occupiedUnits.Sum(x => x.MarketRent ?? 0), occupiedUnits.Count(x => x.MarketRent > 0)), 0)));
                }
            }

            return new ReadRents(floorPlans);
        }

        private ReadRents BuildRentsRenovatedOccupiedUnits(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadRentsFloorPlan>();

            foreach (var floorPanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant == false && x.IsRenovated).ToList();

                if (occupiedUnits.Any())
                {
                    floorPlans.Add(new ReadRentsFloorPlan(floorPlan: floorPanGroup.FloorPlan,
                        units: occupiedUnits.Count,
                        averageContractRent: Math.Round(Divide(occupiedUnits.Sum(x => x.ContractRent.Value), occupiedUnits.Count), 0),
                        averageMarketRent: Math.Round(Divide(occupiedUnits.Sum(x => x.MarketRent ?? 0), occupiedUnits.Count(x => x.MarketRent > 0)), 0)));
                }
            }

            return new ReadRents(floorPlans);
        }

        private ReadOccupancy BuildOccupancyAllUnits(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadOccupancyFloorPlan>();

            foreach (var floorPanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant == false).Count();
                var vacantUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant).Count();

                floorPlans.Add(new ReadOccupancyFloorPlan(floorPlan: floorPanGroup.FloorPlan,
                    occupiedUnits: occupiedUnits,
                    vacantUnits: vacantUnits));
            }

            return new ReadOccupancy(floorPlans);
        }

        private ReadOccupancy BuildOccupancyNonRenovatedUnits(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadOccupancyFloorPlan>();

            foreach (var floorPanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant == false && x.IsRenovated == false).Count();
                var vacantUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant && x.IsRenovated == false).Count();

                floorPlans.Add(new ReadOccupancyFloorPlan(floorPlan: floorPanGroup.FloorPlan,
                    occupiedUnits: occupiedUnits,
                    vacantUnits: vacantUnits));
            }

            return new ReadOccupancy(floorPlans);
        }

        private ReadOccupancy BuildOccupancyRenovatedUnits(List<ReadFloorPanGroup> floorPanGroups)
        {
            var floorPlans = new List<ReadOccupancyFloorPlan>();

            foreach (var floorPanGroup in floorPanGroups)
            {
                var occupiedUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant == false && x.IsRenovated).Count();
                var vacantUnits = floorPanGroup.RentRollItems.Where(x => x.IsVacant && x.IsRenovated).Count();

                floorPlans.Add(new ReadOccupancyFloorPlan(floorPlan: floorPanGroup.FloorPlan,
                    occupiedUnits: occupiedUnits,
                    vacantUnits: vacantUnits));
            }

            return new ReadOccupancy(floorPlans);
        }

        private decimal Divide(decimal numerator, decimal denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }

            return numerator / denominator;
        }

        private List<ReadAverageContractRentByMonth> GetReadAverageContractRentByYearMonth(List<RentRollItem> rentRollItems, ReadFloorPlanAverageContractRentByMonthSummary rentSummary)
        {
            var dates = rentRollItems.Where(x => x.LeaseStartDate.HasValue)
                .OrderBy(x => x.LeaseStartDate)
                .Select(x => new DateTime(x.LeaseStartDate.Value.Year, x.LeaseStartDate.Value.Month, 2))
                .Distinct()
                .ToList();

            var avgRentGroups = new List<List<decimal>>();

            foreach (var rentGroup in rentSummary.FloorPlanAverageContractRentByMonthGroups)
            {
                var orderedItems = rentGroup.Items.OrderByDescending(x => x.Date).ToList();
                var avgRentGroupItems = new List<decimal>();

                foreach (var date in dates)
                {
                    var item = orderedItems.FirstOrDefault(x => new DateTime(x.Date.Year, x.Date.Month, 1) <= date);

                    if (item != null)
                    {
                        avgRentGroupItems.Add(item.AverageContractRent);
                    }
                    else
                    {
                        item = orderedItems.Last();
                        avgRentGroupItems.Add(item.AverageContractRent);
                    }
                }

                avgRentGroups.Add(avgRentGroupItems);
            }

            var averages = new List<ReadAverageContractRentByMonth>();

            for (var i = 0; i < dates.Count; i++)
            {
                var sum = 0M;
                var count = 0;

                foreach (var group in avgRentGroups)
                {
                    sum += group[i];
                    count++;
                }

                var average = Math.Round(Divide(sum, count), 0);
                averages.Add(new ReadAverageContractRentByMonth(average, dates[i]));
            }

            return averages;
        }

        private List<ReadFloorPlanAverageContractRentByMonthGroup> GetFloorPlanAverageContractRentByYearMonthGroups(List<RentRollItem> rentRollItems)
        {
            var results = new List<ReadFloorPlanAverageContractRentByMonthGroup>();
            var floorPlanGroups = rentRollItems.Where(x => x.LeaseStartDate.HasValue).GroupBy(x => new { x.SquareFootage, x.Bedrooms, x.Bathrooms });

            foreach (var floorPlanGroup in floorPlanGroups)
            {
                var floorPlan = GetFloorPlan(floorPlanGroup.Key.Bedrooms, floorPlanGroup.Key.Bathrooms, floorPlanGroup.Key.SquareFootage);
                var contractRentByYearMonthGroup = floorPlanGroup
                    .OrderBy(x => x.LeaseStartDate)
                    .GroupBy(x => new { x.LeaseStartDate.Value.Year, x.LeaseStartDate.Value.Month });

                var groupValues = contractRentByYearMonthGroup.Select(x => new ReadFloorPlanAverageContractRentByMonth(
                    averageContractRent: Math.Round(Divide(x.Sum(y => y.ContractRent ?? 0), x.Count(y => y.ContractRent > 0)), 0),
                    date: x.First().LeaseStartDate.Value))
                    .ToList();

                results.Add(new ReadFloorPlanAverageContractRentByMonthGroup(floorPlan, groupValues));
            }

            return results;
        }

        private string GetFloorPlan(int bedrooms, double bathrooms, int squareFootage) =>
            $"{bedrooms}x{bathrooms}x{squareFootage}";
    }
}