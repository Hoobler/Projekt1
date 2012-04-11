using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class TileTexture
    {
        private char symbol;
        private Texture2D texture;

        public TileTexture()
        {
        }

        public TileTexture(Texture2D texture, char symbol)
        {
            this.texture = texture;
            this.symbol = symbol;
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        } 
    }
}
