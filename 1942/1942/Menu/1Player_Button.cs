using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _1942
{
    class _1Player_Button : BaseButton
    {

        public _1Player_Button()
        {
            
        }
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.texture_1Player, Position, mColor);
        }
    }
}
