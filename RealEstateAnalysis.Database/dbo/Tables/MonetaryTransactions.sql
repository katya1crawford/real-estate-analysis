CREATE TABLE [dbo].[MonetaryTransactions] (
    [Id]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate]       DATETIME2 (7)   NOT NULL,
    [Amount]            DECIMAL (18, 2) NOT NULL,
    [Balance]           DECIMAL (18, 2) NOT NULL,
    [TransactionNumber] NVARCHAR (150)  NULL,
    [Description]       NVARCHAR (500)  NULL,
    [UserId]            NVARCHAR (450)  NOT NULL,
    CONSTRAINT [PK_MonetaryTransactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MonetaryTransactions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_MonetaryTransactions_UserId]
    ON [dbo].[MonetaryTransactions]([UserId] ASC);

