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
        protected List<Enemy_Zero> list_Zero;

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public bool IsCompleted()
        {
            return completed;
        }

        
    }
}
