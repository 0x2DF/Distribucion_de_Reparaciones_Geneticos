using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribucion_de_Reparaciones_Geneticos
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
