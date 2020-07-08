USE [master]
GO
/****** Object:  Database [LaptopStore]    Script Date: 08/07/2020 23:47:26 ******/
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
/****** Object:  Table [dbo].[tblLaptop]    Script Date: 08/07/2020 23:47:26 ******/
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
	[LaptopImportDate] [date] NOT NULL,
	[LaptopDiscountPercentage] [float] NOT NULL,
	[LaptopImage] [varchar](200) NOT NULL,
 CONSTRAINT [PK_tblLaptop] PRIMARY KEY CLUSTERED 
(
	[LaptopID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrder]    Script Date: 08/07/2020 23:47:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrder](
	[OrderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[OrderPrice] [float] NOT NULL,
	[OrderDate] [date] NOT NULL,
 CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderUnit]    Script Date: 08/07/2020 23:47:26 ******/
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
/****** Object:  Table [dbo].[tblUser]    Script Date: 08/07/2020 23:47:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserID] [int] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserRole] [varchar](20) NOT NULL,
	[UserEmail] [varchar](100) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[UserAddress] [varchar](200) NULL,
	[UserPhone] [varchar](10) NULL,
 CONSTRAINT [PK__tblUser__1788CCAC94E759D3] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopImportDate], [LaptopDiscountPercentage], [LaptopImage]) VALUES (1, N'Asus Zenbook Pro Duo', N'i7-9750H', N'RTX 2060 6GB', N'DDR4 32GB', N'SSD 1TB', N'LCD 15.6 UHD', 74990000, 10, CAST(N'2020-07-08' AS Date), 0, N'~/wwwroot/images/1.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopImportDate], [LaptopDiscountPercentage], [LaptopImage]) VALUES (2, N'Asus ROG', N'i9-9980HK', N'RTX 2080 8GB', N'DDR4 32GB', N'SSD 1.5TB', N'LCD 17.3 FHD IPS 144Hz', 99990000, 8, CAST(N'2020-07-08' AS Date), 3, N'~/wwwroot/images/2.png')
INSERT [dbo].[tblLaptop] ([LaptopID], [LaptopName], [LaptopCPU], [LaptopGPU], [LaptopRAM], [LaptopStorage], [LaptopDisplay], [LaptopPrice], [LaptopQuantity], [LaptopImportDate], [LaptopDiscountPercentage], [LaptopImage]) VALUES (3, N'Asus ROG Zephyrus S', N'i7-9750H', N'RTX 2080-MaxQ 8GB', N'DDR4 32GB', N'SSD 1T-PCIE', N'LCD 17.3 FHD IPS 300Hz', 84990000, 12, CAST(N'2020-07-08' AS Date), 5, N'~/wwwroot/images/2.png')
INSERT [dbo].[tblUser] ([UserID], [UserName], [UserRole], [UserEmail], [UserPassword], [UserAddress], [UserPhone]) VALUES (1, N'user1', N'User', N'user1@gmail.com', N'1', NULL, NULL)
INSERT [dbo].[tblUser] ([UserID], [UserName], [UserRole], [UserEmail], [UserPassword], [UserAddress], [UserPhone]) VALUES (2, N'user2', N'User', N'user2@gmail.com', N'1', NULL, NULL)
INSERT [dbo].[tblUser] ([UserID], [UserName], [UserRole], [UserEmail], [UserPassword], [UserAddress], [UserPhone]) VALUES (3, N'admin1', N'Admin', N'admin1@gmail', N'1', NULL, NULL)
INSERT [dbo].[tblUser] ([UserID], [UserName], [UserRole], [UserEmail], [UserPassword], [UserAddress], [UserPhone]) VALUES (4, N'admin2', N'Admin', N'admin2@gmail.com', N'1', NULL, NULL)
INSERT [dbo].[tblUser] ([UserID], [UserName], [UserRole], [UserEmail], [UserPassword], [UserAddress], [UserPhone]) VALUES (5, N'user3', N'User', N'user3@gmail.com', N'1', NULL, NULL)
ALTER TABLE [dbo].[tblOrder]  WITH CHECK ADD  CONSTRAINT [FK_tblOrder_tblUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([UserID])
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
