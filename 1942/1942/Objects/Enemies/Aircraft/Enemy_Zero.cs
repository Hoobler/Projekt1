using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Enemy_Zero : FlyingObject
    {

        private float timeUntilNextShot;

        public Enemy_Zero(Vector2 position, Vector2 speed)
        {

            this.position = position;
            this.speed = speed;
            
            layerDepth = 1f;
            color = Color.White;
            size = new Point(20,20);
            spriteEffect = SpriteEffects.FlipVertically;
            texture = Texture2DLibrary.enemy_zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeUntilNextShot >= Settings.zero_projectile_frequency)
            {
                Objects.enemyProjectileList.Add(new Projectile_Enemy_Zero(new Vector2(position.X, position.Y + size.Y), speed));
                Objects.enemyProjectileList.Add(new Projectile_Enemy_Zero(new Vector2(position.X+size.X, position.Y + size.Y), speed));
                timeUntilNextShot -= Settings.zero_projectile_frequency;
            }

            if (position.Y > Settings.window.ClientBounds.Height)
                dead = true;


        }

        

    }
}
