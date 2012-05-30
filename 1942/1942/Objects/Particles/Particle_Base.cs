using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Particle_Base : BaseObject
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (position.Y >= windowbounds.Y+ 500)
                dead = true;
        }

        
    }
}
