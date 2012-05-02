﻿using System;
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

        public virtual void Update(GameTime gameTime)
        {
            timer++;
            for (int i = 0; i < list_Zero.Count; i++)
            {
                list_Zero[i].Update(gameTime);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < list_Zero.Count; i++)
            {
                list_Zero[i].Draw(spriteBatch);
            }
        }

        public bool IsCompleted()
        {
            return completed;
        }

        
    }
}
