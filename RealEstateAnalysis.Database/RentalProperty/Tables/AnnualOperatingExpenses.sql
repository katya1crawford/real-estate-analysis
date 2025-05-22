CREATE TABLE [RentalProperty].[AnnualOperatingExpenses] (
    [Id]                     BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]            DATETIME2 (7)   NOT NULL,
    [ModifiedDate]           DATETIME2 (7)   NULL,
    [Amount]                 DECIMAL (18, 2) NOT NULL,
    [OperatingExpenseTypeId] BIGINT          NOT NULL,
    [PropertyId]             BIGINT          NOT NULL,
    CONSTRAINT [PK_AnnualOperatingExpenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualOperatingExpenses_OperatingExpenseTypes_OperatingExpenseTypeId] FOREIGN KEY ([OperatingExpenseTypeId]) REFERENCES [Lookup].[OperatingExpenseTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnnualOperatingExpenses_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnualOperatingExpenses_PropertyId]
    ON [RentalProperty].[AnnualOperatingExpenses]([PropertyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnualOperatingExpenses_OperatingExpenseTypeId]
    ON [RentalProperty].[AnnualOperatingExpenses]([OperatingExpenseTypeId] ASC);

