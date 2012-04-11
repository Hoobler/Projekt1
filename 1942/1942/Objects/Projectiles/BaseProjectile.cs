using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class BaseProjectile: BaseObject
    {

        public BaseProjectile(): base() { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            position += speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
