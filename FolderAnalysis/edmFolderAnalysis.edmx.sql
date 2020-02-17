
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/19/2015 14:11:48
-- Generated from EDMX file: C:\Users\osp40a\OneDrive\Development\FolderAnalysis\FolderAnalysis\edmFolderAnalysis.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [folder_analysis];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_filesattribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[attributes] DROP CONSTRAINT [FK_filesattribute];
GO
IF OBJECT_ID(N'[dbo].[FK_sessionfiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[files] DROP CONSTRAINT [FK_sessionfiles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[files];
GO
IF OBJECT_ID(N'[dbo].[sessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sessions];
GO
IF OBJECT_ID(N'[dbo].[attributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[attributes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'files'
CREATE TABLE [dbo].[files] (
    [filesid] uniqueidentifier  NOT NULL,
    [file_name_full] nvarchar(max)  NOT NULL,
    [folder] nvarchar(max)  NOT NULL,
    [file] nvarchar(max)  NOT NULL,
    [session_sessionid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'sessions'
CREATE TABLE [dbo].[sessions] (
    [sessionid] uniqueidentifier  NOT NULL,
    [run_time] datetime  NOT NULL,
    [root] nvarchar(max)  NOT NULL,
    [completed] bit  NOT NULL
);
GO

-- Creating table 'attributes'
CREATE TABLE [dbo].[attributes] (
    [attributeid] uniqueidentifier  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [value] nvarchar(max)  NOT NULL,
    [files_filesid] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [filesid] in table 'files'
ALTER TABLE [dbo].[files]
ADD CONSTRAINT [PK_files]
    PRIMARY KEY CLUSTERED ([filesid] ASC);
GO

-- Creating primary key on [sessionid] in table 'sessions'
ALTER TABLE [dbo].[sessions]
ADD CONSTRAINT [PK_sessions]
    PRIMARY KEY CLUSTERED ([sessionid] ASC);
GO

-- Creating primary key on [attributeid] in table 'attributes'
ALTER TABLE [dbo].[attributes]
ADD CONSTRAINT [PK_attributes]
    PRIMARY KEY CLUSTERED ([attributeid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [files_filesid] in table 'attributes'
ALTER TABLE [dbo].[attributes]
ADD CONSTRAINT [FK_filesattribute]
    FOREIGN KEY ([files_filesid])
    REFERENCES [dbo].[files]
        ([filesid])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_filesattribute'
CREATE INDEX [IX_FK_filesattribute]
ON [dbo].[attributes]
    ([files_filesid]);
GO

-- Creating foreign key on [session_sessionid] in table 'files'
ALTER TABLE [dbo].[files]
ADD CONSTRAINT [FK_sessionfiles]
    FOREIGN KEY ([session_sessionid])
    REFERENCES [dbo].[sessions]
        ([sessionid])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_sessionfiles'
CREATE INDEX [IX_FK_sessionfiles]
ON [dbo].[files]
    ([session_sessionid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------