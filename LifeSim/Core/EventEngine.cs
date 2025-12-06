// EventEngine.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSim.Core
{
    public class EventEngine
    {
        private List<EventDef> eventPool = new List<EventDef>();
        private RandomUtil RNG;
        public EventEngine(RandomUtil rng)
        {
            RNG = rng;
        }
        public void LoadEvents(IEnumerable<EventDef> events)
        {
            eventPool = events.ToList();
        }

        // Evaluate eligible events given player & world state
        public List<EventDef> GetEligibleEvents(Player p, WorldState w)
        {
            var eligible = new List<EventDef>();
            foreach (var e in eventPool)
            {
                bool ok = true;
                foreach (var cond in e.conditions)
                {
                    // simple conditions: stat comparisons or equality
                    if (cond.Key == "job")
                    {
                        if (p.jobId != cond.Value) ok = false;
                    }
                    else if (cond.Key == "minMoney")
                    {
                        if (!(p.money >= double.Parse(cond.Value))) ok = false;
                    }
                    else if (cond.Key == "maxStress")
                    {
                        p.EnsureSkill("stress");
                    }
                    if (!ok) break;
                }
                if (ok) eligible.Add(e);
            }
            return eligible;
        }

        // Pick an event by weighted randomness
        public EventDef PickEvent(Player p, WorldState w)
        {
            var elig = GetEligibleEvents(p, w);
            if (elig.Count == 0) return null;
            double[] weights = elig.Select(e => e.baseWeight).ToArray();
            int idx = RNG.WeightedPick(weights);
            return elig[idx];
        }

        // Apply a choice's effects to player/world/npcs (simple convention)
        public void ApplyChoiceEffects(Player p, WorldState w, EventChoice choice)
        {
            foreach (var eff in choice.effects)
            {
                switch (eff.Key)
                {
                    case "health": p.health += (float)eff.Value; break;
                    case "energy": p.energy += (float)eff.Value; break;
                    case "happiness": p.happiness += (float)eff.Value; break;
                    case "money": p.money += eff.Value; break;
                    default:
                        // treat as skill xp if starts with skill_
                        if (eff.Key.StartsWith("skill_"))
                        {
                            string sk = eff.Key.Substring(6);
                            p.AddSkillXP(sk, (int)eff.Value);
                        }
                        break;
                }
            }
        }
    }
}
