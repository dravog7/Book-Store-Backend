namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        WishListId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.WishListId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WishListBooks",
                c => new
                    {
                        WishList_WishListId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WishList_WishListId, t.Book_BookId })
                .ForeignKey("dbo.WishList", t => t.WishList_WishListId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.WishList_WishListId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishList", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WishListBooks", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.WishListBooks", "WishList_WishListId", "dbo.WishList");
            DropIndex("dbo.WishListBooks", new[] { "Book_BookId" });
            DropIndex("dbo.WishListBooks", new[] { "WishList_WishListId" });
            DropIndex("dbo.WishList", new[] { "UserId" });
            DropTable("dbo.WishListBooks");
            DropTable("dbo.WishList");
        }
    }
}
