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
            size = new Point(10, 10);
            color = Color.Yellow;
            texture = Texture2DLibrary.particle_smoke;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (color.R > 0)
                color.R-=1;
            if (color.B > 0)
                color.B-=1;
            if (color.G > 0)
                color.G-=1;
            position.Y += 2f;
            size.X++;
            size.Y++;
        }

    }
}
