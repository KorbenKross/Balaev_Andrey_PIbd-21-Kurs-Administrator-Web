using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarShopService.ConnectingModel;
using Kurs;

namespace CarShopService.RealiseInterface
{
    public class BaseListSingleton
    {
        private static BaseListSingleton instance;

        public List<Administrator> Administrators { get; set; }

        public List<Car> Cars { get; set; }

        public List<Car_kit> Car_kits { get; set; }

        public List<Order> Orders { get; set; }

        public List<Detail> Details { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public List<Stock> Stocks { get; set; }

        private BaseListSingleton()
        {
            Administrators = new List<Administrator>();
            Cars = new List<Car>();
            Car_kits = new List<Car_kit>();
            Orders = new List<Order>();
            Details = new List<Detail>();
            Suppliers = new List<Supplier>();
            Stocks = new List<Stock>();
        }

        public static BaseListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new BaseListSingleton();
            }
            return instance;
        }
    }
}
