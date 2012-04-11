using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Formation1 : BaseFormation
    {
        
        Vector2 speed = new Vector2(0, 3f);

        public Formation1(Vector2 startingPos, Texture2D texture, bool mirrored)
        {
            this.mirrored = mirrored;
            list_Zero = new List<Enemy_Zero>();

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }
                list_Zero.Add(new Enemy_Zero(startingPos, new Vector2(0,3f)));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X, startingPos.Y - 45), speed));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X, startingPos.Y - 90), speed));

                for (int i = 0; i < list_Zero.Count; i++)
                {
                    list_Zero[i].Angle = (float)Math.PI;
                }
        }

        public override void Update(GameTime gameTime)
        {
            timer++;

            for (int i = 0; i < list_Zero.Count; i++)
            {
                list_Zero[i].Update(gameTime);
            }

            if (timer == 60)
            {
                for (int i = 0; i < list_Zero.Count; i++)
                {
                    if (mirrored)
                        list_Zero[i].Speed = new Vector2(-3f, list_Zero[i].Speed.Y);
                    else
                        list_Zero[i].Speed = new Vector2(3f, list_Zero[i].Speed.Y);
                }
            }

            if (timer >= 300)
                completed = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < list_Zero.Count; i++)
            {
                list_Zero[i].Draw(spriteBatch);
            }
        }
    }
}
