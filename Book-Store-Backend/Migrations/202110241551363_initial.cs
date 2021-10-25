namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 200),
                        Author = c.String(maxLength: 200),
                        ISBN = c.String(maxLength: 50),
                        Year = c.Int(),
                        Price = c.Double(),
                        Description = c.String(maxLength: 1000),
                        Position = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        featured = c.Boolean(nullable: false),
                        Image = c.String(maxLength: 200),
                        createdAt = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
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
                        CategoryName = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        Image = c.String(maxLength: 200),
                        Status = c.Boolean(nullable: false),
                        Position = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        WishListId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.WishListId)
                .ForeignKey("dbo.AspNetUsers", t => t.WishListId)
                .Index(t => t.WishListId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        isActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        status = c.Int(nullable: false),
                        address = c.String(maxLength: 2000),
                        price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Coupon",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.WishListBooks",
                c => new
                    {
                        WishList_WishListId = c.String(nullable: false, maxLength: 128),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WishList_WishListId, t.Book_BookId })
                .ForeignKey("dbo.WishList", t => t.WishList_WishListId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.WishList_WishListId)
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CouponOrders", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.CouponOrders", "Coupon_Id", "dbo.Coupon");
            DropForeignKey("dbo.BookEntries", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.WishList", "WishListId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WishListBooks", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.WishListBooks", "WishList_WishListId", "dbo.WishList");
            DropForeignKey("dbo.BookEntries", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "CategoryId", "dbo.Category");
            DropIndex("dbo.CouponOrders", new[] { "Order_OrderId" });
            DropIndex("dbo.CouponOrders", new[] { "Coupon_Id" });
            DropIndex("dbo.WishListBooks", new[] { "Book_BookId" });
            DropIndex("dbo.WishListBooks", new[] { "WishList_WishListId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.WishList", new[] { "WishListId" });
            DropIndex("dbo.Book", new[] { "CategoryId" });
            DropIndex("dbo.BookEntries", new[] { "OrderId" });
            DropIndex("dbo.BookEntries", new[] { "BookId" });
            DropTable("dbo.CouponOrders");
            DropTable("dbo.WishListBooks");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Coupon");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.WishList");
            DropTable("dbo.Category");
            DropTable("dbo.Book");
            DropTable("dbo.BookEntries");
        }
    }
}
