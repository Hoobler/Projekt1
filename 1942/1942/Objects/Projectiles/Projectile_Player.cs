using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Projectile_Player : BaseProjectile
    {
        public Projectile_Player(Vector2 startingPos)
            : base()
        {
            position = new Vector2(startingPos.X, startingPos.Y);
            size = new Point(4, 4);
            layerDepth = 1.0f;
            angle = (float)Math.PI / 4;
            speed = new Vector2(0, -10);
            color = Color.White;
            texture = Texture2DLibrary.projectile_player;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (position.Y < -size.Y)
                dead = true;
        }

        


    }
}
