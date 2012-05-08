using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class BaseEnemy: BaseObject
    {
        protected int health;
        protected int maxHealth;
        protected bool flying;
        protected bool activated;

        public BaseEnemy() : base()
        { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (activated)
            {
                if (health <= 0)
                {
                    health = 0;
                    dead = true;
                }
                color.B = (byte)((float)255 * ((float)health / (float)maxHealth));
                color.G = (byte)((float)255 * ((float)health / (float)maxHealth));
            }

            if (position.Y > -size.Y*2)
                activated = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(activated)
                base.Draw(spriteBatch);
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int HealthMax
        {
            get { return maxHealth; }
        }

        public bool IsFlying
        { get { return flying; } }

        public bool Activated
        { 
            get { return activated; }
            set { activated = value; }
        }
    }
}
