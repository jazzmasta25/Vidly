namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'49610fa6-741f-4320-af04-7577bc296fca', N'admin@vidly.com', 0, N'AI244sUzvJrj4R8sws1B0EYK5EMkX1EXfjNjtYFMqMxLMWV/sW8orFCEuDyPfFVqLg==', N'7add2d24-aa8f-4987-a8f1-cb6e0ab335e4', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c6c00166-d1d0-41e8-b5ba-b246e99f5435', N'guest@vidly.com', 0, N'AKHFwxLlKLBrR18CAYEdDhf9+aIr1j6HRJYgYKp7XzUaYWhO5J8VjRTCcCRJVCJUgw==', N'cc0598de-9ebc-4840-b8eb-c4a056f6b7bb', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e8bfbca1-44ef-435d-a6cb-1be375b87fc5', N'darrenrush@gmail.com', 0, N'ABmNcxB53mqV6VtprxdVTrRwOq2Zbt4BkuvmXyygpoVkTSj4rbQvLQvj7poVbBBVWg==', N'9b1308c9-aee7-4393-a6b1-7b97e034acbb', NULL, 0, 0, NULL, 1, 0, N'darrenrush@gmail.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'785b32b7-837a-46f5-a562-f7163f231169', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'49610fa6-741f-4320-af04-7577bc296fca', N'785b32b7-837a-46f5-a562-f7163f231169')
");
        }
        
        public override void Down()
        {
        }
    }
}
