namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "price", c => c.Single(nullable: false));
        }
    }
}
