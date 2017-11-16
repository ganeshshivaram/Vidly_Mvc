namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserSeedData : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'56b5fe95-1c8d-4772-94dc-0c3ba69595ca', N'CanManageMovies')");

            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'66b5fe95-1c8d-4772-94dc-0c3ba69595cb', N'CanManageCustomers')");

            Sql(@"
                INSERT INTO 
                [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], 
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], 
                [AccessFailedCount], [UserName])
                VALUES 
                (N'535cee78-a7fb-49f7-883b-272f3b244924', N'admin@vidly.com', 0, 
                 N'AP8QpaUuYm/g2tfhrkYauyXtvMeUIiA6uFFoAK6sIpMbQPrrQrYMg8vQjQCfNv8pjA==', 
                 N'99b4bb81-bbf2-43f1-a616-e8e34e0362ef', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')");

            Sql(@"INSERT INTO 
                [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], 
                [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled],
                [AccessFailedCount], [UserName])
                VALUES 
                (N'8fd73e88-be68-472f-b185-69f1f742b696', N'guest@vidly.com', 0, 
                 N'AARtWPYpfPX5fMYp+/hHYH1Ou25CxqqNF6j5qyxxjErb3nXU8RDlrSkVKb6m8I0Qtw==', N'9aa89e66-f230-433f-b945-df553267c9e9', 
                NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')");

            Sql(@"INSERT INTO 
                [dbo].[AspNetUserRoles] ([UserId], [RoleId]) 
                VALUES 
                (N'535cee78-a7fb-49f7-883b-272f3b244924', N'56b5fe95-1c8d-4772-94dc-0c3ba69595ca')
                ");

            Sql(@"INSERT INTO 
                [dbo].[AspNetUserRoles] ([UserId], [RoleId]) 
                VALUES 
                (N'535cee78-a7fb-49f7-883b-272f3b244924', N'66b5fe95-1c8d-4772-94dc-0c3ba69595cb')
                ");
        }

        public override void Down()
        {
        }
    }
}
