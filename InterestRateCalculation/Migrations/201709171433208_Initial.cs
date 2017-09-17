namespace InterestRateCalculation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agreements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Margin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Duration = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        BaseRateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseRates", t => t.BaseRateId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.BaseRateId);
            
            CreateTable(
                "dbo.BaseRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaseRateCode = c.String(nullable: false, maxLength: 9),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalId = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agreements", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Agreements", "BaseRateId", "dbo.BaseRates");
            DropIndex("dbo.Agreements", new[] { "BaseRateId" });
            DropIndex("dbo.Agreements", new[] { "CustomerId" });
            DropTable("dbo.Customers");
            DropTable("dbo.BaseRates");
            DropTable("dbo.Agreements");
        }
    }
}
