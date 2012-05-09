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

        public Enemy_Zero(Vector2 position)
        {

            this.position = position;
            this.speed = Settings.zero_speed;

            angle = (float)Math.PI;
            layerDepth = 1f;
            color = Color.White;
            size = Settings.size_zero;
            spriteEffect = SpriteEffects.None;
            texture = Texture2DLibrary.enemy_zero;
            health = Settings.zero_health;
            maxHealth = Settings.zero_health;
            score = 10;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated)
            {
                position += speed;
                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeUntilNextShot >= Settings.zero_projectile_frequency)
                {
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Zero(new Vector2(position.X, position.Y+size.Y)));
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Zero(new Vector2(position.X + size.X, position.Y+size.Y)));
                    timeUntilNextShot -= Settings.zero_projectile_frequency;
                }

                if (position.Y > Settings.window.ClientBounds.Height)
                    dead = true;

                if (dead)
                    Objects.particleList.Add(new Particle_Explosion(position));



            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(activated)
            spriteBatch.Draw(texture,
                new Rectangle((int)Position.X + (Size.X / 2),(int)Position.Y + (Size.Y / 2), Size.X, Size.Y),
                new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                    (animationFrame.Y * (texture.Bounds.Height - 1) / 3) + 1,
                    ((texture.Bounds.Width - 1) / 3)-1,
                    ((texture.Bounds.Height - 1) / 3) - 1),
                color,
                angle,
                new Vector2(Settings.size_zero.X/2, Settings.size_zero.Y/2),
                spriteEffect,
                0.0f);
        }
        

    }
}
