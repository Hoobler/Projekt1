using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _1942
{
    class TileTexture
    {
        private char symbol;
        private Texture2D texture;
        private SpriteEffects spriteEffect;

        public TileTexture()
        {
        }

        public TileTexture(Texture2D texture, char symbol, bool hFlip, bool vFlip)
        {
            this.texture = texture;
            this.symbol = symbol;

            if (hFlip)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            else if (vFlip)
            {
                spriteEffect = SpriteEffects.FlipVertically;
            }

            else
            {
                spriteEffect = SpriteEffects.None;
            }

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

        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
        }
    }
}
