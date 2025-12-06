// Item.cs
using System;
using System.Collections.Generic;

namespace LifeSim.Core
{
    [Serializable]
    public class Item
    {
        public string id;
        public string name;
        public string description;
        public Dictionary<string, double> modifiers = new Dictionary<string, double>();
    }
}
