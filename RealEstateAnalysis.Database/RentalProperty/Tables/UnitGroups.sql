CREATE TABLE [RentalProperty].[UnitGroups] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedDate]   DATETIME2 (7) NOT NULL,
    [ModifiedDate]  DATETIME2 (7) NULL,
    [Bathrooms]     FLOAT (53)    NOT NULL,
    [Bedrooms]      INT           NOT NULL,
    [NumberOfUnits] INT           NOT NULL,
    [SquareFootage] INT           NOT NULL,
    [PropertyId]    BIGINT        NOT NULL,
    CONSTRAINT [PK_UnitGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnitGroups_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_UnitGroups_PropertyId]
    ON [RentalProperty].[UnitGroups]([PropertyId] ASC);

