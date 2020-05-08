﻿CREATE TABLE [IM].[IM_USERS_GROUPS] (
    [ID]          VARCHAR (36)   NOT NULL,
    [GROUP_ID]    VARCHAR (36)   NULL,
    [USER_NAME]   NVARCHAR (320) NOT NULL,
    [ACTIVE]      BIT            DEFAULT ((1)) NOT NULL,
    [REMARKS]     NVARCHAR (420) NULL,
    [CREATEDBY]   NVARCHAR (200) NOT NULL,
    [CREATEDDATE] DATETIME       NOT NULL,
    [UPDATEDBY]   NVARCHAR (200) NULL,
    [UPDATEDDATE] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([GROUP_ID]) REFERENCES [IM].[IM_GROUPS] ([ID]),
    CONSTRAINT [Unique_Constaint_IM_USERS_GROUPS] UNIQUE NONCLUSTERED ([GROUP_ID] ASC, [USER_NAME] ASC)
);

