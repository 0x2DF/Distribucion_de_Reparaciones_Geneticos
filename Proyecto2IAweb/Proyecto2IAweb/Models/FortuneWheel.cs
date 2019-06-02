using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2IAweb
{
    class FortuneWheel : Politic
    {
        private int Spins { get; set; }

        public FortuneWheel(string name, int spins) : base(name)
        {
            this.Spins = spins;
        }
        public override Tuple<int, int> Selection(List<float> fitness)
        {
            if (fitness.Count() <= 1) return Tuple.Create<int, int>(0, 0);

            List<Tuple<int, float>> entries = new List<Tuple<int, float>>();
            for (int i = 0; i < fitness.Count; ++i)
            {
                entries.Add(Tuple.Create<int, float>(i, fitness[i]));
            }

            List<Tuple<int, float>> entriesA = new List<Tuple<int, float>>(entries);
            int parentIndexA = SpinWheel(entriesA);

            int parentIndexB;
            do
            {
                List<Tuple<int, float>> entriesB = new List<Tuple<int, float>>(entries);
                parentIndexB = SpinWheel(entriesB);
            } while (parentIndexB == parentIndexA);

            return Tuple.Create<int, int>(parentIndexA, parentIndexB);
        }

        private int SpinWheel(List<Tuple<int, float>> entries)
        {
            return 0;
        }
    }
}
