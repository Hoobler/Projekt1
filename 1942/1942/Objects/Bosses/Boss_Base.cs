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
        protected bool accessorised;
        public List<Boss_Accessory> accessoryList = new List<Boss_Accessory>();
        
        protected List<Rectangle> targetableRectangles = new List<Rectangle>();
        protected int score;


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = 0; i < accessoryList.Count; i++)
            {
                accessoryList[i].Update(gameTime, speed);
            }

            if (!activated)
            {
                position.Y += Settings.level_speed;
            }

            if (!activated && position.Y >= -1000)
            {
                position.Y = -size.Y;
                if(!accessorised)
                Accessorize();
                activated = true;
                for (int i = 0; i < accessoryList.Count; i++)
                {
                    if (!accessoryList[i].Activated)
                        accessoryList[i].Activated = true;
                }
            }

            if (activated)
            {
                position += speed;
                if (health <= 0)
                {
                    health = 0;
                    dead = true;
                }
                color.B = (byte)((float)255 * ((float)health / (float)maxHealth));
                color.G = (byte)((float)255 * ((float)health / (float)maxHealth));
            }

            DeadRemoval();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < accessoryList.Count; i++)
            {
                accessoryList[i].Draw(spriteBatch);
            }
            
        }

        public virtual void Accessorize()
        {
            accessorised = true;
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

        public void DeadRemoval()
        {

            for (int j = accessoryList.Count - 1; j >= 0; j--)
            {
                if (accessoryList[j].IsDead())
                    accessoryList.RemoveAt(j);
            }

        }
    }
}
