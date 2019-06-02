using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Proyecto2IAweb
{
    class Tourney : Politic
    {
        public Tourney(string name) : base(name)
        {

        }

        public override Tuple<int, int> Selection(List<float> fitness)
        {
            if (fitness.Count() <= 1) return Tuple.Create<int, int>(0, 0);
            
            List<Tuple<int, float>> contestants = new List<Tuple<int, float>>();
            for (int i = 0; i < fitness.Count; ++i)
            {
                contestants.Add(Tuple.Create<int, float>(i, fitness[i]));
            }
            
            List<Tuple<int, float>> contestantsA = new List<Tuple<int, float>>(contestants);
            int parentIndexA = GetWinner(contestantsA);

            int parentIndexB;
            do
            {
                List<Tuple<int, float>> contestantsB = new List<Tuple<int, float>>(contestants);
                parentIndexB = GetWinner(contestantsB);
            } while (parentIndexB == parentIndexA);

            return Tuple.Create<int, int>(parentIndexA, parentIndexB);
        }

        private int GetWinner(List<Tuple<int, float>> contestants)
        {
            List<Tuple<int, float>> winners = new List<Tuple<int, float>>();
            int i = 0;
            do
            {
                Shuffle(contestants);
                winners.Clear();
                for (i = 0; i < (contestants.Count - 1); i += 2)
                {
                    winners.Add(Battle(contestants[i], contestants[i + 1]));
                }
                if ((i + 1) == contestants.Count) winners.Add(contestants[i]);

                contestants.Clear();
                contestants.AddRange(winners);

            } while (winners.Count > 1);

            return winners[0].Item1;
        }

        // Seleccionar el que tiene menor fitness
        private Tuple<int, float> Battle(Tuple<int, float> A, Tuple<int, float> B)
        {
            double draw = random.NextDouble() * (A.Item2 + B.Item2);
            if (draw >= A.Item2) return A;
            else return B;
        }

        // https://stackoverflow.com/questions/273313/randomize-a-listt
        // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        public static void Shuffle<T>(IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
