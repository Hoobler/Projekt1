using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Particle_FireStream : Particle_Base
    {
        public Particle_FireStream(Vector2 position)
        {
            this.position = position;
            size = new Point(2, 2);
            color = Color.Yellow;
            texture = Texture2DLibrary.boss1_projectile;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (color.R > 0)
                color.R-=2;
            if (color.B > 0)
                color.B-=2;
            if (color.G > 0)
                color.G-=2;
            position.Y += 5f;
        }

    }
}
