
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/26/2018 17:53:00
-- Generated from EDMX file: D:\Code\Web\net\lab3\dotNetLab\Shop\Models\ModelShop.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [16211160127];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_T_Base_Address_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Base_Address] DROP CONSTRAINT [FK_T_Base_Address_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_T_Shop_Cart_ToTable_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Shop_Cart] DROP CONSTRAINT [FK_T_Shop_Cart_ToTable_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_T_Shop_Cart_ToTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Shop_Cart] DROP CONSTRAINT [FK_T_Shop_Cart_ToTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_T_Shop_Product_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Shop_Product] DROP CONSTRAINT [FK_T_Shop_Product_ToTable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[T_Base_Address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Base_Address];
GO
IF OBJECT_ID(N'[dbo].[T_Base_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Base_User];
GO
IF OBJECT_ID(N'[dbo].[T_Shop_Cart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Shop_Cart];
GO
IF OBJECT_ID(N'[dbo].[T_Shop_Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Shop_Category];
GO
IF OBJECT_ID(N'[dbo].[T_Shop_Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Shop_Product];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'T_Shop_Category'
CREATE TABLE [dbo].[T_Shop_Category] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(50)  NULL,
    [PicUrl] nvarchar(50)  NULL
);
GO

-- Creating table 'T_Shop_Product'
CREATE TABLE [dbo].[T_Shop_Product] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductName] nvarchar(50)  NULL,
    [PicUrl] nvarchar(50)  NULL,
    [price] nchar(10)  NULL,
    [StockAmount] int  NULL,
    [SelledAmount] int  NULL,
    [CategoryId] int  NULL
);
GO

-- Creating table 'T_Base_User'
CREATE TABLE [dbo].[T_Base_User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(25)  NULL,
    [Password] nvarchar(32)  NULL,
    [Realname] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [type] int  NULL
);
GO

-- Creating table 'T_Base_Address'
CREATE TABLE [dbo].[T_Base_Address] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(max)  NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'T_Shop_Cart'
CREATE TABLE [dbo].[T_Shop_Cart] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [ProductId] int  NULL,
    [Count] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'T_Shop_Category'
ALTER TABLE [dbo].[T_Shop_Category]
ADD CONSTRAINT [PK_T_Shop_Category]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Shop_Product'
ALTER TABLE [dbo].[T_Shop_Product]
ADD CONSTRAINT [PK_T_Shop_Product]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Base_User'
ALTER TABLE [dbo].[T_Base_User]
ADD CONSTRAINT [PK_T_Base_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Base_Address'
ALTER TABLE [dbo].[T_Base_Address]
ADD CONSTRAINT [PK_T_Base_Address]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Shop_Cart'
ALTER TABLE [dbo].[T_Shop_Cart]
ADD CONSTRAINT [PK_T_Shop_Cart]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'T_Shop_Product'
ALTER TABLE [dbo].[T_Shop_Product]
ADD CONSTRAINT [FK_T_Shop_Product_ToTable]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[T_Shop_Category]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_T_Shop_Product_ToTable'
CREATE INDEX [IX_FK_T_Shop_Product_ToTable]
ON [dbo].[T_Shop_Product]
    ([CategoryId]);
GO

-- Creating foreign key on [UserId] in table 'T_Base_Address'
ALTER TABLE [dbo].[T_Base_Address]
ADD CONSTRAINT [FK_T_Base_Address_ToTable]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Base_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_T_Base_Address_ToTable'
CREATE INDEX [IX_FK_T_Base_Address_ToTable]
ON [dbo].[T_Base_Address]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'T_Shop_Cart'
ALTER TABLE [dbo].[T_Shop_Cart]
ADD CONSTRAINT [FK_T_Shop_Cart_ToTable_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Base_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_T_Shop_Cart_ToTable_User'
CREATE INDEX [IX_FK_T_Shop_Cart_ToTable_User]
ON [dbo].[T_Shop_Cart]
    ([UserId]);
GO

-- Creating foreign key on [ProductId] in table 'T_Shop_Cart'
ALTER TABLE [dbo].[T_Shop_Cart]
ADD CONSTRAINT [FK_T_Shop_Cart_ToTable_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[T_Shop_Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_T_Shop_Cart_ToTable_Product'
CREATE INDEX [IX_FK_T_Shop_Cart_ToTable_Product]
ON [dbo].[T_Shop_Cart]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------