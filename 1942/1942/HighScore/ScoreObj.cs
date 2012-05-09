using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1942
{
    class ScoreObj
    {
        private string playerName;
        private int playerScore;
        private int playerPlacment;

        public ScoreObj() : this(String.Empty, 0, 0) 
        {
            playerName = String.Empty;
            playerScore = 0;
            playerPlacment = 0;
        }

        public ScoreObj(string Name, int Score, int placement)
        {
            playerName = Name;
            playerScore = Score;
            playerPlacment = placement;
        }

        public int PlayerScore
        {
            get { return playerScore; }
            set { playerScore = value; }
        }

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public int PlayerPlacement
        {
            get { return playerPlacment; }
            set { playerPlacment = value; }
        }
    }
}
