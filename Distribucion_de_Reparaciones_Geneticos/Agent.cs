using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribucion_de_Reparaciones_Geneticos
{
    class Agent
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<string> ServiceCodes = new List<string>();

        public Agent(int id, string name, List<string> services)
        {
            this.ID = id;
            this.Name = name;
            this.ServiceCodes = services;
        }

        public Agent(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public void addService(string service_code)
        {
            bool found = false;
            foreach (string service in ServiceCodes)
            {
                if (service == service_code)
                {
                    found = true;
                    break;
                }
            }
            if (!found) ServiceCodes.Add(service_code);
        }

    }
}
