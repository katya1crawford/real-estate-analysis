﻿INSERT INTO [Lookup].[States]([Id], [Abbreviation], [Name])
SELECT * FROM (VALUES 
(1, N'AL', N'Alabama'),
(2, N'AK', N'Alaska'),
(3, N'AZ', N'Arizona'),
(4, N'AR', N'Arkansas'),
(5, N'CA', N'California'),
(6, N'CO', N'Colorado'),
(7, N'CT', N'Connecticut'),
(8, N'DE', N'Delaware'),
(9, N'FL', N'Florida'),
(10, N'GA', N'Georgia'),
(11, N'HI', N'Hawaii'),
(12, N'ID', N'Idaho'),
(13, N'IL', N'Illinois'),
(14, N'IN', N'Indiana'),
(15, N'IA', N'Iowa'),
(16, N'KS', N'Kansas'),
(17, N'KY', N'Kentucky'),
(18, N'LA', N'Louisiana'),
(19, N'ME', N'Maine'),
(20, N'MD', N'Maryland'),
(21, N'MA', N'Massachusetts'),
(22, N'MI', N'Michigan'),
(23, N'MN', N'Minnesota'),
(24, N'MS', N'Mississippi'),
(25, N'MO', N'Missouri'),
(26, N'MT', N'Montana'),
(27, N'NE', N'Nebraska'),
(28, N'NV', N'Nevada'),
(29, N'NH', N'New Hampshire'),
(30, N'NJ', N'New Jersey'),
(31, N'NM', N'New Mexico'),
(32, N'NY', N'New York'),
(33, N'NC', N'North Carolina'),
(34, N'ND', N'North Dakota'),
(35, N'OH', N'Ohio'),
(36, N'OK', N'Oklahoma'),
(37, N'OR', N'Oregon'),
(38, N'PA', N'Pennsylvania'),
(39, N'RI', N'Rhode Island'),
(40, N'SC', N'South Carolina'),
(41, N'SD', N'South Dakota'),
(42, N'TN', N'Tennessee'),
(43, N'TX', N'Texas'),
(44, N'UT', N'Utah'),
(45, N'VT', N'Vermont'),
(46, N'VA', N'Virginia'),
(47, N'WA', N'Washington'),
(48, N'WV', N'West Virginia'),
(49, N'WI', N'Wisconsin'),
(50, N'WY', N'Wyoming'),
(51, N'DC', N'District of Columbia')
) AS cols([Id], [Abbreviation], [Name]) WHERE cols.Id NOT IN (SELECT Id FROM [Lookup].[States])
GO