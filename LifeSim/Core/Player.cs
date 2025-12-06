// Player.cs
using System;
using System.Collections.Generic;

namespace LifeSim.Core
{
    [Serializable]
    public class Player
    {
        public string name = "Player";
        public int age = 18;
        public float health = 100f;
        public float energy = 100f;
        public float happiness = 50f;
        public double money = 100.0;
        public Dictionary<string, int> skills = new Dictionary<string, int>();
        public List<string> traits = new List<string>();
        public List<string> inventory = new List<string>();
        public string jobId = null;
        public bool isAlive = true;

        public void EnsureSkill(string key)
        {
            if (!skills.ContainsKey(key)) skills[key] = 0;
        }
        public void AddSkillXP(string skill, int xp)
        {
            EnsureSkill(skill);
            skills[skill] = Math.Min(999, skills[skill] + xp);
        }
    }
}
