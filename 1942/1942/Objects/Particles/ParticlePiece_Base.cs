using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class ParticlePiece_Base : BaseObject
    {
        
        protected float lifeExpectancy;

        public ParticlePiece_Base(Vector2 startingPos, Vector2 speed)
        {
            position = startingPos;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            lifeExpectancy -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (lifeExpectancy <= 0)
                dead = true;
        }
        

    }
}
