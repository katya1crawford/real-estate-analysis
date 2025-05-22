INSERT INTO [Lookup].[ExteriorRepairExpenseTypes]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Roof'),
(2, N'Concrete'),
(3, N'Gutters'),
(4, N'Garage'),
(5, N'Siding'),
(6, N'Landscaping'),
(7, N'Exterior Painting'),
(8, N'Septic'),
(9, N'Decks/Porches'),
(10, N'Foundation'),
(11, N'Other')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[ExteriorRepairExpenseTypes])
GO