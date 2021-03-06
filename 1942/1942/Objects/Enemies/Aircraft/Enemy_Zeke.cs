﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Enemy_Zeke : FlyingObject
    {

        private float timeUntilNextShot;

        public Enemy_Zeke(Vector2 position)
        {

            this.position = position;
            this.speed = Settings.zero_speed;

            angle = (float)Math.PI;
            layerDepth = 1f;
            color = Color.White;
            size = Settings.size_zeke;
            spriteEffect = SpriteEffects.None;
            texture = Texture2DLibrary.enemy_zeke;
            health = Settings.zeke_health;
            maxHealth = Settings.zeke_health;
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
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Zero(new Vector2(position.X, position.Y + size.Y), speed));
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Zero(new Vector2(position.X + size.X, position.Y + size.Y), speed));
                    timeUntilNextShot -= Settings.zero_projectile_frequency;
                }

                if (position.Y > Settings.windowBounds.Y)
                    dead = true;


                if (dead && position.Y < Settings.windowBounds.Y - size.Y)
                    Objects.particleList.Add(new Particle_Explosion(Center, size));

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
                spriteBatch.Draw(texture,
                new Rectangle((int)Position.X - size.X / 2, (int)Position.Y - size.Y / 2, Size.X, Size.Y),
                new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                    (animationFrame.Y * (texture.Bounds.Height - 1) / 3) + 1,
                    ((texture.Bounds.Width - 1) / 3) - 1,
                    ((texture.Bounds.Height - 1) / 3) - 1),
                color,
                angle,
                new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                spriteEffect,
                0.0f);
        }


    }
}
