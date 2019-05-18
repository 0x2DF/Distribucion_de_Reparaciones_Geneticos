using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2IAweb
{
    class Order
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ServiceCode { get; set; }

        public Order(int id, string name, string serviceCode)
        {
            this.ID = id;
            this.Name = name;
            this.ServiceCode = serviceCode;
        }
    }
}
