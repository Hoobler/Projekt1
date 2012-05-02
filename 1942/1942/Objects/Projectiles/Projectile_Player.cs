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
            size = Settings.player_projectile_size;
            layerDepth = 1.0f;
            angle = 0;
            speed = Settings.player_projectile_speed;
            color = Color.PeachPuff;
            texture = Texture2DLibrary.projectile_player;
            damage = 10;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (position.Y < -size.Y)
                dead = true;
        }

        


    }
}
