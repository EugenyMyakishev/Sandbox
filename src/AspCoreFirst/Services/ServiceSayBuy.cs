using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreFirst.Services
{
    public class ServiceSayBuy:IService
    {
        public string Say()
        {
            return "Buy service";
        }
    }
}
