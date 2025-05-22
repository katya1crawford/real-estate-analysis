CREATE TABLE [RentalProperty].[Properties] (
    [Id]                                         BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]                                DATETIME2 (7)   NOT NULL,
    [ModifiedDate]                               DATETIME2 (7)   NULL,
    [Address]                                    NVARCHAR (500)  NOT NULL,
    [City]                                       NVARCHAR (500)  NOT NULL,
    [ZipCode]                                    NVARCHAR (10)   NOT NULL,
    [Latitude]                                   FLOAT (53)      NOT NULL,
    [Longitude]                                  FLOAT (53)      NOT NULL,
    [Neighborhood]                               NVARCHAR (250)  NULL,
    [County]                                     NVARCHAR (250)  NULL,
    [YearBuiltIn]                                INT             NOT NULL,
    [BuildingSquareFootage]                      INT             NOT NULL,
    [LotSquareFootage]                           INT             NOT NULL,
    [PurchasePrice]                              DECIMAL (18, 2) NOT NULL,
    [DownPayment]                                DECIMAL (18, 2) NOT NULL,
    [OtherAnnualIncome]                          DECIMAL (18, 2) NOT NULL,
    [AnnualVacancyRate]                          DECIMAL (5, 2)  NOT NULL,
    [AnnualPropertyManagementFeeRate]            DECIMAL (5, 2)  NOT NULL,
    [LoanApr]                                    DECIMAL (5, 2)  NOT NULL,
    [LoanYears]                                  INT             NOT NULL,
    [Notes]                                      NVARCHAR (MAX)  NULL,
    [ThumbnailImage]                             VARBINARY (MAX) NULL,
    [ThumbnailImageContentType]                  NVARCHAR (MAX)  NULL,
    [AnnualOperatingExpensesGrowthRate]          DECIMAL (18, 2) NOT NULL,
    [PropertyTypeId]                             BIGINT          NOT NULL,
    [StateId]                                    BIGINT          NOT NULL,
    [UserId]                                     NVARCHAR (450)  NOT NULL,
    [AnnualGrossScheduledRentalIncome]           DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    [AnnualGrossScheduledRentalIncomeGrowthRate] DECIMAL (5, 2)  NOT NULL,
    [MarketCapitalizationRate]                   DECIMAL (5, 2)  DEFAULT ((0.0)) NOT NULL,
    [GroupName]                                  NVARCHAR (50)   NULL,
    [PropertyStatusId]                           BIGINT          NOT NULL,
    CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Properties_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Properties_PropertyStatuses_PropertyStatusId] FOREIGN KEY ([PropertyStatusId]) REFERENCES [Lookup].[PropertyStatuses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Properties_PropertyTypes_PropertyTypeId] FOREIGN KEY ([PropertyTypeId]) REFERENCES [Lookup].[PropertyTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Properties_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [Lookup].[States] ([Id]) ON DELETE CASCADE
);
















GO
CREATE NONCLUSTERED INDEX [IX_Properties_UserId]
    ON [RentalProperty].[Properties]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Properties_StateId]
    ON [RentalProperty].[Properties]([StateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Properties_PropertyTypeId]
    ON [RentalProperty].[Properties]([PropertyTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Properties_PropertyStatusId]
    ON [RentalProperty].[Properties]([PropertyStatusId] ASC);

