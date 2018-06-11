﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace CarShopService.UserViewModel
{
    public class CarKitViewModel
    {
        public int carkit_id { get; set; }

        public string kit_name { get; set; }

        public int? cost { get; set; }

        public DateTime date { get; set; }

        public int supplier { get; set; }

        public virtual ICollection<Car> Car { get; set; }

        public virtual Supplier Supplier1 { get; set; }

        public virtual ICollection<Detail> Detail { get; set; }
    }
}
