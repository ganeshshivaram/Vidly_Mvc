namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PanNumber", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.AspNetUsers", "MobileNumber", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MobileNumber");
            DropColumn("dbo.AspNetUsers", "PanNumber");
        }
    }
}
