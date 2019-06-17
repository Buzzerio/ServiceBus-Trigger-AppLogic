/****** Object:  Table [dbo].[Test_Trigger]    Script Date: 14/06/2019 17:58:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Test_Trigger](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[campo] [nchar](50) NULL,
	[valore] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


