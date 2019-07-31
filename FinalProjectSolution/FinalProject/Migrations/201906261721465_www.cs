namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class www : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Rate", "ProductUser_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "Image", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.User_Rate", "Rate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_Rate", "Rate", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "Image", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.User_Rate", "ProductUser_Id");
        }
    }
}
