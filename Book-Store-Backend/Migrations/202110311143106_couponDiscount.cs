namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class couponDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coupon", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Coupon", "MaxDiscount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coupon", "MaxDiscount");
            DropColumn("dbo.Coupon", "Discount");
        }
    }
}
