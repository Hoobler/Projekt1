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
        public List<Enemy_Zero> list_Zero;
        protected Vector2 speed;
        protected bool activated;

        public virtual void Update(GameTime gameTime)
        {

            for (int i = 0; i < list_Zero.Count; i++)
            {
                if (list_Zero[i].Activated)
                {
                    activated = true;
                }
            }

            if(activated)
                timer++;

            for (int i = 0; i < list_Zero.Count; i++)
            {
                list_Zero[i].Update(gameTime);
            }

            DeadRemoval();
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            for (int i = 0; i < list_Zero.Count; i++)
            {
                list_Zero[i].Draw(spriteBatch);
            }
        }

        public void DeadRemoval()
        {

            for (int j = list_Zero.Count - 1; j >= 0; j--)
            {
                if (list_Zero[j].IsDead())
                    list_Zero.RemoveAt(j);
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
