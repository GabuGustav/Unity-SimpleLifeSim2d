// RandomUtil.cs
using System;

namespace LifeSim.Core
{
    public class RandomUtil
    {
        private Random rng;
        public int Seed { get; private set; }
        public RandomUtil(int? seed = null)
        {
            if (seed == null)
            {
                Seed = Environment.TickCount;
            }
            else
            {
                Seed = seed.Value;
            }
            rng = new Random(Seed);
        }
        public int NextInt(int minInclusive, int maxExclusive) => rng.Next(minInclusive, maxExclusive);
        public double NextDouble() => rng.NextDouble();
        // Weighted pick from weights array, returns index
        public int WeightedPick(double[] weights)
        {
            double sum = 0;
            foreach (var w in weights) sum += w;
            double pick = NextDouble() * sum;
            double accum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                accum += weights[i];
                if (pick <= accum) return i;
            }
            return weights.Length - 1;
        }
    }
}
