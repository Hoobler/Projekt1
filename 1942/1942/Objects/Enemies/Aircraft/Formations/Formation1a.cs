using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Formation1a : BaseFormation
    {
        
        public Formation1a(Vector2 startingPos, bool mirrored)
        {
            this.mirrored = mirrored;
            enemyInFormationList = new List<BaseEnemy>();
            speed = new Vector2(1.5f, 0f);

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }
                enemyInFormationList.Add(new Enemy_Zero(startingPos));
                enemyInFormationList.Add(new Enemy_Zero(new Vector2(startingPos.X, startingPos.Y - 45)));
                enemyInFormationList.Add(new Enemy_Zero(new Vector2(startingPos.X, startingPos.Y - 90)));

                //for (int i = 0; i < list_Zero.Count; i++)
                //{
                //    list_Zero[i].Angle = (float)Math.PI;
                //}
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            for (int i = 0; i < enemyInFormationList.Count; i++)
            {
                if (timer >= (i * 30 + 45))
                {
                    enemyInFormationList[i].PosX = enemyInFormationList[i].Position.X + speed.X;
                    enemyInFormationList[i].animationFrame.Y = 1;
                    if (mirrored)
                        enemyInFormationList[i].spriteEffect = SpriteEffects.FlipHorizontally;
                    else
                        enemyInFormationList[i].spriteEffect = SpriteEffects.None;

                }
            
            }


            if (timer >= 300)
                completed = true;
        }

        
    }
}
