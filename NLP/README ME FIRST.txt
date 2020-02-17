To All,

This is a very simple sample Add, Edit, Delete, and Search code for VB.Net and MS SQL server.

Before you begin do this first:
(I assume that you have MS SQL Server 2005 Express or Standard Edition installed in your PC.)

1. Open your SQL Server Management Studio (If you don't have server management studio, you can download it here http://www.microsoft.com/download/en/details.aspx?id=8961)

2. Login to your server then make a new query
   (Database creation query)
Create Database db_trial_welch;

3. Run the query belowto create table
USE [db_trial_welch]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_info](
	[fldid] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[fldname] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[fldmname] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[fldlname] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[fldage] [int] NOT NULL,
	[ent_dt] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_info] PRIMARY KEY CLUSTERED 
(
	[fldid] DESC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];

4.  Now open the solution file 'A Simple Add Edit Delete.sln'

5.  Go to modConn module

6.  Edit the concDB procedure

If you have questions, send your message using this contact form
http://thisiswelch.hostei.com/index.php/contact-me

To find some tips and tutorials in vb programming; go to my website http://thisiswelch.hostei.com and you are also welcome to register.