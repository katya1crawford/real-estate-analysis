INSERT INTO [Lookup].[PropertyStatuses]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Listing'),
(2, N'In-Review'),
(3, N'Purchased')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[PropertyStatuses])
GO