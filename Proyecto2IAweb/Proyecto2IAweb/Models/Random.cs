using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2IAweb
{
    class Random : Politic
    {
        public Random(string name) : base(name)
        {

        }

        public override Tuple<int, int> Selection(List<float> fitness)
        {
            if (fitness.Count() <= 1) return Tuple.Create<int, int>(0, 0);

            int parentIndexA = random.Next(0, fitness.Count());
            int parentIndexB = random.Next(0, fitness.Count());
            while (parentIndexB == parentIndexA)
            {
                parentIndexB = random.Next(0, fitness.Count());
            }
            return Tuple.Create<int, int>(parentIndexA, parentIndexB);
        }
    }
}
