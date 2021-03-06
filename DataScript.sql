USE [CarVendor]
GO
SET IDENTITY_INSERT [dbo].[Brands] ON

INSERT [dbo].[Brands]
    ([Id], [Name])
VALUES
    (1, N'BMW')
INSERT [dbo].[Brands]
    ([Id], [Name])
VALUES
    (2, N'Mercedes')
INSERT [dbo].[Brands]
    ([Id], [Name])
VALUES
    (3, N'KIA')
INSERT [dbo].[Brands]
    ([Id], [Name])
VALUES
    (4, N'Ford')
INSERT [dbo].[Brands]
    ([Id], [Name])
VALUES
    (5, N'Nissan')
INSERT [dbo].[Brands]
    ([Id], [Name])
VALUES
    (6, N'Fiat')
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Cars] ON

INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (2, N'CarBMW', N'2019', 0, 1, 1)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (3, N'CarBMW1', N'2020', 0, 1, 1)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (4, N'Mercedes1', N'2019', 0, 1, 2)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (5, N'Mercedes2', N'2020', 0, 1, 2)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (6, N'Mercedes3', N'2020', 0, 1, 2)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (7, N'KIA_S', N'2019', 0, 1, 3)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (8, N'Ford_D', N'2019', 0, 1, 4)
INSERT [dbo].[Cars]
    ([Id], [Name], [Model], [Condition], [Type], [BrandId])
VALUES
    (9, N'Nissan Sunny ', N'2020', 0, 1, 5)
SET IDENTITY_INSERT [dbo].[Cars] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON

INSERT [dbo].[Categories]
    ([Id], [Name])
VALUES
    (1, N'Manual')
INSERT [dbo].[Categories]
    ([Id], [Name])
VALUES
    (2, N'Automatic')
INSERT [dbo].[Categories]
    ([Id], [Name])
VALUES
    (3, N'Full Options')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[CarCategories] ON


INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (2, CAST(1200000.00 AS Decimal(18, 2)), 2, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (3, CAST(1000000.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (4, CAST(1400000.00 AS Decimal(18, 2)), 2, 3)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (5, CAST(1200000.00 AS Decimal(18, 2)), 3, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (6, CAST(1000000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (7, CAST(1400000.00 AS Decimal(18, 2)), 3, 3)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (8, CAST(700000.00 AS Decimal(18, 2)), 4, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (9, CAST(600000.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (10, CAST(800000.00 AS Decimal(18, 2)), 4, 3)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (11, CAST(550000.00 AS Decimal(18, 2)), 5, 1)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (12, CAST(700000.00 AS Decimal(18, 2)), 5, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (13, CAST(620000.00 AS Decimal(18, 2)), 6, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (14, CAST(730000.00 AS Decimal(18, 2)), 7, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (15, CAST(610000.00 AS Decimal(18, 2)), 8, 2)
INSERT [dbo].[CarCategories]
    ([Id], [Price], [CarId], [CategoryId])
VALUES
    (16, CAST(400000.00 AS Decimal(18, 2)), 9, 1)
SET IDENTITY_INSERT [dbo].[CarCategories] OFF
SET IDENTITY_INSERT [dbo].[Colors] ON

INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (1, N'Red')
INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (2, N'White')
INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (3, N'Black')
INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (4, N'Grey')
INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (5, N'Yellow')
INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (6, N'Silver')
INSERT [dbo].[Colors]
    ([Id], [Name])
VALUES
    (7, N'Gold')
SET IDENTITY_INSERT [dbo].[Colors] OFF
SET IDENTITY_INSERT [dbo].[CarColors] ON

INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (1, 2, 1)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (2, 2, 2)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (3, 2, 3)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (4, 3, 4)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (5, 3, 3)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (6, 3, 1)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (8, 4, 5)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (9, 4, 2)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (10, 5, 1)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (11, 6, 2)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (12, 7, 6)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (13, 8, 5)
INSERT [dbo].[CarColors]
    ([Id], [CarId], [ColorId])
VALUES
    (14, 9, 2)
SET IDENTITY_INSERT [dbo].[CarColors] OFF
SET IDENTITY_INSERT [dbo].[CarImages] ON

INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (2, N'page1_img1.png', 1)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (3, N'page1_img2.png', 2)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (5, N'page1_img3.png', 3)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (6, N'page1_img4.png', 4)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (7, N'page1_img5.png', 5)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (8, N'page1_img6.png', 6)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (9, N'page1_img7.png', 8)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (10, N'page1_img8.png', 9)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (11, N'page1_img9.png', 10)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (12, N'page1_img10.png', 11)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (13, N'page1_img11.png', 12)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (14, N'page1_img12.png', 13)
INSERT [dbo].[CarImages]
    ([Id], [ImageURL], [CarColorId])
VALUES
    (15, N'page1_img13.png', 14)
SET IDENTITY_INSERT [dbo].[CarImages] OFF


select *
from dbo.CarImages