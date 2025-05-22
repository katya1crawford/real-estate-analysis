CREATE TABLE [RentalProperty].[InteriorRepairExpenses] (
    [Id]                          BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]                 DATETIME2 (7)   NOT NULL,
    [ModifiedDate]                DATETIME2 (7)   NULL,
    [Amount]                      DECIMAL (18, 2) NOT NULL,
    [InteriorRepairExpenseTypeId] BIGINT          NOT NULL,
    [PropertyId]                  BIGINT          NOT NULL,
    CONSTRAINT [PK_InteriorRepairExpenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InteriorRepairExpenses_InteriorRepairExpenseTypes_InteriorRepairExpenseTypeId] FOREIGN KEY ([InteriorRepairExpenseTypeId]) REFERENCES [Lookup].[InteriorRepairExpenseTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_InteriorRepairExpenses_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_InteriorRepairExpenses_PropertyId]
    ON [RentalProperty].[InteriorRepairExpenses]([PropertyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InteriorRepairExpenses_InteriorRepairExpenseTypeId]
    ON [RentalProperty].[InteriorRepairExpenses]([InteriorRepairExpenseTypeId] ASC);

