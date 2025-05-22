using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstateAnalysis.Data.Abstract.LocationAnalysis;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Reads.DataScraper;
using RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class DataScraperService : IDataScraperService
    {
        private readonly LocationAnalysisSettings locationAnalysisSettings;
        private readonly ICityDataRepository cityDataRepository;
        private readonly IErrorLogService errorLogService;

        public DataScraperService(IOptions<LocationAnalysisSettings> locationAnalysisOptions, ICityDataRepository cityDataRepository, 
            IErrorLogService errorLogService)
        {
            locationAnalysisSettings = locationAnalysisOptions.Value;
            this.cityDataRepository = cityDataRepository;
            this.errorLogService = errorLogService;
        }

        private HtmlAgilityPack.HtmlDocument departmentOfNumbersDataDocument;

        public async Task<List<string>> GetCityDataCitiesUrls(int minimumPopulationCount)
        {
            var cityDataStatesUrls = await cityDataRepository.GetAllStatesUrlsAsync();
            var citiesDetails = new List<CityDataCityDetailsDto>();

            foreach (var cityDataStateUrl in cityDataStatesUrls)
            {
                var htmlResult = "";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(cityDataStateUrl.Url);
                    htmlResult = await response.Content.ReadAsStringAsync();
                }

                var docToParse = new HtmlAgilityPack.HtmlDocument();
                docToParse.LoadHtml(htmlResult);

                var citiesTableRows = docToParse.QuerySelectorAll("#cityTAB tbody tr");

                foreach (var citiesRow in citiesTableRows)
                {
                    try
                    {
                        var tds = citiesRow.QuerySelectorAll("td");
                        var cityUrl = "http://www.city-data.com/city/" + tds[1].QuerySelector("a").GetAttributeValue("href", "");

                        if (cityUrl.Contains("javascript"))
                        {
                            continue;
                        }

                        var populationRaw = tds[2].QuerySelector("b")?.InnerText ?? tds[2].InnerText;
                        var population = (int)GetNumbersFromString(populationRaw);
                        citiesDetails.Add(new CityDataCityDetailsDto { Population = population, Url = cityUrl });
                    }
                    catch (Exception ex)
                    {
                        await errorLogService.LogErrorAsync(new WriteErrorLog()
                        {
                            ClassName = nameof(DataScraperService),
                            MethodName = nameof(GetCityDataCitiesUrls),
                            Values = JsonConvert.SerializeObject(new Dictionary<string, object> { { "CityDataStateUrl", cityDataStateUrl } }),
                            Error = JsonConvert.SerializeObject(ex)
                        });
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(6));
            }

            return citiesDetails.Where(x => x.Population >= minimumPopulationCount).Select(x => x.Url).ToList();
        }

        public async Task<ReadCityData> GetCityData(string cityDataUrl)
        {
            var htmlResult = "";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(cityDataUrl);
                htmlResult = await response.Content.ReadAsStringAsync();
            }

            var docToParse = new HtmlAgilityPack.HtmlDocument();
            docToParse.LoadHtml(htmlResult);
            var cityState = docToParse.QuerySelector("h1.city span").InnerText;
            var cityName = GetTextBefore(cityState, ",");
            var stateFullName = GetTextAfter(cityState, ",");

            //Population Growth
            var populationGrowthSummary = await GetPopulationGrowthFromGoogle(cityName, stateFullName);

            //Median Household Income
            var medianHouseholdIncomeSection = docToParse.QuerySelector("#median-income");
            var medianHouseholdIncomeYearEnd = (int)GetNumbersFromString(medianHouseholdIncomeSection.QuerySelector("b").InnerText);
            var medianHouseholdIncomeInYearEnd = GetNumbersFromString(GetTextAfterAndBefore(medianHouseholdIncomeSection.InnerText, ":", "("));
            var medianHouseholdIncomeInYearStart = GetNumbersFromString(GetTextAfterAndBefore(medianHouseholdIncomeSection.InnerText, "(it", "in"));

            //Median House or Condo Value
            var medianHouseOrCondoYearEndAndAmount = GetTextAfterAndBefore(medianHouseholdIncomeSection.InnerText, "Estimated median house or", "(");
            var medianHouseOrCondoValueYearEnd = (int)GetNumbersFromString(GetTextBefore(medianHouseOrCondoYearEndAndAmount, ":"));
            var medianHouseOrCondoValueInYearEnd = GetNumbersFromString(GetTextAfter(medianHouseOrCondoYearEndAndAmount, ":"));
            var medianHouseOrCondoYearStartAndAmountAfter1 = GetTextAfter(medianHouseholdIncomeSection.InnerText, "Estimated median house or");
            var medianHouseOrCondoYearStartAndAmount = GetTextAfterAndBefore(medianHouseOrCondoYearStartAndAmountAfter1, "(it", ")");
            var medianHouseOrCondoValueInYearStart = GetNumbersFromString(GetTextBefore(medianHouseOrCondoYearStartAndAmount, "in"));

            //Crime Index
            var crimeIndexTable = docToParse.QuerySelector("#crimeTab");

            var crimeIndexYearStart = 0;
            var crimeIndexYearEnd = 0;
            var crimeIndexInYearStart = 0M;
            var crimeIndexInYearEnd = 0M;

            if (crimeIndexTable != null)
            {
                crimeIndexYearStart = (int)GetNumbersFromString(crimeIndexTable.QuerySelector("th:nth-child(2) > h4").InnerText);
                crimeIndexYearEnd = (int)GetNumbersFromString(crimeIndexTable.QuerySelector("th:last-child > h4").InnerText);
                var crimeIndexTableLastRow = crimeIndexTable.QuerySelector("tfoot tr");
                crimeIndexInYearStart = decimal.Parse(crimeIndexTableLastRow.QuerySelector("td:nth-child(2)").InnerText);
                crimeIndexInYearEnd = decimal.Parse(crimeIndexTableLastRow.QuerySelector("td:last-child").InnerText);
            }            

            //Jobs
            var (jobsAdded, jobsPercentChange) = await GetDepartmentOfNumbersData(cityName, stateFullName);

            return new ReadCityData(stateName: stateFullName,
                cityName: cityName,
                populationInYearStart: populationGrowthSummary.PopulationInYearStart,
                populationInYearEnd: populationGrowthSummary.PopulationInYearEnd,
                populationYearStart: populationGrowthSummary.PopulationYearStart,
                populationYearEnd: populationGrowthSummary.PopulationYearEnd,
                medianHouseholdIncomeInYearStart: medianHouseholdIncomeInYearStart,
                medianHouseholdIncomeInYearEnd: medianHouseholdIncomeInYearEnd,
                medianHouseOrCondoValueInYearStart: medianHouseOrCondoValueInYearStart,
                medianHouseOrCondoValueInYearEnd: medianHouseOrCondoValueInYearEnd,
                crimeIndexYearStart: crimeIndexYearStart,
                crimeIndexYearEnd: crimeIndexYearEnd,
                crimeIndexInYearStart: crimeIndexInYearStart,
                crimeIndexInYearEnd: crimeIndexInYearEnd,
                recentYearJobsGrowthRate: jobsPercentChange,
                medianHouseholdIncomeYearStart: locationAnalysisSettings.City.MedianHouseholdIncomeYearStart,
                medianHouseholdIncomeYearEnd: medianHouseholdIncomeYearEnd,
                medianHouseOrCondoValueYearStart: locationAnalysisSettings.City.MedianHouseOrCondoValueYearStart,
                medianHouseOrCondoValueYearEnd: medianHouseOrCondoValueYearEnd,
                numberOfJobsAdded: jobsAdded);
        }

        private ReadPopulationGrowthSummary GetPopulationGrowthFromCityData(HtmlAgilityPack.HtmlDocument docToParse)
        {
            var populationSection = docToParse.QuerySelector("#city-population");
            var populationYearEnd = (int)GetNumbersFromString(populationSection.QuerySelector("b").InnerText);
            var populationInYearEnd = (int)GetNumbersFromString(GetTextAfterAndBefore(populationSection.InnerText, ":", "("));
            var populationPercentChangeText = GetTextAfterAndBefore(populationSection.InnerText, "2000:", "%");
            var increaseInPopulation = populationPercentChangeText.Contains('+');
            var populationInYearStart = (int)Math.Round(GetBeginningPopulation(populationInYearEnd, GetNumbersFromString(populationPercentChangeText), increaseInPopulation), 0);

            return new ReadPopulationGrowthSummary(populationInYearStart: populationInYearStart,
                populationInYearEnd: populationInYearEnd,
                populationYearStart: locationAnalysisSettings.City.CityDataPopulationYearStart,
                populationYearEnd: populationYearEnd);
        }

        private async Task<ReadPopulationGrowthSummary> GetPopulationGrowthFromGoogle(string cityName, string fullStateName)
        {
            var baseUrl = @$"https://www.google.com/search?q=population+{cityName.ToLower().Replace(" ", "+")}+{fullStateName.ToLower().Replace(" ", "+")}";
            var htmlResult = "";

            using (var client = new HttpClient())
            {
                var url = baseUrl + $"+{locationAnalysisSettings.City.GooglePopulationYearStart}";
                var response = await client.GetAsync(url);
                htmlResult = await response.Content.ReadAsStringAsync();
            }

            var populationInYearStartDocToParse = new HtmlAgilityPack.HtmlDocument();
            populationInYearStartDocToParse.LoadHtml(htmlResult);
            var textWithPopulationInYearStart = populationInYearStartDocToParse.QuerySelector(@"div.BNeawe.iBp4i.AP7Wnd")?.LastChild?.InnerText ?? populationInYearStartDocToParse.QuerySelector(@"div.BNeawe.iBp4i.AP7Wnd").InnerText;
            var populationInYearStartText = GetTextBefore(textWithPopulationInYearStart, "(");
            int populationInYearStart;

            if (populationInYearStartText.Contains("million"))
            {
                populationInYearStart = (int)(GetNumbersFromString(GetTextBefore(textWithPopulationInYearStart, "(")) * 1000000);
            }
            else
            {
                populationInYearStart = (int)GetNumbersFromString(GetTextBefore(textWithPopulationInYearStart, "("));
            }

            using (var client = new HttpClient())
            {
                var url = baseUrl;
                var response = await client.GetAsync(url);
                htmlResult = await response.Content.ReadAsStringAsync();
            }

            var populationInLatestYearDocToParse = new HtmlAgilityPack.HtmlDocument();
            populationInLatestYearDocToParse.LoadHtml(htmlResult);

            var textWithPopulationAndYear = populationInLatestYearDocToParse.QuerySelector(@"div.BNeawe.iBp4i.AP7Wnd").LastChild.InnerText;
            var populationInLatestYearText = GetTextBefore(textWithPopulationAndYear, "(");
            int populationInYearEnd;

            if (populationInLatestYearText.Contains("million"))
            {
                populationInYearEnd = (int)(GetNumbersFromString(GetTextBefore(textWithPopulationAndYear, "(")) * 1000000);
            }
            else
            {
                populationInYearEnd = (int)GetNumbersFromString(GetTextBefore(textWithPopulationAndYear, "("));
            }

            var populationYearEnd = (int)GetNumbersFromString(GetTextAfter(textWithPopulationAndYear, "("));

            return new ReadPopulationGrowthSummary(populationInYearStart: populationInYearStart,
                populationInYearEnd: populationInYearEnd,
                populationYearStart: locationAnalysisSettings.City.GooglePopulationYearStart,
                populationYearEnd: populationYearEnd);
        }

        private async Task<(int jobsAdded, decimal jobsPercentChange)> GetDepartmentOfNumbersData(string cityName, string fullStateName)
        {
            if (departmentOfNumbersDataDocument == null)
            {
                var htmlResult = "";

                using (var client = new HttpClient())
                {
                    var url = "https://www.deptofnumbers.com/employment/metros/";
                    var response = await client.GetAsync(url);
                    htmlResult = await response.Content.ReadAsStringAsync();
                }

                departmentOfNumbersDataDocument = new HtmlAgilityPack.HtmlDocument();
                departmentOfNumbersDataDocument.LoadHtml(htmlResult);
            }

            var jobsAdded = 0;
            var jobsPercentChange = 0M;

            var jobsGrowthTable = departmentOfNumbersDataDocument.QuerySelector("#metro_table");
            var matchingStates = jobsGrowthTable.QuerySelectorAll(@$"a[href*=""{fullStateName.ToLower().Replace(" ", "-")}""]");
            var cityRow = matchingStates?.QuerySelectorAll(@$"a[href*=""{cityName.ToLower().Replace(" ", "-")}""]")?.FirstOrDefault()?.ParentNode?.ParentNode;

            if (cityRow != null)
            {
                jobsAdded = int.Parse(cityRow.QuerySelector("td:nth-child(2)").InnerText.Replace(",", ""));
                jobsPercentChange = decimal.Parse(cityRow.QuerySelector("td:last-child").InnerText.Replace("%", "").Replace(",", "").Trim());
            }

            return (jobsAdded, jobsPercentChange);
        }

        private string GetTextAfter(string value, string search)
        {
            return value.Split(search)[1].Trim();
        }

        private string GetTextBefore(string value, string search)
        {
            return value.Split(search)[0].Trim();
        }

        private string GetTextAfterAndBefore(string value, string after, string before)
        {
            var textAfter = value.Split(after)[1].Trim();
            var textBefore = textAfter.Split(before)[0].Trim();

            return textBefore;
        }

        private decimal GetNumbersFromString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            var numbers = Regex.Match(text.Replace(",", ""), @"\d+(\.\d+)?").Value;

            return decimal.Parse(numbers);
        }

        private decimal GetBeginningPopulation(decimal endingPopulation, decimal percent, bool increase)
        {
            var adjustedPercent = increase ? percent : percent * -1;
            var formattedPercent = (adjustedPercent / 100);
            return endingPopulation / (formattedPercent + 1);
        }
    }

    internal class CityDataCityDetailsDto
    {
        public int Population { get; set; }
        public string Url { get; set; }
    }
}