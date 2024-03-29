
SET IDENTITY_INSERT Elections ON

INSERT [dbo].[Elections] ([ID], [ElectionName], [ElectionLogoUrl], [OpenDate], [CloseDate], [LoginScreenOpenMessage], [LoginScreenCloseMessage], [LoginIDLabelTxt], [LoginPINLabelTxt], [LandingPageTitle], [LandingPageMessage], [IVRPhoneNumber]) 
	VALUES (1, N'2019 Member-at-Large Board Election', NULL, CAST(N'2019-05-06T00:00:00.000' AS DateTime), CAST(N'2019-05-24T15:29:59.000' AS DateTime), N'To begin the voting process, locate your Personal Identification Number (PIN) on your ballot package received by mail. See example (<a href=\"http://www.pdf995.com/samples/pdf.pdf\" target=\"_blank\" >PDF</a>)<BR /><BR />Call the Everyone Counts/IVS Customer Service Line at (888) 492-4763 if you need assistance locating your PIN.', N'Sorry dude, Voting is like, totally closed and stuff.', N'PIN (Required)', N'Last 4 digits of Social Security number (required)', N'Success!', N'Landing Page MESSAGE', N'2065551212')
GO

INSERT [dbo].[Elections] ([ID], [ElectionName], [ElectionLogoUrl], [OpenDate], [CloseDate], [LoginScreenOpenMessage], [LoginScreenCloseMessage], [LoginIDLabelTxt], [LoginPINLabelTxt], [LandingPageTitle], [LandingPageMessage], [IVRPhoneNumber]) 
	VALUES (2, N'Test 2nd Election', NULL, CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-05-06T11:59:59.000' AS DateTime), N'To begin the voting process, locate your Personal Identification Number (PIN) on your ballot package received by mail. See example (<a href=\"http://www.pdf995.com/samples/pdf.pdf\" target=\"_blank\" >PDF</a>)<BR /><BR />Call the Everyone Counts/IVS Customer Service Line at (888) 492-4763 if you need assistance locating your PIN.', N'TEST LoginScreenCloseMessage', N'PIN (Required)', N'Last 4 digits of Social Security number (required)', N'Success!', N'Okay, awesome. You''re totally logged in! O.M.G. ;)', N'2065551234')
GO

SET IDENTITY_INSERT Elections OFF
GO


SET IDENTITY_INSERT BallotType ON

INSERT [BallotType] ([Id], [ElectionId]) VALUES (1, 1)
GO
INSERT [BallotType] ([Id], [ElectionId]) VALUES (2, 2)
GO

SET IDENTITY_INSERT BallotType OFF
GO


SET IDENTITY_INSERT Contests ON

INSERT [dbo].[Contests] ([ID], [BallotTypeID], [Title], [Description], [MaxVotes], [SortOrder]) VALUES (1, 1, N'Position A', N'blank for CalPERS', 2, 1)
GO

INSERT [dbo].[Contests] ([ID], [BallotTypeID], [Title], [Description], [MaxVotes], [SortOrder]) VALUES (2, 1, N'Position B', NULL, 2, 2)
GO

INSERT [dbo].[Contests] ([ID], [BallotTypeID], [Title], [Description], [MaxVotes], [SortOrder]) VALUES (3, 2, N'Bank CEO', NULL, 1, 2)
GO

INSERT [dbo].[Contests] ([ID], [BallotTypeID], [Title], [Description], [MaxVotes], [SortOrder]) VALUES (4, 2, N'Bank Chairman', NULL, 1, 1)
GO

SET IDENTITY_INSERT Contests OFF
GO


SET IDENTITY_INSERT BallotTypeMapping ON

INSERT [dbo].[BallotTypeMapping] ([ID], [BallotTypeID], [ContestID], [SortOrder]) VALUES (1, 1, 1, NULL)
GO
INSERT [dbo].[BallotTypeMapping] ([ID], [BallotTypeID], [ContestID], [SortOrder]) VALUES (2, 2, 2, NULL)
GO

SET IDENTITY_INSERT BallotTypeMapping OFF
GO


SET IDENTITY_INSERT Candidates ON

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (1, 1, 1, N'John Doe', N'Traffic Officer', N'This is where the Candidate''s looooooooooooooonnnnnnnnnnnnnong description goes.', 1)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (2, 1, 1, N'Elizabeth Banks', N'Total Hottie', N'One of the most awesome actresses of our time!', 2)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (3, 1, 1, N'Jack Smith', N'Certificed Public Accountant', N'I like, do people''s taxes and stuff.', 3)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (4, 1, 1, N'Mary Jones', N'Investment Officer', N'I help people figure out which stocks they should buy.. Stuff like that.', 4)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (5, 1, 1, N'Henry Brown', N'Budget Analyst', N'I help companies figure out how to effeciently spend their moneies.', 5)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (10, 1, 2, N'Bob Smith', N'Car Dealer', N'This is where the Candidate''s looooooooooooooonnnnnnnnnnnnnong description goes.', 5)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (11, 1, 2, N'Chevel Shepard', N'Country Singer', N'Has a very Old Country voice.. sounds like Loretta Lynn, etc.', 4)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (12, 1, 2, N'Yolandi Visser', N'Certificed Public Accountant', N'I like, do people''s taxes and stuff.', 2)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (13, 1, 2, N'Andy Johnson', N'Investment Officer', N'I help people figure out which stocks they should buy.. Stuff like that.', 3)
GO

INSERT [dbo].[Candidates] ([ID], [ElectionID], [ContestID], [CandidateName], [ShortDescription], [LongDescription], [SortOrder]) VALUES (14, 1, 2, N'Hailey Dunphy', N'Retail Clerk', N'I help people buy stuff, and stuff.', 1)
GO

SET IDENTITY_INSERT Candidates OFF
GO


SET IDENTITY_INSERT LoginAttempts ON

INSERT [dbo].[LoginAttempts] ([ID], [UserIP], [BrowserAgent], [TimeStamp], [EnteredLoginID], [SuccessfulLogin]) VALUES (1, N'1.2.3.4', N'NoneYa', CAST(N'2019-05-02T09:56:00.000' AS DateTime), N'98766780', 0)
GO

SET IDENTITY_INSERT LoginAttempts OFF
GO


SET IDENTITY_INSERT Voters ON

INSERT [dbo].[Voters] ([ID], [VoterName], [Affiliation], [ElectionID], [BallotType], [LoginID], [LoginPIN], [VoteCompleted]) VALUES (1, N'Siouxsie Sioux', N'Democrate', 1, 1, N'22222222', N'3456', 0)
GO

INSERT [dbo].[Voters] ([ID], [VoterName], [Affiliation], [ElectionID], [BallotType], [LoginID], [LoginPIN], [VoteCompleted]) VALUES (2, N'Budgie', N'Drums', 1, 1, N'98766789', N'1234', 0)
GO

INSERT [dbo].[Voters] ([ID], [VoterName], [Affiliation], [ElectionID], [BallotType], [LoginID], [LoginPIN], [VoteCompleted]) VALUES (3, N'Steve Sevren', N'Bass Player', 1, 1, N'54321023', N'4321', 0)
GO

INSERT [dbo].[Voters] ([ID], [VoterName], [Affiliation], [ElectionID], [BallotType], [LoginID], [LoginPIN], [VoteCompleted]) VALUES (4, N'Rick Wakeman', N'Keyboards', 2, 2, N'98273498', N'3984', 0)
GO

INSERT [dbo].[Voters] ([ID], [VoterName], [Affiliation], [ElectionID], [BallotType], [LoginID], [LoginPIN], [VoteCompleted]) VALUES (5, N'Jon Anderson', N'Vocalist', 2, 2, N'37885663', N'8376', 0)
GO

INSERT [dbo].[Voters] ([ID], [VoterName], [Affiliation], [ElectionID], [BallotType], [LoginID], [LoginPIN], [VoteCompleted]) VALUES (6, N'Budgie', N'Drums', 2, 2, N'98766789', N'1234', 0)
GO

SET IDENTITY_INSERT Voters OFF
GO
