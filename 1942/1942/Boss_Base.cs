using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss_Base : BaseObject
    {
        protected int maxHealth;
        protected int health;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            color.B = (byte)((float)255 * ((float)health / (float)maxHealth));
            color.G = (byte)((float)255 * ((float)health / (float)maxHealth));

            if (health <= 0)
                dead = true;
        }

        public int Health
        { 
            get { return health; }
            set { health = value; }
        }
    }
}
