USE [master]
GO
/****** Object:  Database [DigiMoorDB]    Script Date: 09/13/2019 17:01:46 ******/
CREATE DATABASE [DigiMoorDB]
GO
ALTER DATABASE [DigiMoorDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DigiMoorDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DigiMoorDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DigiMoorDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DigiMoorDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DigiMoorDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DigiMoorDB] SET ARITHABORT OFF
GO
ALTER DATABASE [DigiMoorDB] SET AUTO_CLOSE ON
GO
ALTER DATABASE [DigiMoorDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DigiMoorDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DigiMoorDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DigiMoorDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DigiMoorDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DigiMoorDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DigiMoorDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DigiMoorDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DigiMoorDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DigiMoorDB] SET  ENABLE_BROKER
GO
ALTER DATABASE [DigiMoorDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DigiMoorDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DigiMoorDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DigiMoorDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DigiMoorDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DigiMoorDB] SET READ_COMMITTED_SNAPSHOT ON
GO
ALTER DATABASE [DigiMoorDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DigiMoorDB] SET  READ_WRITE
GO
ALTER DATABASE [DigiMoorDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [DigiMoorDB] SET  MULTI_USER
GO
ALTER DATABASE [DigiMoorDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DigiMoorDB] SET DB_CHAINING OFF
GO
USE [DigiMoorDB]
GO
/****** Object:  Table [dbo].[TblMSMPMenus]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblMSMPMenus](
	[MId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](100) NULL,
	[Action] [varchar](100) NULL,
	[Controller] [varchar](50) NULL,
	[AreaName] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
 CONSTRAINT [PK_TblMenu] PRIMARY KEY CLUSTERED 
(
	[MId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMenuName]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMenuName](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mid] [int] NULL,
	[MenuName] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblMenuName] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblMenuName] ON
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (1, 4, N'MSMP')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (2, 5, N'Cover')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (3, 6, N'Introduction')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (4, 7, N'1.1 Objective of MSMP')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (5, 8, N'1.2 Relation between terminal')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (6, 9, N'Part-A General ship particulars')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (7, 10, N'Part B - Mooring equipment design philosophy')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (8, 11, N'B.1 - Design considerations to achieve the ships optimal mooring pattern including information on the siting of mooring winches and fairleads to provide for direct leads and minimal lines across open decks')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (9, 12, N'B.2 mooring against')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (10, 13, N'LMP')
INSERT [dbo].[tblMenuName] ([Id], [Mid], [MenuName]) VALUES (11, 14, N'Training Content')
SET IDENTITY_INSERT [dbo].[tblMenuName] OFF
/****** Object:  Table [dbo].[tblLooseEquipInspectionSetting]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLooseEquipInspectionSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentType] [int] NOT NULL,
	[InspectionFrequency] [int] NOT NULL,
	[MaximumRunningHours] [int] NULL,
	[MaximumMonthsAllowed] [int] NULL,
 CONSTRAINT [PK_tblLooseEquipInspectionSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCommon]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCommon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_tblCommon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblCommon] ON
INSERT [dbo].[tblCommon] ([Id], [Name], [Type]) VALUES (1, N'ManuFNameTest', 1)
INSERT [dbo].[tblCommon] ([Id], [Name], [Type]) VALUES (2, N'ManuFNameTest2', 2)
SET IDENTITY_INSERT [dbo].[tblCommon] OFF
/****** Object:  Table [dbo].[WorkHoursPlanner]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkHoursPlanner](
	[wid] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[TotalHours] [decimal](18, 2) NOT NULL,
	[RestHours] [decimal](18, 2) NOT NULL,
	[options] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[dates] [datetime] NOT NULL,
	[NonConfirmities] [nvarchar](max) NULL,
	[hrs] [nvarchar](max) NULL,
	[Colors] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[Col1] [nvarchar](max) NULL,
	[Col2] [nvarchar](max) NULL,
	[Col3] [nvarchar](max) NULL,
	[Col4] [nvarchar](max) NULL,
	[Col5] [nvarchar](max) NULL,
	[Col6] [nvarchar](max) NULL,
	[Col7] [nvarchar](max) NULL,
	[Col8] [nvarchar](max) NULL,
	[Col9] [nvarchar](max) NULL,
	[Col10] [nvarchar](max) NULL,
	[Col11] [nvarchar](max) NULL,
	[Col12] [nvarchar](max) NULL,
	[Col13] [nvarchar](max) NULL,
	[Col14] [nvarchar](max) NULL,
	[Col15] [nvarchar](max) NULL,
	[Col16] [nvarchar](max) NULL,
	[Col17] [nvarchar](max) NULL,
	[Col18] [nvarchar](max) NULL,
	[Col19] [nvarchar](max) NULL,
	[Col20] [nvarchar](max) NULL,
	[Col21] [nvarchar](max) NULL,
	[Col22] [nvarchar](max) NULL,
	[Col23] [nvarchar](max) NULL,
	[Col24] [nvarchar](max) NULL,
	[Col25] [nvarchar](max) NULL,
	[Col26] [nvarchar](max) NULL,
	[Col27] [nvarchar](max) NULL,
	[Col28] [nvarchar](max) NULL,
	[Col29] [nvarchar](max) NULL,
	[Col30] [nvarchar](max) NULL,
	[Col31] [nvarchar](max) NULL,
	[Col32] [nvarchar](max) NULL,
	[Col33] [nvarchar](max) NULL,
	[Col34] [nvarchar](max) NULL,
	[Col35] [nvarchar](max) NULL,
	[Col36] [nvarchar](max) NULL,
	[Col37] [nvarchar](max) NULL,
	[Col38] [nvarchar](max) NULL,
	[Col39] [nvarchar](max) NULL,
	[Col40] [nvarchar](max) NULL,
	[Col41] [nvarchar](max) NULL,
	[Col42] [nvarchar](max) NULL,
	[Col43] [nvarchar](max) NULL,
	[Col44] [nvarchar](max) NULL,
	[Col45] [nvarchar](max) NULL,
	[Col46] [nvarchar](max) NULL,
	[Col47] [nvarchar](max) NULL,
	[Col48] [nvarchar](max) NULL,
	[Col49] [nvarchar](max) NULL,
	[Col50] [nvarchar](max) NULL,
	[Col51] [nvarchar](max) NULL,
	[Col52] [nvarchar](max) NULL,
	[Col53] [nvarchar](max) NULL,
	[Col54] [nvarchar](max) NULL,
	[Col55] [nvarchar](max) NULL,
	[Col56] [nvarchar](max) NULL,
	[Col57] [nvarchar](max) NULL,
	[Col58] [nvarchar](max) NULL,
	[Col59] [nvarchar](max) NULL,
	[Col60] [nvarchar](max) NULL,
	[Col61] [nvarchar](max) NULL,
	[Col62] [nvarchar](max) NULL,
	[Col63] [nvarchar](max) NULL,
	[Col64] [nvarchar](max) NULL,
	[Col65] [nvarchar](max) NULL,
	[Col66] [nvarchar](max) NULL,
	[Col67] [nvarchar](max) NULL,
	[Col68] [nvarchar](max) NULL,
	[Col69] [nvarchar](max) NULL,
	[Col70] [nvarchar](max) NULL,
	[Col71] [nvarchar](max) NULL,
	[Col72] [nvarchar](max) NULL,
	[Col73] [nvarchar](max) NULL,
	[Col74] [nvarchar](max) NULL,
	[Col75] [nvarchar](max) NULL,
	[Col76] [nvarchar](max) NULL,
	[Col77] [nvarchar](max) NULL,
	[Col78] [nvarchar](max) NULL,
	[Col79] [nvarchar](max) NULL,
	[Col80] [nvarchar](max) NULL,
	[Col81] [nvarchar](max) NULL,
	[Col82] [nvarchar](max) NULL,
	[Col83] [nvarchar](max) NULL,
	[Col84] [nvarchar](max) NULL,
	[Col85] [nvarchar](max) NULL,
	[Col86] [nvarchar](max) NULL,
	[Col87] [nvarchar](max) NULL,
	[Col88] [nvarchar](max) NULL,
	[Col89] [nvarchar](max) NULL,
	[Col90] [nvarchar](max) NULL,
	[Col91] [nvarchar](max) NULL,
	[Col92] [nvarchar](max) NULL,
	[Col93] [nvarchar](max) NULL,
	[Col94] [nvarchar](max) NULL,
	[Col95] [nvarchar](max) NULL,
	[Col96] [nvarchar](max) NULL,
	[Col97] [nvarchar](max) NULL,
	[Col98] [nvarchar](max) NULL,
	[Col99] [nvarchar](max) NULL,
	[Col100] [nvarchar](max) NULL,
	[Col101] [nvarchar](max) NULL,
	[Col102] [nvarchar](max) NULL,
	[Col103] [nvarchar](max) NULL,
	[Col104] [nvarchar](max) NULL,
	[Col105] [nvarchar](max) NULL,
	[Col106] [nvarchar](max) NULL,
	[Col107] [nvarchar](max) NULL,
	[Col108] [nvarchar](max) NULL,
	[Col109] [nvarchar](max) NULL,
	[Col110] [nvarchar](max) NULL,
	[Col111] [nvarchar](max) NULL,
	[Col112] [nvarchar](max) NULL,
	[Col113] [nvarchar](max) NULL,
	[Col114] [nvarchar](max) NULL,
	[Col115] [nvarchar](max) NULL,
	[Col116] [nvarchar](max) NULL,
	[Col117] [nvarchar](max) NULL,
	[Col118] [nvarchar](max) NULL,
	[Col119] [nvarchar](max) NULL,
	[Col120] [nvarchar](max) NULL,
	[MonthName] [nvarchar](max) NULL,
	[YearValue] [nvarchar](max) NULL,
	[cid] [int] NOT NULL,
	[Normal_WKH] [int] NOT NULL,
	[DaysOfMonth] [nvarchar](max) NULL,
	[overtime] [decimal](18, 2) NOT NULL,
	[datetimes] [datetime] NOT NULL,
	[UserType] [nvarchar](max) NULL,
	[opa] [bit] NOT NULL,
	[RestHour7day] [decimal](18, 2) NOT NULL,
	[NormalOT1] [decimal](18, 2) NOT NULL,
	[RuleNames] [nvarchar](max) NULL,
	[RestHourAny24] [nvarchar](max) NULL,
	[RestHourAny7day] [nvarchar](max) NULL,
	[options1] [nvarchar](max) NULL,
	[opayoung] [bit] NOT NULL,
	[WRID] [nvarchar](max) NULL,
	[Days] [nvarchar](max) NULL,
	[Cellcount] [int] NOT NULL,
	[NonConfirmities1] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.WorkHoursPlanner] PRIMARY KEY CLUSTERED 
(
	[wid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkHours]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkHours](
	[wid] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[TotalHours] [decimal](18, 2) NOT NULL,
	[RestHours] [decimal](18, 2) NOT NULL,
	[options] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[dates] [datetime] NOT NULL,
	[NonConfirmities] [nvarchar](max) NULL,
	[hrs] [nvarchar](max) NULL,
	[Colors] [nvarchar](max) NULL,
	[ColorName] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[Col1] [nvarchar](max) NULL,
	[Col2] [nvarchar](max) NULL,
	[Col3] [nvarchar](max) NULL,
	[Col4] [nvarchar](max) NULL,
	[Col5] [nvarchar](max) NULL,
	[Col6] [nvarchar](max) NULL,
	[Col7] [nvarchar](max) NULL,
	[Col8] [nvarchar](max) NULL,
	[Col9] [nvarchar](max) NULL,
	[Col10] [nvarchar](max) NULL,
	[Col11] [nvarchar](max) NULL,
	[Col12] [nvarchar](max) NULL,
	[Col13] [nvarchar](max) NULL,
	[Col14] [nvarchar](max) NULL,
	[Col15] [nvarchar](max) NULL,
	[Col16] [nvarchar](max) NULL,
	[Col17] [nvarchar](max) NULL,
	[Col18] [nvarchar](max) NULL,
	[Col19] [nvarchar](max) NULL,
	[Col20] [nvarchar](max) NULL,
	[Col21] [nvarchar](max) NULL,
	[Col22] [nvarchar](max) NULL,
	[Col23] [nvarchar](max) NULL,
	[Col24] [nvarchar](max) NULL,
	[Col25] [nvarchar](max) NULL,
	[Col26] [nvarchar](max) NULL,
	[Col27] [nvarchar](max) NULL,
	[Col28] [nvarchar](max) NULL,
	[Col29] [nvarchar](max) NULL,
	[Col30] [nvarchar](max) NULL,
	[Col31] [nvarchar](max) NULL,
	[Col32] [nvarchar](max) NULL,
	[Col33] [nvarchar](max) NULL,
	[Col34] [nvarchar](max) NULL,
	[Col35] [nvarchar](max) NULL,
	[Col36] [nvarchar](max) NULL,
	[Col37] [nvarchar](max) NULL,
	[Col38] [nvarchar](max) NULL,
	[Col39] [nvarchar](max) NULL,
	[Col40] [nvarchar](max) NULL,
	[Col41] [nvarchar](max) NULL,
	[Col42] [nvarchar](max) NULL,
	[Col43] [nvarchar](max) NULL,
	[Col44] [nvarchar](max) NULL,
	[Col45] [nvarchar](max) NULL,
	[Col46] [nvarchar](max) NULL,
	[Col47] [nvarchar](max) NULL,
	[Col48] [nvarchar](max) NULL,
	[Col49] [nvarchar](max) NULL,
	[Col50] [nvarchar](max) NULL,
	[Col51] [nvarchar](max) NULL,
	[Col52] [nvarchar](max) NULL,
	[Col53] [nvarchar](max) NULL,
	[Col54] [nvarchar](max) NULL,
	[Col55] [nvarchar](max) NULL,
	[Col56] [nvarchar](max) NULL,
	[Col57] [nvarchar](max) NULL,
	[Col58] [nvarchar](max) NULL,
	[Col59] [nvarchar](max) NULL,
	[Col60] [nvarchar](max) NULL,
	[Col61] [nvarchar](max) NULL,
	[Col62] [nvarchar](max) NULL,
	[Col63] [nvarchar](max) NULL,
	[Col64] [nvarchar](max) NULL,
	[Col65] [nvarchar](max) NULL,
	[Col66] [nvarchar](max) NULL,
	[Col67] [nvarchar](max) NULL,
	[Col68] [nvarchar](max) NULL,
	[Col69] [nvarchar](max) NULL,
	[Col70] [nvarchar](max) NULL,
	[Col71] [nvarchar](max) NULL,
	[Col72] [nvarchar](max) NULL,
	[Col73] [nvarchar](max) NULL,
	[Col74] [nvarchar](max) NULL,
	[Col75] [nvarchar](max) NULL,
	[Col76] [nvarchar](max) NULL,
	[Col77] [nvarchar](max) NULL,
	[Col78] [nvarchar](max) NULL,
	[Col79] [nvarchar](max) NULL,
	[Col80] [nvarchar](max) NULL,
	[Col81] [nvarchar](max) NULL,
	[Col82] [nvarchar](max) NULL,
	[Col83] [nvarchar](max) NULL,
	[Col84] [nvarchar](max) NULL,
	[Col85] [nvarchar](max) NULL,
	[Col86] [nvarchar](max) NULL,
	[Col87] [nvarchar](max) NULL,
	[Col88] [nvarchar](max) NULL,
	[Col89] [nvarchar](max) NULL,
	[Col90] [nvarchar](max) NULL,
	[Col91] [nvarchar](max) NULL,
	[Col92] [nvarchar](max) NULL,
	[Col93] [nvarchar](max) NULL,
	[Col94] [nvarchar](max) NULL,
	[Col95] [nvarchar](max) NULL,
	[Col96] [nvarchar](max) NULL,
	[Col97] [nvarchar](max) NULL,
	[Col98] [nvarchar](max) NULL,
	[Col99] [nvarchar](max) NULL,
	[Col100] [nvarchar](max) NULL,
	[Col101] [nvarchar](max) NULL,
	[Col102] [nvarchar](max) NULL,
	[Col103] [nvarchar](max) NULL,
	[Col104] [nvarchar](max) NULL,
	[Col105] [nvarchar](max) NULL,
	[Col106] [nvarchar](max) NULL,
	[Col107] [nvarchar](max) NULL,
	[Col108] [nvarchar](max) NULL,
	[Col109] [nvarchar](max) NULL,
	[Col110] [nvarchar](max) NULL,
	[Col111] [nvarchar](max) NULL,
	[Col112] [nvarchar](max) NULL,
	[Col113] [nvarchar](max) NULL,
	[Col114] [nvarchar](max) NULL,
	[Col115] [nvarchar](max) NULL,
	[Col116] [nvarchar](max) NULL,
	[Col117] [nvarchar](max) NULL,
	[Col118] [nvarchar](max) NULL,
	[Col119] [nvarchar](max) NULL,
	[Col120] [nvarchar](max) NULL,
	[ColNA] [nvarchar](max) NULL,
	[MonthName] [nvarchar](max) NULL,
	[YearValue] [nvarchar](max) NULL,
	[cid] [int] NOT NULL,
	[Normal_WKH] [int] NOT NULL,
	[DaysOfMonth] [nvarchar](max) NULL,
	[overtime] [decimal](18, 2) NOT NULL,
	[StatusCrew] [bit] NOT NULL,
	[CrewNC] [bit] NOT NULL,
	[MasterCrewNC] [bit] NOT NULL,
	[HODCrewNC] [bit] NOT NULL,
	[datetimes] [datetime] NOT NULL,
	[UserType] [nvarchar](max) NULL,
	[opa] [bit] NOT NULL,
	[RestHour7day] [decimal](18, 2) NOT NULL,
	[NormalOT1] [decimal](18, 2) NOT NULL,
	[RuleNames] [nvarchar](max) NULL,
	[NC1] [nvarchar](max) NULL,
	[NCD1] [datetime] NULL,
	[NC2] [nvarchar](max) NULL,
	[NCD2] [datetime] NULL,
	[NC3] [nvarchar](max) NULL,
	[NCD3] [datetime] NULL,
	[NC4] [nvarchar](max) NULL,
	[NCD4] [datetime] NULL,
	[NC5] [nvarchar](max) NULL,
	[NCD5] [datetime] NULL,
	[NC6] [nvarchar](max) NULL,
	[NCD6] [datetime] NULL,
	[NC7] [nvarchar](max) NULL,
	[NCD7] [datetime] NULL,
	[NC8] [nvarchar](max) NULL,
	[NCD8] [datetime] NULL,
	[NC9] [nvarchar](max) NULL,
	[NCD9] [datetime] NULL,
	[NC10] [nvarchar](max) NULL,
	[NCD10] [datetime] NULL,
	[NC11] [nvarchar](max) NULL,
	[NCD11] [datetime] NULL,
	[NC12] [nvarchar](max) NULL,
	[NCD12] [datetime] NULL,
	[NC13] [nvarchar](max) NULL,
	[NCD13] [datetime] NULL,
	[NC14] [nvarchar](max) NULL,
	[NCD14] [datetime] NULL,
	[NC15] [nvarchar](max) NULL,
	[NCD15] [datetime] NULL,
	[NC16] [nvarchar](max) NULL,
	[NCD16] [datetime] NULL,
	[NC17] [nvarchar](max) NULL,
	[NCD17] [datetime] NULL,
	[NC18] [nvarchar](max) NULL,
	[NCD18] [datetime] NULL,
	[YNC1] [nvarchar](max) NULL,
	[YNCD1] [datetime] NULL,
	[YNC2] [nvarchar](max) NULL,
	[YNCD2] [datetime] NULL,
	[YNC3] [nvarchar](max) NULL,
	[YNCD3] [datetime] NULL,
	[YNC4] [nvarchar](max) NULL,
	[YNCD4] [datetime] NULL,
	[YNC5] [nvarchar](max) NULL,
	[YNCD5] [datetime] NULL,
	[YNC6] [nvarchar](max) NULL,
	[YNCD6] [datetime] NULL,
	[YNC7] [nvarchar](max) NULL,
	[YNCD7] [datetime] NULL,
	[YNC8] [nvarchar](max) NULL,
	[YNCD8] [datetime] NULL,
	[RestHourAny24] [nvarchar](max) NULL,
	[RestHourAny7day] [nvarchar](max) NULL,
	[options1] [nvarchar](max) NULL,
	[opayoung] [bit] NOT NULL,
	[MNCCD1] [datetime] NULL,
	[MNCCD2] [datetime] NULL,
	[MNCCD3] [datetime] NULL,
	[MNCCD4] [datetime] NULL,
	[MNCCD5] [datetime] NULL,
	[MNCCD6] [datetime] NULL,
	[MNCCD7] [datetime] NULL,
	[MNCCD8] [datetime] NULL,
	[MNCCD9] [datetime] NULL,
	[MNCCD10] [datetime] NULL,
	[MNCCD11] [datetime] NULL,
	[MNCCD12] [datetime] NULL,
	[MNCCD13] [datetime] NULL,
	[MNCCD14] [datetime] NULL,
	[MNCCD15] [datetime] NULL,
	[MNCCD16] [datetime] NULL,
	[MNCCD17] [datetime] NULL,
	[MNCCD18] [datetime] NULL,
	[MYNCCD1] [datetime] NULL,
	[MYNCCD2] [datetime] NULL,
	[MYNCCD3] [datetime] NULL,
	[MYNCCD4] [datetime] NULL,
	[MYNCCD5] [datetime] NULL,
	[MYNCCD6] [datetime] NULL,
	[MYNCCD7] [datetime] NULL,
	[MYNCCD8] [datetime] NULL,
	[Last_update] [datetime] NULL,
	[AdminAlarm] [bit] NOT NULL,
	[MasterAlarm] [bit] NOT NULL,
	[HODAlarm] [bit] NOT NULL,
	[HNCCD1] [datetime] NULL,
	[HNCCD2] [datetime] NULL,
	[HNCCD3] [datetime] NULL,
	[HNCCD4] [datetime] NULL,
	[HNCCD5] [datetime] NULL,
	[HNCCD6] [datetime] NULL,
	[HNCCD7] [datetime] NULL,
	[HNCCD8] [datetime] NULL,
	[HNCCD9] [datetime] NULL,
	[HNCCD10] [datetime] NULL,
	[HNCCD11] [datetime] NULL,
	[HNCCD12] [datetime] NULL,
	[HNCCD13] [datetime] NULL,
	[HNCCD14] [datetime] NULL,
	[HNCCD15] [datetime] NULL,
	[HNCCD16] [datetime] NULL,
	[HNCCD17] [datetime] NULL,
	[HNCCD18] [datetime] NULL,
	[HYNCCD1] [datetime] NULL,
	[HYNCCD2] [datetime] NULL,
	[HYNCCD3] [datetime] NULL,
	[HYNCCD4] [datetime] NULL,
	[HYNCCD5] [datetime] NULL,
	[HYNCCD6] [datetime] NULL,
	[HYNCCD7] [datetime] NULL,
	[HYNCCD8] [datetime] NULL,
	[WRID] [nvarchar](max) NULL,
	[Days] [nvarchar](max) NULL,
	[Cellcount] [int] NOT NULL,
	[UnFreezeDates] [datetime] NULL,
	[NonConfirmities1] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.WorkHours] PRIMARY KEY CLUSTERED 
(
	[wid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vessel]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vessel](
	[Vid] [int] IDENTITY(1,1) NOT NULL,
	[vessel_ID] [int] NOT NULL,
	[VesselName] [nvarchar](max) NULL,
	[imo] [int] NOT NULL,
	[Flag] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[website] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Vessel] PRIMARY KEY CLUSTERED 
(
	[Vid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Vessel] ON
INSERT [dbo].[Vessel] ([Vid], [vessel_ID], [VesselName], [imo], [Flag], [Email], [website]) VALUES (8, 9669665, N'CIELO DI NEW YORK', 9669665, N'Malta', N'cs@Work-Ship.com', N'www.work-ship.com')
SET IDENTITY_INSERT [dbo].[Vessel] OFF
/****** Object:  Table [dbo].[version_tbl]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[version_tbl](
	[vids] [int] IDENTITY(1,1) NOT NULL,
	[versions] [nvarchar](max) NULL,
	[ClientVersions] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.version_tbl] PRIMARY KEY CLUSTERED 
(
	[vids] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[version_tbl] ON
INSERT [dbo].[version_tbl] ([vids], [versions], [ClientVersions]) VALUES (1, N'1.0.0', N'1.0.0')
SET IDENTITY_INSERT [dbo].[version_tbl] OFF
/****** Object:  Table [dbo].[UserAccess]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[CrewManagement] [bit] NOT NULL,
	[CrewDetail] [bit] NOT NULL,
	[CrewRank] [bit] NOT NULL,
	[Department] [bit] NOT NULL,
	[HolidayGroup] [bit] NOT NULL,
	[HOD] [bit] NOT NULL,
	[ResetPassword] [bit] NOT NULL,
	[ResetPasswordAll] [bit] NOT NULL,
	[FreezeUnfreeze] [bit] NOT NULL,
	[FreezeUnfreezeAll] [bit] NOT NULL,
	[Report] [bit] NOT NULL,
	[OverView] [bit] NOT NULL,
	[OverViewAll] [bit] NOT NULL,
	[OverTime] [bit] NOT NULL,
	[CrewWorkHours] [bit] NOT NULL,
	[NonConfirmity] [bit] NOT NULL,
	[WorkSchedule] [bit] NOT NULL,
	[RestHours] [bit] NOT NULL,
	[WorkandResthour] [bit] NOT NULL,
	[WorkandRestHoursAll] [bit] NOT NULL,
	[Administration] [bit] NOT NULL,
	[ImportExport] [bit] NOT NULL,
	[BackupRestore] [bit] NOT NULL,
	[ApplicationLog] [bit] NOT NULL,
	[Rules] [bit] NOT NULL,
	[MainCertificate] [bit] NOT NULL,
	[Certificate] [bit] NOT NULL,
	[Lincenc] [bit] NOT NULL,
	[Notification] [bit] NOT NULL,
	[NCNotification] [bit] NOT NULL,
	[CerNotification] [bit] NOT NULL,
	[OCNotification] [bit] NOT NULL,
	[NonConformityAll] [bit] NOT NULL,
	[CrewDetailAll] [bit] NOT NULL,
	[CertificateAdd] [bit] NOT NULL,
	[CertificateEdit] [bit] NOT NULL,
	[CertificateDelete] [bit] NOT NULL,
	[ErrorLog] [bit] NOT NULL,
	[ManilaRules] [bit] NOT NULL,
	[GroupPlanning] [bit] NOT NULL,
	[SelectAll] [bit] NOT NULL,
	[HODName] [nvarchar](max) NULL,
	[DepartmentName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserAccess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSubMenu]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblSubMenu](
	[SubId] [int] IDENTITY(1,1) NOT NULL,
	[MId] [int] NULL,
	[SubMenuName] [varchar](100) NULL,
	[Action] [varchar](100) NULL,
	[Controller] [varchar](50) NULL,
	[AreaName] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
 CONSTRAINT [PK_TblSubMenu] PRIMARY KEY CLUSTERED 
(
	[SubId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSmartMenus]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSmartMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SmartMenuContent] [nvarchar](max) NULL,
	[HtmlContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblSmartMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblSmartMenus] ON
INSERT [dbo].[tblSmartMenus] ([Id], [SmartMenuContent], [HtmlContent]) VALUES (1, N'[{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=4","type":1,"text":"MSMP","id":4,"children":[{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=5","type":2,"text":"Cover","id":5},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=6","type":1,"text":"Introduction","id":6,"children":[{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=7","type":2,"text":"1.1 Objective of MSMP","id":7},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=8","type":2,"text":"1.2 Relation between terminal","id":8}]},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=9","type":1,"text":"Part-A General ship particulars","id":9},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=10","type":1,"text":"Part B - Mooring equipment design philosophy","id":10,"children":[{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=11","type":2,"text":"B.1 - Design considerations to achieve the ships optimal mooring pattern including information on the siting of mooring winches and fairleads to provide for direct leads and minimal lines across open decks","id":11},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=12","type":2,"text":"B.2 mooring against","id":12}]}]},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=13","type":1,"text":"LMP","id":13},{"deleted":0,"new":1,"slug":1,"href":"http://localhost:14642/MSPS/DetailsView?id=14","type":1,"text":"Training Content","id":14}]', N'




                        




                        




                        




                        




                        




                        




                        




                        




                        




						




						





						
					<li class="dd-item" data-id="0" data-text="Dashboard" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=0" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Dashboard</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="0"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="0"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="1" data-text="Notifications" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=1" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Notifications</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="1"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="1"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="3" data-text="Analysis" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=3" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Analysis</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="3"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="3"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="4" data-text="MSMP" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=4" data-slug="1" data-new="1" data-deleted="0"><button data-action="collapse" type="button">Collapse</button><button data-action="expand" type="button" style="display: none;">Expand</button><div class="dd-handle">MSMP</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="4"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="4"><i class="fa fa-pencil" aria-hidden="true"></i></span><ol class="dd-list"><li class="dd-item" data-id="5" data-text="Cover" data-type="2" data-href="http://localhost:14642/MSPS/DetailsView?id=5" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Cover</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="5"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="5"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="6" data-text="Introduction" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=6" data-slug="1" data-new="1" data-deleted="0"><button data-action="collapse" type="button">Collapse</button><button data-action="expand" type="button" style="display: none;">Expand</button><div class="dd-handle">Introduction</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="6"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="6"><i class="fa fa-pencil" aria-hidden="true"></i></span><ol class="dd-list"><li class="dd-item" data-id="7" data-text="1.1 Objective of MSMP" data-type="2" data-href="http://localhost:14642/MSPS/DetailsView?id=7" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">1.1 Objective of MSMP</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="7"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="7"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="8" data-text="1.2 Relation between terminal" data-type="2" data-href="http://localhost:14642/MSPS/DetailsView?id=8" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">1.2 Relation between terminal</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="8"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="8"><i class="fa fa-pencil" aria-hidden="true"></i></span></li></ol></li><li class="dd-item" data-id="9" data-text="Part-A General ship particulars" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=9" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Part-A General ship particulars</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="9"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="9"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="10" data-text="Part B - Mooring equipment design philosophy" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=10" data-slug="1" data-new="1" data-deleted="0"><button data-action="collapse" type="button">Collapse</button><button data-action="expand" type="button" style="display: none;">Expand</button><div class="dd-handle">Part B - Mooring equipment design philosophy</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="10"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="10"><i class="fa fa-pencil" aria-hidden="true"></i></span><ol class="dd-list"><li class="dd-item" data-id="11" data-text="B.1 - Design considerations to achieve the ship''s optimal mooring pattern including information on the siting of mooring winches and fairleads to provide for direct leads and minimal lines across open decks" data-type="2" data-href="http://localhost:14642/MSPS/DetailsView?id=11" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">B.1 - Design considerations to achieve the ship''s optimal mooring pattern including information on the siting of mooring winches and fairleads to provide for direct leads and minimal lines across open decks</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="11"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="11"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="12" data-text="B.2 mooring against" data-type="2" data-href="http://localhost:14642/MSPS/DetailsView?id=12" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">B.2 mooring against</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="12"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="12"><i class="fa fa-pencil" aria-hidden="true"></i></span></li></ol></li></ol></li><li class="dd-item" data-id="13" data-text="LMP" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=13" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">LMP</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="13"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="13"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="14" data-text="Training Content" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=14" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Training Content</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="14"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="14"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="15" data-text="Revision Master List" data-type="3" data-href="http://localhost:14642/MSPS/approve" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Revision Master List</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="15"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="15"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="16" data-text="Setting" data-type="1" data-href="http://localhost:14642/MSPS/DetailsView?id=16" data-slug="1" data-new="1" data-deleted="0"><button data-action="collapse" type="button">Collapse</button><button data-action="expand" type="button" style="display: none;">Expand</button><div class="dd-handle">Setting</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="16"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="16"><i class="fa fa-pencil" aria-hidden="true"></i></span><ol class="dd-list"><li class="dd-item" data-id="17" data-text="Menu Setting" data-type="3" data-href="http://localhost:14642/dynamicmenus/menus" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Menu Setting</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="17"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="17"><i class="fa fa-pencil" aria-hidden="true"></i></span></li><li class="dd-item" data-id="18" data-text="Export / Import" data-type="3" data-href="http://localhost:14642/importexport" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Export / Import</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="18"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="18"><i class="fa fa-pencil" aria-hidden="true"></i></span></li></ol></li>





						
					





                        
                    





                        
                    





                        
                    





                        
                    





                        
                    





                        
                    





                        
                    





                        
                    <li class="dd-item" data-id="19" data-text="Test Anil" data-prefix="Anil" data-type="2" data-href="http://localhost:14642/MSPS/DetailsView?id=19" data-slug="1" data-new="1" data-deleted="0"><div class="dd-handle">Test Anil</div> <span class="button-delete btn btn-default btn-xs pull-right" data-owner-id="19"> <i class="fa fa-times-circle-o" aria-hidden="true"></i> </span><span class="button-edit btn btn-default btn-xs pull-right" data-owner-id="19"><i class="fa fa-pencil" aria-hidden="true"></i></span></li>





                        
                    ')
SET IDENTITY_INSERT [dbo].[tblSmartMenus] OFF
/****** Object:  Table [dbo].[tblShipSpecificContent]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShipSpecificContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MId] [int] NULL,
	[ShipId] [nvarchar](50) NULL,
	[Content] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblShipSpecificContent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblShipSpecificContent] ON
INSERT [dbo].[tblShipSpecificContent] ([Id], [MId], [ShipId], [Content], [CreatedDate]) VALUES (1, 2, N'9669665', N'<table class="MsoNormalTable" border="1" cellspacing="0" cellpadding="0" style="margin-left: 12.1pt; border: none;">
 <tbody><tr style="mso-yfti-irow:0;mso-yfti-firstrow:yes;height:213.05pt">
  <td width="646" valign="top" style="width:484.45pt;border:solid black 2.25pt;
  border-bottom:solid black 1.0pt;mso-border-alt:solid black 2.25pt;mso-border-bottom-alt:
  solid black .5pt;padding:0cm 0cm 0cm 0cm;height:213.05pt">
  <p class="TableParagraph" align="left"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">&nbsp;</span></p>
  <p class="TableParagraph" align="left"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">&nbsp;</span></p>
  <p class="TableParagraph" align="center" style="margin-top:.05pt;margin-right:
  26.45pt;margin-bottom:0cm;margin-left:36.6pt;margin-bottom:.0001pt;
  text-align:center;line-height:41.0pt;mso-line-height-rule:exactly"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">M/T
  CIELO DI NEW YORK<o:p></o:p></span></p>
  <p class="TableParagraph" align="center" style="margin-top:0cm;margin-right:34.4pt;
  margin-bottom:0cm;margin-left:36.15pt;margin-bottom:.0001pt;text-align:center;
  line-height:33.8pt;mso-line-height-rule:exactly"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">IMO
  9669665<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style="mso-yfti-irow:1;height:58.0pt">
  <td width="646" valign="top" style="width:484.45pt;border-top:none;border-left:
  solid black 2.25pt;border-bottom:solid black 1.0pt;border-right:solid black 2.25pt;
  mso-border-top-alt:solid black .5pt;mso-border-top-alt:.5pt;mso-border-left-alt:
  2.25pt;mso-border-bottom-alt:.5pt;mso-border-right-alt:2.25pt;mso-border-color-alt:
  black;mso-border-style-alt:solid;background:#9CC2E4;padding:0cm 0cm 0cm 0cm;
  height:58.0pt">
  <p class="TableParagraph" align="left" style="margin: 0.2pt 6.45pt 0.0001pt 125.65pt; text-indent: -36.05pt; line-height: 29pt;"><span lang="EN-US" style="font-size:
  12.0pt;font-family:&quot;Arial&quot;,sans-serif">MOORING SYSTEM &amp; LINE MANAGEMENT
  PLAN<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style="mso-yfti-irow:2;height:202.35pt">
  <td width="646" valign="top" style="width:484.45pt;border-top:none;border-left:
  solid black 2.25pt;border-bottom:solid black 1.0pt;border-right:solid black 2.25pt;
  mso-border-top-alt:solid black .5pt;mso-border-top-alt:.5pt;mso-border-left-alt:
  2.25pt;mso-border-bottom-alt:.5pt;mso-border-right-alt:2.25pt;mso-border-color-alt:
  black;mso-border-style-alt:solid;padding:0cm 0cm 0cm 0cm;height:202.35pt">
  <p class="TableParagraph" align="left" style="margin-top: 0.3pt;"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">&nbsp;</span></p>
  <p class="TableParagraph" align="center" style="margin-top:0cm;margin-right:30.55pt;
  margin-bottom:0cm;margin-left:36.6pt;margin-bottom:.0001pt;text-align:center"><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">This plan has been developed
  in accordance with:<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin-left: 37.1pt; text-indent: -14.75pt;"><!--[if !supportLists]--><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Tahoma&quot;,sans-serif;
  mso-fareast-font-family:Tahoma;color:windowtext;letter-spacing:-.25pt;
  mso-bidi-font-weight:bold">•<span style="font-variant-numeric: normal; font-variant-east-asian: normal; font-stretch: normal; font-size: 7pt; line-height: normal; font-family: &quot;Times New Roman&quot;;">&nbsp;&nbsp; </span></span><!--[endif]--><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">OCIMF Mooring Equipment<span style="letter-spacing:-.15pt"> </span>Guide<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin: 0.05pt 0cm 0.0001pt 37.1pt; text-indent: -14.75pt;"><!--[if !supportLists]--><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Tahoma&quot;,sans-serif;
  mso-fareast-font-family:Tahoma;color:windowtext;letter-spacing:-.25pt;
  mso-bidi-font-weight:bold">•<span style="font-variant-numeric: normal; font-variant-east-asian: normal; font-stretch: normal; font-size: 7pt; line-height: normal; font-family: &quot;Times New Roman&quot;;">&nbsp;&nbsp; </span></span><!--[endif]--><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">SOLAS regulation<span style="letter-spacing:-.15pt"> </span>II-1/3-8<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin-left: 36.6pt; text-indent: -14.25pt;"><!--[if !supportLists]--><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Tahoma&quot;,sans-serif;
  mso-fareast-font-family:Tahoma;color:windowtext;letter-spacing:-.25pt;
  mso-bidi-font-weight:bold">•<span style="font-variant-numeric: normal; font-variant-east-asian: normal; font-stretch: normal; font-size: 7pt; line-height: normal; font-family: &quot;Times New Roman&quot;;">&nbsp;&nbsp; </span></span><!--[endif]--><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">Vessel Inspection<span style="letter-spacing:-.15pt"> </span>Questionnaire<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin: 0.05pt 0cm 0.0001pt 37.1pt; text-indent: -14.75pt;"><!--[if !supportLists]--><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Tahoma&quot;,sans-serif;
  mso-fareast-font-family:Tahoma;color:windowtext;letter-spacing:-.25pt;
  mso-bidi-font-weight:bold">•<span style="font-variant-numeric: normal; font-variant-east-asian: normal; font-stretch: normal; font-size: 7pt; line-height: normal; font-family: &quot;Times New Roman&quot;;">&nbsp;&nbsp; </span></span><!--[endif]--><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">Mooring Arrangement<span style="letter-spacing:-.15pt"> </span>Plan<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin-left: 36.6pt; text-indent: -14.25pt; line-height: 14.35pt;"><!--[if !supportLists]--><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Tahoma&quot;,sans-serif;
  mso-fareast-font-family:Tahoma;color:windowtext;letter-spacing:-.25pt;
  mso-bidi-font-weight:bold">•<span style="font-variant-numeric: normal; font-variant-east-asian: normal; font-stretch: normal; font-size: 7pt; line-height: normal; font-family: &quot;Times New Roman&quot;;">&nbsp;&nbsp; </span></span><!--[endif]--><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">Code of Safe Working
  Practices for Merchant<span style="letter-spacing:-.4pt"> </span>Seafarers.<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin-left: 36.6pt; text-indent: -14.25pt; line-height: 14.35pt;"><!--[if !supportLists]--><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Tahoma&quot;,sans-serif;
  mso-fareast-font-family:Tahoma;color:windowtext;letter-spacing:-.25pt;
  mso-bidi-font-weight:bold">•<span style="font-variant-numeric: normal; font-variant-east-asian: normal; font-stretch: normal; font-size: 7pt; line-height: normal; font-family: &quot;Times New Roman&quot;;">&nbsp;&nbsp; </span></span><!--[endif]--><b><span lang="EN-US" style="font-size:12.0pt;
  font-family:&quot;Arial&quot;,sans-serif;color:windowtext">Relevant IACS<span style="letter-spacing:-.2pt"> </span>Recommendations<o:p></o:p></span></b></p>
  <p class="TableParagraph" align="left" style="margin-top: 0.4pt;"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">&nbsp;</span></p>
  <p class="TableParagraph" align="center" style="margin-top:0cm;margin-right:34.4pt;
  margin-bottom:0cm;margin-left:36.6pt;margin-bottom:.0001pt;text-align:center;
  line-height:96%"><b><span lang="EN-US" style="font-size:12.0pt;line-height:96%;font-family:&quot;Arial&quot;,sans-serif;
  color:windowtext">This plan should be kept by the Master and used as a
  practical guide regarding Mooring System &amp; Line management.<o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style="mso-yfti-irow:3;height:140.05pt">
  <td width="646" valign="top" style="width:484.45pt;border-top:none;border-left:
  solid black 2.25pt;border-bottom:solid black 1.0pt;border-right:solid black 2.25pt;
  mso-border-top-alt:solid black .5pt;mso-border-top-alt:.5pt;mso-border-left-alt:
  2.25pt;mso-border-bottom-alt:.5pt;mso-border-right-alt:2.25pt;mso-border-color-alt:
  black;mso-border-style-alt:solid;padding:0cm 0cm 0cm 0cm;height:140.05pt">
  <p class="TableParagraph" align="left"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">&nbsp;</span></p>
  </td>
 </tr>
 <tr style="mso-yfti-irow:4;height:23.95pt">
  <td width="646" valign="top" style="width:484.45pt;border-top:none;border-left:
  solid black 2.25pt;border-bottom:solid black 1.0pt;border-right:solid black 2.25pt;
  mso-border-top-alt:solid black .5pt;mso-border-top-alt:.5pt;mso-border-left-alt:
  2.25pt;mso-border-bottom-alt:.5pt;mso-border-right-alt:2.25pt;mso-border-color-alt:
  black;mso-border-style-alt:solid;padding:0cm 0cm 0cm 0cm;height:23.95pt">
  <p class="TableParagraph" align="left" style="margin: 0.15pt 6.45pt 0.0001pt 17.85pt; text-indent: -8.75pt; line-height: 12pt;"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">The
  present manual is property of the manager of the vessel and may not be
  removed from the vessel or reproduced wholly or partly in any manner without
  the prior agreement of the manager of the vessel.<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style="mso-yfti-irow:5;mso-yfti-lastrow:yes;height:16.35pt">
  <td width="646" valign="top" style="width:484.45pt;border:solid black 2.25pt;
  border-top:none;mso-border-top-alt:solid black .5pt;padding:0cm 0cm 0cm 0cm;
  height:16.35pt">
  <p class="TableParagraph" align="left"><span lang="EN-US" style="font-size:12.0pt;font-family:&quot;Arial&quot;,sans-serif;color:windowtext">&nbsp;</span></p>
  </td>
 </tr>
</tbody></table>', CAST(0x0000AAB4016CF513 AS DateTime))
SET IDENTITY_INSERT [dbo].[tblShipSpecificContent] OFF
/****** Object:  Table [dbo].[tblShipSpecificAttachment]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShipSpecificAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AttachmentName] [nvarchar](200) NULL,
	[AttachmentPath] [nvarchar](max) NULL,
	[MId] [int] NULL,
	[ShipId] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreateBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblShipSpecificAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblShipSpecificAttachment] ON
INSERT [dbo].[tblShipSpecificAttachment] ([Id], [AttachmentName], [AttachmentPath], [MId], [ShipId], [CreatedDate], [CreateBy], [ModifiedBy], [ModifiedDate], [Type]) VALUES (1, N'Testing Uploading', N'C:\Work-Ship_DB_Backup\Attachment\637025115562863757.png', 1, N'9669665', CAST(0x0000AAB600E85806 AS DateTime), N'ADMIN', N'ÁDMIN', CAST(0x0000AAB600E85806 AS DateTime), NULL)
INSERT [dbo].[tblShipSpecificAttachment] ([Id], [AttachmentName], [AttachmentPath], [MId], [ShipId], [CreatedDate], [CreateBy], [ModifiedBy], [ModifiedDate], [Type]) VALUES (2, N'Testing Uploading', N'C:\Work-Ship_DB_Backup\Attachment\637025115562863757.png\637025117931333592.png', 1, N'9669665', CAST(0x0000AAB600E97346 AS DateTime), N'ADMIN', N'ÁDMIN', CAST(0x0000AAB600E97346 AS DateTime), NULL)
INSERT [dbo].[tblShipSpecificAttachment] ([Id], [AttachmentName], [AttachmentPath], [MId], [ShipId], [CreatedDate], [CreateBy], [ModifiedBy], [ModifiedDate], [Type]) VALUES (3, N'Attachment_1', N'C:\Work-Ship_DB_Backup\Attachment\637025115562863757.png\637025117931333592.png\637025162604862654.png', 1, N'9669665', CAST(0x0000AAB600FDE77F AS DateTime), N'ADMIN', N'ÁDMIN', CAST(0x0000AAB600FDE77F AS DateTime), NULL)
INSERT [dbo].[tblShipSpecificAttachment] ([Id], [AttachmentName], [AttachmentPath], [MId], [ShipId], [CreatedDate], [CreateBy], [ModifiedBy], [ModifiedDate], [Type]) VALUES (4, N'Attachment_1', N'C:\Work-Ship_DB_Backup\Attachment\637025115562863757.png\637025117931333592.png\637025162604862654.png\637025162700760420.png', 1, N'9669665', CAST(0x0000AAB600FDEED4 AS DateTime), N'ADMIN', N'ÁDMIN', CAST(0x0000AAB600FDEED4 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tblShipSpecificAttachment] OFF
/****** Object:  Table [dbo].[Notifications]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Notification] [nvarchar](500) NULL,
	[NotificationType] [int] NULL,
	[ShipActionTaken] [nvarchar](200) NULL,
	[Acknowledge] [bit] NULL,
	[AckRecord] [nvarchar](100) NULL,
	[NotificationDueDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (18, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 1, N'Acknowledged on 05-Sep-2019 11:05 Hours', NULL, CAST(0x0000AAB3011D37BC AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (59, N'Cropped more than 10% - Rope Polyster on winch M4 located at Fwd Spring Line - Aft', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAB5010C7351 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (60, N'Spliced - Rope Polyster on winch M4 located at Fwd Spring Line - Aft', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAB501183991 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (61, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAB5012F6B10 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (62, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAB5012F6B10 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (63, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 1, N'Acknowledged on 28-Aug-2019 09:57 Hours', NULL, CAST(0x0000AAB5012F6B2D AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (64, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 1, N'Acknowledged on 27-Aug-2019 16:50 Hours', NULL, CAST(0x0000AAB5012F6B2D AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (65, N'Damaged - Rope Polyster on winch M4 located at Fwd Spring Line - Aft', 1, NULL, 1, N'Acknowledged on 27-Aug-2019 16:50 Hours', NULL, CAST(0x0000AAB5012F92A5 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (66, N'Spliced - Rope Polyster on winch M4 located at Fwd Spring Line - Aft', 1, NULL, 1, N'Acknowledged on 27-Aug-2019 17:18 Hours', NULL, CAST(0x0000AAB5012F92B6 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (67, N'Spliced - Rope Polyster on winch M4 located at Fwd Spring Line - Aft', 1, NULL, 1, N'Acknowledged on 27-Aug-2019 16:27 Hours', NULL, CAST(0x0000AAB600D5D6B3 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (68, N'Spliced - Rope Nylon on winch W1 located at Forecastle Port Outer', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAB900C5BD90 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (69, N'Cropped more than 10% - Rope Nylon on winch W1 located at Forecastle Port Outer', 2, NULL, 1, N'Acknowledged on 03-Sep-2019 16:58 Hours', NULL, CAST(0x0000AAB900CB6A95 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (70, N'Out of Service / discarded - Rope Nylon on winch W1 located at Forecastle Port Outer', 1, NULL, 1, N'Acknowledged on 04-Sep-2019 11:34 Hours', NULL, CAST(0x0000AAB900D2C9B8 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (71, N'Out of Service / discarded - Rope Nylon on winch W1 located at Forecastle Port Outer', 1, NULL, 1, N'Acknowledged on 04-Sep-2019 11:34 Hours', NULL, CAST(0x0000AAB900DCEA72 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (72, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 1, N'Acknowledged on 04-Sep-2019 11:35 Hours', NULL, CAST(0x0000AABD00B23C56 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (73, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00B23C56 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (74, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00B23C73 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (75, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00B23C73 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (76, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00B8496E AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (77, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00B8496E AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (78, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00B84988 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (79, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 1, N'Acknowledged on 03-Sep-2019 16:52 Hours', NULL, CAST(0x0000AABD00B84989 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (80, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00BA6041 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (81, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 1, N'Acknowledged on 03-Sep-2019 16:53 Hours', NULL, CAST(0x0000AABD00BA6041 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (82, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00BA6056 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (83, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00BA605A AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (84, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 1, N'Acknowledged on 03-Sep-2019 16:52 Hours', NULL, CAST(0x0000AABD00D03262 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (85, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 1, N'Acknowledged on 03-Sep-2019 16:52 Hours', NULL, CAST(0x0000AABD00D03262 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (86, N'Maximum running hours or period or tail replacement getting due -', 1, NULL, 1, N'Acknowledged on 04-Sep-2019 11:56 Hours', NULL, CAST(0x0000AABD00D0327B AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (87, N'Maximum running hours or period or tail replacement getting due -', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00D0327B AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (88, N'Maximum running hours or period or tail replacement getting due', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00D03296 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (89, N'Maximum running hours or period or tail replacement getting due', 2, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD00D03296 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (90, N'Loose Equipment condition Not Acceptable of (2) Joining Shackle on Number Cergfdfdghf', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD0110EC42 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (91, N'Loose Equipment condition Not Acceptable of (1) Chain Stopper on CertificateNumber - ''Sdsf''', 1, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AABD011419C8 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (92, N'Inspection Due on 7 days- Rope #91310 on Winch Spare 9 located at Spare Rope Fwd', 0, NULL, 0, N'Not yet acknowledged', CAST(0x0000AB9B00000000 AS DateTime), CAST(0x0000AAC600B22459 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (93, N'Rating 5 - Rope #91310 on Winch Spare 9 located at Spare Rope Fwd', 0, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAC600D3AB4A AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (94, N'Inspection Due on 7 days- Rope #91310 on Winch Spare 9 located at Spare Rope Fwd', 0, NULL, 0, N'Not yet acknowledged', CAST(0x0000AB7C00000000 AS DateTime), CAST(0x0000AAC600D3BAC1 AS DateTime), N'Admin', 0)
INSERT [dbo].[Notifications] ([Id], [Notification], [NotificationType], [ShipActionTaken], [Acknowledge], [AckRecord], [NotificationDueDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (95, N'Rating 5 - Rope #652 63532323 Asda3 2 on Winch W1 located at Forecastle Port Outer', 0, NULL, 0, N'Not yet acknowledged', NULL, CAST(0x0000AAC600D3BAC2 AS DateTime), N'Admin', 0)
SET IDENTITY_INSERT [dbo].[Notifications] OFF
/****** Object:  Table [dbo].[NotificationComment]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotificationId] [int] NULL,
	[CommentsType] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_NotificationComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[NotificationComment] ON
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (9, 1, 1, N'Will Be Inspecting', CAST(0x0000AA9C01039037 AS DateTime), N'Admin', 1)
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (10, 1, 1, N'Inspected And Report Entered In Software', CAST(0x0000AA9C010468DA AS DateTime), N'Admin', 1)
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (11, 1, 2, N'Noted Satisfactory Inspection', CAST(0x0000AA9C0104A876 AS DateTime), N'Admin', 1)
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (12, 1, 1, N'Dfdfdf', CAST(0x0000AAA800C47AAA AS DateTime), N'Admin', 1)
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (13, 1, 1, N'Fghdfgf', CAST(0x0000AAB000FE094D AS DateTime), N'Admin', 1)
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (14, 1, 1, N'Sdsdsdsdsdsdsdsdsdsdsdsdsd', CAST(0x0000AAB000FE0FDE AS DateTime), N'Admin', 1)
INSERT [dbo].[NotificationComment] ([Id], [NotificationId], [CommentsType], [Comments], [CreatedDate], [CreatedBy], [IsActive]) VALUES (1013, 67, 1, N'Zsdasd', CAST(0x0000AAB600D6AFF5 AS DateTime), N'Admin', 1)
SET IDENTITY_INSERT [dbo].[NotificationComment] OFF
/****** Object:  Table [dbo].[Department]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[did] [int] IDENTITY(1,1) NOT NULL,
	[DeptName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Department] PRIMARY KEY CLUSTERED 
(
	[did] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefineRules]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefineRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rule6Hrs] [decimal](18, 2) NOT NULL,
	[Rule10Hrs] [decimal](18, 2) NOT NULL,
	[Rule10DevideTwo] [bit] NOT NULL,
	[Manila10DevideThree] [bit] NOT NULL,
	[Rule77Hrs] [decimal](18, 2) NOT NULL,
	[Manila70Hrs] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.DefineRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DefineRules] ON
INSERT [dbo].[DefineRules] ([Id], [Rule6Hrs], [Rule10Hrs], [Rule10DevideTwo], [Manila10DevideThree], [Rule77Hrs], [Manila70Hrs]) VALUES (2, CAST(6.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 1, 1, CAST(77.00 AS Decimal(18, 2)), CAST(70.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[DefineRules] OFF
/****** Object:  Table [dbo].[DamageObserved]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DamageObserved](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DamageObserved] [nvarchar](50) NULL,
 CONSTRAINT [PK_DamageObserved] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DamageObserved] ON
INSERT [dbo].[DamageObserved] ([Id], [DamageObserved]) VALUES (1, N'Inspection')
INSERT [dbo].[DamageObserved] ([Id], [DamageObserved]) VALUES (2, N'Mooring Operation')
SET IDENTITY_INSERT [dbo].[DamageObserved] OFF
/****** Object:  Table [dbo].[Currency]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[curid] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyName] [nvarchar](max) NULL,
	[CurrencySymbol] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Currency] PRIMARY KEY CLUSTERED 
(
	[curid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrossShiftingWinch]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrossShiftingWinch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[WinchId] [int] NULL,
	[OutboardEndinUse] [bit] NULL,
	[DateofShifting] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_CrossShiftingWinch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrewRank]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrewRank](
	[cid] [int] IDENTITY(1,1) NOT NULL,
	[SrNo] [int] NOT NULL,
	[Rank] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CrewRank] PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrewDetail]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrewDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[position] [nvarchar](max) NULL,
	[department] [nvarchar](max) NULL,
	[ServiceFrom] [datetime] NOT NULL,
	[ServiceTo] [datetime] NOT NULL,
	[CDC] [nvarchar](max) NULL,
	[empno] [nvarchar](max) NULL,
	[pswd] [nvarchar](max) NULL,
	[comments] [nvarchar](max) NULL,
	[overtime] [bit] NOT NULL,
	[SeaWk] [nvarchar](max) NULL,
	[SeaNWK] [nvarchar](max) NULL,
	[PortWk] [nvarchar](max) NULL,
	[PortNWK] [nvarchar](max) NULL,
	[dates] [datetime] NOT NULL,
	[SeaWH] [decimal](18, 2) NOT NULL,
	[portWH] [decimal](18, 2) NOT NULL,
	[did] [int] NOT NULL,
	[rid] [int] NOT NULL,
	[SeaWk1] [nvarchar](max) NULL,
	[SeaNWK1] [nvarchar](max) NULL,
	[PortWk1] [nvarchar](max) NULL,
	[PortNWK1] [nvarchar](max) NULL,
	[OpaStatus] [bit] NOT NULL,
	[CertificateView] [bit] NOT NULL,
	[CertificateAdd] [bit] NOT NULL,
	[CertificateEdit] [bit] NOT NULL,
	[CertificateDelete] [bit] NOT NULL,
	[DOB] [datetime] NOT NULL,
	[chkyoungs] [bit] NOT NULL,
	[SeaWkYoung] [nvarchar](max) NULL,
	[SeaNWKYoung] [nvarchar](max) NULL,
	[PortWkYoung] [nvarchar](max) NULL,
	[PortNWKYoung] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[WatchKeeper] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.CrewDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comment_ofVS]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment_ofVS](
	[Comnt_ID] [int] IDENTITY(1,1) NOT NULL,
	[Vessel_ID] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[DepartmentName] [nvarchar](max) NULL,
	[NC_Date] [datetime] NOT NULL,
	[Office_Comment] [nvarchar](max) NULL,
	[Comment_Date] [datetime] NOT NULL,
	[Acknowledge_Status] [nvarchar](max) NULL,
	[AdminAlarm] [bit] NOT NULL,
	[MasterAlarm] [bit] NOT NULL,
	[HODAlarm] [bit] NOT NULL,
	[Acknowledge_Status_MASTER] [nvarchar](max) NULL,
	[Acknowledge_Status_HOD] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.comment_ofVS] PRIMARY KEY CLUSTERED 
(
	[Comnt_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChainStopper]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChainStopper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseETypeId] [int] NULL,
	[ManufactureName] [nvarchar](100) NULL,
	[CertificateNumber] [nvarchar](50) NULL,
	[MBL] [decimal](18, 3) NULL,
	[Length] [decimal](18, 3) NULL,
	[DateReceived] [datetime] NULL,
	[DateInstalled] [datetime] NULL,
	[OutofServiceDate] [datetime] NULL,
	[ReasonOutofService] [nvarchar](50) NULL,
	[OtherReason] [nvarchar](100) NULL,
	[DamagedObserved] [nvarchar](50) NULL,
	[MooringOperation] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ChainStopper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChainStopper] ON
INSERT [dbo].[ChainStopper] ([Id], [LooseETypeId], [ManufactureName], [CertificateNumber], [MBL], [Length], [DateReceived], [DateInstalled], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamagedObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 3, N'Testt', N'Ce65465', CAST(65.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(0x0000AA7D00000000 AS DateTime), CAST(0x0000AA7E00000000 AS DateTime), CAST(0x0000AA8B00000000 AS DateTime), N'Damaged', NULL, N'Mooring Operation', N'All Operation', CAST(0x0000AA92013387F1 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[ChainStopper] ([Id], [LooseETypeId], [ManufactureName], [CertificateNumber], [MBL], [Length], [DateReceived], [DateInstalled], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamagedObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 3, N'Testin 2', N'Sdgfsdsdf', CAST(54.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(0x0000AA9300000000 AS DateTime), CAST(0x0000AA9300000000 AS DateTime), CAST(0x0000AA9300000000 AS DateTime), N'Other', N'Dfggdfg', NULL, NULL, CAST(0x0000AA9300D6CAAE AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[ChainStopper] ([Id], [LooseETypeId], [ManufactureName], [CertificateNumber], [MBL], [Length], [DateReceived], [DateInstalled], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamagedObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 5, N'Manufnametest', N'Sdsf', CAST(344.000 AS Decimal(18, 3)), CAST(33.000 AS Decimal(18, 3)), CAST(0x0000AAB100000000 AS DateTime), CAST(0x0000AAB100000000 AS DateTime), CAST(0x0000AAB100000000 AS DateTime), N'Completed Running Hours', NULL, NULL, NULL, CAST(0x0000AAB1010D737D AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ChainStopper] OFF
/****** Object:  Table [dbo].[certificates]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[certificates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SrNo] [int] NOT NULL,
	[CName] [nvarchar](max) NULL,
	[DOI] [datetime] NOT NULL,
	[DOE] [datetime] NOT NULL,
	[DOS] [datetime] NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[Acknowledgements] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[AcknowledgDate] [datetime] NULL,
	[month6] [bit] NOT NULL,
	[AckDOE6m] [nvarchar](max) NULL,
	[NCDOE6m] [nvarchar](max) NULL,
	[month3] [bit] NOT NULL,
	[AckDOE3m] [nvarchar](max) NULL,
	[NCDOE3m] [nvarchar](max) NULL,
	[month1] [bit] NOT NULL,
	[AckDOE1m] [nvarchar](max) NULL,
	[NCDOE1m] [nvarchar](max) NULL,
	[Days15] [bit] NOT NULL,
	[AckDOE15d] [nvarchar](max) NULL,
	[NCDOE15d] [nvarchar](max) NULL,
	[Days7] [bit] NOT NULL,
	[AckDOE7d] [nvarchar](max) NULL,
	[NCDOE7d] [nvarchar](max) NULL,
	[Days1] [bit] NOT NULL,
	[AckDOE1d] [nvarchar](max) NULL,
	[NCDOE1d] [nvarchar](max) NULL,
	[DOSmonth6] [bit] NOT NULL,
	[AckDOS6m] [nvarchar](max) NULL,
	[NCDOS6m] [nvarchar](max) NULL,
	[DOSmonth3] [bit] NOT NULL,
	[AckDOS3m] [nvarchar](max) NULL,
	[NCDOS3m] [nvarchar](max) NULL,
	[DOSmonth1] [bit] NOT NULL,
	[AckDOS1m] [nvarchar](max) NULL,
	[NCDOS1m] [nvarchar](max) NULL,
	[DOSDays15] [bit] NOT NULL,
	[AckDOS15d] [nvarchar](max) NULL,
	[NCDOS15d] [nvarchar](max) NULL,
	[DOSDays7] [bit] NOT NULL,
	[AckDOS7d] [nvarchar](max) NULL,
	[NCDOS7d] [nvarchar](max) NULL,
	[DOSDays1] [bit] NOT NULL,
	[AckDOS1d] [nvarchar](max) NULL,
	[NCDOS1d] [nvarchar](max) NULL,
	[OverDOE] [nvarchar](max) NULL,
	[OverDOS] [nvarchar](max) NULL,
	[AckDOEOver] [nvarchar](max) NULL,
	[AckDOSOver] [nvarchar](max) NULL,
	[AlarmNCDOE6m] [bit] NOT NULL,
	[AlarmNCDOE3m] [bit] NOT NULL,
	[AlarmNCDOE1m] [bit] NOT NULL,
	[AlarmNCDOE15d] [bit] NOT NULL,
	[AlarmNCDOE7d] [bit] NOT NULL,
	[AlarmNCDOE1d] [bit] NOT NULL,
	[AlarmNCDOEXXX] [bit] NOT NULL,
	[AlarmNCDOS6m] [bit] NOT NULL,
	[AlarmNCDOS3m] [bit] NOT NULL,
	[AlarmNCDOS1m] [bit] NOT NULL,
	[AlarmNCDOS15d] [bit] NOT NULL,
	[AlarmNCDOS7d] [bit] NOT NULL,
	[AlarmNCDOS1d] [bit] NOT NULL,
	[AlarmNCDOSXXX] [bit] NOT NULL,
	[AckDOE6m_MASTER] [nvarchar](max) NULL,
	[AckDOE3m_MASTER] [nvarchar](max) NULL,
	[AckDOE1m_MASTER] [nvarchar](max) NULL,
	[AckDOE15d_MASTER] [nvarchar](max) NULL,
	[AckDOE7d_MASTER] [nvarchar](max) NULL,
	[AckDOE1d_MASTER] [nvarchar](max) NULL,
	[AckDOEOver_MASTER] [nvarchar](max) NULL,
	[AckDOS6m_MASTER] [nvarchar](max) NULL,
	[AckDOS3m_MASTER] [nvarchar](max) NULL,
	[AckDOS1m_MASTER] [nvarchar](max) NULL,
	[AckDOS15d_MASTER] [nvarchar](max) NULL,
	[AckDOS7d_MASTER] [nvarchar](max) NULL,
	[AckDOS1d_MASTER] [nvarchar](max) NULL,
	[AckDOSOver_MASTER] [nvarchar](max) NULL,
	[AckDOE6m_HOD] [nvarchar](max) NULL,
	[AckDOE3m_HOD] [nvarchar](max) NULL,
	[AckDOE1m_HOD] [nvarchar](max) NULL,
	[AckDOE15d_HOD] [nvarchar](max) NULL,
	[AckDOE7d_HOD] [nvarchar](max) NULL,
	[AckDOE1d_HOD] [nvarchar](max) NULL,
	[AckDOEOver_HOD] [nvarchar](max) NULL,
	[AckDOS6m_HOD] [nvarchar](max) NULL,
	[AckDOS3m_HOD] [nvarchar](max) NULL,
	[AckDOS1m_HOD] [nvarchar](max) NULL,
	[AckDOS15d_HOD] [nvarchar](max) NULL,
	[AckDOS7d_HOD] [nvarchar](max) NULL,
	[AckDOS1d_HOD] [nvarchar](max) NULL,
	[AckDOSOver_HOD] [nvarchar](max) NULL,
	[AlarmNCDOE6m_MASTER] [bit] NOT NULL,
	[AlarmNCDOE3m_MASTER] [bit] NOT NULL,
	[AlarmNCDOE1m_MASTER] [bit] NOT NULL,
	[AlarmNCDOE15d_MASTER] [bit] NOT NULL,
	[AlarmNCDOE7d_MASTER] [bit] NOT NULL,
	[AlarmNCDOE1d_MASTER] [bit] NOT NULL,
	[AlarmNCDOEXXX_MASTER] [bit] NOT NULL,
	[AlarmNCDOS6m_MASTER] [bit] NOT NULL,
	[AlarmNCDOS3m_MASTER] [bit] NOT NULL,
	[AlarmNCDOS1m_MASTER] [bit] NOT NULL,
	[AlarmNCDOS15d_MASTER] [bit] NOT NULL,
	[AlarmNCDOS7d_MASTER] [bit] NOT NULL,
	[AlarmNCDOS1d_MASTER] [bit] NOT NULL,
	[AlarmNCDOSXXX_MASTER] [bit] NOT NULL,
	[AlarmNCDOE6m_HOD] [bit] NOT NULL,
	[AlarmNCDOE3m_HOD] [bit] NOT NULL,
	[AlarmNCDOE1m_HOD] [bit] NOT NULL,
	[AlarmNCDOE15d_HOD] [bit] NOT NULL,
	[AlarmNCDOE7d_HOD] [bit] NOT NULL,
	[AlarmNCDOE1d_HOD] [bit] NOT NULL,
	[AlarmNCDOEXXX_HOD] [bit] NOT NULL,
	[AlarmNCDOS6m_HOD] [bit] NOT NULL,
	[AlarmNCDOS3m_HOD] [bit] NOT NULL,
	[AlarmNCDOS1m_HOD] [bit] NOT NULL,
	[AlarmNCDOS15d_HOD] [bit] NOT NULL,
	[AlarmNCDOS7d_HOD] [bit] NOT NULL,
	[AlarmNCDOS1d_HOD] [bit] NOT NULL,
	[AlarmNCDOSXXX_HOD] [bit] NOT NULL,
	[ExpiryStatus] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.certificates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CertificateNotification]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CertificateNotification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CName] [nvarchar](max) NULL,
	[DOI] [datetime] NOT NULL,
	[DOE] [datetime] NOT NULL,
	[DOS] [datetime] NOT NULL,
	[AlertFrequency] [nvarchar](max) NULL,
	[AdminAck] [nvarchar](max) NULL,
	[MasterAck] [nvarchar](max) NULL,
	[HODAck] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CertificateNotification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[certificate_order]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[certificate_order](
	[serial_ID] [int] IDENTITY(1,1) NOT NULL,
	[certificate_ID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.certificate_order] PRIMARY KEY CLUSTERED 
(
	[serial_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignRopeToWinch]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignRopeToWinch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[Outboard] [bit] NULL,
	[WinchId] [int] NULL,
	[AssignedLocation] [nvarchar](100) NULL,
	[AssignedDate] [datetime] NULL,
	[RopeTail] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_AssignRoptToWinch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AssignRopeToWinch] ON
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 16, 0, 20, N'Spare Rope Fwd', CAST(0x0000AAA000000000 AS DateTime), 0, CAST(0x0000AAA00123EB22 AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 9, 0, 13, N'Spare Rope Fwd', CAST(0x0000AA9900000000 AS DateTime), 0, CAST(0x0000AAAE011DAEB1 AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (45, 9, 1, 11, NULL, CAST(0x0000AAAF00000000 AS DateTime), 0, CAST(0x0000AAAF01320B4D AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (46, 16, 1, 13, NULL, CAST(0x0000AAAF00000000 AS DateTime), 0, CAST(0x0000AAAF01321A51 AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (47, 9, 1, 22, NULL, CAST(0x0000AAAF00000000 AS DateTime), 0, CAST(0x0000AAAF01322E69 AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1008, 16, 1, 22, NULL, CAST(0x0000AAB000000000 AS DateTime), 0, CAST(0x0000AAB000ACCCB8 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1009, 9, 1, 8, NULL, CAST(0x0000AAB000000000 AS DateTime), 0, CAST(0x0000AAB000B02418 AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1010, 22, 0, 4, NULL, CAST(0x0000AAB000000000 AS DateTime), 0, CAST(0x0000AAB000B034F7 AS DateTime), N'Admin', CAST(0x0000AAB000CF0066 AS DateTime), N'Admin', 1)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1011, 23, 1, 13, NULL, CAST(0x0000AAA900000000 AS DateTime), 1, CAST(0x0000AAB900BA8255 AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1013, 23, 0, 1, NULL, CAST(0x0000AA7E00000000 AS DateTime), 1, CAST(0x0000AAB900BF0A6C AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1014, 18, 1, 1, NULL, CAST(0x0000AAA900000000 AS DateTime), 0, CAST(0x0000AAB901218696 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1015, 23, 1, 12, NULL, CAST(0x0000AABC00000000 AS DateTime), 1, CAST(0x0000AABC00B6247D AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1017, 23, 1, 17, NULL, CAST(0x0000AABC00000000 AS DateTime), 1, CAST(0x0000AABC00C831BD AS DateTime), N'Admin', NULL, NULL, 0)
INSERT [dbo].[AssignRopeToWinch] ([Id], [RopeId], [Outboard], [WinchId], [AssignedLocation], [AssignedDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1018, 23, 1, 1, NULL, CAST(0x0000AABC00000000 AS DateTime), 1, CAST(0x0000AABC00C8682B AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AssignRopeToWinch] OFF
/****** Object:  Table [dbo].[AssignLooseEquipToWinch]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignLooseEquipToWinch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseETypeId] [int] NULL,
	[AssignWinchId] [int] NULL,
	[AssignedLocation] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_AssignLooseEquipToWinch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AssignLooseEquipToWinch] ON
INSERT [dbo].[AssignLooseEquipToWinch] ([Id], [LooseETypeId], [AssignWinchId], [AssignedLocation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 4, 1003, NULL, CAST(0x0000AA9400C0E08A AS DateTime), N'Admin', CAST(0x0000AA9400D1CCB1 AS DateTime), NULL, 1)
INSERT [dbo].[AssignLooseEquipToWinch] ([Id], [LooseETypeId], [AssignWinchId], [AssignedLocation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 3, 4, NULL, CAST(0x0000AA9400C85B08 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[AssignLooseEquipToWinch] ([Id], [LooseETypeId], [AssignWinchId], [AssignedLocation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 5, 1002, NULL, CAST(0x0000AA9700C25754 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[AssignLooseEquipToWinch] ([Id], [LooseETypeId], [AssignWinchId], [AssignedLocation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 2, 9, NULL, CAST(0x0000AAAE00FCCAE5 AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AssignLooseEquipToWinch] OFF
/****** Object:  Table [dbo].[LooseEDisposal]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LooseEDisposal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseETypeId] [int] NULL,
	[LooseECertiNo] [nvarchar](100) NULL,
	[DisposalPortName] [nvarchar](100) NULL,
	[ReceptionFacilityName] [nvarchar](100) NULL,
	[DisposalDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_LooseEDisposal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LooseEDisposal] ON
INSERT [dbo].[LooseEDisposal] ([Id], [LooseETypeId], [LooseECertiNo], [DisposalPortName], [ReceptionFacilityName], [DisposalDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (1, 2, N'Cesdfsf554645', N'aaaaaaaaaa', N'Wewewewewewewewewe', CAST(0x0000AAA700000000 AS DateTime), CAST(0x0000AAA9011357BB AS DateTime), N'Admin', 1)
INSERT [dbo].[LooseEDisposal] ([Id], [LooseETypeId], [LooseECertiNo], [DisposalPortName], [ReceptionFacilityName], [DisposalDate], [CreatedDate], [CreatedBy], [IsActive]) VALUES (13, 2, N'Fghfh', N'kkkkkkkk', N'llllll', CAST(0x0000AAB800000000 AS DateTime), CAST(0x0000AAA901130E30 AS DateTime), N'Admin', 1)
SET IDENTITY_INSERT [dbo].[LooseEDisposal] OFF
/****** Object:  Table [dbo].[LooseEDamageRecord]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LooseEDamageRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseETypeId] [int] NULL,
	[CertificateNumber] [nvarchar](100) NULL,
	[DamageObserved] [nvarchar](50) NULL,
	[MooringOperation] [nvarchar](100) NULL,
	[NotificationId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_LooseEDamageRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LooseEDamageRecord] ON
INSERT [dbo].[LooseEDamageRecord] ([Id], [LooseETypeId], [CertificateNumber], [DamageObserved], [MooringOperation], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 2, N'Ce8787-4545', N'Mooring Operation', N'All Operation', NULL, CAST(0x0000AA9500BB3F42 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[LooseEDamageRecord] ([Id], [LooseETypeId], [CertificateNumber], [DamageObserved], [MooringOperation], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 2, N'Fghfh', N'Inspection', NULL, NULL, CAST(0x0000AA9500C61D54 AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[LooseEDamageRecord] OFF
/****** Object:  Table [dbo].[Logbook]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logbook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NOT NULL,
	[EventName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[CrewGroup] [nvarchar](max) NULL,
	[CrewGroup1] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[DateFrom1] [varchar](max) NULL,
	[DateTo1] [varchar](max) NULL,
	[TimeFrom] [varchar](max) NULL,
	[TimeTo] [varchar](max) NULL,
	[IDLFrom] [varchar](max) NULL,
	[IDLTo] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.Logbook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JoiningShackle]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoiningShackle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseETypeId] [int] NULL,
	[IdentificationNumber] [nvarchar](50) NULL,
	[ManufactureName] [nvarchar](100) NULL,
	[MBL] [decimal](18, 3) NULL,
	[Type] [nvarchar](20) NULL,
	[CertificateNumber] [nvarchar](50) NULL,
	[DateReceived] [datetime] NULL,
	[DateInstalled] [datetime] NULL,
	[OutofServiceDate] [datetime] NULL,
	[ReasonOutofService] [nvarchar](50) NULL,
	[OtherReason] [nvarchar](100) NULL,
	[DamagedObserved] [nvarchar](50) NULL,
	[MooringOperation] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_JoiningShackle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[JoiningShackle] ON
INSERT [dbo].[JoiningShackle] ([Id], [LooseETypeId], [IdentificationNumber], [ManufactureName], [MBL], [Type], [CertificateNumber], [DateReceived], [DateInstalled], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamagedObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 1, N'Abchj1281910000', N'Testttttttt', CAST(75.000 AS Decimal(18, 3)), N'Tonsberg', N'Cergfdfdghf', CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9500000000 AS DateTime), N'Damaged', NULL, N'Mooring Operation', N'All Operation', CAST(0x0000AA9200F95750 AS DateTime), N'Admin', CAST(0x0000AA9501294EC6 AS DateTime), N'Admin', 1)
INSERT [dbo].[JoiningShackle] ([Id], [LooseETypeId], [IdentificationNumber], [ManufactureName], [MBL], [Type], [CertificateNumber], [DateReceived], [DateInstalled], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamagedObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 1, N'Sdfsfad', N'Gfsadfg', CAST(5.000 AS Decimal(18, 3)), N'Tonsberg', N'Cergfdfdghf', CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AA9400000000 AS DateTime), N'Damaged', NULL, N'Mooring Operation', N'All Operation', CAST(0x0000AA94012BFA5A AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[JoiningShackle] OFF
/****** Object:  Table [dbo].[International]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[International](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RDate] [datetime] NOT NULL,
	[RET_ADV] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.International] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InfoLog]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[dt] [datetime] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[ModuleName] [nvarchar](max) NULL,
	[ActionName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Editdate] [datetime] NULL,
 CONSTRAINT [PK_dbo.InfoLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[HolidayDate] [datetime] NOT NULL,
	[Gid] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Holidays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoliDayGroupName]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoliDayGroupName](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.HoliDayGroupName] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPlanner]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPlanner](
	[PlanID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](max) NULL,
	[Rank] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Place] [nvarchar](max) NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NOT NULL,
	[NonConfirmity] [nvarchar](max) NULL,
	[WorkingHRS] [decimal](18, 2) NOT NULL,
	[HRS] [nvarchar](max) NULL,
	[Colors] [nvarchar](max) NULL,
	[Opa] [bit] NOT NULL,
	[Col1] [nvarchar](max) NULL,
	[Col2] [nvarchar](max) NULL,
	[Col3] [nvarchar](max) NULL,
	[Col4] [nvarchar](max) NULL,
	[Col5] [nvarchar](max) NULL,
	[Col6] [nvarchar](max) NULL,
	[Col7] [nvarchar](max) NULL,
	[Col8] [nvarchar](max) NULL,
	[Col9] [nvarchar](max) NULL,
	[Col10] [nvarchar](max) NULL,
	[Col11] [nvarchar](max) NULL,
	[Col12] [nvarchar](max) NULL,
	[Col13] [nvarchar](max) NULL,
	[Col14] [nvarchar](max) NULL,
	[Col15] [nvarchar](max) NULL,
	[Col16] [nvarchar](max) NULL,
	[Col17] [nvarchar](max) NULL,
	[Col18] [nvarchar](max) NULL,
	[Col19] [nvarchar](max) NULL,
	[Col20] [nvarchar](max) NULL,
	[Col21] [nvarchar](max) NULL,
	[Col22] [nvarchar](max) NULL,
	[Col23] [nvarchar](max) NULL,
	[Col24] [nvarchar](max) NULL,
	[Col25] [nvarchar](max) NULL,
	[Col26] [nvarchar](max) NULL,
	[Col27] [nvarchar](max) NULL,
	[Col28] [nvarchar](max) NULL,
	[Col29] [nvarchar](max) NULL,
	[Col30] [nvarchar](max) NULL,
	[Col31] [nvarchar](max) NULL,
	[Col32] [nvarchar](max) NULL,
	[Col33] [nvarchar](max) NULL,
	[Col34] [nvarchar](max) NULL,
	[Col35] [nvarchar](max) NULL,
	[Col36] [nvarchar](max) NULL,
	[Col37] [nvarchar](max) NULL,
	[Col38] [nvarchar](max) NULL,
	[Col39] [nvarchar](max) NULL,
	[Col40] [nvarchar](max) NULL,
	[Col41] [nvarchar](max) NULL,
	[Col42] [nvarchar](max) NULL,
	[Col43] [nvarchar](max) NULL,
	[Col44] [nvarchar](max) NULL,
	[Col45] [nvarchar](max) NULL,
	[Col46] [nvarchar](max) NULL,
	[Col47] [nvarchar](max) NULL,
	[Col48] [nvarchar](max) NULL,
	[Col49] [nvarchar](max) NULL,
	[Col50] [nvarchar](max) NULL,
	[Col51] [nvarchar](max) NULL,
	[Col52] [nvarchar](max) NULL,
	[Col53] [nvarchar](max) NULL,
	[Col54] [nvarchar](max) NULL,
	[Col55] [nvarchar](max) NULL,
	[Col56] [nvarchar](max) NULL,
	[Col57] [nvarchar](max) NULL,
	[Col58] [nvarchar](max) NULL,
	[Col59] [nvarchar](max) NULL,
	[Col60] [nvarchar](max) NULL,
	[Col61] [nvarchar](max) NULL,
	[Col62] [nvarchar](max) NULL,
	[Col63] [nvarchar](max) NULL,
	[Col64] [nvarchar](max) NULL,
	[Col65] [nvarchar](max) NULL,
	[Col66] [nvarchar](max) NULL,
	[Col67] [nvarchar](max) NULL,
	[Col68] [nvarchar](max) NULL,
	[Col69] [nvarchar](max) NULL,
	[Col70] [nvarchar](max) NULL,
	[Col71] [nvarchar](max) NULL,
	[Col72] [nvarchar](max) NULL,
	[Col73] [nvarchar](max) NULL,
	[Col74] [nvarchar](max) NULL,
	[Col75] [nvarchar](max) NULL,
	[Col76] [nvarchar](max) NULL,
	[Col77] [nvarchar](max) NULL,
	[Col78] [nvarchar](max) NULL,
	[Col79] [nvarchar](max) NULL,
	[Col80] [nvarchar](max) NULL,
	[Col81] [nvarchar](max) NULL,
	[Col82] [nvarchar](max) NULL,
	[Col83] [nvarchar](max) NULL,
	[Col84] [nvarchar](max) NULL,
	[Col85] [nvarchar](max) NULL,
	[Col86] [nvarchar](max) NULL,
	[Col87] [nvarchar](max) NULL,
	[Col88] [nvarchar](max) NULL,
	[Col89] [nvarchar](max) NULL,
	[Col90] [nvarchar](max) NULL,
	[Col91] [nvarchar](max) NULL,
	[Col92] [nvarchar](max) NULL,
	[Col93] [nvarchar](max) NULL,
	[Col94] [nvarchar](max) NULL,
	[Col95] [nvarchar](max) NULL,
	[Col96] [nvarchar](max) NULL,
	[GroupID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.GroupPlanner] PRIMARY KEY CLUSTERED 
(
	[PlanID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MOUsedWinchTbl]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MOUsedWinchTbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OperationID] [int] NULL,
	[GridID] [nvarchar](50) NULL,
	[OPDateFrom] [datetime] NULL,
	[OPDateTo] [datetime] NULL,
	[WinchId] [int] NULL,
	[RopeId] [int] NULL,
	[RunningHours] [int] NULL,
	[RopeTail] [int] NULL,
	[NotificationId] [int] NULL,
 CONSTRAINT [PK_MOUsedWinchTbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MOUsedWinchTbl] ON
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1, 1027, N'Grid0', CAST(0x0000AAAA00F91D70 AS DateTime), CAST(0x0000AAB100F91D70 AS DateTime), 4, 22, 168, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (2, 1027, N'Grid1', CAST(0x0000AAAA0022E1B0 AS DateTime), CAST(0x0000AAAA00F91D70 AS DateTime), 22, 16, 13, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (3, 1027, N'Grid1', CAST(0x0000AAAA0022E1B0 AS DateTime), CAST(0x0000AAAA00F91D70 AS DateTime), 8, 9, 13, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1018, 2034, N'Grid0', CAST(0x0000AAB400335C70 AS DateTime), CAST(0x0000AAF000213BD0 AS DateTime), 8, 9, 1439, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1019, 2034, N'Grid0', CAST(0x0000AAB400335C70 AS DateTime), CAST(0x0000AAF000213BD0 AS DateTime), 4, 22, 1439, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1020, 2034, N'Grid1', CAST(0x0000AAB400335C70 AS DateTime), CAST(0x0000AAB400213BD0 AS DateTime), 22, 16, -1, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1021, 2035, N'Grid0', CAST(0x0000AAB50021C870 AS DateTime), CAST(0x0000AAB800317040 AS DateTime), 8, 9, 73, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1022, 2035, N'Grid0', CAST(0x0000AAB50021C870 AS DateTime), CAST(0x0000AAB800317040 AS DateTime), 4, 22, 73, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1023, 2035, N'Grid1', CAST(0x0000AAB50021C870 AS DateTime), CAST(0x0000AAB500317040 AS DateTime), 22, 16, 1, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1024, 2036, N'Grid0', CAST(0x0000AACB0053C550 AS DateTime), CAST(0x0000AB030053C550 AS DateTime), 22, 16, 1344, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1025, 2036, N'Grid0', CAST(0x0000AACB0053C550 AS DateTime), CAST(0x0000AB030053C550 AS DateTime), 1, 18, 1344, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1026, 2036, N'Grid1', CAST(0x0000AACB00225510 AS DateTime), CAST(0x0000AACB0053C550 AS DateTime), 4, 22, 3, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1033, 2038, N'Grid0', CAST(0x0000AABD00114DB0 AS DateTime), CAST(0x0000AAD400114DB0 AS DateTime), 22, 16, 552, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1034, 2038, N'Grid0', CAST(0x0000AABD00114DB0 AS DateTime), CAST(0x0000AAD400114DB0 AS DateTime), 4, 22, 552, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1035, 2038, N'Grid1', CAST(0x0000AABD0021C870 AS DateTime), CAST(0x0000AABD00114DB0 AS DateTime), 1, 18, -1, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1036, 2038, N'Grid1', CAST(0x0000AABD0021C870 AS DateTime), CAST(0x0000AABD00114DB0 AS DateTime), 1, 23, -1, 1, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1037, 2039, N'Grid0', CAST(0x0000AAC50031FCE0 AS DateTime), CAST(0x0000AB28005338B0 AS DateTime), 22, 16, 2378, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1038, 2039, N'Grid0', CAST(0x0000AAC50031FCE0 AS DateTime), CAST(0x0000AB28005338B0 AS DateTime), 1, 18, 2378, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1039, 2039, N'Grid1', CAST(0x0000AAC50031FCE0 AS DateTime), CAST(0x0000AAC5005338B0 AS DateTime), 4, 22, 2, 0, NULL)
INSERT [dbo].[MOUsedWinchTbl] ([Id], [OperationID], [GridID], [OPDateFrom], [OPDateTo], [WinchId], [RopeId], [RunningHours], [RopeTail], [NotificationId]) VALUES (1040, 2039, N'Grid0', CAST(0x0000AAC50031FCE0 AS DateTime), CAST(0x0000AB28005338B0 AS DateTime), 1, 23, 2378, 1, NULL)
SET IDENTITY_INSERT [dbo].[MOUsedWinchTbl] OFF
/****** Object:  Table [dbo].[MOperationBirthDetail]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MOperationBirthDetail](
	[OPId] [int] IDENTITY(1,1) NOT NULL,
	[PortName] [nvarchar](200) NULL,
	[FastDatetime] [datetime] NULL,
	[CastDatetime] [datetime] NULL,
	[BirthName] [nvarchar](200) NULL,
	[BirthType] [nvarchar](150) NULL,
	[MooringType] [nvarchar](150) NULL,
	[DraftArrivalFWD] [decimal](18, 1) NULL,
	[DraftArrivalAFT] [decimal](18, 1) NULL,
	[DraftDepartureFWD] [decimal](18, 1) NULL,
	[DraftDepartureAFT] [decimal](18, 1) NULL,
	[DepthAtBerth] [decimal](18, 1) NULL,
	[BerthSide] [nvarchar](150) NULL,
	[VesselCondition] [nvarchar](150) NULL,
	[ShipAccess] [nvarchar](150) NULL,
	[RangOfTide] [decimal](18, 1) NULL,
	[WindDirection] [nvarchar](150) NULL,
	[WindSpeed] [int] NULL,
	[AnySquall] [nvarchar](150) NULL,
	[CurrentSpeed] [decimal](18, 1) NULL,
	[Berth_exposed_SeaSwell] [nvarchar](50) NULL,
	[SurgingObserved] [nvarchar](50) NULL,
	[Any_Affect_Passing_Traffic] [nvarchar](50) NULL,
	[Ship_was_continuously_contact_with_fender] [nvarchar](50) NULL,
	[Any_Rope_Damaged] [nvarchar](50) NULL,
 CONSTRAINT [PK_MOperationBirthDetail] PRIMARY KEY CLUSTERED 
(
	[OPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MOperationBirthDetail] ON
INSERT [dbo].[MOperationBirthDetail] ([OPId], [PortName], [FastDatetime], [CastDatetime], [BirthName], [BirthType], [MooringType], [DraftArrivalFWD], [DraftArrivalAFT], [DraftDepartureFWD], [DraftDepartureAFT], [DepthAtBerth], [BerthSide], [VesselCondition], [ShipAccess], [RangOfTide], [WindDirection], [WindSpeed], [AnySquall], [CurrentSpeed], [Berth_exposed_SeaSwell], [SurgingObserved], [Any_Affect_Passing_Traffic], [Ship_was_continuously_contact_with_fender], [Any_Rope_Damaged]) VALUES (2035, N'Amit', CAST(0x0000AAB800000000 AS DateTime), CAST(0x0000AAB800000000 AS DateTime), N'test', N'Dolphin', N'Sheltered berth', CAST(4.0 AS Decimal(18, 1)), CAST(4.0 AS Decimal(18, 1)), CAST(4.0 AS Decimal(18, 1)), CAST(4.0 AS Decimal(18, 1)), CAST(4.0 AS Decimal(18, 1)), N'Port', N'Discharging', N'Shore Ladder', CAST(4.0 AS Decimal(18, 1)), N'2', 4, N'No', CAST(4.0 AS Decimal(18, 1)), N'Yes', N'Yes', N'No', N'Yes', N'Yes')
INSERT [dbo].[MOperationBirthDetail] ([OPId], [PortName], [FastDatetime], [CastDatetime], [BirthName], [BirthType], [MooringType], [DraftArrivalFWD], [DraftArrivalAFT], [DraftDepartureFWD], [DraftDepartureAFT], [DepthAtBerth], [BerthSide], [VesselCondition], [ShipAccess], [RangOfTide], [WindDirection], [WindSpeed], [AnySquall], [CurrentSpeed], [Berth_exposed_SeaSwell], [SurgingObserved], [Any_Affect_Passing_Traffic], [Ship_was_continuously_contact_with_fender], [Any_Rope_Damaged]) VALUES (2036, N'Rana', CAST(0x0000AACB0053C550 AS DateTime), CAST(0x0000AB030053C550 AS DateTime), N'trrrrrrrr', N'Straight', N'Open berth', CAST(5.0 AS Decimal(18, 1)), CAST(5.0 AS Decimal(18, 1)), CAST(6.0 AS Decimal(18, 1)), CAST(6.0 AS Decimal(18, 1)), CAST(6.0 AS Decimal(18, 1)), N'Stbd', N'Discharging', N'MOT', CAST(8.0 AS Decimal(18, 1)), N'6', 8, N'No', CAST(77.0 AS Decimal(18, 1)), N'No', N'Yes', N'No', N'Yes', N'Yes')
INSERT [dbo].[MOperationBirthDetail] ([OPId], [PortName], [FastDatetime], [CastDatetime], [BirthName], [BirthType], [MooringType], [DraftArrivalFWD], [DraftArrivalAFT], [DraftDepartureFWD], [DraftDepartureAFT], [DepthAtBerth], [BerthSide], [VesselCondition], [ShipAccess], [RangOfTide], [WindDirection], [WindSpeed], [AnySquall], [CurrentSpeed], [Berth_exposed_SeaSwell], [SurgingObserved], [Any_Affect_Passing_Traffic], [Ship_was_continuously_contact_with_fender], [Any_Rope_Damaged]) VALUES (2038, N'Test', CAST(0x0000AABD00114DB0 AS DateTime), CAST(0x0000AAD400114DB0 AS DateTime), N'5', N'Straight', N'Open berth', CAST(2.0 AS Decimal(18, 1)), CAST(2.0 AS Decimal(18, 1)), CAST(2.0 AS Decimal(18, 1)), CAST(2.0 AS Decimal(18, 1)), CAST(4.0 AS Decimal(18, 1)), N'Stbd', N'Discharging', N'MOT', CAST(3.0 AS Decimal(18, 1)), N'1', 3, N'No', CAST(3.0 AS Decimal(18, 1)), N'No', N'No', N'Yes', N'No', N'No')
INSERT [dbo].[MOperationBirthDetail] ([OPId], [PortName], [FastDatetime], [CastDatetime], [BirthName], [BirthType], [MooringType], [DraftArrivalFWD], [DraftArrivalAFT], [DraftDepartureFWD], [DraftDepartureAFT], [DepthAtBerth], [BerthSide], [VesselCondition], [ShipAccess], [RangOfTide], [WindDirection], [WindSpeed], [AnySquall], [CurrentSpeed], [Berth_exposed_SeaSwell], [SurgingObserved], [Any_Affect_Passing_Traffic], [Ship_was_continuously_contact_with_fender], [Any_Rope_Damaged]) VALUES (2039, N'Chennai', CAST(0x0000AAC50031FCE0 AS DateTime), CAST(0x0000AB28005338B0 AS DateTime), N'ff', N'Straight', N'Open berth', CAST(5.0 AS Decimal(18, 1)), CAST(5.0 AS Decimal(18, 1)), CAST(5.0 AS Decimal(18, 1)), CAST(5.0 AS Decimal(18, 1)), CAST(44.0 AS Decimal(18, 1)), N'Port', N'Discharging', N'MOT', CAST(5.0 AS Decimal(18, 1)), N'1', 5, N'No', CAST(4.0 AS Decimal(18, 1)), N'No', N'Yes', N'No', N'No', N'No')
SET IDENTITY_INSERT [dbo].[MOperationBirthDetail] OFF
/****** Object:  Table [dbo].[MooringWinchDetail]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MooringWinchDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignedNumber] [nvarchar](100) NULL,
	[Location] [nvarchar](100) NULL,
	[MBL] [decimal](18, 2) NULL,
	[SortingOrder] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MooringWinchDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MooringWinchDetail] ON
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'W1', N'Forecastle Port Outer', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B01240450 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'W2', N'Forecastle Port Inner', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B01241A9C AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'W3', N'Forecastle - Stbd Inner', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B01242DBD AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'W4', N'Forecastle - Stbd Outer', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B012454AA AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'M1', N'Main Deck - Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B01246AF7 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'M2', N'Main Deck - Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124758C AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'M3', N'Fwd Spring Line - Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124A882 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, N'M4', N'Fwd Spring Line - Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124B3D6 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, N'M5', N'Aft Spring Line - Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124C3CB AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, N'M6', N'Aft Spring Line - Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124CFA1 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, N'M7', N'Poop Deck - Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124EC7D AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, N'M8', N'Poop Deck - Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0124F4B2 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, N'Spare 1', N'Spare Rope Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B012538C0 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, N'Spare 2', N'Spare Rope Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B01254F1F AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, N'Spare 3', N'Spare Rope Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B012559F6 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, N'Spare 4', N'Spare Rope Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B01257AA0 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, N'Spare 5', N'Spare Rope Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B012584EE AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, N'Spare 6', N'Spare Rope Aft', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B012590FE AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, N'Spare 7', N'Spare Rope 7', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0125A2D4 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (20, N'Spare 8', N'Spare Rope Fwd', CAST(5.00 AS Decimal(18, 2)), NULL, CAST(0x0000AA9B0125DC7B AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringWinchDetail] ([Id], [AssignedNumber], [Location], [MBL], [SortingOrder], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, N'Spare 9', N'Spare Rope Fwd', CAST(10.00 AS Decimal(18, 2)), NULL, CAST(0x0000AAAF00FA6DA8 AS DateTime), N'Admin', CAST(0x0000AAAF00FA6DA8 AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[MooringWinchDetail] OFF
/****** Object:  Table [dbo].[MooringRopeType]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MooringRopeType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeType] [nvarchar](100) NULL,
 CONSTRAINT [PK_MooringRopeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MooringRopeType] ON
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (1, N'SteelWire')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (2, N'Polypropylene')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (3, N'Polyster')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (4, N'Nylon')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (5, N'Synthetic Fiber')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (6, N'Braided')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (7, N'Hemp')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (8, N'Sisal')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (9, N'Coir')
INSERT [dbo].[MooringRopeType] ([Id], [RopeType]) VALUES (10, N'Manila')
SET IDENTITY_INSERT [dbo].[MooringRopeType] OFF
/****** Object:  Table [dbo].[MooringRopeInspection]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MooringRopeInspection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InspectBy] [nvarchar](200) NULL,
	[InspectDate] [datetime] NULL,
	[RopeId] [int] NULL,
	[WinchId] [int] NULL,
	[ExternalRating_A] [int] NULL,
	[InternalRating_A] [int] NULL,
	[AverageRating_A] [int] NULL,
	[LengthOFAbrasion_A] [decimal](18, 3) NULL,
	[DistanceOutboard_A] [decimal](18, 3) NULL,
	[CutYarnCount_A] [decimal](18, 3) NULL,
	[LengthOFGlazing_A] [decimal](18, 3) NULL,
	[RopeTail] [int] NULL,
	[ExternalRating_B] [int] NULL,
	[InternalRating_B] [int] NULL,
	[AverageRating_B] [int] NULL,
	[LengthOFAbrasion_B] [decimal](18, 3) NULL,
	[DistanceOutboard_B] [decimal](18, 3) NULL,
	[CutYarnCount_B] [decimal](18, 3) NULL,
	[LengthOFGlazing_B] [decimal](18, 3) NULL,
	[Chafe_guard_condition] [nvarchar](100) NULL,
	[Twist] [int] NULL,
	[Photo1] [nvarchar](max) NULL,
	[Photo2] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[NotificationId] [int] NULL,
 CONSTRAINT [PK_MooringRopeInspection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MooringRopeInspection] ON
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (1, NULL, CAST(0x0000AAA900000000 AS DateTime), 9, 1, 2, 4, 3, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 1, 3, 2, CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), N'Not Accpetable', 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (2, NULL, CAST(0x0000AAA900000000 AS DateTime), 16, 20, 6, 7, 7, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 3, 4, 4, CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), N'Not Accpetable', 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (3, NULL, CAST(0x0000AAA900000000 AS DateTime), 23, 1, 2, 3, 3, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), 1, 0, 0, 0, NULL, NULL, NULL, NULL, N'Not Accpetable', 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (4, N'Admin', CAST(0x0000AAC600000000 AS DateTime), 16, 22, 7, 6, 7, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 2, 4, 3, CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), N'Satisfactory', 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (5, N'Admin', CAST(0x0000AAC600000000 AS DateTime), 22, 4, 1, 4, 3, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 7, 5, 6, CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), N'Acceptable', 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (6, N'Admin', CAST(0x0000AAC600000000 AS DateTime), 18, 1, 5, 5, 5, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 5, 5, 5, CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), CAST(2.000 AS Decimal(18, 3)), N'Acceptable', 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (7, N'Admin', CAST(0x0000AAC600000000 AS DateTime), 16, 22, 5, 6, 6, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 3, 5, 4, CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), N'Not Accpetable', 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (8, N'Admin', CAST(0x0000AAC600000000 AS DateTime), 22, 4, 2, 4, 3, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 3, 4, 4, CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), N'Acceptable', 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[MooringRopeInspection] ([Id], [InspectBy], [InspectDate], [RopeId], [WinchId], [ExternalRating_A], [InternalRating_A], [AverageRating_A], [LengthOFAbrasion_A], [DistanceOutboard_A], [CutYarnCount_A], [LengthOFGlazing_A], [RopeTail], [ExternalRating_B], [InternalRating_B], [AverageRating_B], [LengthOFAbrasion_B], [DistanceOutboard_B], [CutYarnCount_B], [LengthOFGlazing_B], [Chafe_guard_condition], [Twist], [Photo1], [Photo2], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [NotificationId]) VALUES (9, N'Admin', CAST(0x0000AAC600000000 AS DateTime), 18, 1, 5, 5, 5, CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), CAST(3.000 AS Decimal(18, 3)), 0, 2, 6, 4, CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), CAST(4.000 AS Decimal(18, 3)), N'Acceptable', 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[MooringRopeInspection] OFF
/****** Object:  Table [dbo].[MooringRopeDetail]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MooringRopeDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeTypeId] [int] NULL,
	[RopeConstruction] [nvarchar](100) NULL,
	[DiaMeter] [decimal](18, 2) NULL,
	[Length] [decimal](18, 2) NULL,
	[MBL] [decimal](18, 2) NULL,
	[LDBF] [decimal](18, 2) NULL,
	[WLL] [decimal](18, 2) NULL,
	[ManufacturerId] [int] NULL,
	[CertificateNumber] [nvarchar](50) NULL,
	[ReceivedDate] [datetime] NULL,
	[InstalledDate] [datetime] NULL,
	[RopeTagging] [nvarchar](50) NULL,
	[OutofServiceDate] [datetime] NULL,
	[ReasonOutofService] [nvarchar](100) NULL,
	[OtherReason] [nvarchar](100) NULL,
	[DamageObserved] [nvarchar](100) NULL,
	[IncidentReport] [nvarchar](20) NULL,
	[MooringOperation] [nvarchar](100) NULL,
	[CurrentRunningHours] [int] NULL,
	[MaxRunningHours] [int] NULL,
	[MaxMonthsAllowed] [int] NULL,
	[InspectionDueDate] [datetime] NULL,
	[RopeTail] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MooringRopeDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MooringRopeDetail] ON
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 3, N'Polyster', CAST(25.22 AS Decimal(18, 2)), CAST(234.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), 1, N'60499', CAST(0x0000AA1F00000000 AS DateTime), CAST(0x0000A74500000000 AS DateTime), N'Yes', CAST(0x0000AA9B00000000 AS DateTime), N'Completed Running Hours', NULL, NULL, NULL, NULL, 7950, NULL, NULL, NULL, 0, CAST(0x0000AA9B012D9D35 AS DateTime), N'Admin', CAST(0x0000AAB5012F6B04 AS DateTime), N'Admin', 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 3, N'25% Polyster & 75% P', CAST(52.00 AS Decimal(18, 2)), CAST(220.00 AS Decimal(18, 2)), CAST(53.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'91310', CAST(0x0000AA3E00000000 AS DateTime), CAST(0x0000A8D100000000 AS DateTime), N'Yes', CAST(0x0000AAB100000000 AS DateTime), NULL, NULL, N'Mooring Operation', NULL, N'Port Name', 4296, NULL, NULL, NULL, 0, CAST(0x0000AA9D00A55C8F AS DateTime), N'Admin', CAST(0x0000AABD00D03256 AS DateTime), N'Admin', 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 10, N'Fgfdgg', CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'Ghj676767', CAST(0x0000AAA900000000 AS DateTime), CAST(0x0000AAA900000000 AS DateTime), N'No', CAST(0x0000AAAF00000000 AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, CAST(0x0000AAAF00BD88BA AS DateTime), NULL, 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, 5, N'24 X 4 Dlk Fwdl Asdl', CAST(123.00 AS Decimal(18, 2)), CAST(1111.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), 1, N'652 63532323 Asda3 2', CAST(0x0000ACFF00000000 AS DateTime), CAST(0x0000A64400000000 AS DateTime), N'Yes', NULL, N'Damaged', NULL, N'Inspection', N'Yes', NULL, 4946, NULL, NULL, NULL, 0, CAST(0x0000AAAE0116FD06 AS DateTime), N'Admin', CAST(0x0000AABD00D03275 AS DateTime), N'Admin', 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, 6, N'Ffgfgfgfg', CAST(455.00 AS Decimal(18, 2)), CAST(454.00 AS Decimal(18, 2)), CAST(44.00 AS Decimal(18, 2)), CAST(44.80 AS Decimal(18, 2)), CAST(77.80 AS Decimal(18, 2)), 2, N'Grhre445454', CAST(0x0000AA9C00000000 AS DateTime), CAST(0x0000AAA700000000 AS DateTime), N'Yes', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(0x0000AAAF00BA79C5 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (20, 6, N'Dddddddd', CAST(55.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), 2, N'Ce-123456789', CAST(0x0000AAA700000000 AS DateTime), CAST(0x0000AAA700000000 AS DateTime), N'Yes', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(0x0000AAAF00FE6B49 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (21, 8, N'Hhh', CAST(77.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), 2, N'Ce-4545454', CAST(0x0000AAA000000000 AS DateTime), CAST(0x0000AAA100000000 AS DateTime), N'Yes', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(0x0000AAAF00FF1C8D AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, 6, N'Jjjjjj', CAST(77.00 AS Decimal(18, 2)), CAST(78.00 AS Decimal(18, 2)), CAST(89.00 AS Decimal(18, 2)), CAST(99.00 AS Decimal(18, 2)), CAST(99.00 AS Decimal(18, 2)), 2, N'Cer-789456', CAST(0x0000AAA000000000 AS DateTime), CAST(0x0000AA9F00000000 AS DateTime), N'Yes', NULL, N'Completed running hours', NULL, NULL, NULL, NULL, 8866, NULL, NULL, NULL, 0, CAST(0x0000AAAF01053DFF AS DateTime), N'Admin', CAST(0x0000AABD00D0328F AS DateTime), N'Admin', 1)
INSERT [dbo].[MooringRopeDetail] ([Id], [RopeTypeId], [RopeConstruction], [DiaMeter], [Length], [MBL], [LDBF], [WLL], [ManufacturerId], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [IncidentReport], [MooringOperation], [CurrentRunningHours], [MaxRunningHours], [MaxMonthsAllowed], [InspectionDueDate], [RopeTail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (23, 4, N'Sss', CAST(4.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'Ce-Test', CAST(0x0000AAA200000000 AS DateTime), CAST(0x0000AAB100000000 AS DateTime), N'Yes', CAST(0x0000AABB00000000 AS DateTime), N'Completed running hours', NULL, NULL, NULL, NULL, 3602, NULL, NULL, NULL, 1, CAST(0x0000AAB900B63A59 AS DateTime), N'Admin', CAST(0x0000AABD00D03458 AS DateTime), N'Admin', 1)
SET IDENTITY_INSERT [dbo].[MooringRopeDetail] OFF
/****** Object:  Table [dbo].[MooringLooseEquipInspection]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MooringLooseEquipInspection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InspectBy] [nvarchar](100) NULL,
	[InspectDate] [datetime] NULL,
	[LooseETypeId] [int] NULL,
	[Number] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[NotificationId] [int] NULL,
 CONSTRAINT [PK_MooringLooseEquipInspection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MooringLooseEquipInspection] ON
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (1, N'ssss', CAST(0x0000AAA300000000 AS DateTime), 2, N'Cesdfsf554645', N'Good', N'ddd', CAST(0x0000AAB20123AE05 AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (2, N'ssss', CAST(0x0000AAA300000000 AS DateTime), 2, N'Fghfh', N'Acceptable', N'dddddddd', CAST(0x0000AAB20123AE08 AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (3, N'ssss', CAST(0x0000AAA300000000 AS DateTime), 2, N'Fghfh', N'Good', N'ffffff', CAST(0x0000AAB20123AE11 AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (4, N'ssssddddddd', CAST(0x0000AAB700000000 AS DateTime), 1, N'Cergfdfdghf', N'Good', N'ggggggg', CAST(0x0000AAB20123C1F4 AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (5, N'ssssddddddd', CAST(0x0000AAB700000000 AS DateTime), 1, N'Cergfdfdghf', N'Excellent', N'jhnjjjjjjjjjjjjj', CAST(0x0000AAB20123C1F4 AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (6, N'rrrrrr', CAST(0x0000AAC700000000 AS DateTime), 1, N'Cergfdfdghf', N'Excellent', N'', CAST(0x0000AABD0110D0AC AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (7, N'rrrrrr', CAST(0x0000AAC700000000 AS DateTime), 1, N'Cergfdfdghf', N'Not Acceptable', N'', CAST(0x0000AABD0110DC47 AS DateTime), 1, NULL)
INSERT [dbo].[MooringLooseEquipInspection] ([Id], [InspectBy], [InspectDate], [LooseETypeId], [Number], [Condition], [Remarks], [CreatedDate], [IsActive], [NotificationId]) VALUES (8, N'rana', CAST(0x0000AACD00000000 AS DateTime), 5, N'Sdsf', N'Not Acceptable', N'ggggggggggggg', CAST(0x0000AABD011419C2 AS DateTime), 1, NULL)
SET IDENTITY_INSERT [dbo].[MooringLooseEquipInspection] OFF
/****** Object:  Table [dbo].[LooseEType]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LooseEType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseEquipmentType] [nvarchar](100) NULL,
 CONSTRAINT [PK_LooseEType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LooseEType] ON
INSERT [dbo].[LooseEType] ([Id], [LooseEquipmentType]) VALUES (1, N'Joining Shackle')
INSERT [dbo].[LooseEType] ([Id], [LooseEquipmentType]) VALUES (2, N'Rope Tail')
INSERT [dbo].[LooseEType] ([Id], [LooseEquipmentType]) VALUES (3, N'Messanger Rope')
INSERT [dbo].[LooseEType] ([Id], [LooseEquipmentType]) VALUES (4, N'Rope Stopper')
INSERT [dbo].[LooseEType] ([Id], [LooseEquipmentType]) VALUES (5, N'Chain Stopper')
INSERT [dbo].[LooseEType] ([Id], [LooseEquipmentType]) VALUES (1002, N'FireWire')
SET IDENTITY_INSERT [dbo].[LooseEType] OFF
/****** Object:  Table [dbo].[RopeEndtoEnd2]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeEndtoEnd2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[EndtoEndDoneDate] [datetime] NULL,
	[CurrentOutboadEndinUse] [bit] NULL,
	[WinchCrossShifting] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RopeEndtoEnd2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RopeEndtoEnd2] ON
INSERT [dbo].[RopeEndtoEnd2] ([Id], [RopeId], [EndtoEndDoneDate], [CurrentOutboadEndinUse], [WinchCrossShifting], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 9, CAST(0x0000AA9A00000000 AS DateTime), 1, NULL, CAST(0x0000AA9B0134A9E6 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeEndtoEnd2] ([Id], [RopeId], [EndtoEndDoneDate], [CurrentOutboadEndinUse], [WinchCrossShifting], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 16, CAST(0x0000AAA000000000 AS DateTime), 1, NULL, CAST(0x0000AAA000C44CBF AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeEndtoEnd2] ([Id], [RopeId], [EndtoEndDoneDate], [CurrentOutboadEndinUse], [WinchCrossShifting], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 16, CAST(0x0000AAA000000000 AS DateTime), 0, NULL, CAST(0x0000AAA000EF743D AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeEndtoEnd2] ([Id], [RopeId], [EndtoEndDoneDate], [CurrentOutboadEndinUse], [WinchCrossShifting], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 16, CAST(0x0000AAA000000000 AS DateTime), 1, NULL, CAST(0x0000AAA000F02BC4 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeEndtoEnd2] ([Id], [RopeId], [EndtoEndDoneDate], [CurrentOutboadEndinUse], [WinchCrossShifting], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 16, CAST(0x0000AAA000000000 AS DateTime), 0, NULL, CAST(0x0000AAA000F14075 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeEndtoEnd2] ([Id], [RopeId], [EndtoEndDoneDate], [CurrentOutboadEndinUse], [WinchCrossShifting], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1010, 22, CAST(0x0000AAB000000000 AS DateTime), 0, NULL, CAST(0x0000AAB000CF01C3 AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RopeEndtoEnd2] OFF
/****** Object:  Table [dbo].[RopeEndtoEnd]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeEndtoEnd](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CertificateNumber] [nvarchar](50) NULL,
	[AssignedWinch] [nvarchar](50) NULL,
	[AssignedLocation] [nvarchar](50) NULL,
	[EndtoEndDoneDate] [datetime] NULL,
	[CurrentOutboadEndinUse] [bit] NULL,
	[OutboardEndinUse] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RopeEndtoEnd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RopeDisposal]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeDisposal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[DisposalPortName] [nvarchar](100) NULL,
	[ReceptionFacilityName] [nvarchar](100) NULL,
	[DisposalDate] [datetime] NULL,
	[RopeTail] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RopeDisposal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RopeDisposal] ON
INSERT [dbo].[RopeDisposal] ([Id], [RopeId], [DisposalPortName], [ReceptionFacilityName], [DisposalDate], [RopeTail], [CreatedDate], [CreatedBy], [IsActive]) VALUES (2, 9, N'aaaaa', N'bbbbb', NULL, 0, NULL, NULL, 0)
INSERT [dbo].[RopeDisposal] ([Id], [RopeId], [DisposalPortName], [ReceptionFacilityName], [DisposalDate], [RopeTail], [CreatedDate], [CreatedBy], [IsActive]) VALUES (12, 16, N'Ssssss', N'Dfdfdfdfdfdf', CAST(0x0000AAB800000000 AS DateTime), 0, CAST(0x0000AAA800CD2EC6 AS DateTime), N'Admin', 1)
INSERT [dbo].[RopeDisposal] ([Id], [RopeId], [DisposalPortName], [ReceptionFacilityName], [DisposalDate], [RopeTail], [CreatedDate], [CreatedBy], [IsActive]) VALUES (13, 23, N'Testtttttt', N'Ssss', CAST(0x0000AAB000000000 AS DateTime), 1, CAST(0x0000AAB900F4F2F0 AS DateTime), N'Admin', 1)
SET IDENTITY_INSERT [dbo].[RopeDisposal] OFF
/****** Object:  Table [dbo].[RopeDamageRecord]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeDamageRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[DamageObserved] [nvarchar](50) NULL,
	[IncidentReport] [nvarchar](20) NULL,
	[DamageLocation] [nvarchar](50) NULL,
	[DamageReason] [nvarchar](50) NULL,
	[MOPId] [int] NULL,
	[RopeTail] [int] NULL,
	[NotificationId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IncidentActlion] [nvarchar](50) NULL,
 CONSTRAINT [PK_RopeDamageRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RopeDamageRecord] ON
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1, 4, N'Mooring Operation', N'Yes', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA8700EF9260 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (2, 4, N'Inspection', N'No', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA8700F36838 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (3, 2, N'Mooring Operation', N'No', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA8800D5A723 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (4, 2, N'Inspection', N'No', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA8800D87A2A AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1002, 6, N'Mooring Operation', N'Yes', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA8C00D46F23 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1003, 8, N'Mooring Operation', N'Yes', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA9500B4626A AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1004, 5, N'Mooring Operation', N'No', N'Outboard - B', N'During tightening', 6, 0, NULL, CAST(0x0000AA9B00F15F44 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1005, 6, N'Mooring Operation', N'No', N'Zone 1', N'During lowering', 7, 0, NULL, CAST(0x0000AA9B010B2BC5 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1006, 5, N'Mooring Operation', N'Yes', N'Zone 2', NULL, 10, 0, NULL, CAST(0x0000AA9B0113E48E AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1007, 9, N'Inspection', N'Yes', NULL, NULL, NULL, 0, NULL, CAST(0x0000AA9B0138F550 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1009, 16, N'Mooring Operation', N'Yes', N'Zone 1', N'Entagled in Jetty', 19, 0, NULL, CAST(0x0000AA9D010895D8 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1011, 22, N'Inspection', N'No', NULL, NULL, NULL, 0, NULL, CAST(0x0000AAB000F73322 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (1012, 22, NULL, N'No', NULL, NULL, NULL, 0, NULL, CAST(0x0000AAB001106A36 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (2010, 9, N'Mooring Operation', N'Yes', N'Outboard - B', NULL, 2035, 0, NULL, CAST(0x0000AAB5012F9274 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (2011, 23, N'Mooring Operation', N'No', NULL, NULL, NULL, 1, NULL, CAST(0x0000AAB900D2C7A9 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[RopeDamageRecord] ([Id], [RopeId], [DamageObserved], [IncidentReport], [DamageLocation], [DamageReason], [MOPId], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [IncidentActlion]) VALUES (2012, 23, N'Inspection', N'No', NULL, NULL, NULL, 1, NULL, CAST(0x0000AAB900DCE815 AS DateTime), N'Admin', NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[RopeDamageRecord] OFF
/****** Object:  Table [dbo].[RopeCropping]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeCropping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[CroppedDate] [datetime] NULL,
	[CroppedOutboardEnd] [nvarchar](20) NULL,
	[LengthofCroppedRope] [decimal](18, 2) NULL,
	[ReasonofCropping] [nvarchar](100) NULL,
	[RopeTail] [int] NULL,
	[NotificationId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RopeCropping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RopeCropping] ON
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 2, CAST(0x0000AA9D00000000 AS DateTime), N'B', CAST(4.00 AS Decimal(18, 2)), N'Cut Strands,Kinked,Deformation', 0, NULL, CAST(0x0000AA86013221AF AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 4, CAST(0x0000AA8600000000 AS DateTime), N'A', CAST(58.00 AS Decimal(18, 2)), N'Cut Strands,Kinked,Abrasion,Paiint Damage,Deformation', 0, NULL, CAST(0x0000AA860134574B AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1002, 9, CAST(0x0000AA9300000000 AS DateTime), N'A', CAST(5.00 AS Decimal(18, 2)), N'Abrasion', 0, NULL, CAST(0x0000AA9B01353035 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1006, 9, CAST(0x0000AAAE00000000 AS DateTime), N'B', CAST(23.00 AS Decimal(18, 2)), N'Kinked,Abrasion,Paiint Damage', 0, NULL, CAST(0x0000AAAE011FBD33 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2007, 9, CAST(0x0000AAB500000000 AS DateTime), N'A', CAST(45.00 AS Decimal(18, 2)), N'Deformation', 0, NULL, CAST(0x0000AAB501050430 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2008, 9, CAST(0x0000AAB500000000 AS DateTime), N'A', CAST(44.00 AS Decimal(18, 2)), N'Cut Strands', 0, NULL, CAST(0x0000AAB5010C61A6 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeCropping] ([Id], [RopeId], [CroppedDate], [CroppedOutboardEnd], [LengthofCroppedRope], [ReasonofCropping], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2009, 23, CAST(0x0000AAA800000000 AS DateTime), N'A', CAST(5.00 AS Decimal(18, 2)), N'Cut Strands', 1, NULL, CAST(0x0000AAB900CB687D AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RopeCropping] OFF
/****** Object:  Table [dbo].[RetardtblClass]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetardtblClass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateID] [nvarchar](max) NULL,
	[RDate] [datetime] NOT NULL,
	[RET_ADV] [nvarchar](max) NULL,
	[Times] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.RetardtblClass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportsMataData]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportsMataData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Company] [varchar](150) NULL,
	[ReportName] [varchar](250) NULL,
	[LOGO] [image] NULL,
	[Middle_RightImage] [image] NULL,
	[FooterImage] [image] NULL,
	[FooterLine] [varchar](150) NULL,
 CONSTRAINT [PK_ReportsMataData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OverTime]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OverTime](
	[cid] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[NrmlWHrs] [int] NOT NULL,
	[SatWHrs] [int] NOT NULL,
	[SunWhrs] [int] NOT NULL,
	[HolidayWHrs] [int] NOT NULL,
	[NrmlRates] [int] NOT NULL,
	[SatRates] [int] NOT NULL,
	[SunRates] [int] NOT NULL,
	[HolidayRates] [int] NOT NULL,
	[FixedOverTime] [int] NOT NULL,
	[HourlyRate] [decimal](18, 2) NOT NULL,
	[Currency] [nvarchar](max) NULL,
	[holidays] [nvarchar](max) NULL,
	[did] [int] NOT NULL,
 CONSTRAINT [PK_dbo.OverTime] PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutofserviceReason]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutofserviceReason](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Reason] [nvarchar](50) NULL,
 CONSTRAINT [PK_OutofserviceReason] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OutofserviceReason] ON
INSERT [dbo].[OutofserviceReason] ([Id], [Reason]) VALUES (1, N'Completed running hours')
INSERT [dbo].[OutofserviceReason] ([Id], [Reason]) VALUES (2, N'Damaged')
INSERT [dbo].[OutofserviceReason] ([Id], [Reason]) VALUES (3, N'Other')
SET IDENTITY_INSERT [dbo].[OutofserviceReason] OFF
/****** Object:  Table [dbo].[OPAStartStop]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OPAStartStop](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[OPAStart] [datetime] NOT NULL,
	[OPAStop] [datetime] NOT NULL,
	[startDateID] [nvarchar](max) NULL,
	[stopDateID] [nvarchar](max) NULL,
	[LongLimit] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.OPAStartStop] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OPAStartStop] ON
INSERT [dbo].[OPAStartStop] ([id], [OPAStart], [OPAStop], [startDateID], [stopDateID], [LongLimit]) VALUES (2, CAST(0x0000AA0400C5C100 AS DateTime), CAST(0x0000AA0400E6B680 AS DateTime), N'2', N'2', 0)
INSERT [dbo].[OPAStartStop] ([id], [OPAStart], [OPAStop], [startDateID], [stopDateID], [LongLimit]) VALUES (3, CAST(0x0000AA04010FE960 AS DateTime), CAST(0x0000AA0500DE7920 AS DateTime), N'2IDL', N'3', 0)
INSERT [dbo].[OPAStartStop] ([id], [OPAStart], [OPAStop], [startDateID], [stopDateID], [LongLimit]) VALUES (6, CAST(0x0000AA5A008C1360 AS DateTime), CAST(0x0000AA5A0130DEE0 AS DateTime), N'27', N'27', 0)
INSERT [dbo].[OPAStartStop] ([id], [OPAStart], [OPAStop], [startDateID], [stopDateID], [LongLimit]) VALUES (7, CAST(0x0000AA6000C5C100 AS DateTime), CAST(0x0001379900C5C100 AS DateTime), N'27', N'27', 1)
SET IDENTITY_INSERT [dbo].[OPAStartStop] OFF
/****** Object:  Table [dbo].[AdminLogin]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminLogin](
	[Aid] [int] IDENTITY(1,1) NOT NULL,
	[uname] [nvarchar](max) NULL,
	[pswd] [nvarchar](max) NULL,
	[LastLogin] [datetime] NULL,
	[Loginfo] [nvarchar](max) NULL,
	[productinfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AdminLogin] PRIMARY KEY CLUSTERED 
(
	[Aid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddGroup]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddGroup](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](max) NULL,
	[GroupMember] [nvarchar](max) NULL,
	[GRank] [nvarchar](max) NULL,
	[GFullName] [nvarchar](max) NULL,
	[GUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AddGroup] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Freeze]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Freeze](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FreezeDays] [int] NOT NULL,
	[FreezeStatus] [nvarchar](max) NULL,
	[ApplyDate] [datetime] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Freeze] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Freeze] ON
INSERT [dbo].[Freeze] ([Id], [FreezeDays], [FreezeStatus], [ApplyDate], [DateFrom], [DateTo]) VALUES (2, 30, N'Freeze', CAST(0x0000AAC700000000 AS DateTime), CAST(0x0000AAA900FA5DF6 AS DateTime), CAST(0x0000AAE500FA5DF6 AS DateTime))
SET IDENTITY_INSERT [dbo].[Freeze] OFF
/****** Object:  Table [dbo].[DocsPages]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocsPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[ContentPath] [nvarchar](max) NULL,
	[Mid] [int] NULL,
	[ShipId] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreateBy] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_DocsPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DocsPages] ON
INSERT [dbo].[DocsPages] ([Id], [Content], [ContentPath], [Mid], [ShipId], [CreatedDate], [ModifiedDate], [CreateBy], [ModifiedBy]) VALUES (1, N'<p>Freight transport is the physical process of transporting commodities and merchandise goods and cargo. The term shipping originally referred to transport by sea but in American English, it has been extended to refer to transport by land or air (International English: "carriage") as well. "Logistics", a term borrowed from the military environment, is also used in the same sense.<br><br>Shipment of cargo by trucks, directly from the shipper''s place to the destination, is known as a door-to-door shipment, or more formally as multi modal transport. Trucks and trains make deliveries to sea and airports where cargo is moved in bulk.<br></p>', NULL, 7, N'9669665', CAST(0x0000AAB600EEF39A AS DateTime), CAST(0x0000AAB6013D9AC4 AS DateTime), N'Admin', N'ADMIN')
INSERT [dbo].[DocsPages] ([Id], [Content], [ContentPath], [Mid], [ShipId], [CreatedDate], [ModifiedDate], [CreateBy], [ModifiedBy]) VALUES (2, N'<p>Shipment of cargo by trucks, directly from the shipper''s place to the 
destination, is known as a door-to-door shipment, or more formally as 
multi modal transport. Trucks and trains make deliveries to sea and 
airports where cargo is moved in bulk.</p>', NULL, 9, N'9669665', CAST(0x0000AAB6010778EC AS DateTime), CAST(0x0000AAB6013D9AC4 AS DateTime), N'Admin', N'ADMIN')
SET IDENTITY_INSERT [dbo].[DocsPages] OFF
/****** Object:  Table [dbo].[RuleTable]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RuleTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RuleNames] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[StatusOPA90] [nvarchar](max) NULL,
	[OneHour] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.RuleTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RuleTable] ON
INSERT [dbo].[RuleTable] ([ID], [RuleNames], [Status], [StatusOPA90], [OneHour]) VALUES (1, N'Manila', 1, N'OPA90option1', 1)
SET IDENTITY_INSERT [dbo].[RuleTable] OFF
/****** Object:  Table [dbo].[RuffCode]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RuffCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[keyno] [nvarchar](max) NULL,
	[keycode] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.RuffCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RuffCode] ON
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (1, N'0cENGo4LS6M=', N'VsOqrUW+Q7nambsuIjtrguM91BEuNL2YeOjmcB+6iYY=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (2, N'EXn/WQzN3W8=', N'pagQcAr88+jr+1Y4DfxOutyx9UdO0j+9tnyhMM5T1Tg=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (3, N'ewnk52MpDHY=', N'KeVmxyxAEHGagfsr72/rhTgW4YU0C2IN/kLEPNau9J8=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (4, N'0M2Bg1SZ9/A=', N'v4eczk+plq/3ar5XzdWLQgGD0soUf3E8kwYRrkRS0wM=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (5, N'6ALJZuuZn9g=', N'Jj5kXwEpEEUVUPUkSL8FkGIijUYoL/ueI19g0L09x1k=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (6, N'ut4XHi+84OI=', N'lICHNoNthyCzANu5cB+kIeGt1NJjAgj8zEQ+MJyl3yA=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (7, N'5cL0pOm2Kw0=', N'ApPytTF991fI+Z+8WzkxtLVnSThyiOjfQ8lhLAkRLzI=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (8, N'c17T9RuKGbw=', N'QNXPHhnPeMVkRoRUMsJYIX/e2pq2pIdKpn9e+WbZoJ8=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (9, N'06U1lSiTO3Y=', N'Cq6selrAN1e8sADYUc1typQvIBv6DehHecM4E469CcU=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (10, N'/HuZWtunQMk=', N'zf3UYmAb539WRGfKGjS3ukbWNNCnnh4uELuppFsVMck=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (11, N'ZAhS/KhZEVA=', N'pegR9Huyxl8sxgkIlAi5j25qHaOx/L/oqX/EeBidK24=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (12, N'dCQ45Pu0beY=', N'aYPj/vtArkFqs8/dY/xU3tvbTrEV84TsSj1jpL6kdzU=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (13, N'O2L4IwKTq+Y=', N'C6uPA0wmprbQ9GhiwfP3yD9Jn9M+Ho7nD7RnStn3Wqs=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (14, N'uq9uCpb4fI0=', N'PvnnWCPDbJ9LOVspdMWTXXF2P1dn8mgX8s53Htkw3iM=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (15, N'ZkGx8SCGZMY=', N'JlTnwe0lWFRIVV/buHZd9/w23eQiQ82eZgMWAyCL49o=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (16, N'iBe89FYCVuI=', N'GdH6GwSG0Ub3i4/iCmDPWy/Ssm2i/qqiyGrktSPQWQg=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (17, N'EB22cJqpZgw=', N'snYR4stVn0MGx/+vyD5EKyRdgkkwGdLkQfRjfTwxFOQ=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (18, N'IvT8l1VuXXg=', N'yBEygSYG5SA/VcAR1c3j+wS50PVmwAx+mr3sC2ur2bI=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (19, N'4opvZWibRbo=', N'h2InuxCiQbSuTCXYnvtZ72Ien/Ad/tuzpwMBdts2Njo=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (20, N'ttfper3IoQE=', N'ViNFgzHjWVITK4nwekoTcABRyOh7xctCX/a5VkmUz+0=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (21, N'AXr5nIhAGWI=', N'3nWgr0h0Fbm5gOFtnupRd6M6qRrUdn1KQX0Io1HY5Nc=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (22, N'yX44qQDg71k=', N'/vgmhJ8lGNxCiXINk9Q62j7zDDI5pKpIqsHUSUi/lhM=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (23, N'UqLCdhE51Cs=', N'WWNzzGR7iiez/0yw2OFDrpPkjIH5JHjQOJrbxu2tlwM=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (24, N'g4tl+M4CbXU=', N'HB1z5lXjht5UzmZ3+yvCflQdwmUgb/P4CzVAdvuYNO8=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (25, N'zdZhdu+EUHo=', N'2SQhgY1eEByB0SI43BQhCmPTU2YNNSvpNpsAq0BCMBc=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (26, N'7kWZotIspmU=', N'5ET3VQdONfjhqG9kRlpQFYcDREUTBRl9njR2qjlH5Qs=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (27, N'nlkz6/6cwhE=', N'S9or2qSAE2HvhU5Z3qF+8x32nAzT7lRt7rKC8UYQCuI=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (28, N'sEVrVDuL2Fo=', N'E+fNPIM/DLFY4F/u+8y4PWZlZgCoPI77r0n4PyAorGU=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (29, N'ultiRRfpWl8=', N'CQk7EmJuSOZbQXTc6wThE55wO6tbQ4NdiSa9+aQlrq8=')
INSERT [dbo].[RuffCode] ([Id], [keyno], [keycode]) VALUES (30, N'2ib4fD9zSzc=', N'GRkOcSYX3nNf4p6C4t9VpOAettcbReh+8N6l7ORCTOg=')
SET IDENTITY_INSERT [dbo].[RuffCode] OFF
/****** Object:  Table [dbo].[RopeTail]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeTail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LooseETypeId] [int] NULL,
	[ManufactureName] [nvarchar](100) NULL,
	[RopeConstruction] [nvarchar](100) NULL,
	[Diameter] [nvarchar](50) NULL,
	[Length] [decimal](18, 2) NULL,
	[MBL] [decimal](18, 2) NULL,
	[LDBF] [decimal](18, 2) NULL,
	[WLL] [decimal](18, 2) NULL,
	[MaxRunHours] [int] NULL,
	[MaxYearServiceMonth] [int] NULL,
	[CertificateNumber] [nvarchar](50) NULL,
	[ReceivedDate] [datetime] NULL,
	[InstalledDate] [datetime] NULL,
	[RopeTagging] [nvarchar](50) NULL,
	[OutofServiceDate] [datetime] NULL,
	[ReasonOutofService] [nvarchar](50) NULL,
	[OtherReason] [nvarchar](100) NULL,
	[DamageObserved] [nvarchar](50) NULL,
	[MooringOperation] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RopeTail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RopeTail] ON
INSERT [dbo].[RopeTail] ([Id], [LooseETypeId], [ManufactureName], [RopeConstruction], [Diameter], [Length], [MBL], [LDBF], [WLL], [MaxRunHours], [MaxYearServiceMonth], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 2, N'Testttt', N'Nylon', N'Hemp', CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(56.00 AS Decimal(18, 2)), CAST(56.00 AS Decimal(18, 2)), 45, 54, N'Cesdfsf554645', CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime), N'Dfgdsagfdsgfdsa', CAST(0x0000AA9200000000 AS DateTime), N'Damaged', NULL, N'Mooring Operation', N'Inspection', CAST(0x0000AA92011A11D5 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeTail] ([Id], [LooseETypeId], [ManufactureName], [RopeConstruction], [Diameter], [Length], [MBL], [LDBF], [WLL], [MaxRunHours], [MaxYearServiceMonth], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 2, N'Fhfdghfdh', N'Polypropylene', N'Manila', CAST(4.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 44, 66, N'Fghfh', CAST(0x0000AA9000000000 AS DateTime), CAST(0x0000AA9300000000 AS DateTime), N'Fghfghfh', CAST(0x0000AA9300000000 AS DateTime), N'Other', NULL, NULL, NULL, CAST(0x0000AA9300D72DE2 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeTail] ([Id], [LooseETypeId], [ManufactureName], [RopeConstruction], [Diameter], [Length], [MBL], [LDBF], [WLL], [MaxRunHours], [MaxYearServiceMonth], [CertificateNumber], [ReceivedDate], [InstalledDate], [RopeTagging], [OutofServiceDate], [ReasonOutofService], [OtherReason], [DamageObserved], [MooringOperation], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 2, N'Testt3', N'Braided', N'Sisal', CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 65, 56, N'Fghfh', CAST(0x0000AA9300000000 AS DateTime), CAST(0x0000AA9300000000 AS DateTime), N'Dfghfdgh', CAST(0x0000AA9300000000 AS DateTime), N'Damaged', NULL, N'Mooring Operation', N'Inspection', CAST(0x0000AA93010FAF91 AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RopeTail] OFF
/****** Object:  Table [dbo].[RopeSplicingRecord]    Script Date: 09/13/2019 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RopeSplicingRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RopeId] [int] NULL,
	[SplicingDoneDate] [datetime] NULL,
	[SplicingMethod] [nvarchar](100) NULL,
	[SplicingDoneBy] [nvarchar](100) NULL,
	[LengthofCroppedRope] [decimal](18, 2) NULL,
	[RopeTail] [int] NULL,
	[NotificationId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RopeSplicingRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RopeSplicingRecord] ON
INSERT [dbo].[RopeSplicingRecord] ([Id], [RopeId], [SplicingDoneDate], [SplicingMethod], [SplicingDoneBy], [LengthofCroppedRope], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1010, 16, CAST(0x0000AAAC00000000 AS DateTime), N'ssd', N'Shore Assistance', CAST(3.00 AS Decimal(18, 2)), 0, NULL, CAST(0x0000AAA800D45DEA AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeSplicingRecord] ([Id], [RopeId], [SplicingDoneDate], [SplicingMethod], [SplicingDoneBy], [LengthofCroppedRope], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3011, 9, CAST(0x0000AAB000000000 AS DateTime), N'dfdfd', N'Shore Assistance', CAST(3.00 AS Decimal(18, 2)), 0, NULL, CAST(0x0000AAB50118379D AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeSplicingRecord] ([Id], [RopeId], [SplicingDoneDate], [SplicingMethod], [SplicingDoneBy], [LengthofCroppedRope], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3012, 9, CAST(0x0000AAA900000000 AS DateTime), N'reerer', N'Shore assistance', CAST(44.00 AS Decimal(18, 2)), 0, NULL, CAST(0x0000AAB5012F92B6 AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeSplicingRecord] ([Id], [RopeId], [SplicingDoneDate], [SplicingMethod], [SplicingDoneBy], [LengthofCroppedRope], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3013, 9, CAST(0x0000AAB600000000 AS DateTime), N'Manual', N'Onboard', CAST(2.50 AS Decimal(18, 2)), 0, NULL, CAST(0x0000AAB600D5D44F AS DateTime), N'Admin', NULL, NULL, 1)
INSERT [dbo].[RopeSplicingRecord] ([Id], [RopeId], [SplicingDoneDate], [SplicingMethod], [SplicingDoneBy], [LengthofCroppedRope], [RopeTail], [NotificationId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3014, 23, CAST(0x0000AAA100000000 AS DateTime), N'aasaa', N'Onboard', CAST(3.00 AS Decimal(18, 2)), 1, NULL, CAST(0x0000AAB900C5BBAC AS DateTime), N'Admin', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[RopeSplicingRecord] OFF
/****** Object:  StoredProcedure [dbo].[RopeInspection]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RopeInspection]
@RopeTail int
as

select b.AssignedNumber,b.Location,d.RopeType,c.CertificateNumber,a.WinchId,a.RopeId
 from AssignRopeToWinch a join MooringWinchDetail  b on a.WinchId=b.Id join MooringRopeDetail c on c.Id = a.RopeId
 inner join MooringRopeType d on c.ropetypeid=d.id where a.IsActive=1 and a.RopeTail=@RopeTail
GO
/****** Object:  View [dbo].[dgvNC_ArchivesView_Master]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dgvNC_ArchivesView_Master]
AS
SELECT wid,UserName,FullName,dates,NonConfirmities,MasterCrewNC, ROW_NUMBER() OVER(ORDER BY dates desc) ROW_NUM from (

select wid,UserName,FullName,dates,NC1 as NonConfirmities,CASE WHEN MNCCD1 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD1, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD1, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC1!='' 

union
select wid,UserName,FullName,dates,NC2 as NonConfirmities,CASE WHEN MNCCD2 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD2, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD2, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC2!='' 

union
select wid,UserName,FullName,dates,NC3 as NonConfirmities,CASE WHEN MNCCD3 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD3, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD3, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC3!=''

union
select wid,UserName,FullName,dates,NC4 as NonConfirmities,CASE WHEN MNCCD4 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD4, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD4, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC4!='' 
  
  union
select wid,UserName,FullName,dates,NC5 as NonConfirmities,CASE WHEN MNCCD5 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD5, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD5, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC5!='' 
  
  union
select wid,UserName,FullName,dates,NC6 as NonConfirmities,CASE WHEN MNCCD6 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD6, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD6, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC6!='' 
  
  union
select wid,UserName,FullName,dates,NC7 as NonConfirmities,CASE WHEN MNCCD7 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD7, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD7, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC7!=''
  
  union
select wid,UserName,FullName,dates,NC8 as NonConfirmities,CASE WHEN MNCCD8 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12),MNCCD8, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD8, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC8!='' 
  
  union
select wid,UserName,FullName,dates,NC9 as NonConfirmities,CASE WHEN MNCCD9 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD9, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD9, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC9!='' 
  
  union
select wid,UserName,FullName,dates,NC10 as NonConfirmities,CASE WHEN MNCCD10 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12),MNCCD10, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD10, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC10!='' 
  
  union
select wid,UserName,FullName,dates,NC11 as NonConfirmities,CASE WHEN MNCCD11 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD11, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD11, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC11!='' 

union
select wid,UserName,FullName,dates,NC12 as NonConfirmities,CASE WHEN MNCCD12 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD12, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD12, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC12!='' 

union
select wid,UserName,FullName,dates,NC13 as NonConfirmities,CASE WHEN MNCCD13 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12),MNCCD13, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD13, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC13!='' 
  
  union
select wid,UserName,FullName,dates,NC14 as NonConfirmities,CASE WHEN MNCCD14 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD14, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD14, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC14!='' 
  
  union
select wid,UserName,FullName,dates,NC15 as NonConfirmities,CASE WHEN MNCCD15 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD15, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD15, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC15!='' 
  
  union
select wid,UserName,FullName,dates,NC16 as NonConfirmities,CASE WHEN MNCCD16 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD16, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD16, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC16!='' 
  
  union
select wid,UserName,FullName,dates,NC17 as NonConfirmities,CASE WHEN MNCCD17 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD17, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD17, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC17!='' 
  
  union
select wid,UserName,FullName,dates,NC18 as NonConfirmities,CASE WHEN MNCCD18 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD18, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MNCCD18, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where NC18!='' 
----------
union
select wid,UserName,FullName,dates,YNC1 as NonConfirmities,CASE WHEN MYNCCD1 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD1, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD1, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC1!='' 

union
select wid,UserName,FullName,dates,YNC2 as NonConfirmities,CASE WHEN MYNCCD2 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD2, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD2, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC2!='' 
  
  union
select wid,UserName,FullName,dates,YNC3 as NonConfirmities,CASE WHEN MYNCCD3 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD3, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD3, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC3!='' 
  
  union
select wid,UserName,FullName,dates,YNC4 as NonConfirmities,CASE WHEN MYNCCD4 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD4, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD4, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC4!='' 
  
  union
select wid,UserName,FullName,dates,YNC5 as NonConfirmities,CASE WHEN MYNCCD5 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD5, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD5, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC5!='' 
  
  union
select wid,UserName,FullName,dates,YNC6 as NonConfirmities,CASE WHEN MYNCCD6 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD6, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD6, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC6!='' 
  
  union
select wid,UserName,FullName,dates,YNC7 as NonConfirmities,CASE WHEN MYNCCD7 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD7, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD7, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC7!='' 
  
  union
select wid,UserName,FullName,dates,YNC8 as NonConfirmities,CASE WHEN MYNCCD8 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MYNCCD8, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), MYNCCD8, 114)) + ' ' + 'Hours' END AS MasterCrewNC from WorkHours where YNC8!='' 


) T
GO
/****** Object:  View [dbo].[dgvNC_ArchivesView_HOD]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[dgvNC_ArchivesView_HOD]
AS

SELECT wid,UserName,FullName,dates,NonConfirmities,HODCrewNC, ROW_NUMBER() OVER(ORDER BY dates desc) ROW_NUM from (

select wid,UserName,FullName,dates,NC1 as NonConfirmities,CASE WHEN HNCCD1 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD1, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD1, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC1!='' 

union
select wid,UserName,FullName,dates,NC2 as NonConfirmities,CASE WHEN HNCCD2 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD2, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD2, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC2!='' 

union
select wid,UserName,FullName,dates,NC3 as NonConfirmities,CASE WHEN HNCCD3 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD3, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD3, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC3!='' 

union
select wid,UserName,FullName,dates,NC4 as NonConfirmities,CASE WHEN HNCCD4 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD4, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD4, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC4!='' 
  
  union
select wid,UserName,FullName,dates,NC5 as NonConfirmities,CASE WHEN HNCCD5 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD5, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD5, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC5!='' 
  
  union
select wid,UserName,FullName,dates,NC6 as NonConfirmities,CASE WHEN HNCCD6 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), MNCCD6, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD6, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC6!='' 
  
  union
select wid,UserName,FullName,dates,NC7 as NonConfirmities,CASE WHEN HNCCD7 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD7, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD7, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC7!='' 
  
  union
select wid,UserName,FullName,dates,NC8 as NonConfirmities,CASE WHEN HNCCD8 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD8, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD8, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC8!='' 
  
  union
select wid,UserName,FullName,dates,NC9 as NonConfirmities,CASE WHEN HNCCD9 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD9, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD9, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC9!='' 
  
  union
select wid,UserName,FullName,dates,NC10 as NonConfirmities,CASE WHEN HNCCD10 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD10, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD10, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC10!='' 
  
  union
select wid,UserName,FullName,dates,NC11 as NonConfirmities,CASE WHEN HNCCD11 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD11, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD11, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC11!='' 

union
select wid,UserName,FullName,dates,NC12 as NonConfirmities,CASE WHEN HNCCD12 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD12, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD12, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC12!='' 

union
select wid,UserName,FullName,dates,NC13 as NonConfirmities,CASE WHEN HNCCD13 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD13, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD13, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC13!='' 
  
  union
select wid,UserName,FullName,dates,NC14 as NonConfirmities,CASE WHEN HNCCD14 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD14, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD14, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC14!='' 
  
  union
select wid,UserName,FullName,dates,NC15 as NonConfirmities,CASE WHEN HNCCD15 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD15, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD15, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC15!='' 
  
  union
select wid,UserName,FullName,dates,NC16 as NonConfirmities,CASE WHEN HNCCD16 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD16, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), datetimes, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC16!='' 
  
  union
select wid,UserName,FullName,dates,NC17 as NonConfirmities,CASE WHEN HNCCD17 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD17, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD16, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC17!='' 
  
  union
select wid,UserName,FullName,dates,NC18 as NonConfirmities,CASE WHEN HNCCD18 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HNCCD18, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HNCCD18, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where NC18!='' 
----------
union
select wid,UserName,FullName,dates,YNC1 as NonConfirmities,CASE WHEN HYNCCD1 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD1, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD1, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC1!='' 

union
select wid,UserName,FullName,dates,YNC2 as NonConfirmities,CASE WHEN HYNCCD2 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD2, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD2, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC2!='' 
  
  union
select wid,UserName,FullName,dates,YNC3 as NonConfirmities,CASE WHEN HYNCCD3 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD3, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD3, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC3!='' 
  
  union
select wid,UserName,FullName,dates,YNC4 as NonConfirmities,CASE WHEN HYNCCD4 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD4, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD4, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC4!='' 
  
  union
select wid,UserName,FullName,dates,YNC5 as NonConfirmities,CASE WHEN HYNCCD5 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD5, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD5, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC5!='' 
  
  union
select wid,UserName,FullName,dates,YNC6 as NonConfirmities,CASE WHEN HYNCCD6 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD6, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD6, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC6!='' 
  
  union
select wid,UserName,FullName,dates,YNC7 as NonConfirmities,CASE WHEN HYNCCD7 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD7, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD7, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC7!='' 
  
  union
select wid,UserName,FullName,dates,YNC8 as NonConfirmities,CASE WHEN HYNCCD8 is null THEN 'To be Acknowledged' ELSE ('Acknowledged on ' + REPLACE(CONVERT(VARCHAR(12), HYNCCD8, 106), ' ', '-') 
  + ' ' + CONVERT(VARCHAR(5), HYNCCD8, 114)) + ' ' + 'Hours' END AS HODCrewNC from WorkHours where YNC8!='' 


) HOD
GO
/****** Object:  StoredProcedure [dbo].[NotificationsListing]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NotificationsListing]  -- NotificationsListing ' ', ' ' 
@DateFrom datetime,
@DateTo datetime,
@Action nvarchar(50)
as

if(@Action ='Search')
begin
select * from Notifications NotificationsListing WHERE CreatedDate BETWEEN @DateFrom and @DateTo  order by AckRecord desc



 select count(*)  as total  from Notifications WHERE AckRecord ='Not yet acknowledged' and CreatedDate 
 BETWEEN @DateFrom and @DateTo select count(*) as total from Notifications 
 WHERE AckRecord !='Not yet acknowledged' and CreatedDate BETWEEN @DateFrom and @DateTo 


 end
 else
 begin

 --select * from Notifications  order by AckRecord desc


  SELECT id,notification,NotificationType,Acknowledge,AckRecord,CAST( CreatedDate as date) as CreatedDate,
  IsActive FROM Notifications 
  WHERE AckRecord ='Not yet acknowledged' 
 union all

SELECT id,notification,NotificationType,Acknowledge,AckRecord,CAST( CreatedDate as date) as CreatedDate,IsActive FROM Notifications 
WHERE AckRecord !='Not yet acknowledged' and 
 MONTH(createddate) = MONTH(dateadd(dd, -1, GetDate())) AND YEAR(createddate) = YEAR(dateadd(dd, -1, GetDate()))

order by CreatedDate desc

 
 end
GO
/****** Object:  StoredProcedure [dbo].[LooseEquipmentTypeDetails]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[LooseEquipmentTypeDetails]
@Action nvarchar(50)
as

if(@Action ='JShackle')
begin
select * from joiningshackle
end
if(@Action ='RTail')
begin
select * from RopeTail
end
if(@Action ='CStopper')
begin
select * from ChainStopper
end
GO
/****** Object:  StoredProcedure [dbo].[LooseEquipInspection]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[LooseEquipInspection] -- LooseEquipInspection 2 ,'RopeTail'
@id int,
@table_name nvarchar(50)
as

if(@table_name ='Rope Tail' or @table_name ='FireWire' or @table_name ='Messanger Rope' or @table_name ='Rope Stopper')
begin
select a.id,a.looseetypeid,a.CertificateNumber as number, 

--concat('(',Row_Number() over(Order By a.id),') ',looseequipmenttype) as looseequipmenttype 


Row_Number() over(Order By a.id) as RNumber , looseequipmenttype


 from ropetail a 
inner join LooseEType b on a.looseetypeid=b.Id where a.LooseETypeId=@id
end

if(@table_name ='Chain Stopper')
begin
select a.id,a.looseetypeid,a.CertificateNumber as number, 

--concat('(',Row_Number() over(Order By a.id),') ',looseequipmenttype) as looseequipmenttype 

Row_Number() over(Order By a.id) as RNumber , looseequipmenttype

 from ChainStopper a 
inner join LooseEType b on a.looseetypeid=b.Id where a.LooseETypeId=@id
end

if(@table_name ='Joining Shackle')
begin
select a.id,a.looseetypeid,a.CertificateNumber as number,

 --concat('(',Row_Number() over(Order By a.id),') ',looseequipmenttype) as looseequipmenttype 

 Row_Number() over(Order By a.id) as RNumber , looseequipmenttype

 from JoiningShackle a 
inner join LooseEType b on a.looseetypeid=b.Id where a.LooseETypeId=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetSearchText]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetSearchText]   -- GetSearchText 'cargo'
@text nvarchar(100)
as


select a.id, a.Mid,a.content,b.Menuname from docspages a left join tblMenuName b 
on a.Mid=b.Mid
 where a.Content like  '%' +@text+ '%'
GO
/****** Object:  StoredProcedure [dbo].[GetRopeSplicing]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetRopeSplicing]
@RopeTail int 
as


select a.Id,e.Location as AssignedLocation,c.AssignedNumber, a.SplicingDoneDate,a.SplicingDoneBy,
a.SplicingMethod, d.CertificateNumber



 from ropesplicingRecord a inner join AssignRopeToWinch b on a.RopeId=b.RopeId inner join MooringWinchDetail c
 on b.WinchId=c.Id inner join MooringRopeDetail d on a.RopeId=d.id inner join MooringWinchDetail e on b.WinchId=e.Id
 where b.IsActive=1 and a.ropetail=@RopeTail
GO
/****** Object:  StoredProcedure [dbo].[GetRopeEndtoEnd]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetRopeEndtoEnd]
as


select a.Id,c.Location,c.AssignedNumber, a.EndtoEndDoneDate,d.CertificateNumber,

case when a.CurrentOutboadEndinUse=1 then 'A'
when a.CurrentOutboadEndinUse=0 then 'B'
end as CurrentOutboadEndinUse1

 from RopeEndtoEnd2 a inner join AssignRopeToWinch b on a.RopeId=b.RopeId inner join MooringWinchDetail c
 on b.WinchId=c.Id inner join MooringRopeDetail d on b.RopeId=d.id where b.IsActive=1
GO
/****** Object:  StoredProcedure [dbo].[GetRopeDiscardList]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetRopeDiscardList]
@RopeTail int
as


select a.certificatenumber,a.outofservicedate,a.reasonoutofservice,b.ropetype from MooringRopeDetail  a inner join
MooringRopeType b on a.RopeTypeId=b.Id where a.outofservicedate is not null and a.RopeTail=@RopeTail
GO
/****** Object:  StoredProcedure [dbo].[GetRopeCropping]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetRopeCropping]
as

select a.Id,e.Location as AssignedLocation,c.AssignedNumber, a.CroppedDate,a.CroppedOutboardEnd,
a.LengthofCroppedRope,a.reasonofcropping, d.CertificateNumber
 from RopeCropping a inner join AssignRopeToWinch b on a.RopeId=b.RopeId inner join MooringWinchDetail c
 on b.WinchId=c.Id inner join MooringRopeDetail d on a.RopeId=d.id
 inner join MooringWinchDetail e on b.WinchId=e.Id
  where b.IsActive=1
GO
/****** Object:  StoredProcedure [dbo].[GetMOperationBirthD]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetMOperationBirthD]
as
select * from MOperationBirthDetail
GO
/****** Object:  StoredProcedure [dbo].[GetMooringRopeDetailList]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetMooringRopeDetailList]
@RopeTail int
as

select  a.Id,a.ropeconstruction,m.ropetype,a.RopeTypeId,a.ManufacturerId, a.diameter,a.Length,a.WLL,a.ReceivedDate,a.RopeTagging, a.CertificateNumber,a.LDBF,a.MBL,cast( a.InstalledDate as date) as InstalledDate,k.Name as ManufacturerName,
(Select  c.location from AssignRopeToWinch b inner join MooringWinchDetail c on b.WinchId=c.Id  where b.RopeId=a.Id and b.isactive=1 ) as Location,
(Select  c.assignednumber from AssignRopeToWinch e inner join MooringWinchDetail c on e.WinchId=c.Id 
 where e.RopeId=a.Id and e.isactive=1 ) as AssignedWinch
from MooringRopeDetail a inner join tblCommon k on a.ManufacturerId=k.Id inner join MooringRopeType m on a.ropetypeid=m.id
where a.ropetail=@RopeTail
GO
/****** Object:  StoredProcedure [dbo].[GetLooseEquipInspectionList]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetLooseEquipInspectionList]
as

select a.Id, a.Inspectby,cast(a.Inspectdate as date) as Inspectdate,b.looseequipmenttype +' - '+ a.number as
 looseequipmenttype, a.Condition, a.Remarks
 from MooringLooseEquipInspection a inner join LooseEType b on a.LooseETypeId=b.Id
GO
/****** Object:  StoredProcedure [dbo].[GetDamageRope]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetDamageRope] -- GetDamageRope 1
@RopeTail int
as

select a.Id,e.Location as AssignedLocation,c.AssignedNumber, a.DamageObserved,a.IncidentReport,

--case when a.MooringOperation is null then ''
--when a.MooringOperation is not null then a.MooringOperation
--end as MooringOperation,



 d.CertificateNumber
 from RopeDamageRecord a inner join AssignRopeToWinch b on a.RopeId=b.RopeId inner join MooringWinchDetail c
 on b.WinchId=c.Id inner join MooringRopeDetail d on a.RopeId=d.id
 inner join MooringWinchDetail e on b.WinchId=e.Id
  where b.IsActive=1 and a.RopeTail=@RopeTail
GO
/****** Object:  StoredProcedure [dbo].[GetDamageLooseE]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetDamageLooseE]
as

select a.Id, a.CertificateNumber,a.DamageObserved,a.MooringOperation,b.LooseEquipmentType as LooseEtype
 from LooseEDamageRecord a inner join LooseEType b on a.LooseETypeId=b.Id
GO
/****** Object:  StoredProcedure [dbo].[GetCrossShiftingWinch]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetCrossShiftingWinch]
as


select a.Id,c.Location,c.AssignedNumber, a.DateofShifting,d.CertificateNumber,

case when a.OutboardEndinUse=1 then 'A'
when a.OutboardEndinUse=0 then 'B'
end as CurrentOutboardEndinUse

 from CrossShiftingWinch a  inner join MooringWinchDetail c
 on a.WinchId=c.Id inner join MooringRopeDetail d on a.RopeId=d.id
GO
/****** Object:  StoredProcedure [dbo].[GetAssignRopeToWinch]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetAssignRopeToWinch]
@RopeTail int
as

select a.id,a.Outboard,a.RopeId,a.WinchId,a.CreatedBy, e.Location as AssignedLocation,a.createddate,a.AssignedDate,
 b.certificatenumber,c.assignednumber,
 CASE
    WHEN a.IsActive=1 THEN 'Active'
   
    ELSE 'InActive'
END AS Status
 
  from AssignRopeToWinch a
 inner join MooringRopeDetail b on a.RopeId=b.Id inner join MooringWinchDetail c on a.WinchId=c.Id
  inner join MooringWinchDetail e on a.WinchId=e.Id
  where a.IsActive=1 and a.RopeTail=@RopeTail

 
select a.id,a.Outboard,a.RopeId,a.WinchId,a.CreatedBy, e.Location as AssignedLocation,a.createddate,a.AssignedDate,
 b.certificatenumber,c.assignednumber,
 
 CASE
    WHEN a.IsActive=1 THEN 'Active'
   
    ELSE 'InActive'
END AS Status

  from AssignRopeToWinch a
 inner join MooringRopeDetail b on a.RopeId=b.Id inner join MooringWinchDetail c on a.WinchId=c.Id
   inner join MooringWinchDetail e on a.WinchId=e.Id
  where a.IsActive=0 and a.RopeTail=@RopeTail
GO
/****** Object:  StoredProcedure [dbo].[GetAssignedWinchesList]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetAssignedWinchesList]
as
select a.WinchId as  Id,b.AssignedNumber  from AssignRopeToWinch a inner join MooringWinchDetail b on
a.WinchId=b.Id where a.IsActive=1
GO
/****** Object:  StoredProcedure [dbo].[GetAssignedRopeTail]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetAssignedRopeTail]
as

 select * into #tb1 from(  select a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a 
 inner join MooringWinchDetail b on a.WinchId=b.Id where a.IsActive=1 and a.RopeTail=0) as sds

  Select * into #tb2 from (select a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a 
 inner join MooringWinchDetail b on a.WinchId=b.Id where a.IsActive=1 and a.RopeTail=1) as dd

 select a.winchid,a.id,a.assignednumber, 
 
 CASE
    WHEN  b.WinchId is null THEN 'Hidden'
       WHEN  b.WinchId is not null THEN 'Visible'
 
END AS VisibilityCheck 
 
 from #tb1 a left join #tb2 b on a.winchid=b.Winchid

 drop table #tb1
 drop table #tb2
GO
/****** Object:  StoredProcedure [dbo].[GetActiveAssignRopeType]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetActiveAssignRopeType] -- GetActiveAssignRopeType 1
@RopeTail int
as

select a.id,a.certificatenumber,b.ropeid from MooringRopeDetail a inner join AssignRopeToWinch b  on
a.id=b.ropeid where b.IsActive=1 and b.RopeTail=@RopeTail
GO
/****** Object:  StoredProcedure [dbo].[AssignLooseEquipDetail]    Script Date: 09/13/2019 17:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[AssignLooseEquipDetail]
as
select a.id,a.LooseETypeId,b.Looseequipmenttype, a.AssignWinchId,a.CreatedBy, a.createddate,c.Location,
c.assignednumber from AssignLooseEquipToWinch a inner join LooseEType b
  on a.LooseETypeId=b.Id inner join MooringWinchDetail c on a.AssignWinchId=c.Id
GO
/****** Object:  StoredProcedure [dbo].[NotificationCmntList]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NotificationCmntList]
@NotifiId int,
@Action int
as

if(@Action=1)
begin
select id,comments,createddate,NotificationId,CommentsType from notificationcomment where CommentsType=1 and NotificationId = @NotifiId order by Id desc
end
if(@Action=2)
begin
select id,comments,createddate,NotificationId,CommentsType from notificationcomment where CommentsType=2 and NotificationId = @NotifiId order by Id desc
end
GO
/****** Object:  Table [dbo].[tblRopeInspectionSetting]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRopeInspectionSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MooringRopeType] [int] NOT NULL,
	[ManufacturerType] [int] NOT NULL,
	[MaximumRunningHours] [int] NULL,
	[MaximumMonthsAllowed] [int] NULL,
	[EndtoEndMonth] [int] NULL,
	[Rating1] [decimal](18, 2) NOT NULL,
	[Rating2] [decimal](18, 2) NOT NULL,
	[Rating3] [decimal](18, 2) NOT NULL,
	[Rating4] [decimal](18, 2) NOT NULL,
	[Rating5] [decimal](18, 2) NOT NULL,
	[Rating6] [decimal](18, 2) NOT NULL,
	[Rating7] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblRopeInspectionSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblRopeInspectionSetting] ON
INSERT [dbo].[tblRopeInspectionSetting] ([Id], [MooringRopeType], [ManufacturerType], [MaximumRunningHours], [MaximumMonthsAllowed], [EndtoEndMonth], [Rating1], [Rating2], [Rating3], [Rating4], [Rating5], [Rating6], [Rating7]) VALUES (1, 16, 1, 200, 18, 10, CAST(1.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[tblRopeInspectionSetting] OFF
/****** Object:  StoredProcedure [dbo].[SPGetWorkHoursPlanner]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetWorkHoursPlanner]
AS 
BEGIN
select * from WorkHoursPlanner

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetWorkHours]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetWorkHours]
AS 
BEGIN
select * from WorkHours

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetVessel]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetVessel]
AS 
BEGIN
select * from Vessel

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetversion_tbl]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetversion_tbl]
AS 
BEGIN
select * from version_tbl

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetUserAccess]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetUserAccess]
AS 
BEGIN
select * from UserAccess

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetRuleTable]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetRuleTable]
AS 
BEGIN
select * from RuleTable

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetRuffCode]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetRuffCode]
AS 
BEGIN
select * from RuffCode

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetRetardtblClass]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetRetardtblClass]
AS 
BEGIN
select * from RetardtblClass

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetOverTime]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetOverTime]
AS 
BEGIN
select * from OverTime

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetOPAStartStop]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetOPAStartStop]
AS 
BEGIN
select * from OPAStartStop

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetLogbook]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetLogbook]
AS 
BEGIN
select * from Logbook

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetInternational]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetInternational]
AS 
BEGIN
select * from International

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetInfoLog]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetInfoLog]
AS 
BEGIN
select * from InfoLog

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetHolidays]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetHolidays]
AS 
BEGIN
select * from Holidays

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetHoliDayGroupName]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetHoliDayGroupName]
AS 
BEGIN
select * from HoliDayGroupName

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetGroupPlanner]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetGroupPlanner]
AS 
BEGIN
select * from GroupPlanner

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetFreeze]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetFreeze]
AS 
BEGIN
select * from Freeze

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetDepartment]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetDepartment]
AS 
BEGIN
select * from Department

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetDefineRules]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetDefineRules]
AS 
BEGIN
select * from DefineRules

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetCurrency]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetCurrency]
AS 
BEGIN
select * from Currency

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetCrewRank]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetCrewRank]
AS 
BEGIN
select * from CrewRank

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetCrewDetail]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetCrewDetail]
AS 
BEGIN
select * from CrewDetail

END

EXEC SPGetCrewDetail
GO
/****** Object:  StoredProcedure [dbo].[SPGetcomment_ofVS]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetcomment_ofVS]
AS 
BEGIN
select * from comment_ofVS

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetcertificates]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetcertificates]
AS 
BEGIN
select * from certificates

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetCertificateNotification]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetCertificateNotification]
AS 
BEGIN
select * from CertificateNotification

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetcertificate_order]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetcertificate_order]
AS 
BEGIN
select * from certificate_order

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetAdminLogin]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetAdminLogin]
AS 
BEGIN
select * from AdminLogin

END
GO
/****** Object:  StoredProcedure [dbo].[SPGetAddGroup]    Script Date: 09/13/2019 17:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPGetAddGroup]
AS 
BEGIN
select * from AddGroup

END

EXEC SPGetAdminLogin
GO
/****** Object:  ForeignKey [FK_tblRopeInspectionSetting_tblCommon]    Script Date: 09/13/2019 17:01:50 ******/
ALTER TABLE [dbo].[tblRopeInspectionSetting]  WITH CHECK ADD  CONSTRAINT [FK_tblRopeInspectionSetting_tblCommon] FOREIGN KEY([ManufacturerType])
REFERENCES [dbo].[tblCommon] ([Id])
GO
ALTER TABLE [dbo].[tblRopeInspectionSetting] CHECK CONSTRAINT [FK_tblRopeInspectionSetting_tblCommon]
GO
