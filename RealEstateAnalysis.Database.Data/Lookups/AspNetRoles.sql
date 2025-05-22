INSERT INTO [dbo].[AspNetRoles]([Id], [Name], [NormalizedName], [ConcurrencyStamp])
SELECT * FROM (VALUES 
(N'34262b9b-3a82-41ca-bb42-d93aabcff5a8', N'Admin', N'ADMIN',  N'bdf9fb93-a446-47e3-8ed8-caa02bd94a0c'), 
(N'd083ae30-dcd9-4d93-af40-3ba97342342d', N'Member', N'MEMBER', N'c60b53cc-febc-4338-b1f7-5566da5df132')
) AS cols([Id], [Name], [NormalizedName], [ConcurrencyStamp]) WHERE cols.Id NOT IN (SELECT Id FROM [dbo].[AspNetRoles])
GO