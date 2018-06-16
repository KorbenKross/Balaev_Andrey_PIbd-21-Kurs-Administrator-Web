namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        admin_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        login = c.String(nullable: false),
                        password = c.String(nullable: false),
                        mail = c.String(),
                    })
                .PrimaryKey(t => t.admin_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        AdministratorId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        order_date = c.DateTime(nullable: false),
                        Sum = c.Int(nullable: false),
                        CarKitId = c.Int(nullable: false),
                        CarKitName = c.String(nullable: false),
                        Count = c.Int(nullable: false),
                        order_status = c.Int(nullable: false),
                        CarKit_carkit_id = c.Int(),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.CarKits", t => t.CarKit_carkit_id)
                .Index(t => t.AdministratorId)
                .Index(t => t.SupplierId)
                .Index(t => t.CarKit_carkit_id);
            
            CreateTable(
                "dbo.CarKits",
                c => new
                    {
                        carkit_id = c.Int(nullable: false, identity: true),
                        kit_name = c.String(),
                        Count = c.Int(nullable: false),
                        car_id = c.Int(nullable: false),
                        DetailId = c.Int(nullable: false),
                        Supplier_supplier_id = c.Int(),
                    })
                .PrimaryKey(t => t.carkit_id)
                .ForeignKey("dbo.Cars", t => t.car_id, cascadeDelete: true)
                .ForeignKey("dbo.Details", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_supplier_id)
                .Index(t => t.car_id)
                .Index(t => t.DetailId)
                .Index(t => t.Supplier_supplier_id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        car_id = c.Int(nullable: false, identity: true),
                        brand = c.String(nullable: false),
                        cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.car_id);
            
            CreateTable(
                "dbo.OrderCars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cost = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                        carkit_id = c.Int(nullable: false),
                        kit_name = c.String(),
                        order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CarKits", t => t.carkit_id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.carkit_id, cascadeDelete: true)
                .Index(t => t.carkit_id);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        detail_id = c.Int(nullable: false, identity: true),
                        DetailName = c.String(nullable: false),
                        type = c.String(nullable: false),
                        count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.detail_id);
            
            CreateTable(
                "dbo.StockDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DetailId = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Details", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StockId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        supplier_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        login = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.supplier_id);
            
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        AdministratorId = c.Int(),
                        Administrator_admin_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Administrators", t => t.Administrator_admin_id)
                .Index(t => t.Administrator_admin_id);
            
            CreateTable(
                "dbo.RequestDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        DetailId = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                        Detail_detail_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Details", t => t.Detail_detail_id)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.RequestId)
                .Index(t => t.Detail_detail_id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestDetails", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestDetails", "Detail_detail_id", "dbo.Details");
            DropForeignKey("dbo.MessageInfoes", "Administrator_admin_id", "dbo.Administrators");
            DropForeignKey("dbo.Orders", "CarKit_carkit_id", "dbo.CarKits");
            DropForeignKey("dbo.CarKits", "Supplier_supplier_id", "dbo.Suppliers");
            DropForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.StockDetails", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockDetails", "DetailId", "dbo.Details");
            DropForeignKey("dbo.CarKits", "DetailId", "dbo.Details");
            DropForeignKey("dbo.OrderCars", "carkit_id", "dbo.Orders");
            DropForeignKey("dbo.OrderCars", "carkit_id", "dbo.CarKits");
            DropForeignKey("dbo.CarKits", "car_id", "dbo.Cars");
            DropForeignKey("dbo.Orders", "AdministratorId", "dbo.Administrators");
            DropIndex("dbo.RequestDetails", new[] { "Detail_detail_id" });
            DropIndex("dbo.RequestDetails", new[] { "RequestId" });
            DropIndex("dbo.MessageInfoes", new[] { "Administrator_admin_id" });
            DropIndex("dbo.StockDetails", new[] { "StockId" });
            DropIndex("dbo.StockDetails", new[] { "DetailId" });
            DropIndex("dbo.OrderCars", new[] { "carkit_id" });
            DropIndex("dbo.CarKits", new[] { "Supplier_supplier_id" });
            DropIndex("dbo.CarKits", new[] { "DetailId" });
            DropIndex("dbo.CarKits", new[] { "car_id" });
            DropIndex("dbo.Orders", new[] { "CarKit_carkit_id" });
            DropIndex("dbo.Orders", new[] { "SupplierId" });
            DropIndex("dbo.Orders", new[] { "AdministratorId" });
            DropTable("dbo.Requests");
            DropTable("dbo.RequestDetails");
            DropTable("dbo.MessageInfoes");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Stocks");
            DropTable("dbo.StockDetails");
            DropTable("dbo.Details");
            DropTable("dbo.OrderCars");
            DropTable("dbo.Cars");
            DropTable("dbo.CarKits");
            DropTable("dbo.Orders");
            DropTable("dbo.Administrators");
        }
    }
}
