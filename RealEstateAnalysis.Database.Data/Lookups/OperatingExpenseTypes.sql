INSERT INTO [Lookup].[OperatingExpenseTypes]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Advertising'),
(2, N'Cleaning & Maintenance'),
(3, N'Insurance'),
(4, N'Legal & Professional Fees'),
(5, N'Repairs'),
(6, N'Supplies'),
(7, N'Taxes'),
(8, N'Utilities'),
(9, N'HOA'),
(10, N'Other'),
(11, N'Wages'),
(12, N'Replacement Reserves')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[OperatingExpenseTypes])
GO