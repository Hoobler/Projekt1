using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace _1942
{
    class Splittershot : BaseShot
    {
        Vector2 vectorSpeed;

        public Splittershot(float angle, Vector2 origin)
        {
            Position = origin;
            
            Speed = 2;
            vectorSpeed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Speed;
        }

        public void Update(GameTime gameTime)
        {
            Position += vectorSpeed;
            Rectangle = new Rectangle((int)posX, (int)posY, Size, Size);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.boss1_projectile, Rectangle, Color.White);
        }
    }
}
