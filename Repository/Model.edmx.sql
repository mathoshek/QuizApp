
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2015 21:14:19
-- Generated from EDMX file: E:\Programming\QuizApp\Repository\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QuizApp];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Quiz_QuizQuestion_Quiz]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quiz_QuizQuestion] DROP CONSTRAINT [FK_Quiz_QuizQuestion_Quiz];
GO
IF OBJECT_ID(N'[dbo].[FK_Quiz_QuizQuestion_QuizQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quiz_QuizQuestion] DROP CONSTRAINT [FK_Quiz_QuizQuestion_QuizQuestion];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Quiz]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quiz];
GO
IF OBJECT_ID(N'[dbo].[Quiz_QuizQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quiz_QuizQuestion];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestion];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Quizs'
CREATE TABLE [dbo].[Quizs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuizTitle] varchar(max)  NOT NULL,
    [Time] int  NOT NULL,
    [PassingScore] float  NOT NULL,
    [Questions] int  NOT NULL
);
GO

-- Creating table 'Quiz_QuizQuestion'
CREATE TABLE [dbo].[Quiz_QuizQuestion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuizId] int  NOT NULL,
    [QuizQuestionId] int  NOT NULL
);
GO

-- Creating table 'QuizQuestions'
CREATE TABLE [dbo].[QuizQuestions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionText] varchar(max)  NOT NULL,
    [Answer1Text] varchar(max)  NOT NULL,
    [Answer1Correct] bit  NOT NULL,
    [Answer2Text] varchar(max)  NOT NULL,
    [Answer2Correct] bit  NOT NULL,
    [Answer3Text] varchar(max)  NOT NULL,
    [Answer3Correct] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(max)  NOT NULL,
    [Password] varchar(max)  NOT NULL,
    [Type] varchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Quizs'
ALTER TABLE [dbo].[Quizs]
ADD CONSTRAINT [PK_Quizs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Quiz_QuizQuestion'
ALTER TABLE [dbo].[Quiz_QuizQuestion]
ADD CONSTRAINT [PK_Quiz_QuizQuestion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestions'
ALTER TABLE [dbo].[QuizQuestions]
ADD CONSTRAINT [PK_QuizQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [QuizId] in table 'Quiz_QuizQuestion'
ALTER TABLE [dbo].[Quiz_QuizQuestion]
ADD CONSTRAINT [FK_Quiz_QuizQuestion_Quiz]
    FOREIGN KEY ([QuizId])
    REFERENCES [dbo].[Quizs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Quiz_QuizQuestion_Quiz'
CREATE INDEX [IX_FK_Quiz_QuizQuestion_Quiz]
ON [dbo].[Quiz_QuizQuestion]
    ([QuizId]);
GO

-- Creating foreign key on [QuizQuestionId] in table 'Quiz_QuizQuestion'
ALTER TABLE [dbo].[Quiz_QuizQuestion]
ADD CONSTRAINT [FK_Quiz_QuizQuestion_QuizQuestion]
    FOREIGN KEY ([QuizQuestionId])
    REFERENCES [dbo].[QuizQuestions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Quiz_QuizQuestion_QuizQuestion'
CREATE INDEX [IX_FK_Quiz_QuizQuestion_QuizQuestion]
ON [dbo].[Quiz_QuizQuestion]
    ([QuizQuestionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------