﻿using System;
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
        protected bool activated;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!activated)
            {
                position.Y += Settings.level_speed;
            }

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

            
        }

        public int Health
        { 
            get { return health; }
            set { health = value; }
        }

        public bool IsActivated()
        {
            return activated;
        }
    }
}
