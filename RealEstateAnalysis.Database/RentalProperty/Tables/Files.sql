CREATE TABLE [RentalProperty].[Files] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7)  NOT NULL,
    [ContentType] NVARCHAR (50)  NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [PropertyId]  BIGINT         NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Files_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Files_PropertyId]
    ON [RentalProperty].[Files]([PropertyId] ASC);

