USE [master]
GO
/****** Object:  Database [LaptopStore]    Script Date: 23/07/2020 0:35:02 ******/
CREATE DATABASE [LaptopStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LaptopStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\LaptopStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LaptopStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\LaptopStore.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LaptopStore] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LaptopStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LaptopStore] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [LaptopStore] SET ANSI_NULLS ON 
GO
ALTER DATABASE [LaptopStore] SET ANSI_PADDING ON 
GO
ALTER DATABASE [LaptopStore] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [LaptopStore] SET ARITHABORT ON 
GO
ALTER DATABASE [LaptopStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LaptopStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LaptopStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LaptopStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LaptopStore] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [LaptopStore] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [LaptopStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LaptopStore] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [LaptopStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LaptopStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LaptopStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LaptopStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LaptopStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LaptopStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LaptopStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LaptopStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LaptopStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LaptopStore] SET RECOVERY FULL 
GO
ALTER DATABASE [LaptopStore] SET  MULTI_USER 
GO
ALTER DATABASE [LaptopStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LaptopStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LaptopStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LaptopStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LaptopStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LaptopStore] SET QUERY_STORE = OFF
GO
USE [LaptopStore]
GO
/****** Object:  Table [dbo].[tblLaptop]    Script Date: 23/07/2020 0:35:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLaptop](
	[LaptopID] [int] NOT NULL,
	[LaptopName] [varchar](50) NOT NULL,
	[LaptopCPU] [varchar](50) NOT NULL,
	[LaptopGPU] [varchar](50) NOT NULL,
	[LaptopRAM] [varchar](50) NOT NULL,
	[LaptopStorage] [varchar](50) NOT NULL,
	[LaptopDisplay] [varchar](50) NOT NULL,
	[LaptopPrice] [float] NOT NULL,
	[LaptopQuantity] [int] NOT NULL,
	[LaptopDiscountPercentage] [real] NOT NULL,
	[LaptopImage] [varchar](200) NOT NULL,
 CONSTRAINT [PK_tblLaptop] PRIMARY KEY CLUSTERED 
(
	[LaptopID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrder]    Script Date: 23/07/2020 0:35:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrder](
	[OrderID] [int] NOT NULL,
	[UserEmail] [varchar](100) NOT NULL,
	[OrderPrice] [float] NOT NULL,
	[OrderDate] [date] NOT NULL,
 CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderUnit]    Script Date: 23/07/2020 0:35:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderUnit](
	[OrderUnitID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[LaptopID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_tblOrderUnit] PRIMARY KEY CLUSTERED 
(
	[OrderUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 23/07/2020 0:35:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserEmail] [varchar](100) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[UserPasswordSalt] [varbinary](max) NOT NULL,
	[UserAddress] [varchar](200) NULL,
	[UserPhone] [varchar](10) NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[UserEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (1, N'Asus Zenbook Pro Duo', N'i7-9750H', N'RTX 2060 6GB', N'DDR4 32GB', N'SSD 1TB', N'LCD 15.6 UHD', 74990000, 8, 0, N'images/1.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (2, N'Asus ROG', N'i9-9980HK', N'RTX 2080 8GB', N'DDR4 32GB', N'SSD 1.5TB', N'LCD 17.3 FHD IPS 144Hz', 99990000, 8, 3.5, N'images/2.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (3, N'Asus ROG Zephyrus S', N'i7-9750H', N'RTX 2080-MaxQ 8GB', N'DDR4 32GB', N'SSD 1T-PCIE', N'LCD 17.3 FHD IPS 300Hz', 84990000, 11, 5, N'images/3.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (4, N'Asus ROG Strix Scar III', N'i7-9750H', N'RTX 2070 8GB', N'DDR4 16GB', N'SSD 1TB', N'LCD 17.3 FHD 240', 57990000, 15, 2, N'images/4.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (5, N'Asus ProArt', N'i7-9750H', N'Quadro T2000 4GB', N'DDR4 32GB', N'SSD 1TB', N'LCD 17 FHD', 55990000, 18, 0, N'images/5.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (6, N'GE66 Raider', N'i7-10750H', N'RTX 2070 8GB', N'DDR4 16GB', N'SSD 1TB', N'LCD 15 FHD IPS 240Hz', 54990000, 24, 7, N'images/6.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (7, N'Acer Swift 7', N'i7-8500Y', N'Intel Graphics HD', N'DDR4 16GB', N'SSD 512GB', N'LCD 14 FHD', 49990000, 2, 10, N'images/7.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopDiscountPercentage], [LaptopImage]) VALUES (8, N'HP Spectre X360', N'i7-1065G7', N'Intel UHD', N'DDR4 16GB', N'SSD 512GB', N'LCD 13.3 UHD', 47990000, 9, 12, N'images/8.png')
INSERT [dbo].[tblOrder] ([OrderID], [UserEmail], [OrderPrice], [OrderDate]) VALUES (1, N'user1@gmail.com', 55990000, CAST(N'2020-07-22' AS Date))
INSERT [dbo].[tblOrder] ([OrderID], [UserEmail], [OrderPrice], [OrderDate]) VALUES (2, N'user2@gmail.com', 139980000, CAST(N'2020-07-22' AS Date))
INSERT [dbo].[tblOrder] ([OrderID], [UserEmail], [OrderPrice], [OrderDate]) VALUES (3, N'user3@gmail.com', 149980000, CAST(N'2020-07-22' AS Date))
INSERT [dbo].[tblOrder] ([OrderID], [UserEmail], [OrderPrice], [OrderDate]) VALUES (4, N'user2@gmail.com', 55990000, CAST(N'2020-07-23' AS Date))
INSERT [dbo].[tblOrderUnit] ([OrderUnitID], [OrderID], [LaptopID], [Quantity], [Price]) VALUES (1, 1, 5, 1, 55990000)
INSERT [dbo].[tblOrderUnit] ([OrderUnitID], [OrderID], [LaptopID], [Quantity], [Price]) VALUES (2, 2, 6, 1, 51140700)
INSERT [dbo].[tblOrderUnit] ([OrderUnitID], [OrderID], [LaptopID], [Quantity], [Price]) VALUES (3, 2, 3, 1, 80740500)
INSERT [dbo].[tblOrderUnit] ([OrderUnitID], [OrderID], [LaptopID], [Quantity], [Price]) VALUES (4, 3, 1, 2, 74990000)
INSERT [dbo].[tblOrderUnit] ([OrderUnitID], [OrderID], [LaptopID], [Quantity], [Price]) VALUES (5, 4, 5, 1, 55990000)
INSERT [dbo].[tblUser] ([UserEmail], [UserName], [UserPassword], [UserPasswordSalt], [UserAddress], [UserPhone]) VALUES (N'user1@gmail.com', N'Hai Trieu', N'cUHksRZ9NhHfbm+WL4mlfj0RX4GH+acR+WS2BL6w12Q=', 0xD63F223235D0392452B18D0B9AC216EF, N'555 Hai Trieu', N'0985557777')
INSERT [dbo].[tblUser] ([UserEmail], [UserName], [UserPassword], [UserPasswordSalt], [UserAddress], [UserPhone]) VALUES (N'user2@gmail.com', N'Minh Tri', N'PIkrp3N+oKvcTLFHDwh8BSQOqSuTfY500WB16Ve2quc=', 0x0FD7745F81EE653F068B495FE78F27D1, N'222 Quoc Lo 1A', N'0975558888')
INSERT [dbo].[tblUser] ([UserEmail], [UserName], [UserPassword], [UserPasswordSalt], [UserAddress], [UserPhone]) VALUES (N'user3@gmail.com', N'Nhat Phuong', N'+MY7igJ2RcqPRLQxpUd6yZRjdisJ+NBtNNWwgg3DEmU=', 0xFEBD5E6504593C5F6984B891B2AFC25E, N'123 Hoang Van Thu', N'0951234567')
INSERT [dbo].[tblUser] ([UserEmail], [UserName], [UserPassword], [UserPasswordSalt], [UserAddress], [UserPhone]) VALUES (N'user4@gmail.com', N'User4', N'+n51Zoxfh3wu0Cwmec/YmU549Bw4A8CTNbIM6ICydVo=', 0x5EBA4DD2497F8E9E12F184F27586D901, NULL, NULL)
ALTER TABLE [dbo].[tblOrder]  WITH CHECK ADD  CONSTRAINT [FK_tblOrder_tblUser] FOREIGN KEY([UserEmail])
REFERENCES [dbo].[tblUser] ([UserEmail])
GO
ALTER TABLE [dbo].[tblOrder] CHECK CONSTRAINT [FK_tblOrder_tblUser]
GO
ALTER TABLE [dbo].[tblOrderUnit]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderUnit_tblLaptop] FOREIGN KEY([LaptopID])
REFERENCES [dbo].[tblLaptop] ([LaptopID])
GO
ALTER TABLE [dbo].[tblOrderUnit] CHECK CONSTRAINT [FK_tblOrderUnit_tblLaptop]
GO
ALTER TABLE [dbo].[tblOrderUnit]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderUnit_tblOrder] FOREIGN KEY([OrderID])
REFERENCES [dbo].[tblOrder] ([OrderID])
GO
ALTER TABLE [dbo].[tblOrderUnit] CHECK CONSTRAINT [FK_tblOrderUnit_tblOrder]
GO
USE [master]
GO
ALTER DATABASE [LaptopStore] SET  READ_WRITE 
GO
