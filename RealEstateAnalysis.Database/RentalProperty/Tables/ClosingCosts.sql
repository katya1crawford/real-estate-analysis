CREATE TABLE [RentalProperty].[ClosingCosts] (
    [Id]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]       DATETIME2 (7)   NOT NULL,
    [ModifiedDate]      DATETIME2 (7)   NULL,
    [Amount]            DECIMAL (18, 2) NOT NULL,
    [ClosingCostTypeId] BIGINT          NOT NULL,
    [PropertyId]        BIGINT          NOT NULL,
    CONSTRAINT [PK_ClosingCosts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosingCosts_ClosingCostTypes_ClosingCostTypeId] FOREIGN KEY ([ClosingCostTypeId]) REFERENCES [Lookup].[ClosingCostTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClosingCosts_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClosingCosts_PropertyId]
    ON [RentalProperty].[ClosingCosts]([PropertyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ClosingCosts_ClosingCostTypeId]
    ON [RentalProperty].[ClosingCosts]([ClosingCostTypeId] ASC);

