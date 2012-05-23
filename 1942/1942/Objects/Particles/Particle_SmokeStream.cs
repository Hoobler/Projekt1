using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Particle_SmokeStream : Particle_Base
    {

        public Particle_SmokeStream(Vector2 position)
        {
            this.position = position;
            size = new Point(10, 10);
            color = Color.White;
            texture = Texture2DLibrary.particle_smoke;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(color.R > 0)
                color.R-=2;
            if (color.B > 0) 
                color.B-=2;
            if (color.G > 0) 
                color.G-=2;
            if (color.A > 0)
                color.A -= 2;
            position.Y += 5f;
            size.X++;
            size.Y++;
        }
    }
}
