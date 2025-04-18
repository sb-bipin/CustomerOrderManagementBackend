
----Start: Bipin Paudel 2025-04-18 17:34:42 ----

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserType] int NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [CreatedOn] nvarchar(max) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedOn] nvarchar(max) NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [CustomerOrders] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerName] nvarchar(max) NOT NULL,
    [TableNumber] nvarchar(max) NOT NULL,
    [OrderNumber] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CustomerOrders] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [DessertsOrders] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [HalfQuantity] smallint NOT NULL,
    [HalfPrice] smallint NOT NULL,
    [FullQuantity] smallint NOT NULL,
    [FullPrice] smallint NOT NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_DessertsOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DessertsOrders_CustomerOrders_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [CustomerOrders] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [DrinksOrders] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [HalfQuantity] smallint NOT NULL,
    [HalfPrice] smallint NOT NULL,
    [FullQuantity] smallint NOT NULL,
    [FullPrice] smallint NOT NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_DrinksOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DrinksOrders_CustomerOrders_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [CustomerOrders] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SnacksOrders] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [HalfQuantity] smallint NOT NULL,
    [HalfPrice] smallint NOT NULL,
    [FullQuantity] smallint NOT NULL,
    [FullPrice] smallint NOT NULL,
    [CustomerOrderId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SnacksOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SnacksOrders_CustomerOrders_CustomerOrderId] FOREIGN KEY ([CustomerOrderId]) REFERENCES [CustomerOrders] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_DessertsOrders_CustomerOrderId] ON [DessertsOrders] ([CustomerOrderId]);

CREATE INDEX [IX_DrinksOrders_CustomerOrderId] ON [DrinksOrders] ([CustomerOrderId]);

CREATE INDEX [IX_SnacksOrders_CustomerOrderId] ON [SnacksOrders] ([CustomerOrderId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250418173442_Init', N'9.0.4');

COMMIT;
GO

----End: Bipin Paudel 2025-04-18 17:34:42 ----
