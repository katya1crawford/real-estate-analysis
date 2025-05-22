CREATE TABLE [RentalProperty].[GalleryImages] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7)   NOT NULL,
    [ContentType] NVARCHAR (50)   NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Content]     VARBINARY (MAX) NOT NULL,
    [PropertyId]  BIGINT          NOT NULL,
    CONSTRAINT [PK_GalleryImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GalleryImages_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [RentalProperty].[Properties] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_GalleryImages_PropertyId]
    ON [RentalProperty].[GalleryImages]([PropertyId] ASC);

