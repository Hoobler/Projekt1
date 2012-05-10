using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss_Base : BaseObject
    {
        protected int maxHealth;
        protected int health;
        protected bool activated;
        protected bool killable;
        public List<Boss1_Gun> gunList = new List<Boss1_Gun>();
        protected List<Rectangle> targetableRectangles = new List<Rectangle>();
        protected int score;


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = 0; i < gunList.Count; i++)
            {
                gunList[i].Update(gameTime, speed);
            }

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < gunList.Count; i++)
            {
                gunList[i].Draw(spriteBatch);
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

        public bool IsKillable()
        {
            return killable;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public List<Rectangle> TargetRectangles
        {
            get { return targetableRectangles; }
        }
    }
}
