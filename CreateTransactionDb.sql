CREATE DATABASE [TransactionDb]
GO

USE [TransactionDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaction](
	[Id] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CurrencyCode] [nvarchar](10) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionStatus](
	[Status] [nvarchar](10) NOT NULL,
	[Symbol] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_TransactionStatus] PRIMARY KEY CLUSTERED 
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


INSERT INTO [dbo].[TransactionStatus] ([Status], [Symbol]) VALUES(N'Approved', N'A')
INSERT INTO [dbo].[TransactionStatus] ([Status], [Symbol]) VALUES(N'Done', N'D')
INSERT INTO [dbo].[TransactionStatus] ([Status], [Symbol]) VALUES(N'Failed', N'R')
INSERT INTO [dbo].[TransactionStatus] ([Status], [Symbol]) VALUES(N'Finished', N'D')
INSERT INTO [dbo].[TransactionStatus] ([Status], [Symbol]) VALUES(N'Rejected', N'R')
GO