// TimeSystem.cs
using System;

namespace LifeSim.Core
{
    public class TimeSystem
    {
        public int Hour { get; private set; } = 8;
        public int Day { get; private set; } = 0;
        public void AdvanceHours(int hours)
        {
            Hour += hours;
            while (Hour >= 24)
            {
                Hour -= 24;
                Day++;
            }
        }
    }
}
