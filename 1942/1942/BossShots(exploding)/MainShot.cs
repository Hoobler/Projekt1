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
    class MainShot : BaseShot
    {
        Vector2 direction;

        public MainShot(Vector2 goal, Vector2 origin)
        {
            Position = origin;
            direction = goal - Position;
            direction.Normalize();
            Speed = 100;
        }

        public void Update(GameTime gameTime)
        {
            posX += direction.X * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            posY += direction.Y * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Rectangle = new Rectangle((int)posX, (int)posY, Size, Size);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.boss1_projectile, Rectangle, Color.White);
        }
    }
}
