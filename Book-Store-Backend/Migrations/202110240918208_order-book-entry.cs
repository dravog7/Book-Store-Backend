namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderbookentry : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderBooks", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderBooks", "Book_BookId", "dbo.Book");
            DropIndex("dbo.OrderBooks", new[] { "Order_OrderId" });
            DropIndex("dbo.OrderBooks", new[] { "Book_BookId" });
            CreateTable(
                "dbo.BookEntries",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.OrderId })
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
            DropColumn("dbo.Orders", "Title");
            DropTable("dbo.OrderBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderBooks",
                c => new
                    {
                        Order_OrderId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderId, t.Book_BookId });
            
            AddColumn("dbo.Orders", "Title", c => c.String());
            DropForeignKey("dbo.BookEntries", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.BookEntries", "BookId", "dbo.Book");
            DropIndex("dbo.BookEntries", new[] { "OrderId" });
            DropIndex("dbo.BookEntries", new[] { "BookId" });
            DropTable("dbo.BookEntries");
            CreateIndex("dbo.OrderBooks", "Book_BookId");
            CreateIndex("dbo.OrderBooks", "Order_OrderId");
            AddForeignKey("dbo.OrderBooks", "Book_BookId", "dbo.Book", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.OrderBooks", "Order_OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
    }
}
