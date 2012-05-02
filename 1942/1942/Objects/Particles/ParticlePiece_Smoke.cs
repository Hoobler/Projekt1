using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class ParticlePiece_Smoke : ParticlePiece_Base
    {


        public ParticlePiece_Smoke(Vector2 startingPos, Vector2 speed)
            : base(startingPos, speed)
        {
            texture = Texture2DLibrary.particle_smoke;
            color = Color.Black;
            lifeExpectancy = 120;
        }

        public override void Update(GameTime gameTime)
        {
            
            position += speed;
                       
        }

        

    }
}
