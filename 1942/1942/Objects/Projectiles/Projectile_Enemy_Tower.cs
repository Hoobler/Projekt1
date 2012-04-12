﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Projectile_Enemy_Tower : BaseProjectile
    {

        public Projectile_Enemy_Tower(Vector2 startingPos, float angle)
        {
            position = startingPos;
            this.angle = angle;
            this.size = new Point(4, 4);
            layerDepth = 1.0f;
            color = Color.White;
            texture = Texture2DLibrary.projectile_enemy_tower;

            speed = new Vector2((float)Math.Cos(angle) * Settings.tower_projectile_speed, (float)Math.Sin(angle) * Settings.tower_projectile_speed);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (position.X < 0 || position.X > Settings.window.ClientBounds.Width || position.Y < 0 || position.Y > Settings.window.ClientBounds.Height)
                dead = true;
        }
    }
}