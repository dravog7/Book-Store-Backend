namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Author", c => c.String(maxLength: 200));
            AddColumn("dbo.Book", "featured", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Book", "Title", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "Title", c => c.String(maxLength: 100));
            DropColumn("dbo.Book", "featured");
            DropColumn("dbo.Book", "Author");
        }
    }
}
