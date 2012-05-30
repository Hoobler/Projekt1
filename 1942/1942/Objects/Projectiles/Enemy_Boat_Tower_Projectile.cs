using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Enemy_Boat_Tower_Projectile : BaseProjectile
    {

        public Enemy_Boat_Tower_Projectile(Vector2 startingPos, float angle)
        {
            position = startingPos;
            this.angle = angle;
            this.size = Settings.boat_tower_projectile_size;
            layerDepth = 0.1f;
            color = Color.White;
            texture = Texture2DLibrary.enemy_boat_tower_projectile;
            damage = 2;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Settings.boat_tower_projectile_speed;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            
            if (position.X < 0 || position.X > Settings.windowBounds.X || position.Y < 0 || position.Y > Settings.windowBounds.Y)
                dead = true;
        }
    }
}
