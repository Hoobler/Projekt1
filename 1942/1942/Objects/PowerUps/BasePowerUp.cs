using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    class BasePowerUp : BaseObject
    {
        protected bool isalive;

        public BasePowerUp()
        {
            size = new Point(25, 25);
        }
        
        public bool IsAlive
        {
            get { return isalive; }
            set
            {
                isalive = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.texture_PowerUp_Health, new Rectangle((int)Position.X, (int)Position.Y, size.X, size.Y), Color.White);
        }
    }
}
