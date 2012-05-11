using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1942
{
    class SortHighScore_Ascending : IComparer<ScoreObj>
    {
        public int Compare(ScoreObj x, ScoreObj y)
        {
            if (x.PlayerScore < y.PlayerScore) return 1;
            else if (x.PlayerScore > y.PlayerScore) return -1;
            else return 0;
        }
    }
}
