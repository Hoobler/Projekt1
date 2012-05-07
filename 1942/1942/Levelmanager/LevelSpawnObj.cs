using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class LevelSpawnObj
    {
        private Vector2 position;
        private string formation;
        private int spawnTime;

        public LevelSpawnObj(Vector2 pos, string formation, int spawntime)
        {
            position = pos;
            this.formation = formation;
            this.spawnTime = spawntime;
        }

        public string Formation
        {
            get { return formation; }
            set { formation = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
