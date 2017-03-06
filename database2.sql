USE [epimon]
GO
/****** Object:  Table [dbo].[characters]    Script Date: 3/6/2017 2:54:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[characters](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](255) NULL,
	[name] [varchar](255) NULL,
	[health] [int] NULL,
	[attack] [int] NULL,
	[speed] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[characters_moves]    Script Date: 3/6/2017 2:54:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[characters_moves](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[character_id] [int] NULL,
	[move_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[moves]    Script Date: 3/6/2017 2:54:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[moves](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[type] [varchar](255) NULL,
	[dmg] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[characters] ON 

INSERT [dbo].[characters] ([id], [type], [name], [health], [attack], [speed]) VALUES (1, N'fire', N'Varchar', 80, 20, 90)
INSERT [dbo].[characters] ([id], [type], [name], [health], [attack], [speed]) VALUES (2, N'rock', N'Rubydude', 100, 16, 75)
INSERT [dbo].[characters] ([id], [type], [name], [health], [attack], [speed]) VALUES (3, N'electric', N'Thunderscript', 85, 19, 100)
INSERT [dbo].[characters] ([id], [type], [name], [health], [attack], [speed]) VALUES (4, N'ice', NULL, 95, 17, 85)
SET IDENTITY_INSERT [dbo].[characters] OFF
SET IDENTITY_INSERT [dbo].[characters_moves] ON 

INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (1, 1, 1)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (2, 1, 2)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (3, 1, 4)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (4, 1, 5)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (5, 2, 1)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (6, 2, 2)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (7, 2, 9)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (8, 2, 10)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (9, 3, 1)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (10, 3, 2)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (11, 3, 7)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (12, 3, 8)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (13, 4, 1)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (14, 4, 2)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (15, 4, 5)
INSERT [dbo].[characters_moves] ([id], [character_id], [move_id]) VALUES (16, 4, 6)
SET IDENTITY_INSERT [dbo].[characters_moves] OFF
SET IDENTITY_INSERT [dbo].[moves] ON 

INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (1, N'tackle', N'normal', 15)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (2, N'slash', N'normal', 13)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (3, N'flamethrower', N'fire', 25)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (4, N'ember', N'fire', 20)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (5, N'ice hammer', N'ice', 19)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (6, N'ice punch', N'ice', 16)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (7, N'thunderbolt', N'electric', 22)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (8, N'bolt strike', N'electric', 19)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (9, N'stone blast', N'rock', 18)
INSERT [dbo].[moves] ([id], [name], [type], [dmg]) VALUES (10, N'rock tomb', N'rock', 15)
SET IDENTITY_INSERT [dbo].[moves] OFF
