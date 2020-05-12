-- COMPANY
IF NOT EXISTS(SELECT 1 FROM [IM].[IM_COMPANY] WHERE [DOMAIN] = N'ACT')
	INSERT [IM].[IM_COMPANY] ([ID], [NAME], [ADDRESS], [ADDRESS1], [ADDRESS2], [PHONENUMBER], [FAXNUMBER], [DOMAIN], [TRADELICENSE], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE], [ACTIVE]) VALUES (N'b7e25529-6a4a-41bb-8326-0a569137a0ab', N'Acme Transports LLC', N'Florida, US ', N'Alpha St.', NULL, N'1600123124', N'+166598745', N'ACT', N'CN-123-456', N'SYS-ADMIN', GETDATE(), NULL, NULL, 1)
	GO

-- ORG. UNIT
IF NOT EXISTS(SELECT 1 FROM [IM].[IM_OU] WHERE [CODE] = N'ADMIN-DEPT')
	INSERT [IM].[IM_OU] ([ID], [NAME], [COMPANY_ID], [CODE], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE], [ACTIVE]) VALUES (N'1a386b7f-0676-48a4-8bb8-8ccdb6e89bd7', N'Admin Dept.', N'b7e25529-6a4a-41bb-8326-0a569137a0ab', N'ADMIN-DEPT', N'SYS-ADMIN', GETDATE(), NULL, NULL, 1)
	GO



--USER
IF NOT EXISTS(SELECT 1 FROM [IM].[IM_USERS] WHERE [USERNAME] = N'admin')
	INSERT [IM].[IM_USERS] ([ID], [FIRSTNAME], [USERNAME], [USEREMAIL], [PASSWORD], [TELEPHONE], [MOBILE], [ADDRESS], [ADDRESS1], [ADDRESS2], [REMARKS], [ISINTERNAL], [ACTIVE], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) VALUES (N'ef0ab1a8-2541-4839-b04d-788a99d827c8', N'Admin', N'admin', N'admin@fleetmgmt.com', N'161ebd7d45089b3446ee4e0d86dbcf92', N'1234567890', N'1234567890', N'Home', N'Home', N'Florida', N'Admin User Added', 0, 1, N'ADMIN', GETDATE(), NULL, NULL)

IF NOT EXISTS(SELECT 1 FROM [IM].[IM_USERS] WHERE [USERNAME] = N'serviceaccount')
	INSERT [IM].[IM_USERS] ([ID], [FIRSTNAME], [USERNAME], [USEREMAIL], [PASSWORD], [TELEPHONE], [MOBILE], [ADDRESS], [ADDRESS1], [ADDRESS2], [REMARKS], [ISINTERNAL], [ACTIVE], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) VALUES (N'09E0AD01-F8E1-4560-9EE9-5951B0DB95C2', N'Service Account', N'serviceaccount', N'serviceaccount@fleetmgmt.com', N'161ebd7d45089b3446ee4e0d86dbcf92', N'1234567890', N'1234567890', N'Home', N'Florida', N'Florida', N'Admin User Added', 0, 1, N'ADMIN', GETDATE(), NULL, NULL)


--GROUP 
IF NOT EXISTS(SELECT 1 FROM [IM].[IM_GROUPS] WHERE [NAME] = N'ADMINGROUP')
	INSERT [IM].[IM_GROUPS] ([ID], [NAME], [REMARKS], [ACTIVE], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) VALUES (N'c8375bb6-9dec-420f-b6da-0b77a5a0da15', N'ADMINGROUP', N'Admin', 1, N'ADMIN', getdate(), NULL, NULL)

IF NOT EXISTS(SELECT 1 FROM [IM].[IM_GROUPS] WHERE [NAME] = N'SVCA')
	INSERT [IM].[IM_GROUPS] ([ID], [NAME], [REMARKS], [ACTIVE], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) VALUES (N'a5e9824d-c981-4e5f-b2cf-ee31e4561ae9', N'SVCA', N'Service Account', 1, N'ADMIN', getdate(), NULL, NULL)

--ASSIGN USER GROUP
IF NOT EXISTS(SELECT 1 FROM [IM].[IM_USERS_GROUPS] WHERE [USER_NAME] = 'admin' AND [GROUP_ID] = N'c8375bb6-9dec-420f-b6da-0b77a5a0da15')
	INSERT [IM].[IM_USERS_GROUPS] ([ID], [GROUP_ID], [USER_NAME], [ACTIVE], [REMARKS], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) VALUES (N'1d1523f2-6737-4ec8-97e8-d350193ecc60', N'c8375bb6-9dec-420f-b6da-0b77a5a0da15', N'admin', 1, NULL, N'admin', GETDATE(), NULL, NULL)

IF NOT EXISTS(SELECT 1 FROM [IM].[IM_USERS_GROUPS] WHERE [USER_NAME] = 'serviceaccount' AND [GROUP_ID] = N'a5e9824d-c981-4e5f-b2cf-ee31e4561ae9')
	INSERT [IM].[IM_USERS_GROUPS] ([ID], [GROUP_ID], [USER_NAME], [ACTIVE], [REMARKS], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) VALUES (N'D48FA621-6ED5-4185-AA42-86959BDFC994', N'a5e9824d-c981-4e5f-b2cf-ee31e4561ae9', N'serviceaccount', 1, NULL, N'service Account', GETDATE(), NULL, NULL)


-- CREATE CONTRIBUTOR GROUP
 IF NOT EXISTS(SELECT 1 FROM [IM].[IM_GROUPS] WHERE [NAME] = 'CONTRIBUTORS')
	INSERT INTO IM.IM_GROUPS (ID, [NAME], REMARKS, ACTIVE, CREATEDBY, CREATEDDATE)
	VALUES ('4E0CECED-0E58-4377-BCB8-B06FB4B3D809', 'CONTRIBUTORS', 'CAN CREATE/UPDATE RECORDS', 1, 'ADMIN', GETDATE())

IF NOT EXISTS(SELECT 1 FROM IM.IM_GROUPS_OU WHERE GROUP_ID = '4E0CECED-0E58-4377-BCB8-B06FB4B3D809' AND OU_ID = '1a386b7f-0676-48a4-8bb8-8ccdb6e89bd7')
	INSERT INTO IM.IM_GROUPS_OU (ID, GROUP_ID, OU_ID, CREATEDDATE, CREATEDBY, ACTIVE)
	VALUES (NEWID(), '4E0CECED-0E58-4377-BCB8-B06FB4B3D809', '1a386b7f-0676-48a4-8bb8-8ccdb6e89bd7', GETDATE(), 'ADMIN', 1)


-- EMAIL TEMPLATES
IF NOT EXISTS(SELECT 1 FROM IM.IM_TEMPLATE_SETTING WHERE [KEY] = 'FORGOT_PASSWORD_TEMPLATE')
    Insert into IM.IM_TEMPLATE_SETTING (ID, [NAME], [KEY], [VALUE], ACTIVE, [DESCRIPTION], [CREATEDBY], [CREATEDDATE])
    values ('9DBEC1D1-0AB0-4570-A8AA-29D734074680','Forgot Password - Fleet Management','FORGOT_PASSWORD_TEMPLATE',
    '<tbody>
       <tr>
          <td style="padding:14pt 0px 18px 0px; line-height:22px; text-align:inherit;" height="100%" valign="top" bgcolor="" role="module-content">
             <div>
                <div style="font-family: inherit; text-align: inherit">Dear Maqta {username},</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">To reset your Password, please <a href="{link}">Click Here</a> or use the below link.</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">{link}</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">The Password reset links are time-sensitive and will expire after 1 hour.</div>
                <div style="font-family: inherit; text-align: inherit">If you have not requested to reset your password, your account may have been compromised.</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><strong>Best Regards,</strong></div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">Fleet Management Admin</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><span style="color: #f60404">Please Note: This is system generated mail. Do not reply.</span></div>
                <div></div>
             </div>
          </td>
       </tr>
    </tbody>',
    1,'no-reply.test@sys.maqta.ae',N'ADMIN',GETDATE());

IF NOT EXISTS(SELECT 1 FROM IM.IM_TEMPLATE_SETTING WHERE [KEY] = 'RESET_PASSWORD_TEMPLATE')
    Insert into IM.IM_TEMPLATE_SETTING (ID, [NAME], [KEY], [VALUE], ACTIVE, [DESCRIPTION], [CREATEDBY], [CREATEDDATE])
    values ('F8F0EEDA-A25C-43D2-BDEA-65ACE9136322','Reset Password - Fleet Management','RESET_PASSWORD_TEMPLATE',
    '<tbody>
       <tr>
          <td style="padding:14pt 0px 18px 0px; line-height:22px; text-align:inherit;" height="100%" valign="top" bgcolor="" role="module-content">
             <div>
                <div style="font-family: inherit; text-align: inherit">Dear {username},</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">To reset your Password, please <a href="{link}">Click Here</a> or use the below link.</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">{link}</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">The Password reset links are time-sensitive and will expire after 1 hour.</div>
                <div style="font-family: inherit; text-align: inherit">If you have not requested to reset your password, your account may have been compromised.</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><strong>Best Regards,</strong></div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">Fleet Management Admin</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><span style="color: #f60404">Please Note: This is system generated mail. Do not reply.</span></div>
                <div></div>
             </div>
          </td>
       </tr>
    </tbody>'
    ,1,'no-reply.test@sys.maqta.ae',N'ADMIN',GETDATE());

IF NOT EXISTS(SELECT 1 FROM IM.IM_TEMPLATE_SETTING WHERE [KEY] = 'NEW_USER_TEMPLATE')
    INSERT [IM].[IM_TEMPLATE_SETTING] ([ID], [NAME], [KEY], [VALUE], [ACTIVE], [DESCRIPTION], [CREATEDBY], [CREATEDDATE], [UPDATEDBY], [UPDATEDDATE]) 
    VALUES (N'C1F10A91-42FC-4CC3-ADF6-CCF35431AAFD', N'New User - Fleet Management', N'NEW_USER_TEMPLATE', N'<tbody>
       <tr>
          <td style="padding:14pt 0px 18px 0px; line-height:22px; text-align:inherit;" height="100%" valign="top" bgcolor="" role="module-content">
             <div>
                <div style="font-family: inherit; text-align: inherit">Dear User,</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">User {username} has been created successfully.</div>
                <div style="font-family: inherit; text-align: inherit"><br><a href="{link}">Click Here</a> to login to the application</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><strong>Best Regards,</strong></div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit">Fleet Management Admin</div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><br></div>
                <div style="font-family: inherit; text-align: inherit"><span style="color: #f60404">Please Note: This is system generated mail. Do not reply.</span></div>
                <div></div>
             </div>
          </td>
       </tr>
    </tbody>', 1, N'MAQTA GATEWAY', N'ADMIN', CAST(N'2020-05-05T10:27:32.590' AS DateTime), NULL, NULL)
GO