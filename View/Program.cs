using Service.LogicInterface;
using Service.RealiseInterfaceBD;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using System.Data.Entity;

namespace View
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<LoginForm>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, DBContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISupplierService, SupplierService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoService, MessageInfoService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAdministratorService, AdministratorService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDetailService, DetailService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarService, CarService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarKitService, CarKitService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGeneralService, GeneralService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportService>(new HierarchicalLifetimeManager());
            return currentContainer;
        }


    }
}
