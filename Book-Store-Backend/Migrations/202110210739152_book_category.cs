namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class book_category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        Title = c.String(maxLength: 100),
                        ISBN = c.Long(),
                        Year = c.Int(),
                        Price = c.Double(),
                        Description = c.String(maxLength: 500),
                        Position = c.Double(),
                        Status = c.Boolean(),
                        Image = c.String(maxLength: 50),
                        createdAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        Image = c.String(maxLength: 50),
                        Status = c.Boolean(),
                        Position = c.Double(),
                        CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "CategoryId", "dbo.Category");
            DropIndex("dbo.Book", new[] { "CategoryId" });
            DropTable("dbo.Category");
            DropTable("dbo.Book");
        }
    }
}
