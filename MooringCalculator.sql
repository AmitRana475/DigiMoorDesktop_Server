USE [DigiMoorDB]
GO
/****** Object:  Database [MooringDB]    Script Date: 11/18/2019 1:26:03 AM ******/

/****** Object:  Table [dbo].[tblCurrentLoad]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCurrentLoad](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Notation] [varchar](250) NULL,
	[MainValue] [decimal](18, 2) NULL,
	[Units] [varchar](150) NULL,
 CONSTRAINT [PK_tblCurrentLoad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblGeneralP]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblGeneralP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Description] [varchar](250) NULL,
	[MainValue] [decimal](18, 2) NULL,
	[DefaultValue] [decimal](18, 2) NULL,
	[Units] [varchar](150) NULL,
 CONSTRAINT [PK_tblGeneralP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMooringLines]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMooringLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Xch] [decimal](18, 2) NULL,
	[Ych] [decimal](18, 2) NULL,
	[Zch] [decimal](18, 2) NULL,
	[Xbl] [decimal](18, 2) NULL,
	[Ybl] [decimal](18, 2) NULL,
	[Zbl] [decimal](18, 2) NULL,
	[l0] [decimal](18, 2) NULL,
	[E] [decimal](18, 3) NULL,
	[n] [decimal](18, 2) NULL,
	[a] [decimal](18, 2) NULL,
	[MBSrope] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblMooringLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVesselP]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVesselP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Description] [varchar](250) NULL,
	[MainValue] [decimal](18, 2) NULL,
	[DefaultValue] [decimal](18, 2) NULL,
	[Units] [varchar](150) NULL,
 CONSTRAINT [PK_tblVesselP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblWindandCurrent]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblWindandCurrent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Description] [varchar](250) NULL,
	[MainValue] [decimal](18, 4) NULL,
	[DefaultValue] [decimal](18, 4) NULL,
	[Units] [varchar](150) NULL,
 CONSTRAINT [PK_tblWindandCurrent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblWindAreas]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblWindAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Description] [varchar](250) NULL,
	[MainValue] [decimal](18, 2) NULL,
	[DefaultValue] [decimal](18, 2) NULL,
	[Units] [varchar](150) NULL,
 CONSTRAINT [PK_tblWindAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblWindLoads]    Script Date: 11/18/2019 1:26:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblWindLoads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Notation] [varchar](250) NULL,
	[MainValue] [decimal](18, 2) NULL,
	[Units] [varchar](150) NULL,
 CONSTRAINT [PK_tblWindLoads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblCurrentLoad] ON 

INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (1, N'Length of Waterline', N'LWL', NULL, N'm')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (2, N'Breadth', N'B', NULL, N'm')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (3, N'Draft', N'T', NULL, N'm')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (4, N'Water Depth', N'WD', NULL, N'm')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (5, N'Wetted Surface Area', N'S', NULL, N'm2')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (6, N'Design Current Speed', N'Vc', NULL, N'm/s')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (7, N'Density of water', N'Pwater', NULL, N'kg/m3')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (8, N'Current Angle', N'Qc', NULL, N'degrees')
INSERT [dbo].[tblCurrentLoad] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (9, N'Ratio of Water Depth to Draft WD/T', N'WD/T', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblCurrentLoad] OFF
SET IDENTITY_INSERT [dbo].[tblGeneralP] ON 

INSERT [dbo].[tblGeneralP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (1, N'Acceleration due to Gravity', N'g', CAST(9.81 AS Decimal(18, 2)), CAST(9.81 AS Decimal(18, 2)), N'm/s2')
INSERT [dbo].[tblGeneralP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (2, N'Density of air', N'Pair', CAST(1.23 AS Decimal(18, 2)), CAST(1.23 AS Decimal(18, 2)), N'kg/m3')
INSERT [dbo].[tblGeneralP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (3, N'Density of Water', N'Pwater', CAST(1025.00 AS Decimal(18, 2)), CAST(1025.00 AS Decimal(18, 2)), N'kg/m3')
SET IDENTITY_INSERT [dbo].[tblGeneralP] OFF
SET IDENTITY_INSERT [dbo].[tblMooringLines] ON 

INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(6.40 AS Decimal(18, 2)), CAST(-7.32 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.370 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (2, CAST(4.88 AS Decimal(18, 2)), CAST(1.54 AS Decimal(18, 2)), CAST(5.79 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(2.44 AS Decimal(18, 2)), CAST(1.370 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (3, CAST(11.58 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(5.49 AS Decimal(18, 2)), CAST(21.95 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(2.44 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (4, CAST(21.34 AS Decimal(18, 2)), CAST(4.88 AS Decimal(18, 2)), CAST(4.72 AS Decimal(18, 2)), CAST(7.32 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(2.44 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (5, CAST(96.93 AS Decimal(18, 2)), CAST(6.10 AS Decimal(18, 2)), CAST(2.90 AS Decimal(18, 2)), CAST(102.41 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(3.05 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (6, CAST(106.68 AS Decimal(18, 2)), CAST(5.33 AS Decimal(18, 2)), CAST(3.05 AS Decimal(18, 2)), CAST(95.10 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(2.44 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (7, CAST(110.95 AS Decimal(18, 2)), CAST(4.88 AS Decimal(18, 2)), CAST(3.20 AS Decimal(18, 2)), CAST(117.04 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(2.44 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (8, CAST(118.87 AS Decimal(18, 2)), CAST(3.05 AS Decimal(18, 2)), CAST(3.20 AS Decimal(18, 2)), CAST(124.36 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(3.35 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
INSERT [dbo].[tblMooringLines] ([Id], [Xch], [Ych], [Zch], [Xbl], [Ybl], [Zbl], [l0], [E], [n], [a], [MBSrope]) VALUES (9, CAST(80.47 AS Decimal(18, 2)), CAST(6.10 AS Decimal(18, 2)), CAST(2.90 AS Decimal(18, 2)), CAST(87.78 AS Decimal(18, 2)), CAST(8.23 AS Decimal(18, 2)), CAST(2.74 AS Decimal(18, 2)), CAST(2.44 AS Decimal(18, 2)), CAST(1.378 AS Decimal(18, 3)), CAST(3.00 AS Decimal(18, 2)), CAST(1385.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[tblMooringLines] OFF
SET IDENTITY_INSERT [dbo].[tblVesselP] ON 

INSERT [dbo].[tblVesselP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (1, N'Length Waterline (LWL)', N'LWL', CAST(316.00 AS Decimal(18, 2)), NULL, N'm')
INSERT [dbo].[tblVesselP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (2, N'Breadth', N'B', CAST(60.00 AS Decimal(18, 2)), NULL, N'm')
INSERT [dbo].[tblVesselP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (3, N'Mean Draft (Working)', N'T', CAST(8.61 AS Decimal(18, 2)), NULL, N'm')
INSERT [dbo].[tblVesselP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (4, N'Ship XCG (Longitudinal CG) - from Bow', N'Xcg', CAST(158.00 AS Decimal(18, 2)), CAST(158.00 AS Decimal(18, 2)), N'm')
INSERT [dbo].[tblVesselP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (5, N'Displacement', N'Delta', CAST(120001.00 AS Decimal(18, 2)), NULL, N'MT')
INSERT [dbo].[tblVesselP] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (6, N'Wetted Surface Area1', N'S', CAST(15936.70 AS Decimal(18, 2)), CAST(15936.70 AS Decimal(18, 2)), N'm2')
SET IDENTITY_INSERT [dbo].[tblVesselP] OFF
SET IDENTITY_INSERT [dbo].[tblWindandCurrent] ON 

INSERT [dbo].[tblWindandCurrent] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (1, N'Design Wind Speed', N'Vw', CAST(30.8600 AS Decimal(18, 4)), CAST(20.0000 AS Decimal(18, 4)), N'm/s')
INSERT [dbo].[tblWindandCurrent] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (2, N'Wind Angle (See Fig 1) - from 0 to 180 deg', N'Qw', CAST(90.0000 AS Decimal(18, 4)), NULL, N'degrees')
INSERT [dbo].[tblWindandCurrent] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (3, N'Current Speed', N'Vc', CAST(0.3800 AS Decimal(18, 4)), CAST(0.5000 AS Decimal(18, 4)), N'm/s')
INSERT [dbo].[tblWindandCurrent] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (4, N'Current Angle (See Fig 2) - from 0 to 180 deg', N'Qc', CAST(90.0000 AS Decimal(18, 4)), NULL, N'degrees')
INSERT [dbo].[tblWindandCurrent] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (5, N'Water Depth', N'WD', CAST(25.8500 AS Decimal(18, 4)), NULL, N'm')
SET IDENTITY_INSERT [dbo].[tblWindandCurrent] OFF
SET IDENTITY_INSERT [dbo].[tblWindAreas] ON 

INSERT [dbo].[tblWindAreas] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (1, N'End projected wind area2', N'Ae', CAST(2562.46 AS Decimal(18, 2)), NULL, N'm2')
INSERT [dbo].[tblWindAreas] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (2, N'Side projected wind Area', N'As', CAST(7476.29 AS Decimal(18, 2)), NULL, N'm2')
INSERT [dbo].[tblWindAreas] ([Id], [Name], [Description], [MainValue], [DefaultValue], [Units]) VALUES (3, N'Vessel Type (1 - for Normal shape, 2 - for Hull Dominated shape)', NULL, CAST(1.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[tblWindAreas] OFF
SET IDENTITY_INSERT [dbo].[tblWindLoads] ON 

INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (1, N'Length of Waterline', N'LWL', NULL, N'm')
INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (2, N'End projected wind area', N'Ae', NULL, N'm2')
INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (3, N'Side projected Wind Area', N'As', NULL, N'm2')
INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (4, N'Design Wind Speed', N'Vw', NULL, N'm/s')
INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (5, N'Density of air', N'Pair', NULL, N'kg/m3')
INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (6, N'Wind Angle', N'Qw', NULL, N'degrees')
INSERT [dbo].[tblWindLoads] ([Id], [Name], [Notation], [MainValue], [Units]) VALUES (7, N'Ship Type ( 1 - Normal, 2 - Hull dominated)', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblWindLoads] OFF

