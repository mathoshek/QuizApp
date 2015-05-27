
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/27/2015 19:45:20
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

IF OBJECT_ID(N'[dbo].[FK_Quiz_QuizQuestionDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quiz] DROP CONSTRAINT [FK_Quiz_QuizQuestionDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizQuestion_QuizQuestionDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestion] DROP CONSTRAINT [FK_QuizQuestion_QuizQuestionDomain];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Quiz]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quiz];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestion];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestionDomain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestionDomain];
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
    [NumQuestions] int  NOT NULL,
    [DomainId] int  NOT NULL
);
GO

-- Creating table 'QuizQuestions'
CREATE TABLE [dbo].[QuizQuestions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionText] varchar(max)  NOT NULL,
    [DomainId] int  NOT NULL,
    [IsSingleChoice] bit  NOT NULL,
    [Answer1Text] varchar(max)  NOT NULL,
    [Answer1Correct] bit  NOT NULL,
    [Answer2Text] varchar(max)  NOT NULL,
    [Answer2Correct] bit  NOT NULL,
    [Answer3Text] varchar(max)  NOT NULL,
    [Answer3Correct] bit  NOT NULL
);
GO

-- Creating table 'QuizQuestionDomains'
CREATE TABLE [dbo].[QuizQuestionDomains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'QuizQuestions'
ALTER TABLE [dbo].[QuizQuestions]
ADD CONSTRAINT [PK_QuizQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestionDomains'
ALTER TABLE [dbo].[QuizQuestionDomains]
ADD CONSTRAINT [PK_QuizQuestionDomains]
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

-- Creating foreign key on [DomainId] in table 'Quizs'
ALTER TABLE [dbo].[Quizs]
ADD CONSTRAINT [FK_Quiz_QuizQuestionDomain]
    FOREIGN KEY ([DomainId])
    REFERENCES [dbo].[QuizQuestionDomains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Quiz_QuizQuestionDomain'
CREATE INDEX [IX_FK_Quiz_QuizQuestionDomain]
ON [dbo].[Quizs]
    ([DomainId]);
GO

-- Creating foreign key on [DomainId] in table 'QuizQuestions'
ALTER TABLE [dbo].[QuizQuestions]
ADD CONSTRAINT [FK_QuizQuestion_QuizQuestionDomain]
    FOREIGN KEY ([DomainId])
    REFERENCES [dbo].[QuizQuestionDomains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizQuestion_QuizQuestionDomain'
CREATE INDEX [IX_FK_QuizQuestion_QuizQuestionDomain]
ON [dbo].[QuizQuestions]
    ([DomainId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------