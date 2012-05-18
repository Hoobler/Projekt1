using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Formation3a : BaseFormation
    {

        public Formation3a(Vector2 startingPos, bool mirrored)
        {
            this.mirrored = mirrored;
            enemyInFormationList = new List<BaseEnemy>();

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }


            enemyInFormationList.Add(new Enemy_Zero(startingPos));
            enemyInFormationList.Add(new Enemy_Zero(new Vector2(startingPos.X - 60, startingPos.Y)));
            enemyInFormationList.Add(new Enemy_Zero(new Vector2(startingPos.X + 60, startingPos.Y)));



        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (timer >= 300)
                completed = true;


        }


    }
}
