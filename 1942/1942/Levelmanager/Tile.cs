using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Tile
    {
        private Vector2 position;
        private char symbol;

        public Tile(Vector2 pos, char symbol)
        {
            position = pos;
            this.symbol = symbol;
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        } 
    }
}
