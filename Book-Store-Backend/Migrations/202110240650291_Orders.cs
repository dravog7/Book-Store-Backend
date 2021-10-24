namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        status = c.Int(nullable: false),
                        address = c.String(maxLength: 2000),
                        price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderBooks",
                c => new
                    {
                        Order_OrderId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderId, t.Book_BookId })
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Book_BookId);
            
            CreateTable(
                "dbo.CouponOrders",
                c => new
                    {
                        Coupon_Id = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Coupon_Id, t.Order_OrderId })
                .ForeignKey("dbo.Coupon", t => t.Coupon_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .Index(t => t.Coupon_Id)
                .Index(t => t.Order_OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CouponOrders", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.CouponOrders", "Coupon_Id", "dbo.Coupon");
            DropForeignKey("dbo.OrderBooks", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.OrderBooks", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.CouponOrders", new[] { "Order_OrderId" });
            DropIndex("dbo.CouponOrders", new[] { "Coupon_Id" });
            DropIndex("dbo.OrderBooks", new[] { "Book_BookId" });
            DropIndex("dbo.OrderBooks", new[] { "Order_OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.CouponOrders");
            DropTable("dbo.OrderBooks");
            DropTable("dbo.Orders");
        }
    }
}
