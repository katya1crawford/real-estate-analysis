CREATE TABLE [RentalProperty].[GeneralRepairExpenses] (
    [Id]                         BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]                DATETIME2 (7)   NOT NULL,
    [ModifiedDate]               DATETIME2 (7)   NULL,
    [Amount]                     DECIMAL (18, 2) NOT NULL,
    [GeneralRepairExpenseTypeId] BIGINT          NOT NULL,
    [PropertyId]                 BIGINT          NOT NULL,
    CONSTRAINT [PK_GeneralRepairExpenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GeneralRepairExpenses_GeneralRepairExpenseTypes_GeneralRepairExpenseTypeId] FOREIGN KEY ([GeneralRepairExpenseTypeId]) REFERENCES [Lookup].[GeneralRepairExpenseTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GeneralRepairExpenses_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_GeneralRepairExpenses_PropertyId]
    ON [RentalProperty].[GeneralRepairExpenses]([PropertyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GeneralRepairExpenses_GeneralRepairExpenseTypeId]
    ON [RentalProperty].[GeneralRepairExpenses]([GeneralRepairExpenseTypeId] ASC);

