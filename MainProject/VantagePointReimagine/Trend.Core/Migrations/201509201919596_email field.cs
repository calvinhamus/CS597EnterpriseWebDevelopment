namespace Trend.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_User", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_User", "email");
        }
    }
}
