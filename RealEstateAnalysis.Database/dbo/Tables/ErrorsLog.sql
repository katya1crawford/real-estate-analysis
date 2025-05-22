CREATE TABLE [dbo].[ErrorsLog] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7)  NOT NULL,
    [ClassName]   NVARCHAR (MAX) NULL,
    [MethodName]  NVARCHAR (MAX) NULL,
    [Values]      NVARCHAR (MAX) NULL,
    [Error]       NVARCHAR (MAX) NULL,
    [UserEmail]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ErrorsLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);



