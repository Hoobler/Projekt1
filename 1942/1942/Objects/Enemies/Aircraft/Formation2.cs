using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Formation2 : BaseFormation
    {

        
        Vector2 speed = new Vector2(3, 0);

        public Formation2(Vector2 startingPos, Texture2D texture, bool mirrored)
        {
            this.mirrored = mirrored;
            list_Zero = new List<Enemy_Zero>();

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }

            
                list_Zero.Add(new Enemy_Zero(startingPos, speed));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X - 60, startingPos.Y), speed));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X - 120, startingPos.Y), speed));
            

                for (int i = 0; i < list_Zero.Count; i++)
                {
                    if (!mirrored)
                        list_Zero[i].Angle = (float)Math.PI / 2;
                    else
                        list_Zero[i].Angle = (float)Math.PI * 3 / 2;
                }
        }

        public override void Update(GameTime gameTime)
        {
            

            for (int i = 0; i < list_Zero.Count; i++)
            {
                
                list_Zero[i].Update(gameTime);
            }

            if (timer >= 300)
                completed = true;

            timer++;
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
