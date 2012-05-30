using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss5_Cannon : Boss_Accessory
    {
        float timeUntilNextShot;
        float timeBetweenShots;
        public Boss5_Cannon(Vector2 position)
        {
            
            
            timeBetweenShots = Settings.boss5_projectile_frequency;
            texture = Texture2DLibrary.boss5_cannon;
            size = new Point(texture.Width, texture.Height);
            maxHealth = 5;
            health = maxHealth;
            killable = true;
            this.position = position - new Vector2(size.X / 2f, size.Y / 2f);
        }

        public override void Update(GameTime gameTime, Vector2 speed)
        {
            base.Update(gameTime, speed);
            timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (reallyActivated)
            {
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



                angle = (float)Math.Atan((Objects.playerList[nearestPlayer].Center.Y - Center.Y) / (Objects.playerList[nearestPlayer].Center.X - Center.X));

                if (playerCenter.X < Center.X)
                    angle += (float)Math.PI;

                Vector2 shotOrigin = new Vector2(0, 0);
                shotOrigin.X = (float)Math.Cos(angle) * size.Y / 2 + Center.X;
                shotOrigin.Y = (float)Math.Sin(angle) * size.Y / 2 + Center.Y;

                if (timeUntilNextShot >= timeBetweenShots)
                {
                    timeUntilNextShot -= timeBetweenShots;
                    Objects.enemyProjectileList.Add(new Boss5_Projectile(shotOrigin, angle, Objects.playerList[nearestPlayer].Center));
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                    new Rectangle((int)Center.X, (int)Center.Y, Size.X, Size.Y),
                    new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                    color,
                    angle + (float)Math.PI / 2,
                    new Vector2(texture.Bounds.Width / 2, 32),
                    spriteEffect, layerDepth);
        }
    }
}
