
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/29/2015 12:59:33
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
    ALTER TABLE [dbo].[Quizs] DROP CONSTRAINT [FK_Quiz_QuizQuestionDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizInstance_Quiz]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizInstances] DROP CONSTRAINT [FK_QuizInstance_Quiz];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizInstanceQuizQuestionInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestionInstances] DROP CONSTRAINT [FK_QuizInstanceQuizQuestionInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizQuestion_QuizQuestionDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestions] DROP CONSTRAINT [FK_QuizQuestion_QuizQuestionDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizQuestionInstance_QuizQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestionInstances] DROP CONSTRAINT [FK_QuizQuestionInstance_QuizQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_User_QuizInstance_QuizInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_QuizInstance] DROP CONSTRAINT [FK_User_QuizInstance_QuizInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_User_QuizInstance_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_QuizInstance] DROP CONSTRAINT [FK_User_QuizInstance_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[QuizInstances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizInstances];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestionDomains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestionDomains];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestionInstances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestionInstances];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestions];
GO
IF OBJECT_ID(N'[dbo].[Quizs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quizs];
GO
IF OBJECT_ID(N'[dbo].[User_QuizInstance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_QuizInstance];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'QuizInstances'
CREATE TABLE [dbo].[QuizInstances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuizId] int  NOT NULL,
    [IsStarted] bit  NOT NULL,
    [StartDate] datetime  NULL
);
GO

-- Creating table 'QuizQuestionDomains'
CREATE TABLE [dbo].[QuizQuestionDomains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NOT NULL
);
GO

-- Creating table 'QuizQuestionInstances'
CREATE TABLE [dbo].[QuizQuestionInstances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuizQuestionId] int  NOT NULL,
    [AnswerSaved] bit  NOT NULL,
    [Choice1] bit  NOT NULL,
    [Choice2] bit  NOT NULL,
    [Choice3] bit  NOT NULL,
    [QuizInstanceId] int  NOT NULL
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

-- Creating table 'User_QuizInstance'
CREATE TABLE [dbo].[User_QuizInstance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [QuizInstanceId] int  NOT NULL
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

-- Creating primary key on [Id] in table 'QuizInstances'
ALTER TABLE [dbo].[QuizInstances]
ADD CONSTRAINT [PK_QuizInstances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestionDomains'
ALTER TABLE [dbo].[QuizQuestionDomains]
ADD CONSTRAINT [PK_QuizQuestionDomains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestionInstances'
ALTER TABLE [dbo].[QuizQuestionInstances]
ADD CONSTRAINT [PK_QuizQuestionInstances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestions'
ALTER TABLE [dbo].[QuizQuestions]
ADD CONSTRAINT [PK_QuizQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Quizs'
ALTER TABLE [dbo].[Quizs]
ADD CONSTRAINT [PK_Quizs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User_QuizInstance'
ALTER TABLE [dbo].[User_QuizInstance]
ADD CONSTRAINT [PK_User_QuizInstance]
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

-- Creating foreign key on [QuizId] in table 'QuizInstances'
ALTER TABLE [dbo].[QuizInstances]
ADD CONSTRAINT [FK_QuizInstance_Quiz]
    FOREIGN KEY ([QuizId])
    REFERENCES [dbo].[Quizs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizInstance_Quiz'
CREATE INDEX [IX_FK_QuizInstance_Quiz]
ON [dbo].[QuizInstances]
    ([QuizId]);
GO

-- Creating foreign key on [QuizInstanceId] in table 'QuizQuestionInstances'
ALTER TABLE [dbo].[QuizQuestionInstances]
ADD CONSTRAINT [FK_QuizInstanceQuizQuestionInstance]
    FOREIGN KEY ([QuizInstanceId])
    REFERENCES [dbo].[QuizInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizInstanceQuizQuestionInstance'
CREATE INDEX [IX_FK_QuizInstanceQuizQuestionInstance]
ON [dbo].[QuizQuestionInstances]
    ([QuizInstanceId]);
GO

-- Creating foreign key on [QuizInstanceId] in table 'User_QuizInstance'
ALTER TABLE [dbo].[User_QuizInstance]
ADD CONSTRAINT [FK_User_QuizInstance_QuizInstance]
    FOREIGN KEY ([QuizInstanceId])
    REFERENCES [dbo].[QuizInstances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_QuizInstance_QuizInstance'
CREATE INDEX [IX_FK_User_QuizInstance_QuizInstance]
ON [dbo].[User_QuizInstance]
    ([QuizInstanceId]);
GO

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

-- Creating foreign key on [QuizQuestionId] in table 'QuizQuestionInstances'
ALTER TABLE [dbo].[QuizQuestionInstances]
ADD CONSTRAINT [FK_QuizQuestionInstance_QuizQuestion]
    FOREIGN KEY ([QuizQuestionId])
    REFERENCES [dbo].[QuizQuestions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizQuestionInstance_QuizQuestion'
CREATE INDEX [IX_FK_QuizQuestionInstance_QuizQuestion]
ON [dbo].[QuizQuestionInstances]
    ([QuizQuestionId]);
GO

-- Creating foreign key on [UserId] in table 'User_QuizInstance'
ALTER TABLE [dbo].[User_QuizInstance]
ADD CONSTRAINT [FK_User_QuizInstance_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_QuizInstance_User'
CREATE INDEX [IX_FK_User_QuizInstance_User]
ON [dbo].[User_QuizInstance]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------