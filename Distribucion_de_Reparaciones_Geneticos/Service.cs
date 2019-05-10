using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribucion_de_Reparaciones_Geneticos
{
    class Service
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Commission { get; set; }

        public Service(string code, string description, int duration, int commission)
        {
            this.Code = code;
            this.Description = description;
            this.Duration = duration;
            this.Commission = commission;
        }

        public Service() { }
        
    }
}
