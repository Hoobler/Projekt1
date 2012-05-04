using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Enemy_Tower_Dead: StationaryObject
    {
        float animationTimer;
        float animationTimerReset = 0.25f;
        public Enemy_Tower_Dead(Vector2 startingPos, Point size)
        {
            texture = Texture2DLibrary.enemy_tower_dead;
            position = startingPos;
            this.size = new Point (size.Y, size.Y);
            color = Color.White;
            layerDepth = 0.5f;
            animationFrame = new Point(0, 0);
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (activated)
            {
                animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (animationTimer >= animationTimerReset)
                {
                    animationFrame.X++;
                    animationTimer -= animationTimerReset;
                }



                if (animationFrame.X > 6)
                    animationFrame.X -= 7;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
            if(activated)
            spriteBatch.Draw(texture,
                Rectangle,
                new Rectangle(1+(41*animationFrame.X),
                    1,
                    40,
                    40),
                color,
                0,
                new Vector2(0, 0),
                spriteEffect,
                0.0f);
        }
    }
}
