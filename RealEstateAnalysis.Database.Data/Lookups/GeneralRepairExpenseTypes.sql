INSERT INTO [Lookup].[GeneralRepairExpenseTypes]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Permits'),
(2, N'Termites'),
(3, N'Mold'),
(4, N'Other')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[GeneralRepairExpenseTypes])
GO