USE [Geldverleih_Schule]
GO
/****** Object:  Table [dbo].[T_Kunde]    Script Date: 06/23/2012 14:21:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Kunde](
	[KD_Nr] [uniqueidentifier] NOT NULL,
	[KD_Name] [nvarchar](100) NOT NULL,
	[KD_Adresse] [nvarchar](150) NOT NULL,
	[KD_WohnOrt] [nvarchar](50) NOT NULL,
	[KD_PLZ] [int] NOT NULL,
	[KD_Vorname] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_Kunde] PRIMARY KEY CLUSTERED 
(
	[KD_Nr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Ausleihvorgang]    Script Date: 06/23/2012 14:21:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Ausleihvorgang](
	[AL_KD_Nr] [uniqueidentifier] NOT NULL,
	[AL_Vorgangsnr] [uniqueidentifier] NOT NULL,
	[AL_Betrag] [money] NOT NULL,
	[AL_Datum] [datetime] NOT NULL,
	[AL_Zinssatz] [float] NOT NULL,
 CONSTRAINT [PK_T_Ausleihvorgang] PRIMARY KEY CLUSTERED 
(
	[AL_Vorgangsnr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Rueckzahlvorgang]    Script Date: 06/23/2012 14:21:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Rueckzahlvorgang](
	[RV_Nr] [uniqueidentifier] NOT NULL,
	[RV_AL_Nr] [uniqueidentifier] NOT NULL,
	[RV_Betrag] [money] NOT NULL,
	[RV_Datum] [datetime] NOT NULL,
 CONSTRAINT [PK_T_Rueckzahlvorgang] PRIMARY KEY CLUSTERED 
(
	[RV_Nr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_T_Ausleihvorgang_T_Kunde]    Script Date: 06/23/2012 14:21:25 ******/
ALTER TABLE [dbo].[T_Ausleihvorgang]  WITH CHECK ADD  CONSTRAINT [FK_T_Ausleihvorgang_T_Kunde] FOREIGN KEY([AL_KD_Nr])
REFERENCES [dbo].[T_Kunde] ([KD_Nr])
GO
ALTER TABLE [dbo].[T_Ausleihvorgang] CHECK CONSTRAINT [FK_T_Ausleihvorgang_T_Kunde]
GO
/****** Object:  ForeignKey [FK_T_Rueckzahlvorgang_T_Ausleihvorgang]    Script Date: 06/23/2012 14:21:25 ******/
ALTER TABLE [dbo].[T_Rueckzahlvorgang]  WITH CHECK ADD  CONSTRAINT [FK_T_Rueckzahlvorgang_T_Ausleihvorgang] FOREIGN KEY([RV_AL_Nr])
REFERENCES [dbo].[T_Ausleihvorgang] ([AL_Vorgangsnr])
GO
ALTER TABLE [dbo].[T_Rueckzahlvorgang] CHECK CONSTRAINT [FK_T_Rueckzahlvorgang_T_Ausleihvorgang]
GO
