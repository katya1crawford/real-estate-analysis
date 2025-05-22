CREATE TABLE [Lookup].[States] (
    [Id]           BIGINT        NOT NULL,
    [Abbreviation] NVARCHAR (2)  NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([Id] ASC)
);

