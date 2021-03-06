USE [Shopping]
GO
/****** Object:  Table [dbo].[Accounting]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounting](
	[IngredientName] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[Unit] [varchar](20) NULL,
	[DateOfArrival] [date] NULL,
	[ExpireDate] [date] NULL,
	[Price] [decimal](10, 2) NULL,
	[IngreCategory] [varchar](80) NULL,
	[Cost] [decimal](10, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[RecipeID] [int] NOT NULL,
	[IngredientName] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[Unit] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[IngredientID] [int] NOT NULL,
	[LocationName] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlannedMeal]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlannedMeal](
	[RecipeID] [int] NOT NULL,
	[RecipeName] [varchar](100) NULL,
	[PlannedPortion] [int] NULL,
	[UsedPortion] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeName] [varchar](100) NULL,
	[CategoryID] [int] NULL,
	[Instructions] [ntext] NULL,
	[EstimatedTime] [varchar](20) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeCategory]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](80) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingList]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingList](
	[IngredientName] [nvarchar](80) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Unit] [nchar](15) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplyItems]    Script Date: 12/14/2021 5:58:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplyItems](
	[IngredientID] [int] IDENTITY(1,1) NOT NULL,
	[IngredientName] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[Unit] [varchar](20) NULL,
	[DateOfArrival] [date] NULL,
	[ExpireDate] [date] NULL,
	[Price] [decimal](10, 2) NULL,
	[IngreCategory] [varchar](80) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Minced meat', 450, N'gram           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(5.25 AS Decimal(10, 2)), N'Meat', CAST(2.36 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Pasta', 300, N'gram           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.50 AS Decimal(10, 2)), N'Rice, Noodle & Cereals', CAST(0.75 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Tomato sauce', 750, N'ml             ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.25 AS Decimal(10, 2)), N'Sauces', CAST(1.69 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Egg', 10, N'unit           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.03 AS Decimal(10, 2)), N'Egg', CAST(20.30 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Onion', 90, N'gram           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.15 AS Decimal(10, 2)), N'Vegetable', CAST(0.19 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Tomato', 90, N'gram           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(3.89 AS Decimal(10, 2)), N'Vegetable', CAST(0.35 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Bread', 400, N'gram           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.34 AS Decimal(10, 2)), N'Rice, Noodle & Cereals', CAST(0.94 AS Decimal(10, 2)))
INSERT [dbo].[Accounting] ([IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory], [Cost]) VALUES (N'Onion', 160, N'gram           ', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(1.50 AS Decimal(10, 2)), N'Vegetable', CAST(0.24 AS Decimal(10, 2)))
GO
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (1, N'Onion', 50, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (1, N'Bread', 80, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (1, N'Egg', 2, N'unit')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (2, N'Banana', 200, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (2, N'Oat', 100, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (3, N'Chicken', 250, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (3, N'Tomato', 50, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (4, N'Minced meat', 150, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (4, N'Tomato', 30, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (4, N'Onion', 30, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (2, N'Almond milk', 100, N'ml')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (2, N'Maple syrup', 15, N'ml')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (2, N'Salt', 5, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (3, N'Rice', 150, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (3, N'Cucumber', 20, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (4, N'Pasta', 100, N'gram')
INSERT [dbo].[Ingredients] ([RecipeID], [IngredientName], [Quantity], [Unit]) VALUES (4, N'Tomato sauce', 250, N'ml')
GO
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (1, N'Fridge')
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (3, N'Dry pantry')
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (5, N'Sauce pantry')
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (7, N'Fridge')
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (2, N'Vegetable pantry')
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (4, N'Fridge')
INSERT [dbo].[Location] ([IngredientID], [LocationName]) VALUES (6, N'Dry pantry')
GO
INSERT [dbo].[PlannedMeal] ([RecipeID], [RecipeName], [PlannedPortion], [UsedPortion]) VALUES (1, N'Breads & Eggs', 5, 5)
GO
SET IDENTITY_INSERT [dbo].[Recipe] ON 

INSERT [dbo].[Recipe] ([RecipeID], [RecipeName], [CategoryID], [Instructions], [EstimatedTime]) VALUES (1, N'Breads & Eggs', 1, N'1. Chopping onion
2. Whisk eggs with chopped onion
3. Applying egg mixture to bread and toasting', N'15 mins')
INSERT [dbo].[Recipe] ([RecipeID], [RecipeName], [CategoryID], [Instructions], [EstimatedTime]) VALUES (2, N'Oat meal', 1, N'1. Mash 3/4 the banana with a fork
2. Mix and bring all ingredients to a boil
3. Season with salt
4.Transfer the mixture to a bowl, slide the remaining banana and dress with prefer topping.', N'20 mins')
INSERT [dbo].[Recipe] ([RecipeID], [RecipeName], [CategoryID], [Instructions], [EstimatedTime]) VALUES (3, N'Chicken & Rice', 3, N'1. Cook the rice
2. Grill chicken, seasoning with BBQ sauce
3. Dress with sliced tomatos and cucumbers', N'1 hour')
INSERT [dbo].[Recipe] ([RecipeID], [RecipeName], [CategoryID], [Instructions], [EstimatedTime]) VALUES (4, N'Spaghetti', 2, N'1. Boil the pasta in 10 mins
2. Boil the tomato sauce with extra chopped fresh tomato and onion
3. Add minced meat to the sauce and boil at mediun heat
4. Bring pasta to a disk and cover by the sauce', N'45 hour')
SET IDENTITY_INSERT [dbo].[Recipe] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeCategory] ON 

INSERT [dbo].[RecipeCategory] ([CategoryID], [CategoryName]) VALUES (1, N'Breakfast')
INSERT [dbo].[RecipeCategory] ([CategoryID], [CategoryName]) VALUES (2, N'Lunch')
INSERT [dbo].[RecipeCategory] ([CategoryID], [CategoryName]) VALUES (3, N'Dinner')
INSERT [dbo].[RecipeCategory] ([CategoryID], [CategoryName]) VALUES (4, N'Drinks')
INSERT [dbo].[RecipeCategory] ([CategoryID], [CategoryName]) VALUES (5, N'Desserts')
SET IDENTITY_INSERT [dbo].[RecipeCategory] OFF
GO
INSERT [dbo].[ShoppingList] ([IngredientName], [Quantity], [Unit]) VALUES (N'Bread', 400, N'gram           ')
INSERT [dbo].[ShoppingList] ([IngredientName], [Quantity], [Unit]) VALUES (N'Egg', 10, N'unit           ')
INSERT [dbo].[ShoppingList] ([IngredientName], [Quantity], [Unit]) VALUES (N'Onion', 160, N'gram           ')
GO
SET IDENTITY_INSERT [dbo].[SupplyItems] ON 

INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (1, N'Minced meat', 450, N'gram', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(5.25 AS Decimal(10, 2)), N'Meat')
INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (3, N'Pasta', 300, N'gram', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.50 AS Decimal(10, 2)), N'Rice, Noodle & Cereals')
INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (5, N'Tomato sauce', 750, N'ml', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-30' AS Date), CAST(2.25 AS Decimal(10, 2)), N'Sauces')
INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (7, N'Egg', 0, N'unit', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.03 AS Decimal(10, 2)), N'Egg')
INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (2, N'Onion', 0, N'gram', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(1.50 AS Decimal(10, 2)), N'Vegetable')
INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (4, N'Tomato', 90, N'gram', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-20' AS Date), CAST(3.89 AS Decimal(10, 2)), N'Vegetable')
INSERT [dbo].[SupplyItems] ([IngredientID], [IngredientName], [Quantity], [Unit], [DateOfArrival], [ExpireDate], [Price], [IngreCategory]) VALUES (6, N'Bread', 0, N'gram', CAST(N'2021-12-14' AS Date), CAST(N'2021-12-19' AS Date), CAST(2.34 AS Decimal(10, 2)), N'Rice, Noodle & Cereals')
SET IDENTITY_INSERT [dbo].[SupplyItems] OFF
GO
