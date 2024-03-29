USE [master]
GO

CREATE DATABASE [Voting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Voting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Voting.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Voting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Voting_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [Voting] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Voting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Voting] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Voting] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Voting] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Voting] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Voting] SET ARITHABORT OFF 
GO
ALTER DATABASE [Voting] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Voting] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Voting] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Voting] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Voting] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Voting] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Voting] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Voting] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Voting] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Voting] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Voting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Voting] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Voting] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Voting] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Voting] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Voting] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Voting] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Voting] SET RECOVERY FULL 
GO
ALTER DATABASE [Voting] SET  MULTI_USER 
GO
ALTER DATABASE [Voting] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Voting] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Voting] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Voting] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Voting] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Voting] SET QUERY_STORE = OFF
GO

USE [Voting]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

USE [Voting]
GO

/****** Object:  Table [dbo].[BallotType]    Script Date: 5/13/2019 3:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BallotType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ElectionID] [int] NULL,
 CONSTRAINT [PK_BallotType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[BallotTypeMapping]    Script Date: 5/13/2019 3:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BallotTypeMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BallotTypeID] [int] NULL,
	[ContestID] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_BallotTypeMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Candidates]    Script Date: 5/13/2019 3:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidates](
	[ID]						int IDENTITY(1,1) NOT NULL,
	[ElectionID]				int				NULL,
	[ContestID]					int				NULL,
	[CandidateName]				varchar(50)		NULL,
	[ShortDescription]			varchar(250)	NULL,
	[LongDescription]			varchar(4096)	NULL,
	[SortOrder]					int				NULL,
	[CandidateNameRecording]	varchar(255)	NULL,
	[CandidateBioRecording]		varchar(255)	NULL,
CONSTRAINT 
	[PK_Candidates] PRIMARY KEY CLUSTERED 
	( [ID] ASC ) WITH (
		PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Contests]    Script Date: 5/13/2019 3:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contests] (
	[ID]					int IDENTITY(1,1) NOT NULL,
	[BallotTypeID]			int				NULL,
	[Title]					varchar(50)		NULL,
	[Description]			varchar(50)		NULL,
	[MaxVotes]				int				NULL,
	[SortOrder]				int				NULL,
	[ContestNameRecording]	varchar(255)	NULL,
CONSTRAINT 
	[PK_Contests] PRIMARY KEY CLUSTERED 
	( [ID] ASC ) WITH (
		PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Elections]    Script Date: 5/23/2019 8:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Elections] (
	[Id]						int IDENTITY(1,1)	NOT NULL,
	[ElectionName]				nvarchar(50)		NOT NULL,	-- made this nvarchar in case we need UTF char(s) in it.
	[ElectionLogoUrl]			varchar(512)		NULL,
	[OpenDate]					datetime			NOT NULL,
	[CloseDate]					datetime			NOT NULL,
	[LoginScreenOpenMessage]	varchar(2048)		NULL,
	[LoginScreenCloseMessage]	varchar(2048)		NULL,
	[LoginIDLabelTxt]			varchar(50)			NULL,
	[LoginPINLabelTxt]			varchar(50)			NULL,
	[LandingPageTitle]			varchar(50)			NULL,
	[LandingPageMessage]		varchar(500)		NULL,
	[IVRPhoneNumber]			varchar(30)			NOT NULL,
CONSTRAINT 
	[PK_Elections] PRIMARY KEY CLUSTERED 
	( [Id] ASC ) WITH (
		PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[LoginAttempts]    Script Date: 5/13/2019 3:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginAttempts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserIP] [varchar](50) NULL,
	[BrowserAgent] [varchar](200) NULL,
	[TimeStamp] [datetime] NULL,
	[EnteredLoginID] [varchar](50) NULL,
	[SuccessfulLogin] [bit] NULL,
 CONSTRAINT [PK_LoginAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Voters]    Script Date: 5/23/2019 8:59:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voters](
	[Id]			int IDENTITY(1,1)	NOT NULL,
	[VoterName]		varchar(50)			NULL,
	[Affiliation]	varchar(50)			NULL,
	[ElectionId]	int					NULL,
	[BallotType]	int					NULL,
	[LoginId]		varchar(50)			NULL,
	[LoginPin]		varchar(50)			NULL,
	[VoteCompleted] bit					NULL,
CONSTRAINT [PK_Voters] 
	PRIMARY KEY CLUSTERED ( [ID] ASC ) WITH (
		PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

/***
	Object:  Table [dbo].[Votes]    
	Script Date: 6/2/2019 2:32 PM 
***/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Votes](
	[ID]			int IDENTITY(1,1)	NOT NULL,
	[CandidateID]	int					NOT NULL,
	[VoteDate]		datetime			NOT NULL,
	[VoterID]		int					NOT NULL,
CONSTRAINT 
	[PK_Votes] PRIMARY KEY CLUSTERED ( [ID] ASC ) WITH (
		PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [LoginId-nc]    Script Date: 5/23/2019 8:59:26 AM ******/
CREATE NONCLUSTERED INDEX [LoginId-nc] ON [dbo].[Voters] ( [LoginID] ASC ) WITH (
	PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[BallotType]  WITH CHECK ADD  CONSTRAINT [FK_BallotType_Elections] FOREIGN KEY([ElectionID])
REFERENCES [dbo].[Elections] ([ID])
GO

ALTER TABLE [dbo].[BallotType] CHECK CONSTRAINT [FK_BallotType_Elections]
GO

ALTER TABLE [dbo].[BallotTypeMapping]  WITH CHECK ADD  CONSTRAINT [FK_BallotTypeMapping_BallotType] FOREIGN KEY([BallotTypeID])
REFERENCES [dbo].[BallotType] ([ID])
GO

ALTER TABLE [dbo].[BallotTypeMapping] CHECK CONSTRAINT [FK_BallotTypeMapping_BallotType]
GO

ALTER TABLE [dbo].[BallotTypeMapping]  WITH CHECK ADD  CONSTRAINT [FK_BallotTypeMapping_Contests] FOREIGN KEY([ContestID])
REFERENCES [dbo].[Contests] ([ID])
GO

ALTER TABLE [dbo].[BallotTypeMapping] CHECK CONSTRAINT [FK_BallotTypeMapping_Contests]
GO

ALTER TABLE [dbo].[Candidates]  WITH CHECK ADD  CONSTRAINT [FK_Candidates_Contests] FOREIGN KEY([ContestID])
REFERENCES [dbo].[Contests] ([ID])
GO

ALTER TABLE [dbo].[Candidates] CHECK CONSTRAINT [FK_Candidates_Contests]
GO

ALTER TABLE [dbo].[Voters]  WITH CHECK ADD  CONSTRAINT [FK_Voters_BallotType] FOREIGN KEY([BallotType])
REFERENCES [dbo].[BallotType] ([ID])
GO

ALTER TABLE [dbo].[Voters] CHECK CONSTRAINT [FK_Voters_BallotType]
GO

ALTER TABLE [dbo].[Voters]  WITH CHECK ADD  CONSTRAINT [FK_Voters_Elections] FOREIGN KEY([ElectionID])
REFERENCES [dbo].[Elections] ([ID])
GO

ALTER TABLE [dbo].[Voters] CHECK CONSTRAINT [FK_Voters_Elections]
GO

ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_Candidates] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidates] ([ID])
GO

ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_Candidates]
GO

ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_Voters] FOREIGN KEY([VoterID])
REFERENCES [dbo].[Voters] ([ID])
GO

ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_Voters]
GO

USE [master]
GO

ALTER DATABASE [Voting] SET READ_WRITE 
GO
