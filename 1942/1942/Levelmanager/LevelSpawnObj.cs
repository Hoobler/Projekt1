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
        private bool mirrored;
        

        public LevelSpawnObj(Vector2 pos, string formation, bool mirrored)
        {
            position = pos;
            this.formation = formation;
            this.mirrored = mirrored;
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

        

        public bool IsMirrored()
        {
            return mirrored;
        }
    }
}
