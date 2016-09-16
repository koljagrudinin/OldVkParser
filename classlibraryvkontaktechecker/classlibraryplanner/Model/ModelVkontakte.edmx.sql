
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/08/2012 23:03:20
-- Generated from EDMX file: C:\cmdfiles\SVN\vkontakte-robot\ClassLibraryVkontakteChecker\ClassLibraryPlanner\Model\ModelVkontakte.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [vkontakteChecker];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GroupsTokenList_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupsTokenList] DROP CONSTRAINT [FK_GroupsTokenList_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsTokenList_TokenList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupsTokenList] DROP CONSTRAINT [FK_GroupsTokenList_TokenList];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentsUsers_Comments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsUsers] DROP CONSTRAINT [FK_CommentsUsers_Comments];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentsUsers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsUsers] DROP CONSTRAINT [FK_CommentsUsers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentsUsers1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsEnt] DROP CONSTRAINT [FK_CommentsUsers1];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsUsers_Posts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostsUsers] DROP CONSTRAINT [FK_PostsUsers_Posts];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsUsers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostsUsers] DROP CONSTRAINT [FK_PostsUsers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsUsers1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostsEnd] DROP CONSTRAINT [FK_PostsUsers1];
GO
IF OBJECT_ID(N'[dbo].[FK_ListsResultTableUsers_ListsResultTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListsResultTableUsers] DROP CONSTRAINT [FK_ListsResultTableUsers_ListsResultTable];
GO
IF OBJECT_ID(N'[dbo].[FK_ListsResultTableUsers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListsResultTableUsers] DROP CONSTRAINT [FK_ListsResultTableUsers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_OperationTableBasicMetricksResultTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BasicMetricksResultTableEnt] DROP CONSTRAINT [FK_OperationTableBasicMetricksResultTable];
GO
IF OBJECT_ID(N'[dbo].[FK_OperationTableListsResults]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListsResultTableEnt] DROP CONSTRAINT [FK_OperationTableListsResults];
GO
IF OBJECT_ID(N'[dbo].[FK_ErrorCodesOperations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OperationTableEnt] DROP CONSTRAINT [FK_ErrorCodesOperations];
GO
IF OBJECT_ID(N'[dbo].[FK_ErrorCodeStatesErrorCodes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ErrorCodesEnt] DROP CONSTRAINT [FK_ErrorCodeStatesErrorCodes];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsComments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsEnt] DROP CONSTRAINT [FK_PostsComments];
GO
IF OBJECT_ID(N'[dbo].[FK_ListsResultsPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostsEnd] DROP CONSTRAINT [FK_ListsResultsPosts];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TokenListEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TokenListEnt];
GO
IF OBJECT_ID(N'[dbo].[GroupsEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupsEnt];
GO
IF OBJECT_ID(N'[dbo].[BasicMetricksResultTableEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BasicMetricksResultTableEnt];
GO
IF OBJECT_ID(N'[dbo].[ListsResultTableEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListsResultTableEnt];
GO
IF OBJECT_ID(N'[dbo].[OperationTableEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OperationTableEnt];
GO
IF OBJECT_ID(N'[dbo].[UsersНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersНабор];
GO
IF OBJECT_ID(N'[dbo].[CommentsEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentsEnt];
GO
IF OBJECT_ID(N'[dbo].[PostsEnd]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostsEnd];
GO
IF OBJECT_ID(N'[dbo].[ErrorCodesEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ErrorCodesEnt];
GO
IF OBJECT_ID(N'[dbo].[ErrorCodeStatesEnt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ErrorCodeStatesEnt];
GO
IF OBJECT_ID(N'[dbo].[GroupsTokenList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupsTokenList];
GO
IF OBJECT_ID(N'[dbo].[CommentsUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentsUsers];
GO
IF OBJECT_ID(N'[dbo].[PostsUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostsUsers];
GO
IF OBJECT_ID(N'[dbo].[ListsResultTableUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListsResultTableUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TokenListEnt'
CREATE TABLE [dbo].[TokenListEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [SecretKey] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NULL
);
GO

-- Creating table 'GroupsEnt'
CREATE TABLE [dbo].[GroupsEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupId] nvarchar(max)  NOT NULL,
    [BasicMetrickRegularReading] int  NOT NULL,
    [BasicMetricksTimeOut] int  NOT NULL,
    [ListsRegulerReading] int  NOT NULL,
    [ListsTimeOut] int  NOT NULL
);
GO

-- Creating table 'BasicMetricksResultTableEnt'
CREATE TABLE [dbo].[BasicMetricksResultTableEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostCount] nvarchar(max)  NOT NULL,
    [AudioCount] nvarchar(max)  NOT NULL,
    [VideoCount] nvarchar(max)  NOT NULL,
    [MembersCount] nvarchar(max)  NOT NULL,
    [OperationTableBasicMetricksResultTable_BasicMetricksResultTable_Id] int  NOT NULL
);
GO

-- Creating table 'ListsResultTableEnt'
CREATE TABLE [dbo].[ListsResultTableEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OperationTableListsResults_ListsResults_Id] int  NOT NULL
);
GO

-- Creating table 'OperationTableEnt'
CREATE TABLE [dbo].[OperationTableEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Time] datetime  NOT NULL,
    [isSuccess] bit  NOT NULL,
    [GroupId] nvarchar(max)  NOT NULL,
    [ErrorCodes_Id] int  NULL
);
GO

-- Creating table 'UsersНабор'
CREATE TABLE [dbo].[UsersНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentsEnt'
CREATE TABLE [dbo].[CommentsEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CommentId] nvarchar(max)  NOT NULL,
    [UserAuthor_Id] int  NOT NULL,
    [Posts_Id] int  NOT NULL
);
GO

-- Creating table 'PostsEnd'
CREATE TABLE [dbo].[PostsEnd] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostId] nvarchar(max)  NOT NULL,
    [UserAuthor_Id] int  NOT NULL,
    [ListsResults_Id] int  NOT NULL
);
GO

-- Creating table 'ErrorCodesEnt'
CREATE TABLE [dbo].[ErrorCodesEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [MethodErrorId] int  NOT NULL,
    [ErrorCodeState_Id] int  NOT NULL
);
GO

-- Creating table 'ErrorCodeStatesEnt'
CREATE TABLE [dbo].[ErrorCodeStatesEnt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ErrorId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GroupsTokenList'
CREATE TABLE [dbo].[GroupsTokenList] (
    [Groups_Id] int  NOT NULL,
    [TokenList_Id] int  NOT NULL
);
GO

-- Creating table 'CommentsUsers'
CREATE TABLE [dbo].[CommentsUsers] (
    [LikedComments_Id] int  NOT NULL,
    [UsersLike_Id] int  NOT NULL
);
GO

-- Creating table 'PostsUsers'
CREATE TABLE [dbo].[PostsUsers] (
    [LikedPosts_Id] int  NOT NULL,
    [UsersLike_Id] int  NOT NULL
);
GO

-- Creating table 'ListsResultTableUsers'
CREATE TABLE [dbo].[ListsResultTableUsers] (
    [ListsResultTables_Id] int  NOT NULL,
    [GroupMembers_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TokenListEnt'
ALTER TABLE [dbo].[TokenListEnt]
ADD CONSTRAINT [PK_TokenListEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupsEnt'
ALTER TABLE [dbo].[GroupsEnt]
ADD CONSTRAINT [PK_GroupsEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BasicMetricksResultTableEnt'
ALTER TABLE [dbo].[BasicMetricksResultTableEnt]
ADD CONSTRAINT [PK_BasicMetricksResultTableEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ListsResultTableEnt'
ALTER TABLE [dbo].[ListsResultTableEnt]
ADD CONSTRAINT [PK_ListsResultTableEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OperationTableEnt'
ALTER TABLE [dbo].[OperationTableEnt]
ADD CONSTRAINT [PK_OperationTableEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersНабор'
ALTER TABLE [dbo].[UsersНабор]
ADD CONSTRAINT [PK_UsersНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentsEnt'
ALTER TABLE [dbo].[CommentsEnt]
ADD CONSTRAINT [PK_CommentsEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PostsEnd'
ALTER TABLE [dbo].[PostsEnd]
ADD CONSTRAINT [PK_PostsEnd]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ErrorCodesEnt'
ALTER TABLE [dbo].[ErrorCodesEnt]
ADD CONSTRAINT [PK_ErrorCodesEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ErrorCodeStatesEnt'
ALTER TABLE [dbo].[ErrorCodeStatesEnt]
ADD CONSTRAINT [PK_ErrorCodeStatesEnt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Groups_Id], [TokenList_Id] in table 'GroupsTokenList'
ALTER TABLE [dbo].[GroupsTokenList]
ADD CONSTRAINT [PK_GroupsTokenList]
    PRIMARY KEY NONCLUSTERED ([Groups_Id], [TokenList_Id] ASC);
GO

-- Creating primary key on [LikedComments_Id], [UsersLike_Id] in table 'CommentsUsers'
ALTER TABLE [dbo].[CommentsUsers]
ADD CONSTRAINT [PK_CommentsUsers]
    PRIMARY KEY NONCLUSTERED ([LikedComments_Id], [UsersLike_Id] ASC);
GO

-- Creating primary key on [LikedPosts_Id], [UsersLike_Id] in table 'PostsUsers'
ALTER TABLE [dbo].[PostsUsers]
ADD CONSTRAINT [PK_PostsUsers]
    PRIMARY KEY NONCLUSTERED ([LikedPosts_Id], [UsersLike_Id] ASC);
GO

-- Creating primary key on [ListsResultTables_Id], [GroupMembers_Id] in table 'ListsResultTableUsers'
ALTER TABLE [dbo].[ListsResultTableUsers]
ADD CONSTRAINT [PK_ListsResultTableUsers]
    PRIMARY KEY NONCLUSTERED ([ListsResultTables_Id], [GroupMembers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Groups_Id] in table 'GroupsTokenList'
ALTER TABLE [dbo].[GroupsTokenList]
ADD CONSTRAINT [FK_GroupsTokenList_Groups]
    FOREIGN KEY ([Groups_Id])
    REFERENCES [dbo].[GroupsEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TokenList_Id] in table 'GroupsTokenList'
ALTER TABLE [dbo].[GroupsTokenList]
ADD CONSTRAINT [FK_GroupsTokenList_TokenList]
    FOREIGN KEY ([TokenList_Id])
    REFERENCES [dbo].[TokenListEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsTokenList_TokenList'
CREATE INDEX [IX_FK_GroupsTokenList_TokenList]
ON [dbo].[GroupsTokenList]
    ([TokenList_Id]);
GO

-- Creating foreign key on [LikedComments_Id] in table 'CommentsUsers'
ALTER TABLE [dbo].[CommentsUsers]
ADD CONSTRAINT [FK_CommentsUsers_Comments]
    FOREIGN KEY ([LikedComments_Id])
    REFERENCES [dbo].[CommentsEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UsersLike_Id] in table 'CommentsUsers'
ALTER TABLE [dbo].[CommentsUsers]
ADD CONSTRAINT [FK_CommentsUsers_Users]
    FOREIGN KEY ([UsersLike_Id])
    REFERENCES [dbo].[UsersНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentsUsers_Users'
CREATE INDEX [IX_FK_CommentsUsers_Users]
ON [dbo].[CommentsUsers]
    ([UsersLike_Id]);
GO

-- Creating foreign key on [UserAuthor_Id] in table 'CommentsEnt'
ALTER TABLE [dbo].[CommentsEnt]
ADD CONSTRAINT [FK_CommentsUsers1]
    FOREIGN KEY ([UserAuthor_Id])
    REFERENCES [dbo].[UsersНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentsUsers1'
CREATE INDEX [IX_FK_CommentsUsers1]
ON [dbo].[CommentsEnt]
    ([UserAuthor_Id]);
GO

-- Creating foreign key on [LikedPosts_Id] in table 'PostsUsers'
ALTER TABLE [dbo].[PostsUsers]
ADD CONSTRAINT [FK_PostsUsers_Posts]
    FOREIGN KEY ([LikedPosts_Id])
    REFERENCES [dbo].[PostsEnd]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UsersLike_Id] in table 'PostsUsers'
ALTER TABLE [dbo].[PostsUsers]
ADD CONSTRAINT [FK_PostsUsers_Users]
    FOREIGN KEY ([UsersLike_Id])
    REFERENCES [dbo].[UsersНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsUsers_Users'
CREATE INDEX [IX_FK_PostsUsers_Users]
ON [dbo].[PostsUsers]
    ([UsersLike_Id]);
GO

-- Creating foreign key on [UserAuthor_Id] in table 'PostsEnd'
ALTER TABLE [dbo].[PostsEnd]
ADD CONSTRAINT [FK_PostsUsers1]
    FOREIGN KEY ([UserAuthor_Id])
    REFERENCES [dbo].[UsersНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsUsers1'
CREATE INDEX [IX_FK_PostsUsers1]
ON [dbo].[PostsEnd]
    ([UserAuthor_Id]);
GO

-- Creating foreign key on [ListsResultTables_Id] in table 'ListsResultTableUsers'
ALTER TABLE [dbo].[ListsResultTableUsers]
ADD CONSTRAINT [FK_ListsResultTableUsers_ListsResultTable]
    FOREIGN KEY ([ListsResultTables_Id])
    REFERENCES [dbo].[ListsResultTableEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GroupMembers_Id] in table 'ListsResultTableUsers'
ALTER TABLE [dbo].[ListsResultTableUsers]
ADD CONSTRAINT [FK_ListsResultTableUsers_Users]
    FOREIGN KEY ([GroupMembers_Id])
    REFERENCES [dbo].[UsersНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ListsResultTableUsers_Users'
CREATE INDEX [IX_FK_ListsResultTableUsers_Users]
ON [dbo].[ListsResultTableUsers]
    ([GroupMembers_Id]);
GO

-- Creating foreign key on [OperationTableBasicMetricksResultTable_BasicMetricksResultTable_Id] in table 'BasicMetricksResultTableEnt'
ALTER TABLE [dbo].[BasicMetricksResultTableEnt]
ADD CONSTRAINT [FK_OperationTableBasicMetricksResultTable]
    FOREIGN KEY ([OperationTableBasicMetricksResultTable_BasicMetricksResultTable_Id])
    REFERENCES [dbo].[OperationTableEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OperationTableBasicMetricksResultTable'
CREATE INDEX [IX_FK_OperationTableBasicMetricksResultTable]
ON [dbo].[BasicMetricksResultTableEnt]
    ([OperationTableBasicMetricksResultTable_BasicMetricksResultTable_Id]);
GO

-- Creating foreign key on [OperationTableListsResults_ListsResults_Id] in table 'ListsResultTableEnt'
ALTER TABLE [dbo].[ListsResultTableEnt]
ADD CONSTRAINT [FK_OperationTableListsResults]
    FOREIGN KEY ([OperationTableListsResults_ListsResults_Id])
    REFERENCES [dbo].[OperationTableEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OperationTableListsResults'
CREATE INDEX [IX_FK_OperationTableListsResults]
ON [dbo].[ListsResultTableEnt]
    ([OperationTableListsResults_ListsResults_Id]);
GO

-- Creating foreign key on [ErrorCodes_Id] in table 'OperationTableEnt'
ALTER TABLE [dbo].[OperationTableEnt]
ADD CONSTRAINT [FK_ErrorCodesOperations]
    FOREIGN KEY ([ErrorCodes_Id])
    REFERENCES [dbo].[ErrorCodesEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ErrorCodesOperations'
CREATE INDEX [IX_FK_ErrorCodesOperations]
ON [dbo].[OperationTableEnt]
    ([ErrorCodes_Id]);
GO

-- Creating foreign key on [ErrorCodeState_Id] in table 'ErrorCodesEnt'
ALTER TABLE [dbo].[ErrorCodesEnt]
ADD CONSTRAINT [FK_ErrorCodeStatesErrorCodes]
    FOREIGN KEY ([ErrorCodeState_Id])
    REFERENCES [dbo].[ErrorCodeStatesEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ErrorCodeStatesErrorCodes'
CREATE INDEX [IX_FK_ErrorCodeStatesErrorCodes]
ON [dbo].[ErrorCodesEnt]
    ([ErrorCodeState_Id]);
GO

-- Creating foreign key on [Posts_Id] in table 'CommentsEnt'
ALTER TABLE [dbo].[CommentsEnt]
ADD CONSTRAINT [FK_PostsComments]
    FOREIGN KEY ([Posts_Id])
    REFERENCES [dbo].[PostsEnd]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsComments'
CREATE INDEX [IX_FK_PostsComments]
ON [dbo].[CommentsEnt]
    ([Posts_Id]);
GO

-- Creating foreign key on [ListsResults_Id] in table 'PostsEnd'
ALTER TABLE [dbo].[PostsEnd]
ADD CONSTRAINT [FK_ListsResultsPosts]
    FOREIGN KEY ([ListsResults_Id])
    REFERENCES [dbo].[ListsResultTableEnt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ListsResultsPosts'
CREATE INDEX [IX_FK_ListsResultsPosts]
ON [dbo].[PostsEnd]
    ([ListsResults_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------