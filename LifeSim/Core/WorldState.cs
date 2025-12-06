// WorldState.cs
using System;
using System.Collections.Generic;

namespace LifeSim.Core
{
    [Serializable]
    public class WorldState
    {
        public int day = 0;
        public int hour = 8;
        public double inflation = 1.0;
        public double crimeLevel = 0.1; // 0..1
        public double economyHealth = 1.0; // 0..2
        public string season = "spring";
        public Dictionary<string, double> regionVars = new Dictionary<string, double>();
        public WorldState()
        {
            regionVars["cityWealth"] = 1.0;
        }
        public void NextDay()
        {
            day++;
            // simple seasonal tick
            if (day % 90 == 0) season = NextSeason(season);
            // dynamic economy drift (example)
            economyHealth += (new Random()).NextDouble() * 0.02 - 0.01;
            economyHealth = Math.Max(0.2, Math.Min(2.0, economyHealth));
            // crime moves a bit randomly
            crimeLevel += (new Random()).NextDouble() * 0.02 - 0.01;
            crimeLevel = Math.Max(0.0, Math.Min(1.0, crimeLevel));
        }
        private string NextSeason(string cur)
        {
            switch (cur)
            {
                case "spring": return "summer";
                case "summer": return "autumn";
                case "autumn": return "winter";
                default: return "spring";
            }
        }
    }
}
