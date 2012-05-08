using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss1_Gun : BaseEnemy
    {
        float timeUntilNextShot;
        float timeBetweenShots = 0.3f;

        public Boss1_Gun(Vector2 position, float timeUntilNextShot)
        {
            angle = (float)Math.PI*0.5f;
            color = Color.White;
            layerDepth = 0.0f;
            this.position = position;
            this.timeUntilNextShot = timeUntilNextShot;
            size = new Point(41, 41);
            maxHealth = 400;
            health = maxHealth;
            activated = false;
            texture = Texture2DLibrary.enemy_tower;
        }

        public void Update(GameTime gameTime, Vector2 speed)
        {
            if (activated)
            {
                if (health <= 0)
                {
                    health = 0;
                    dead = true;
                }
                color.B = (byte)((float)255 * ((float)health / (float)maxHealth));
                color.G = (byte)((float)255 * ((float)health / (float)maxHealth));
            }
            if (!activated)
            {
                position.Y += Settings.level_speed;
            }
            else
            {
                position += speed;
                int nearestPlayer = 0;
                for (int i = 1; i < Objects.playerList.Count; i++)
                {
                    float distanceCurrent = (float)Math.Sqrt(
                        (Position.X - Objects.playerList[i].Position.X) * (Position.X - Objects.playerList[i].Position.X) +
                        (Position.Y - Objects.playerList[i].Position.Y) * (Position.Y - Objects.playerList[i].Position.Y)
                        );

                    float distancePrevious = (float)Math.Sqrt(
                        (Position.X - Objects.playerList[i - 1].Position.X) * (Position.X - Objects.playerList[i - 1].Position.X) +
                        (Position.Y - Objects.playerList[i - 1].Position.Y) * (Position.Y - Objects.playerList[i - 1].Position.Y)
                        );

                    if (distanceCurrent < distancePrevious)
                        nearestPlayer = i;

                }

                Vector2 playerCenter = new Vector2(
                    (float)Objects.playerList[nearestPlayer].Position.X +
                    (float)Objects.playerList[nearestPlayer].Size.X / (float)2
                    ,
                    (float)Objects.playerList[nearestPlayer].Position.Y +
                    (float)Objects.playerList[nearestPlayer].Size.Y / (float)2);

                Vector2 towerCenter = new Vector2((float)Position.X + (float)Size.X / (float)2, (float)Position.Y + (float)Size.Y / (float)2);


                angle = (float)Math.Atan((playerCenter.Y - towerCenter.Y) / (playerCenter.X - towerCenter.X));

                if (playerCenter.X < towerCenter.X)
                    angle += (float)Math.PI;

                Vector2 shotOrigin = new Vector2(0, 0);
                shotOrigin.X = (float)Math.Cos(angle) * size.Y / 2 + towerCenter.X;
                shotOrigin.Y = (float)Math.Sin(angle) * size.Y / 2 + towerCenter.Y;

                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeUntilNextShot >= timeBetweenShots)
                {
                    timeUntilNextShot -= timeBetweenShots;
                    Objects.enemyProjectileList.Add(new Boss1_Projectile2(
                        shotOrigin, angle)
                        );
                }
                if (dead)
                    Objects.particleList.Add(new Particle_Explosion(new Vector2(position.X + size.X / 2, position.Y + size.Y / 2)));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.enemy_tower,
                    new Rectangle((int)Position.X + (Size.X / 2), (int)Position.Y + (Size.Y / 2), (int)(Size.X * (2f / 3f)), Size.Y),
                    new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                    color,
                    angle + (float)Math.PI / 2,
                    new Vector2(texture.Bounds.Width / 2, 32),
                    spriteEffect, layerDepth);
        }

        
    }
}
