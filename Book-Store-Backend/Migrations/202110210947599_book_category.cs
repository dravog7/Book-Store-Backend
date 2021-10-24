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
                        Title = c.String(maxLength: 500),
                        ISBN = c.String(maxLength: 50),
                        Year = c.Int(),
                        Price = c.Double(),
                        Description = c.String(maxLength: 2000),
                        Position = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Image = c.String(maxLength: 300),
                        createdAt = c.DateTime(nullable: false,defaultValueSql:"getutcdate()"),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 500),
                        Description = c.String(maxLength: 2000),
                        Image = c.String(maxLength: 300),
                        Status = c.Boolean(nullable: false),
                        Position = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
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
