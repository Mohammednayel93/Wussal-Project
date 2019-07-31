namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wAssSA : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Contact");
            AlterColumn("dbo.Contact", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Contact", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Contact");
            AlterColumn("dbo.Contact", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Contact", "Id");
        }
    }
}
