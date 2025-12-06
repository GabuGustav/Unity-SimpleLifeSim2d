// Event.cs
using System;
using System.Collections.Generic;

namespace LifeSim.Core
{
    [Serializable]
    public class EventChoice
    {
        public string text;
        public Dictionary<string, double> effects = new Dictionary<string, double>(); // e.g. "health": -5
        public string nextEventId = null;
        public double weight = 1.0; // for random selection when eligible
        public List<string> requiresTags = new List<string>(); // e.g. "has_car"
    }

    [Serializable]
    public class EventDef
    {
        public string id;
        public string text;
        public List<EventChoice> choices = new List<EventChoice>();
        public Dictionary<string, string> conditions = new Dictionary<string, string>(); // simple string conditions e.g. job=programmer
        public double baseWeight = 1.0;
        public List<string> tags = new List<string>();
    }
}
