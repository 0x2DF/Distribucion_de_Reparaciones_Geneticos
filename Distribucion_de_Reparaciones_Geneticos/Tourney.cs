using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribucion_de_Reparaciones_Geneticos
{
    class Tourney : Politic
    {
        public Tourney(string name) : base(name)
        {

        }

        public override Tuple<int, int> Selection(List<float> fitness)
        {
            if (fitness.Count() <= 1) return 0;

            int parentIndexA = random.Next(0, fitness.Count() - 1);
            int parentIndexB = random.Next(0, fitness.Count() - 1);
            while (parentIndexB == parentIndexA)
            {
                parentIndexB = random.Next(0, fitness.Count() - 1);
            }
            return Tuple.Create<int, int>(parentIndexA, parentIndexB);
        }

    }
}
