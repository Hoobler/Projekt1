using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss_Accessory : BaseEnemy
    {
        protected bool reallyActivated;
        protected bool killed;
        

        public virtual void Update(GameTime gameTime, Vector2 speed)
        {
            layerDepth = 0.0f;
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

            if (!activated)
            {
                position.Y += Settings.level_speed;
            }
            else if(activated)
            {
                position += speed;
            }

            
        }

        public virtual void AngleUpdate(float angle)
        {
            this.angle = angle;
        }
        public bool ReallyActivated
        {
            get { return reallyActivated; }
            set { reallyActivated = value; }
        }
        public bool Killed
        {
            get { return killed; }
        }
    }

}
