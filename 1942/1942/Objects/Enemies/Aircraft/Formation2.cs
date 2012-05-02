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

        public Formation2(Vector2 startingPos, bool mirrored)
        {
            this.mirrored = mirrored;
            list_Zero = new List<Enemy_Zero>();

            if (mirrored)
            {
                startingPos.X = Settings.window.ClientBounds.Width - startingPos.X;
                speed.X = -speed.X;
            }

            
                list_Zero.Add(new Enemy_Zero(startingPos));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X - 25, startingPos.Y - 40)));
                list_Zero.Add(new Enemy_Zero(new Vector2(startingPos.X + 25, startingPos.Y - 40)));
            

                
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (timer >= 300)
                completed = true;

            
        }


    }
}
