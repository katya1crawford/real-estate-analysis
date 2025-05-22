CREATE TABLE [LocationAnalysis].[Neighborhoods] (
    [Id]                           BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]                  DATETIME2 (7)   NOT NULL,
    [ModifiedDate]                 DATETIME2 (7)   NULL,
    [MedianHouseholdIncome]        DECIMAL (18, 2) NOT NULL,
    [MedianContractRent]           DECIMAL (18, 2) NOT NULL,
    [CityUnemploymentRate]         DECIMAL (18, 2) NOT NULL,
    [NeighborhoodUnemploymentRate] DECIMAL (18, 2) NOT NULL,
    [City]                         NVARCHAR (500)  DEFAULT (N'') NOT NULL,
    [NeighborhoodName]             NVARCHAR (250)  DEFAULT (N'') NOT NULL,
    [StateId]                      BIGINT          DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    [UserId]                       NVARCHAR (450)  DEFAULT (N'') NOT NULL,
    [EthnicMixLargestSlicePercent] DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    [PovertyRate]                  DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    [HomesMedianDaysOnMarket]      INT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Neighborhoods] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Neighborhoods_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Neighborhoods_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [Lookup].[States] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_Neighborhoods_UserId]
    ON [LocationAnalysis].[Neighborhoods]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Neighborhoods_StateId]
    ON [LocationAnalysis].[Neighborhoods]([StateId] ASC);

