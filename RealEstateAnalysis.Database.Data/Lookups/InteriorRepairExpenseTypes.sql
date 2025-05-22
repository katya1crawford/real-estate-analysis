INSERT INTO [Lookup].[InteriorRepairExpenseTypes]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Demo'),
(2, N'Sheetrock'),
(3, N'Plumbing'),
(4, N'Carpentry'),
(5, N'Electrical'),
(6, N'Interior Painting'),
(7, N'HVAC'),
(8, N'Cabinets'),
(9, N'Framing'),
(10, N'Flooring'),
(11, N'Insulation'),
(12, N'Other')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[InteriorRepairExpenseTypes])
GO