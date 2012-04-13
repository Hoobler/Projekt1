using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Projectile_Enemy_Zero: BaseProjectile
    {

        public Projectile_Enemy_Zero(Vector2 startingPos, Vector2 zero_speed)
        {
            position = new Vector2(startingPos.X, startingPos.Y);
            size = new Point(4, 4);
            layerDepth = 1.0f;
            angle = (float)Math.PI / 4;
            speed = new Vector2(0, 2 + zero_speed.Y + Settings.level_speed);
            color = Color.White;
            texture = Texture2DLibrary.projectile_player;
            damage = 3;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (position.Y > Settings.window.ClientBounds.Height+size.Y)
                dead = true;
        }


    }
}
