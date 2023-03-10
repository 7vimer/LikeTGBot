USE [master]
GO
/****** Object:  Database [db_9b1a3]    Script Date: 17.04.2022 23:08:41 ******/
CREATE DATABASE [db_9b1a3]
 CONTAINMENT = PARTIAL
 ON  PRIMARY 
( NAME = N'user_5ebe2_dat', FILENAME = N'/var/opt/mssql/data/user_5ebe2_dat.mdf' , SIZE = 8192KB , MAXSIZE = 102400KB , FILEGROWTH = 5120KB )
 LOG ON 
( NAME = N'user_5ebe2_log', FILENAME = N'/var/opt/mssql/log/user_5ebe2_log.ldf' , SIZE = 2048KB , MAXSIZE = 20480KB , FILEGROWTH = 1024KB )
GO
ALTER DATABASE [db_9b1a3] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_9b1a3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_9b1a3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_9b1a3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_9b1a3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_9b1a3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_9b1a3] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_9b1a3] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [db_9b1a3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_9b1a3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_9b1a3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_9b1a3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_9b1a3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_9b1a3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_9b1a3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_9b1a3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_9b1a3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db_9b1a3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_9b1a3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_9b1a3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_9b1a3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_9b1a3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_9b1a3] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_9b1a3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_9b1a3] SET RECOVERY FULL 
GO
ALTER DATABASE [db_9b1a3] SET  MULTI_USER 
GO
ALTER DATABASE [db_9b1a3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_9b1a3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_9b1a3] SET DEFAULT_FULLTEXT_LANGUAGE = 1033 
GO
ALTER DATABASE [db_9b1a3] SET DEFAULT_LANGUAGE = 1033 
GO
ALTER DATABASE [db_9b1a3] SET NESTED_TRIGGERS = ON 
GO
ALTER DATABASE [db_9b1a3] SET TRANSFORM_NOISE_WORDS = OFF 
GO
ALTER DATABASE [db_9b1a3] SET TWO_DIGIT_YEAR_CUTOFF = 2049 
GO
ALTER DATABASE [db_9b1a3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_9b1a3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_9b1a3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_9b1a3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [db_9b1a3] SET QUERY_STORE = OFF
GO
USE [db_9b1a3]
GO
/****** Object:  User [user_5ebe2]    Script Date: 17.04.2022 23:08:42 ******/
CREATE USER [user_5ebe2] WITH PASSWORD=N'HNH3YtliDYMzlYBNP/VrKPNz37KQQwsD4k1jKemsbrs=', DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [user_5ebe2]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [user_5ebe2]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 17.04.2022 23:08:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [bigint] NULL,
	[Target] [bigint] NULL,
	[Reason] [nvarchar](max) NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.04.2022 23:08:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[photo] [nvarchar](100) NULL,
	[FirstName] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[City] [nvarchar](50) NULL,
	[VIP] [bit] NOT NULL,
	[Sex] [nchar](7) NULL,
	[TargetSex] [nchar](7) NULL,
	[Active] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Karma] [real] NULL,
	[Rating] [real] NULL,
	[ViewCount] [int] NULL,
	[LikedCount] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (1, 732408922, 778440611, N'Adidas')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (2, 778440611, 794761132, N'Писюн показывал')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (3, 732408922, 778440611, N'Adidas')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (4, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (5, 778440611, 858033223, N'🛒О подписке')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (6, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (7, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (8, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (9, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (10, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (11, 778440611, 858033223, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (12, 632172576, 794761132, N'Zxc')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (13, 1246245924, 436242056, N'❎Отмена')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (14, 778440611, 1255564563, N'жесткий')
INSERT [dbo].[Reports] ([id], [Sender], [Target], [Reason]) VALUES (15, 1246245924, 436242056, N'228')
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (333847084, N'quick_response', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, NULL, 0, 0, 0, 0)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (347061301, N'Faddee', N'AgACAgIAAxkBAAIEwWJQEQQldN5msgwhQC73AAHJPXv5dQACL7sxG8WQgUqtTjJ9DtHWpgEAAwIAA3MAAyME', N'Аб', 92, N'Зюзинск', 0, N'Женский', N'Женский', 1, N'Нет', 0.02, 0.67, 15, 10)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (353115209, N'353115209', N'AgACAgIAAxkBAAIE72JQEp0f5tCrlIFdlqq6c_N-HTEdAAKCuTEbyKWBSocdaSUjyQSBAQADAgADcwADIwQ', N'Бамбучио', 18, N'Москва', 1, N'Мужской', N'Женский', 0, N'овнер попросил зарегаться, никогда не буду тут сидеть', 0, 0, 0, 0)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (436242056, N'MaximStreltsov', N'AgACAgIAAxkBAAIhbmJZrjmideroPg_0hCTTD4-u0uM3AAJYuzEb3FDRSpLInnK8bz1fAQADAgADcwADIwQ', N'Ffgg', 89, N'Миаооор', 0, N'Мужской', N'Мужской', 1, N'Пррк', 0.09, 0.67, 3, 2)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (622209261, N'622209261', N'AgACAgIAAxkBAAIEl2JQDyOMjP0FUTCUHmRVnAQLcmyEAAJlujEb0H6ASmjwUJ-O4DmCAQADAgADcwADIwQ', N'Костя', 17, N'Москва', 0, N'Мужской', N'Мужской', 1, N'Если ты нефор я тебя убью', 0.08, 0.79, 63, 50)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (632172576, N'nockturne0', N'AgACAgIAAxkBAAIQx2JQb6JEIC32ijOUENt3qrDR3cW1AAKauDEbQA2ISo5sA3Vr3-zFAQADAgADcwADIwQ', N'Ручка', 72, N'Москва', 0, N'Мужской', N'Мужской', 1, N'О', 0, 0.71, 7, 5)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (704235814, N'704235814', N'AgACAgIAAxkBAAIXh2JUAS4DwDarOOlmTQi9ZlAaQ4EyAAKSuTEbRVu4SKHLK0FB9O4RAQADAgADcwADIwQ', N'Максик', 14, N'Масква', 0, N'Мужской', N'Мужской', 1, N'четыре', 0.05, 0.71, 7, 5)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (732408922, N'SanT_OG', N'AgACAgIAAxkBAAIEkGJQDtxOkU8WvxCx8Opmw8h84yAXAALJuDEbxbqASpGsydUq_0BwAQADAgADcwADIwQ', N'SanT_OG', 17, N'Москва', 1, N'Мужской', N'Женский', 1, N'.', 0.02, 0.14, 7, 1)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (778440611, N'Bigpenis228', N'AgACAgIAAxkBAAIjXWJZ3mAzN7MjxuFIBxD_H5Jd1O8UAAKhvTEb4_7JSkRxW12rhSRsAQADAgADcwADIwQ', N'Ии', 45, N'Москва', 0, N'Мужской', N'Женский', 1, N'Привет я ВАНЯ', 0, 0.45, 11, 5)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (794761132, N'Vimer7', N'AgACAgIAAxkBAAInPmJbuEQUl67zNshd7psIfkwMyWbrAAJouTEbNnuASvBlu1iBB4L3AQADAgADcwADJAQ', N'Даунил', 18, N'Москва', 0, N'Мужской', N'Женский', 1, N'zxc позер по жизни', 0.08, 1, 1, 1)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (858033223, N'reXt_e', N'AgACAgIAAxkBAAIE8WJQEqMZfPJLP8kMnkXcCcZUu6GBAAK-uTEbavMQSiGtY4-4ttiEAQADAgADcwADIwQ', N'Сева', 17, N'Мытищи', 1, N'Мужской', N'Женский', 1, N'Я лох', 0.04, 0.29, 31, 9)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (1008523333, N'1008523333', N'AgACAgIAAxkBAAIFRWJQFLbEaZaHuonSXyfVZeWdnSb6AAI9uDEb9KeBSgABxDpDfB6wpwEAAwIAA3MAAyME', N'Тимофей', 17, N'Москва', 0, N'Мужской', N'Женский', 0, N'Жопа', 0, 0, 0, 0)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (1017152434, N'V_E_D_MK_Kartser', N'AgACAgIAAxkBAAIFHWJQEwaPhTThn52_X0t_gOXDscFkAAL0ujEbTpphSpo9BI60M6RjAQADAgADcwADIwQ', NULL, NULL, NULL, 0, N'Мужской', N'Мужской', 0, N'Аоктулятвок', 0, 0, 0, 0)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (1094163855, N'kopblch', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, NULL, 0, 0, 0, 0)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (1157749957, N'mulentii', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, NULL, 0, 0, 0, 0)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (1246245924, N'PxSav', N'AgACAgIAAxkBAAIZi2JZRC8NOsoTel7fAuqXxqMKxrH5AAJSvDEbqAHISk4yCl8YHss9AQADAgADcwADIwQ', NULL, NULL, NULL, 0, NULL, NULL, 1, N'В отставке', 0.120000005, 0.14, 7, 1)
INSERT [dbo].[Users] ([id], [username], [photo], [FirstName], [Age], [City], [VIP], [Sex], [TargetSex], [Active], [Description], [Karma], [Rating], [ViewCount], [LikedCount]) VALUES (1255564563, N'ne_ladna', N'AgACAgIAAxkBAAIbGmJZnQ8hKjzrLL0Mkwto2rD6FQJVAAIYvjEbi9qoSs4E06BeBPyjAQADAgADcwADIwQ', N'Рандомный Челик', 666, N'Масква', 0, N'Мужской', N'Женский', 1, N'Чееееел....', 0.1, 0.07, 14, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD  CONSTRAINT [FK_Reports_Users] FOREIGN KEY([Sender])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Reports] CHECK CONSTRAINT [FK_Reports_Users]
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD  CONSTRAINT [FK_Reports_Users1] FOREIGN KEY([Target])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Reports] CHECK CONSTRAINT [FK_Reports_Users1]
GO
USE [master]
GO
ALTER DATABASE [db_9b1a3] SET  READ_WRITE 
GO
