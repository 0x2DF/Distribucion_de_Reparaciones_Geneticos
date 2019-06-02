using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2IAweb
{
    class FortuneWheel : Politic
    {
        public int Spins { get; set; }

        private Tourney tourney = new Tourney("Tourney");

        public FortuneWheel(string name, int spins) : base(name)
        {
            this.Spins = spins;
        }
        public override Tuple<int, int> Selection(List<float> fitness)
        {
            if (fitness.Count() <= 1) return Tuple.Create<int, int>(0, 0);


            int parentIndexA = SpinWheel(fitness);

            int parentIndexB = SpinWheel(fitness);
            while (parentIndexB == parentIndexA)
            {
                parentIndexB = SpinWheel(fitness);
            }
            
            return Tuple.Create<int, int>(parentIndexA, parentIndexB);
        }

        private int SpinWheel(List<float> fitness)
        {
            int[] hits = new int[fitness.Count];
            float[] prefix_sum_array = new float[fitness.Count];

            hits[0] = 0;
            prefix_sum_array[0] = fitness[0];
            // Initialize counter and sum array
            for (int i = 1; i < fitness.Count; ++i)
            {
                hits[i] = 0;
                prefix_sum_array[i] = prefix_sum_array[i - 1] + fitness[i];
            }
            double total_sum = prefix_sum_array[prefix_sum_array.Length - 1];

            // Spin wheel k times
            double roll = 0;
            int k_spins = random.Next(1, Spins);
            for (int i = 0; i < k_spins; ++i)
            {
                roll = (roll + random.NextDouble() * total_sum) % total_sum;
            }

            
            double interval = total_sum / fitness.Count;
            double aggregate = roll % interval;
            // Find starting index
            int starting_index = 0;
            for (int i = 0; i < fitness.Count; ++i)
            {
                // Debug.WriteLine("i : {0}", i);
                if (prefix_sum_array[i] >= aggregate)
                {
                    starting_index = i;
                    break;
                }
            }
            
            // Calculate Hits for each index
            for (int i = starting_index; i < fitness.Count; ++i)
            {
                // Debug.WriteLine("i : {0}", i);
                hits[i]++;
                aggregate += interval;
                while (aggregate <= prefix_sum_array[i])
                {
                    // Debug.WriteLine("While {0} <= {1}", aggregate, prefix_sum_array[i]);
                    hits[i]++;
                    aggregate += interval;
                }
            }
            
            // Find roll with least hits
            int least_qty = hits[0];
            for (int i = 1; i < hits.Length; ++i)
            {
                if (least_qty > hits[i]) least_qty = hits[i];
            }
            
            List<float> tie_breakers = new List<float>();
            for (int i = 0; i < fitness.Count; ++i)
            {
                if (least_qty == hits[i]) tie_breakers.Add(fitness[i]);
            }
            
            return this.tourney.Selection(tie_breakers).Item1;
        }
    }
}