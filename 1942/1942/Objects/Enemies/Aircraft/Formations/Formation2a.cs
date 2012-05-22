using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Formation2a : BaseFormation
    {

        public Formation2a(Vector2 startingPos, bool mirrored)
        {
            this.mirrored = mirrored;
            enemyInFormationList = new List<BaseEnemy>();

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }

            
                enemyInFormationList.Add(new Enemy_Zero(startingPos));
                enemyInFormationList.Add(new Enemy_Zero(new Vector2(startingPos.X - 25, startingPos.Y - 40)));
                enemyInFormationList.Add(new Enemy_Zero(new Vector2(startingPos.X + 25, startingPos.Y - 40)));
            

                
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (timer >= 300)
                completed = true;

            
        }


    }
}
