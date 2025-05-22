CREATE TABLE [RentalProperty].[FilesContent] (
    [Id]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [Content] VARBINARY (MAX) NOT NULL,
    [FileId]  BIGINT          NOT NULL,
    CONSTRAINT [PK_FilesContent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilesContent_Files_FileId] FOREIGN KEY ([FileId]) REFERENCES [RentalProperty].[Files] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FilesContent_FileId]
    ON [RentalProperty].[FilesContent]([FileId] ASC);

