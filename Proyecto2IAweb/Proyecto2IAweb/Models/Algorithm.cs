using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2IAweb
{
    class Algorithm
    {
        private List<Politic> Politics = new List<Politic>();
        public int Politic;
        public int Crossings;
        public float MutationProbability; // 0.005 ~ 0.02
        public float CrossProbability; // 0.5 ~ 1 => to cross, else just copy
        public int PopulationSize;

        private System.Random random = new System.Random();

        // List of Dictionaries
        // Key : Order ID
        // Value : Agent ID
        public List<Dictionary<int, int>> Population = new List<Dictionary<int, int>>();

        // Key : Service Code
        // Value : List -> Agent ID
        public Dictionary<string, List<int>> AgentsByService = new Dictionary<string, List<int>>();

        public Dictionary<int, Order> Orders = new Dictionary<int, Order>();
        public Dictionary<int, Agent> Agents = new Dictionary<int, Agent>();
        public Dictionary<string, Service> Services = new Dictionary<string, Service>();
        
        private List<float> fitnessPerGene;

        public Algorithm(int politic, int crossings, float mutationProbability,
                         int populationSize, float crossProbability,
                         Dictionary<string, Service> services,
                         Dictionary<int, Agent> agents,
                         Dictionary<int, Order> orders)
        {
            this.Politic = politic;
            this.Crossings = crossings;
            this.MutationProbability = mutationProbability;
            this.CrossProbability = crossProbability;
            // Maintain Population Size odd : extra space for the best of previous population
            this.PopulationSize = ((populationSize & 1) == 1 ? populationSize : populationSize + 1);

            this.Services = services;
            this.Agents = agents;
            this.Orders = orders;

            fitnessPerGene = new List<float>(this.PopulationSize);
            for (int i = 0; i < this.PopulationSize; i++) fitnessPerGene.Add(0);
            FillAgentsByService();
            GenerateRandomPopulation();

            Politics.Add(new Random("Al Azar"));
            Politics.Add(new FortuneWheel("Rueda de la fortuna"));
            Politics.Add(new Tourney("Torneo"));
            
        }

        private void FillAgentsByService()
        {
            foreach (KeyValuePair<string, Service> service in Services)
            {
                List<int> list = new List<int>();
                AgentsByService.Add(service.Key, list);
            }
            foreach (KeyValuePair<int, Agent> agent in Agents)
            {
                foreach (string serviceCode in agent.Value.ServiceCodes)
                {
                    AgentsByService[serviceCode].Add(agent.Key);
                }
            }
        }

        private void GenerateRandomPopulation()
        {
            for (int i = 0; i < PopulationSize; ++i)
            {
                Dictionary<int, int> chromosome = new Dictionary<int, int>();

                foreach (KeyValuePair<int, Order> order in Orders)
                {
                    chromosome.Add(order.Key, RandomAgentFromPool(order.Value.ServiceCode));
                }

                Population.Add(chromosome);
            }
        }

        private int RandomAgentFromPool(string serviceCode)
        {
            return AgentsByService[serviceCode][random.Next(0, AgentsByService[serviceCode].Count - 1)];
        }

        private Dictionary<int, Pair<int, int>> CommissionsAndHoursPerAgent(int index)
        {
            // Agent ID : (Commission, Hours)
            Dictionary<int, Pair<int, int>> agentsRate = new Dictionary<int, Pair<int, int>>();

            // Order ID : Agent ID
            foreach (KeyValuePair<int, int> entry in Population[index])
            {
                if (agentsRate.ContainsKey(entry.Value))
                {
                    agentsRate[entry.Value].First += Services[Orders[entry.Key].ServiceCode].Commission;
                    agentsRate[entry.Value].Second += Services[Orders[entry.Key].ServiceCode].Duration;
                }
                else
                {
                    agentsRate[entry.Value] = new Pair<int, int>(Services[Orders[entry.Key].ServiceCode].Commission,
                                                                 Services[Orders[entry.Key].ServiceCode].Duration);
                }
            }
            return agentsRate;
        }

        private float Fitness(int index)
        {
            // Agent ID : (Commission, Hours)
            Dictionary<int, Pair<int, int>> agentsRate = CommissionsAndHoursPerAgent(index);

            int overtime = 0;
            int proportion = 1000;

            float meanCommission = 0.0f;
            foreach (KeyValuePair<int, Pair<int, int> > agent in agentsRate)
            {
                meanCommission += agent.Value.First;

                // Sum total overtime
                if (agent.Value.Second > 40) overtime += (agent.Value.Second - 40);
            }
            meanCommission /= Agents.Count();

            float variance = 0.0f;
            foreach (KeyValuePair<int, Pair<int, int>> agent in agentsRate)
            {
                variance += ( (agent.Value.First - meanCommission) * (agent.Value.First - meanCommission) );
            }
            variance /= Agents.Count();
            
            return variance + (overtime * proportion);
        }

        private Tuple<Dictionary<int, int>, Dictionary<int, int>> Crossover(int parentIndexA, int parentIndexB)
        {
            Dictionary<int, int> offspringA = new Dictionary<int, int>();
            Dictionary<int, int> offspringB = new Dictionary<int, int>();

            if (random.Next(0, 100) > (CrossProbability * 100))
            {
                // Do no cross, copy parents over
                offspringA = Population[parentIndexA];
                offspringB = Population[parentIndexB];
            }
            else
            {
                // Cross
                int interval = Orders.Count() / (Crossings + 1);
                int counter = 0, flipped = 0;
                foreach (KeyValuePair<int, int> gene in Population[parentIndexA])
                {
                    offspringA[gene.Key] = ( (flipped & 1) == 0 ? gene.Value : Population[parentIndexB][gene.Key] );
                    offspringB[gene.Key] = ( (flipped & 1) == 1 ? gene.Value : Population[parentIndexA][gene.Key] );

                    counter++;
                    if (counter >= interval)
                    {
                        flipped ^= 1;
                        counter = 0;
                    }
                }
            }

            return Tuple.Create< Dictionary<int, int>, Dictionary<int, int> >(offspringA, offspringB);
        }

        private Dictionary<int, int> Mutation(Dictionary<int, int> offspring)
        {
            Dictionary<int, int> new_offspring = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> gene in offspring)
            {
                if (random.Next(0, 1000) < (MutationProbability * 1000))
                {
                    // Mutate gene
                    string serviceCode = Orders[gene.Value].ServiceCode;

                    new_offspring[gene.Key] = AgentsByService[serviceCode][random.Next(0, AgentsByService[serviceCode].Count())];
                }
            }
            return new_offspring;
        }

        public void NextGeneration()
        {
            int bestFitnessIndex = -1;

            // Recalculate fitness for every chromosome
            for (int i = 0; i < PopulationSize; ++i)
            {
                fitnessPerGene[i] = Fitness(i);

                if (bestFitnessIndex != -1)
                {
                    if (fitnessPerGene[i] < fitnessPerGene[bestFitnessIndex]) bestFitnessIndex = i;
                }
                else bestFitnessIndex = i;
            }

            List<Dictionary<int, int>> offsprings = new List<Dictionary<int, int>>();

            // Use politic to select Parents -> offsprings
            // Every pair of parents => two offspring
            for (int i = 0; i < (PopulationSize - 1); i+=2)
            {
                Tuple<int, int> parents = Politics[Politic].Selection(fitnessPerGene);
                
                Tuple<Dictionary<int, int>, Dictionary<int, int> > offspring = Crossover(parents.Item1, parents.Item2);

                offsprings.Add(Mutation(offspring.Item1));
                offsprings.Add(Mutation(offspring.Item2));
            }
            // Keep previous best chromosome
            offsprings.Add(Population[bestFitnessIndex]);

            // Replace population
            Population = offsprings;
        }
    }
}
