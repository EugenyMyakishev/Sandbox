using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreFirst.Services
{
    public class ServiceSayHello:IService
    {
        public string Say()
        {
            return "Hello Service";
        }
    }
}
