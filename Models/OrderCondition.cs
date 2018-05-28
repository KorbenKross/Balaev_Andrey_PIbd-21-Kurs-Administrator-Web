using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public enum OrderCondition
    {
        Принят = 0,

        Готовиться = 1,

        Готов = 2,

        Оплачен = 3
    }
}