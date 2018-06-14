using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Models;

namespace Service
{
    public class DBContext : DbContext
    {
        public DBContext() : base("CarShopDatabase")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Administrator> Administrators { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<CarKit> Car_kits { get; set; }

        public virtual DbSet<Stock> Stocks { get; set; }

        public virtual DbSet<StockDetail> Stocks_Details { get; set; }

        public virtual DbSet<Detail> Details { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderCar> OrderCars { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<RequestDetails> RequestDetailses { get; set; }

        public virtual DbSet<CarKitDetail> CarKitDetails { get; set; }
    }
}
