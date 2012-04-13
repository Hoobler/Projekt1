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
        protected bool flying;

        public BaseEnemy() : base()
        { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (health < 0)
                dead = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public bool Flying
        { get { return flying; } }
    }
}
