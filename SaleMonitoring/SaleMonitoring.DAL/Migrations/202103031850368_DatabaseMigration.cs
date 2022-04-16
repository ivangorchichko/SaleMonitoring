namespace SaleMonitoring.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientTelephone = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ContractNumber = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ProductId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManagerName = c.String(),
                        ManagerTelephone = c.String(),
                        Date = c.DateTime(nullable: false),
                        ManagerRank = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Purchase", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.Purchase", "ClientId", "dbo.Client");
            DropIndex("dbo.Purchase", new[] { "ManagerId" });
            DropIndex("dbo.Purchase", new[] { "ProductId" });
            DropIndex("dbo.Purchase", new[] { "ClientId" });
            DropTable("dbo.Product");
            DropTable("dbo.Manager");
            DropTable("dbo.Purchase");
            DropTable("dbo.Client");
        }
    }
}
