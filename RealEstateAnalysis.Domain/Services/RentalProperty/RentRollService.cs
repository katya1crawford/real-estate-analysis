using CsvHelper.Configuration;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Helpers;
using RealEstateAnalysis.Domain.Validators.RentalProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services.RentalProperty
{
    public class RentRollService : IRentRollService
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMembershipService membershipService;
        private readonly IPropertyService propertyService;

        public RentRollService(IPropertyRepository propertyRepository, IMembershipService membershipService, IPropertyService propertyService)
        {
            this.propertyRepository = propertyRepository;
            this.membershipService = membershipService;
            this.propertyService = propertyService;
        }

        public async Task<ReadRentRollSummary> GetRentRollSummary(long propertyId)
        {
            var user = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(propertyId, user.Id);

            if (property?.RentRollItems.Any() == true)
            {
                return new ReadRentRollSummary(property.RentRollItems, property);
            }
            else
            {
                return null;
            }
        }

        public async Task<ReadRentRollSummary> ImportRentRollCsv(WriteImportRentRollCsv model)
        {
            var user = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(model.PropertyId, user.Id);

            var csvValidator = new ImportRentRollCsvValidator();
            csvValidator.Validate(new ImportRentRollCsvValidator.ValidationState(property, model));

            var writeRentRollItems = BuildWriteRentRollItems(model.RentRollCsv);

            var rentRollValidator = new WriteRentRollItemsValidator();
            rentRollValidator.Validate(new WriteRentRollItemsValidator.ValidationState(property, writeRentRollItems));

            var rentRollItems = writeRentRollItems.Select(x => new RentRollItem(unit: x.Unit,
                squareFootage: x.SquareFootage,
                bedrooms: x.Bedrooms,
                bathrooms: x.Bathrooms,
                isVacant: x.IsVacant,
                isRenovated: x.IsRenovated,
                contractRent: x.ContractRent,
                otherIncome: x.OtherIncome,
                marketRent: x.MarketRent,
                leaseStartDate: x.LeaseStartDate,
                leaseEndDate: x.LeaseEndDate)).ToList();

            property.AddRentRollItems(rentRollItems);

            await propertyRepository.SaveOrUpdateAsync(property);

            var writeFiles = new WriteFiles
            {
                PropertyId = property.Id,
                Files = new List<IFormFile> { model.RentRollCsv }
            };

            await propertyService.AddFilesAsync(writeFiles);

            return new ReadRentRollSummary(rentRollItems, property);
        }

        private List<WriteRentRollItem> BuildWriteRentRollItems(IFormFile rentRollCsv)
        {
            var csvRecords = CsvHelpers.CsvToType<ReadRentRollCsvItem, RentRollCsvMap>(rentRollCsv);
            var rowIndex = 1;
            var writeRentRollItems = new List<WriteRentRollItem>();
            var errors = new List<ValidationFailure>();

            foreach (var csvRecord in csvRecords)
            {
                rowIndex++;

                if (string.IsNullOrWhiteSpace(csvRecord.Unit))
                {
                    errors.Add(new ValidationFailure("", $"Row index: { rowIndex }. Unit is required."));
                }

                var squareFootage = CsvHelpers.IntParse(csvRecord.SquareFootage, errors, $"Row index: {rowIndex}. Invalid sqft value.");
                var bedrooms = CsvHelpers.IntParse(csvRecord.Bedrooms, errors, $"Row index: {rowIndex}. Invalid bedrooms value.");
                var bathrooms = CsvHelpers.DoubleParse(csvRecord.Bathrooms, errors, $"Row index: {rowIndex}. Invalid baths value.");
                var isVacant = CsvHelpers.BooleanParse(csvRecord.IsVacant, errors, $"Row index: {rowIndex}. Invalid vacant value.");
                var isRenovated = CsvHelpers.BooleanParse(csvRecord.IsRenovated, errors, $"Row index: {rowIndex}. Invalid renovated value.");

                var contractRent = string.IsNullOrWhiteSpace(csvRecord.ContractRent)
                    ? default(decimal?)
                    : CsvHelpers.DecimalParse(csvRecord.ContractRent, errors, $"Row index: {rowIndex}. Invalid contract rent value.");

                var otherIncome = string.IsNullOrWhiteSpace(csvRecord.OtherIncome)
                    ? default(decimal?)
                    : CsvHelpers.DecimalParse(csvRecord.OtherIncome, errors, $"Row index: {rowIndex}. Invalid contract rent value.");

                var marketRent = string.IsNullOrWhiteSpace(csvRecord.MarketRent)
                    ? default(decimal?)
                    : CsvHelpers.DecimalParse(csvRecord.MarketRent, errors, $"Row index: {rowIndex}. Invalid market rent value.");

                var leaseStartDate = string.IsNullOrWhiteSpace(csvRecord.LeaseStartDate)
                    ? default(DateTime?)
                    : CsvHelpers.DateTimeParse(csvRecord.LeaseStartDate, errors, $"Row index: {rowIndex}. Invalid lease start date value.");

                var leaseEndDate = string.IsNullOrWhiteSpace(csvRecord.LeaseEndDate)
                    ? default(DateTime?)
                    : CsvHelpers.DateTimeParse(csvRecord.LeaseEndDate, errors, $"Row index: {rowIndex}. Invalid lease end date value.");

                if (errors.Any())
                {
                    throw new ValidationException(errors);
                }

                writeRentRollItems.Add(new WriteRentRollItem
                {
                    Unit = csvRecord.Unit,
                    SquareFootage = squareFootage,
                    OtherIncome = otherIncome,
                    Bedrooms = bedrooms,
                    Bathrooms = bathrooms,
                    IsVacant = isVacant,
                    IsRenovated = isRenovated,
                    ContractRent = contractRent,
                    LeaseStartDate = leaseStartDate,
                    LeaseEndDate = leaseEndDate,
                    MarketRent = marketRent
                });
            }

            return writeRentRollItems;
        }
    }

    internal class RentRollCsvMap : ClassMap<ReadRentRollCsvItem>
    {
        public RentRollCsvMap()
        {
            Map(x => x.Unit).Name("Unit");
            Map(x => x.SquareFootage).Name("Sqft");
            Map(x => x.Bedrooms).Name("Bedrooms");
            Map(x => x.Bathrooms).Name("Baths");
            Map(x => x.IsVacant).Name("Vacant");
            Map(x => x.IsRenovated).Name("Renovated");
            Map(x => x.ContractRent).Name("Contract Rent");
            Map(x => x.OtherIncome).Name("Other Income");
            Map(x => x.MarketRent).Name("Market Rent");
            Map(x => x.LeaseStartDate).Name("Lease Start Date");
            Map(x => x.LeaseEndDate).Name("Lease End Date");
        }
    }
}