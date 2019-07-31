namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ww : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(precision: 18, scale: 0),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Image = c.String(nullable: false, maxLength: 50),
                        IsFree = c.Boolean(),
                        IsRent = c.Boolean(),
                        IsExchange = c.Boolean(),
                        IsSell = c.Boolean(),
                        User_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 100),
                        Product_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Date = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fk_Order = c.Int(),
                        IsRead = c.Boolean(),
                        User_Id = c.Int(nullable: false),
                        Comment_Id = c.Int(),
                        Like_Id = c.Int(),
                        CurrentUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Like", t => t.Like_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Order", t => t.Fk_Order)
                .ForeignKey("dbo.Comment", t => t.Comment_Id)
                .Index(t => t.Fk_Order)
                .Index(t => t.User_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.Like_Id);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100, unicode: false),
                        Image = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(nullable: false, maxLength: 50),
                        Status = c.Boolean(),
                        Role_Id = c.Int(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Bio = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Message = c.String(nullable: false, maxLength: 100),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Date = c.DateTime(storeType: "date"),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Rate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Order", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Like", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Comment", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Notification", "Comment_Id", "dbo.Comment");
            DropForeignKey("dbo.User_Rate", "User_Id", "dbo.User");
            DropForeignKey("dbo.User", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Product", "User_Id", "dbo.User");
            DropForeignKey("dbo.Order", "User_Id", "dbo.User");
            DropForeignKey("dbo.Notification", "Fk_Order", "dbo.Order");
            DropForeignKey("dbo.Notification", "User_Id", "dbo.User");
            DropForeignKey("dbo.Like", "User_Id", "dbo.User");
            DropForeignKey("dbo.Contact", "User_Id", "dbo.User");
            DropForeignKey("dbo.Comment", "User_Id", "dbo.User");
            DropForeignKey("dbo.Notification", "Like_Id", "dbo.Like");
            DropIndex("dbo.User_Rate", new[] { "User_Id" });
            DropIndex("dbo.Order", new[] { "User_Id" });
            DropIndex("dbo.Order", new[] { "Product_Id" });
            DropIndex("dbo.Contact", new[] { "User_Id" });
            DropIndex("dbo.User", new[] { "Role_Id" });
            DropIndex("dbo.Like", new[] { "Product_Id" });
            DropIndex("dbo.Like", new[] { "User_Id" });
            DropIndex("dbo.Notification", new[] { "Like_Id" });
            DropIndex("dbo.Notification", new[] { "Comment_Id" });
            DropIndex("dbo.Notification", new[] { "User_Id" });
            DropIndex("dbo.Notification", new[] { "Fk_Order" });
            DropIndex("dbo.Comment", new[] { "User_Id" });
            DropIndex("dbo.Comment", new[] { "Product_Id" });
            DropIndex("dbo.Product", new[] { "Category_Id" });
            DropIndex("dbo.Product", new[] { "User_Id" });
            DropTable("dbo.User_Rate");
            DropTable("dbo.Role");
            DropTable("dbo.Order");
            DropTable("dbo.Contact");
            DropTable("dbo.User");
            DropTable("dbo.Like");
            DropTable("dbo.Notification");
            DropTable("dbo.Comment");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
