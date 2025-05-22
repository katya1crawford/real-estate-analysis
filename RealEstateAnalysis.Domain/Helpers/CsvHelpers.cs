using CsvHelper;
using CsvHelper.Configuration;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RealEstateAnalysis.Domain.Helpers
{
    public class CsvHelpers
    {
        public static List<T> CsvToType<T, TMap>(IFormFile csvFile)
            where T : class
            where TMap : ClassMap<T>
        {
            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {
                var fileContents = reader.ReadToEnd();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    ShouldSkipRecord = x => x.Row.Parser.Record.All(string.IsNullOrWhiteSpace),
                    TrimOptions = TrimOptions.Trim,
                    ReadingExceptionOccurred = ex => throw new FluentValidation.ValidationException(new List<ValidationFailure> { new ValidationFailure("", $"CSV file reading error: {ex.Exception.Message}") }),
                    BadDataFound = context => throw new FluentValidation.ValidationException(new List<ValidationFailure> { new ValidationFailure("", $"Bad data found in row: {context.RawRecord}") }),
                    MissingFieldFound = x => throw new FluentValidation.ValidationException(new List<ValidationFailure> { new ValidationFailure("", $"Fields with names ['{string.Join(", ", x.HeaderNames)}'] at index '{x.Index}' was not found.") })
                };

                using (var textReader = new StringReader(fileContents))
                {
                    using (var csvReader = new CsvReader(textReader, config))
                    {
                        csvReader.Context.RegisterClassMap<TMap>();
                        return csvReader.GetRecords<T>().ToList();
                    }
                }
            }
        }

        public static DateTime DateTimeParse(string value, List<ValidationFailure> errors, string errorMessage)
        {
            DateTime parsedValue = default;

            if (string.IsNullOrWhiteSpace(value) == false && DateTime.TryParse(value, out parsedValue) == false)
            {
                errors.Add(new ValidationFailure("", errorMessage));
            }

            return parsedValue;
        }

        public static decimal DecimalParse(string value, List<ValidationFailure> errors, string errorMessage)
        {
            decimal parsedValue = default;

            if (string.IsNullOrWhiteSpace(value) == false && decimal.TryParse(value, out parsedValue) == false)
            {
                errors.Add(new ValidationFailure("", errorMessage));
            }

            return parsedValue;
        }

        public static double DoubleParse(string value, List<ValidationFailure> errors, string errorMessage)
        {
            double parsedValue = default;

            if (string.IsNullOrWhiteSpace(value) == false && double.TryParse(value, out parsedValue) == false)
            {
                errors.Add(new ValidationFailure("", errorMessage));
            }

            return parsedValue;
        }

        public static int IntParse(string value, List<ValidationFailure> errors, string errorMessage)
        {
            int parsedValue = default;

            if (string.IsNullOrWhiteSpace(value) == false && int.TryParse(value, out parsedValue) == false)
            {
                errors.Add(new ValidationFailure("", errorMessage));
            }

            return parsedValue;
        }

        public static bool BooleanParse(string value, List<ValidationFailure> errors, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value) == false && value != "1" && value != "0")
            {
                errors.Add(new ValidationFailure("", errorMessage));
                return false;
            }
            else
            {
                return value == "1";
            }
        }
    }
}