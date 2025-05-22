CREATE TABLE [RentalProperty].[RentRollItems] (
    [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [Unit]           NVARCHAR (250)  NOT NULL,
    [SquareFootage]  INT             NOT NULL,
    [Bedrooms]       INT             NOT NULL,
    [Bathrooms]      FLOAT (53)      NOT NULL,
    [IsVacant]       BIT             NOT NULL,
    [IsRenovated]    BIT             NOT NULL,
    [ContractRent]   DECIMAL (18, 2) NULL,
    [MarketRent]     DECIMAL (18, 2) NULL,
    [LeaseStartDate] DATETIME2 (7)   NULL,
    [LeaseEndDate]   DATETIME2 (7)   NULL,
    [PropertyId]     BIGINT          NOT NULL,
    [CreatedDate]    DATETIME2 (7)   NOT NULL,
    [ModifiedDate]   DATETIME2 (7)   NULL,
    [OtherIncome]    DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_RentRollItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RentRollItems_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RentRollItems_PropertyId]
    ON [RentalProperty].[RentRollItems]([PropertyId] ASC);

