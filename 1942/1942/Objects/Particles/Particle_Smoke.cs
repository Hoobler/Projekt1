using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Particle_Smoke : Particle_Base
    {
        

        public Particle_Smoke(Vector2 startingPos)
        {
            position = startingPos;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            particles.Add(new ParticlePiece_Smoke(position, new Vector2((float)random.Next(-2, 2) + (float)random.NextDouble(), 2 + (float)random.NextDouble())));
        }

        
    }
}
