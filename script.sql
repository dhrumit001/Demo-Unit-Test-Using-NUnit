USE [master]
GO
/****** Object:  Database [Demo-Unit-Test-Using-NUnit]    Script Date: 16-04-2024 16:08:22 ******/
CREATE DATABASE [Demo-Unit-Test-Using-NUnit]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Demo-Unit-Test-Using-NUnit', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Demo-Unit-Test-Using-NUnit.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Demo-Unit-Test-Using-NUnit_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Demo-Unit-Test-Using-NUnit_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Demo-Unit-Test-Using-NUnit].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ARITHABORT OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET RECOVERY FULL 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET  MULTI_USER 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Demo-Unit-Test-Using-NUnit', N'ON'
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET QUERY_STORE = OFF
GO
USE [Demo-Unit-Test-Using-NUnit]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 16-04-2024 16:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16-04-2024 16:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](500) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Id], [Title], [Description], [UserId], [Date]) VALUES (1005, N'Demo of UnitTesting', N'Demo of UnitTesting using NUnit', 1, CAST(N'2024-02-13T00:00:00.000' AS DateTime))
INSERT [dbo].[Task] ([Id], [Title], [Description], [UserId], [Date]) VALUES (1006, N'Dubuy | Uzbekistan with Native Currency Release', N'Dubuy | Uzbekistan with Native Currency Release on this Wednesday', 1, CAST(N'2024-02-14T00:00:00.000' AS DateTime))
INSERT [dbo].[Task] ([Id], [Title], [Description], [UserId], [Date]) VALUES (1007, N'Passport Appointment ', N'Passport Appointment  on this Friday', 1, CAST(N'2024-02-16T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [Password], [Active]) VALUES (1, N'dhrumit.patel@ics-global.in', N'Test@124', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
USE [master]
GO
ALTER DATABASE [Demo-Unit-Test-Using-NUnit] SET  READ_WRITE 
GO
