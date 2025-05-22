CREATE TABLE [Lookup].[OperatingExpenseTypes] (
    [Id]   BIGINT        NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_OperatingExpenseTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

