using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class StationaryObject: BaseEnemy
    {

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position.Y += Settings.level_speed;
            if (position.Y > Settings.window.ClientBounds.Height + size.Y)
                dead = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
