CREATE TABLE [RentalProperty].[ExteriorRepairExpenses] (
    [Id]                       BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]              DATETIME2 (7)   NOT NULL,
    [ModifiedDate]             DATETIME2 (7)   NULL,
    [Amount]                   DECIMAL (18, 2) NOT NULL,
    [ExteriorRepairItemTypeId] BIGINT          NOT NULL,
    [PropertyId]               BIGINT          NOT NULL,
    CONSTRAINT [PK_ExteriorRepairExpenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExteriorRepairExpenses_ExteriorRepairExpenseTypes_ExteriorRepairItemTypeId] FOREIGN KEY ([ExteriorRepairItemTypeId]) REFERENCES [Lookup].[ExteriorRepairExpenseTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ExteriorRepairExpenses_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ExteriorRepairExpenses_PropertyId]
    ON [RentalProperty].[ExteriorRepairExpenses]([PropertyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExteriorRepairExpenses_ExteriorRepairItemTypeId]
    ON [RentalProperty].[ExteriorRepairExpenses]([ExteriorRepairItemTypeId] ASC);

