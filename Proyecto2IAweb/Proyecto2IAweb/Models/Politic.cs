using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2IAweb
{
    abstract class Politic
    {
        public string Name { get; set; }

        protected System.Random random = new System.Random();

        public Politic(string name)
        {
            this.Name = name;
        }

        public abstract Tuple<int, int> Selection(List<float> fitness);
    }
}
