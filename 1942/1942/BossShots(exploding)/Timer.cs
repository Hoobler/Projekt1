using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1942
{
    class Timer
    {
        float time;
        public Timer(float myTime)
        {
            time = myTime;
        }

        public float Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}
