﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs.LogicInterface;
using Kurs.ConnectingModel;
using Kurs.UserViewModel;
using Kurs;

namespace Kurs.LogicInterface
{
    public interface IStockService
    {
        List<StockViewModel> GetList();

        StockViewModel GetElement(int id);

        void AddElement(StockConnectingModel model);

        void UpdElement(StockConnectingModel model);

        void DelElement(int id);
    }
}
