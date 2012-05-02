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
        
        

        public Formation1(Vector2 startingPos, bool mirrored)
        {
            this.mirrored = mirrored;
            list_Zero = new List<Enemy_Zero>();
            speed = new Vector2(2, 0f);

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }
                list_Zero.Add(new Enemy_Zero(startingPos));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X, startingPos.Y - 45)));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X, startingPos.Y - 90)));

                //for (int i = 0; i < list_Zero.Count; i++)
                //{
                //    list_Zero[i].Angle = (float)Math.PI;
                //}
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            for (int i = 0; i < list_Zero.Count; i++)
            {
                
                if (timer >= (i * 30 + 45))
                {
                    list_Zero[i].PosX = list_Zero[i].Position.X + speed.X;
                    list_Zero[i].animationFrame.Y = 1;
                    if (mirrored)
                        list_Zero[i].spriteEffect = SpriteEffects.FlipHorizontally;
                    else
                        list_Zero[i].spriteEffect = SpriteEffects.None;

                }
            }


            if (timer >= 300)
                completed = true;
        }

        
    }
}
