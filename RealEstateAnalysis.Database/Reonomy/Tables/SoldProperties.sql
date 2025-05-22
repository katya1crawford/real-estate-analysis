CREATE TABLE [Reonomy].[SoldProperties] (
    [Id]                        BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]               DATETIME2 (7)   NOT NULL,
    [ModifiedDate]              DATETIME2 (7)   NULL,
    [Address]                   NVARCHAR (500)  NULL,
    [City]                      NVARCHAR (500)  NULL,
    [ZipCode]                   NVARCHAR (10)   NULL,
    [Neighborhood]              NVARCHAR (250)  NULL,
    [County]                    NVARCHAR (250)  NULL,
    [BuildingSquareFootage]     INT             NULL,
    [Fips]                      NVARCHAR (250)  NULL,
    [SourceId]                  NVARCHAR (100)  NULL,
    [LotSquareFootage]          INT             NULL,
    [MortgageAmount]            DECIMAL (18, 2) NULL,
    [MortgageLenderName]        NVARCHAR (150)  NULL,
    [MortgageRecordingDate]     DATETIME2 (7)   NULL,
    [SalesDate]                 DATETIME2 (7)   NULL,
    [StdLandUseCodeDescription] NVARCHAR (500)  NULL,
    [TotalUnits]                INT             NULL,
    [YearBuilt]                 INT             NULL,
    [SalesPrice]                DECIMAL (18, 2) NULL,
    [Latitude]                  FLOAT (53)      NULL,
    [Longitude]                 FLOAT (53)      NULL,
    [InvalidAddress]            BIT             NOT NULL,
    [StateId]                   BIGINT          NOT NULL,
    [PropertyTypeId]            BIGINT          NOT NULL,
    CONSTRAINT [PK_SoldProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SoldProperties_PropertyTypes_PropertyTypeId] FOREIGN KEY ([PropertyTypeId]) REFERENCES [Lookup].[PropertyTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SoldProperties_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [Lookup].[States] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SoldProperties_StateId]
    ON [Reonomy].[SoldProperties]([StateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SoldProperties_PropertyTypeId]
    ON [Reonomy].[SoldProperties]([PropertyTypeId] ASC);

