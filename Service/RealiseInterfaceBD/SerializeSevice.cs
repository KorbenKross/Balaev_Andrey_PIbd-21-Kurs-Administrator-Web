using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Models;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;

namespace Service.RealiseInterfaceBD
{
    public class SerializeSevice : ISerializeService
    {
        private DBContext context;

        public SerializeSevice(DBContext context)
        {
            this.context = context;
        }

        public string GetData()
        {
            DataContractJsonSerializer clientJS = new DataContractJsonSerializer(typeof(List<Administrator>));
            MemoryStream msClient = new MemoryStream();
            clientJS.WriteObject(msClient, context.Administrators.ToList());
            msClient.Position = 0;
            StreamReader srClient = new StreamReader(msClient);
            string administratorsJSON = srClient.ReadToEnd();
            srClient.Close();
            msClient.Close();

            DataContractJsonSerializer carJS = new DataContractJsonSerializer(typeof(List<Car>));
            MemoryStream msCar = new MemoryStream();
            carJS.WriteObject(msCar, context.Cars.ToList());
            msCar.Position = 0;
            StreamReader srCar = new StreamReader(msCar);
            string carsJSON = srCar.ReadToEnd();
            srCar.Close();
            msCar.Close();

            DataContractJsonSerializer carKitJS = new DataContractJsonSerializer(typeof(List<Car_kit>));
            MemoryStream mscarKit = new MemoryStream();
            carKitJS.WriteObject(mscarKit, context.Car_kits.ToList());
            mscarKit.Position = 0;
            StreamReader srcarKit = new StreamReader(mscarKit);
            string carKitsJSON = srcarKit.ReadToEnd();
            srcarKit.Close();
            mscarKit.Close();

            DataContractJsonSerializer orderJS = new DataContractJsonSerializer(typeof(List<Order>));
            MemoryStream msOrder = new MemoryStream();
            orderJS.WriteObject(msOrder, context.Orders.ToList());
            msOrder.Position = 0;
            StreamReader srOrder = new StreamReader(msOrder);
            string ordersJSON = srOrder.ReadToEnd();
            srOrder.Close();
            msOrder.Close();

            DataContractJsonSerializer orderCarJS = new DataContractJsonSerializer(typeof(List<OrderCar>));
            MemoryStream msOrderCar = new MemoryStream();
            orderCarJS.WriteObject(msOrderCar, context.OrderCars.ToList());
            msOrderCar.Position = 0;
            StreamReader srOrderCar = new StreamReader(msOrderCar);
            string orderCarsJSON = srOrderCar.ReadToEnd();
            srOrderCar.Close();
            msOrderCar.Close();

            DataContractJsonSerializer detailJS = new DataContractJsonSerializer(typeof(List<Detail>));
            MemoryStream msDetail = new MemoryStream();
            detailJS.WriteObject(msDetail, context.Details.ToList());
            msDetail.Position = 0;
            StreamReader srDetail = new StreamReader(msDetail);
            string detailsJSON = srDetail.ReadToEnd();
            srDetail.Close();
            msDetail.Close();

            DataContractJsonSerializer requestJS = new DataContractJsonSerializer(typeof(List<Request>));
            MemoryStream msRequest = new MemoryStream();
            requestJS.WriteObject(msRequest, context.Requests.ToList());
            msRequest.Position = 0;
            StreamReader srRequest = new StreamReader(msRequest);
            string requestsJSON = srRequest.ReadToEnd();
            srRequest.Close();
            msRequest.Close();

            DataContractJsonSerializer requestDetailJS = new DataContractJsonSerializer(typeof(List<RequestDetails>));
            MemoryStream msRequestDetail = new MemoryStream();
            requestDetailJS.WriteObject(msRequestDetail, context.RequestDetailses.ToList());
            msRequestDetail.Position = 0;
            StreamReader srRequestDetail = new StreamReader(msRequestDetail);
            string requestDetailsJSON = srRequestDetail.ReadToEnd();
            srRequestDetail.Close();
            msRequestDetail.Close();

            return
                "{\n" +
                "    \"Administrators\": " + administratorsJSON + ",\n" +
                "    \"Cars\": " + carsJSON + ",\n" +
                "    \"CarDetails\": " + carKitsJSON + ",\n" +
                "    \"Orders\": " + ordersJSON + ",\n" +
                "    \"OrderCars\": " + orderCarsJSON + ",\n" +
                "    \"Details\": " + detailsJSON + ",\n" +
                "    \"Requests\": " + requestsJSON + ",\n" +
                "    \"RequestDetails\": " + requestDetailsJSON + "\n" +
                "}";
        }
    }
}
