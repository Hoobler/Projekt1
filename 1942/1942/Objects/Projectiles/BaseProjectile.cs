﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class BaseProjectile: BaseObject
    {

        protected int damage;

        public BaseProjectile(): base() { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            
            position += speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Center.X < 0 || Center.X > Settings.window.ClientBounds.Width || Center.Y < 0 || Center.Y > Settings.window.ClientBounds.Height)
                dead = true;
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
    }
}
