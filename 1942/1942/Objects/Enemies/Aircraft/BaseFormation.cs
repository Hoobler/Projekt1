using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class BaseFormation
    {

        protected bool mirrored; //true = mirrored, false = not mirrored
        protected int timer;
        protected bool completed;
        public List<BaseEnemy> enemyInFormationList;
        protected Vector2 speed;
        protected bool activated;

        public virtual void Update(GameTime gameTime)
        {

            for (int i = 0; i < enemyInFormationList.Count; i++)
            {
                if (enemyInFormationList[i].Activated)
                {
                    activated = true;
                }
            }

            if(activated)
                timer++;

            for (int i = 0; i < enemyInFormationList.Count; i++)
            {
                enemyInFormationList[i].Update(gameTime);
            }

            DeadRemoval();
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            for (int i = 0; i < enemyInFormationList.Count; i++)
            {
                enemyInFormationList[i].Draw(spriteBatch);
            }
        }

        public void DeadRemoval()
        {

            for (int j = enemyInFormationList.Count - 1; j >= 0; j--)
            {
                if (enemyInFormationList[j].IsDead())
                    enemyInFormationList.RemoveAt(j);
            }

        }

        public bool IsCompleted()
        {
            return completed;
        }

        public bool IsActivated()
        {
            return activated;
        }
    }
}
