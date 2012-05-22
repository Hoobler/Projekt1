using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _1942
{
    class FlyingObject: BaseEnemy
    {


        public FlyingObject()
            : base()
        {
            flying = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position.Y += Settings.level_speed;
        }

        

        
        public Vector2 Speed
        {
            set { speed = value; }
            get { return speed; }
        }
    }
}
