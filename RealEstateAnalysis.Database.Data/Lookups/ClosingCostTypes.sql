INSERT INTO [Lookup].[ClosingCostTypes]([Id], [Name])
SELECT * FROM (VALUES 
(1, N'Attorney Charges'), 
(2, N'Inspection Costs'),
(3, N'Points / Origination Fee'),
(4, N'Prepaid Flood Insurance'),
(5, N'Prepaid Property Taxes'),
(6, N'Recording Fees'),
(7, N'Title / Escrow Fees'),
(8, N'Other Fees / Charges')
) AS cols([Id], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[ClosingCostTypes])
GO