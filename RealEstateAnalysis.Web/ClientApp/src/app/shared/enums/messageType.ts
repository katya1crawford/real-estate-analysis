export enum MessageType {
    newRentalPropertyAdded = 1,
    updatedRentalProperty = 2,
    deleteRentalProperty = 3,
    deleteRentalPropertyFile = 4,
    deleteRentalPropertyThumbnailImage = 5,
    deletedRentalPropertyThumbnailImage = 6,

    deleteUserAccount = 7,
    deleteGalleryImage = 8,

    marketValueNewPropertyAdded = 9,
    marketValuePropertyUpdated = 10,
    marketValueDeleteProperty = 11,
    deleteMarketValueThumbnailImage = 12,

    locationAnalysisNewCityAdded = 13,
    locationAnalysisCityUpdated = 14,
    locationAnalysisDeleteCity = 15,
    locationAnalysisFinishedHarvestingCityData = 16,

    locationAnalysisNewNeighborhoodAdded = 17,
    locationAnalysisNeighborhoodUpdated = 18,
    locationAnalysisDeleteNeighborhood = 19,

    importedRentRollCsv = 20,

    locationAnalysisRemoveCityFromFavorites = 21,
}
