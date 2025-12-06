// NPC.cs
using System;
using System.Collections.Generic;

namespace LifeSim.Core
{
    [Serializable]
    public class NPC
    {
        public string id;
        public string name;
        public Dictionary<string, int> stats = new Dictionary<string, int>(); // friendship, trust, attraction
        public List<string> memory = new List<string>();
        public string role = null;
        public NPC() { }
        public void EnsureStat(string key)
        {
            if (!stats.ContainsKey(key)) stats[key] = 0;
        }
        public void ModifyStat(string key, int delta)
        {
            EnsureStat(key);
            stats[key] = stats[key] + delta;
        }
    }
}
