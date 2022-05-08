USE [GrowByData]
GO
/****** Object:  Table [dbo].[CustomerOrders]    Script Date: 5/8/2022 9:06:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrders](
	[OrdersID] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[ODate] [datetime] NULL,
	[Quantity] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrdersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/8/2022 9:06:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Href]    Script Date: 5/8/2022 9:06:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Href](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Link] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/8/2022 9:06:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Discount] [decimal](5, 2) NULL,
	[TotalPrice] [decimal](7, 2) NULL,
	[Rating] [nvarchar](50) NULL,
	[AddedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD FOREIGN KEY([Id])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Href]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
/****** Object:  StoredProcedure [dbo].[grow_sp_GrowByDataCrud]    Script Date: 5/8/2022 9:06:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--  EXEC grow_sp_GrowByDataCrud @Mode='V'  
CREATE PROC [dbo].[grow_sp_GrowByDataCrud](@ProductID INT=null, @Name NVARCHAR(100) =NULL, @Discount DECIMAL(5, 2) =NULL, @TotalPrice DECIMAL(7, 2) =NULL, @Rating NVARCHAR(100) =NULL, @Mode CHAR(1) =NULL, @Link NVARCHAR(255) =NULL, @AddedOn DATETIME=null)  
AS BEGIN  
    IF(@Mode='V')BEGIN  
        DECLARE @query NVARCHAR(255) =N'';  
        SET @query=N'SELECT * FROM dbo.Product p  
      LEFT JOIN dbo.Href h ON p.ProductID = h.ProductID where 1=1';  
        IF(@ProductID<>NULL OR @ProductID<>0)BEGIN  
            SET @query += N' AND p.ProductID='+CAST(@ProductID AS VARCHAR(10));  
              
        END;  
            EXEC(@query);  
    END;  
    IF(@Mode='I')BEGIN  
        INSERT INTO Product(Name, Discount, TotalPrice, Rating, AddedOn)  
        VALUES(@Name, @Discount, @TotalPrice, @Rating, @AddedOn);  
        SET @ProductID=SCOPE_IDENTITY();  
        IF(@Link IS NOT NULL OR @Link<>'')BEGIN  
            INSERT INTO href(ProductID, Link)VALUES(@ProductID, @Link);  
        END;  
    END;  
    IF(@Mode='U' AND @ProductID IS NOT NULL AND @ProductID>0)BEGIN  
        UPDATE Product  
        SET Name=@Name, Discount=@Discount, TotalPrice=@TotalPrice, Rating=@Rating, AddedOn=@AddedOn  
        WHERE ProductID=@ProductID;  
        UPDATE Href SET Link=@Link WHERE ProductID=@ProductID;  
    END;  
    IF(@Mode='D')BEGIN  
        DELETE FROM Href WHERE ProductID=@ProductID;  
        DELETE FROM Product WHERE ProductID=@ProductID;  
    END;  
END;
GO
