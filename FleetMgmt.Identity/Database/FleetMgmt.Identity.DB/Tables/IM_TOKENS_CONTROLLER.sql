﻿CREATE TABLE [IM].[IM_TOKENS_CONTROLLER](
	[ID] [varchar](36) NOT NULL,
	[USER_NAME] [nvarchar](320) NOT NULL,
	[VALUE] [nvarchar](max) NOT NULL,
	[ISTOKENVALID] [bit] NOT NULL DEFAULT 1,
	[REMARKS] [nvarchar](255) NULL,
	[CREATEDBY] [nvarchar](200) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[UPDATEDBY] [nvarchar](200) NULL,
	[UPDATEDDATE] [datetime] NULL,
	PRIMARY KEY(ID)
	)
