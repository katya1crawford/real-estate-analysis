using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFinancialForecast
    {
        public ReadFinancialForecast(int id, string name, List<decimal> values)
        {
            Id = id;
            Name = name;
            Values = values.Select(x => Math.Round(x, 2)).ToList();
        }

        public int Id { get; }

        public string Name { get; }

        public List<decimal> Values { get; }
    }
}