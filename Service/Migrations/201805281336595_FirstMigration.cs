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
                        CarId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        order_status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.AdministratorId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.OrderCars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cost = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                        car_id = c.Int(nullable: false),
                        order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Cars", t => t.car_id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.car_id, cascadeDelete: true)
                .Index(t => t.car_id);
            
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
                "dbo.Car_kit",
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
                "dbo.Details",
                c => new
                    {
                        detail_id = c.Int(nullable: false, identity: true),
                        DetailName = c.String(nullable: false),
                        type = c.String(nullable: false),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.detail_id);
            
            CreateTable(
                "dbo.Stock_Detail",
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
            DropForeignKey("dbo.OrderCars", "car_id", "dbo.Orders");
            DropForeignKey("dbo.OrderCars", "car_id", "dbo.Cars");
            DropForeignKey("dbo.Car_kit", "Supplier_supplier_id", "dbo.Suppliers");
            DropForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Stock_Detail", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Stock_Detail", "DetailId", "dbo.Details");
            DropForeignKey("dbo.Car_kit", "DetailId", "dbo.Details");
            DropForeignKey("dbo.Car_kit", "car_id", "dbo.Cars");
            DropForeignKey("dbo.Orders", "AdministratorId", "dbo.Administrators");
            DropIndex("dbo.RequestDetails", new[] { "Detail_detail_id" });
            DropIndex("dbo.RequestDetails", new[] { "RequestId" });
            DropIndex("dbo.Stock_Detail", new[] { "StockId" });
            DropIndex("dbo.Stock_Detail", new[] { "DetailId" });
            DropIndex("dbo.Car_kit", new[] { "Supplier_supplier_id" });
            DropIndex("dbo.Car_kit", new[] { "DetailId" });
            DropIndex("dbo.Car_kit", new[] { "car_id" });
            DropIndex("dbo.OrderCars", new[] { "car_id" });
            DropIndex("dbo.Orders", new[] { "SupplierId" });
            DropIndex("dbo.Orders", new[] { "AdministratorId" });
            DropTable("dbo.Requests");
            DropTable("dbo.RequestDetails");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Stocks");
            DropTable("dbo.Stock_Detail");
            DropTable("dbo.Details");
            DropTable("dbo.Car_kit");
            DropTable("dbo.Cars");
            DropTable("dbo.OrderCars");
            DropTable("dbo.Orders");
            DropTable("dbo.Administrators");
        }
    }
}
