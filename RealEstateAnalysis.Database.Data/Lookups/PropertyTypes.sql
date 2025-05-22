INSERT INTO [Lookup].[PropertyTypes]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Single-Family'),
(2, N'Condominium'),
(3, N'Townhouse'),
(4, N'Multi-Family')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[PropertyTypes])
GO