﻿CREATE TABLE [IM].[IM_OU] (
    [ID]          VARCHAR (36)   NOT NULL,
    [NAME]        NVARCHAR (150) NULL,
    [COMPANY_ID]  VARCHAR (36)   NOT NULL,
	[CODE]		  NVARCHAR(50)	 NULL,
    [CREATEDBY]   NVARCHAR (200) NOT NULL,
    [CREATEDDATE] DATETIME       NOT NULL,
    [UPDATEDBY]   NVARCHAR (200) NULL,
    [UPDATEDDATE] DATETIME       NULL,
    [ACTIVE]      BIT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([COMPANY_ID]) REFERENCES [IM].[IM_COMPANY] ([ID])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_IM_OU_COMPANY_ID_CODE ON [IM].[IM_OU] (COMPANY_ID, CODE)
