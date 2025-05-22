CREATE TABLE [LocationAnalysis].[CityDataStateUrls] (
    [Id]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Url] NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_CityDataStateUrls] PRIMARY KEY CLUSTERED ([Id] ASC)
);

