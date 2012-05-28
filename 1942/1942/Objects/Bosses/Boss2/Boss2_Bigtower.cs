using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss2_Bigtower : Boss_Accessory
    {
        int phase;
        float timeUntilNextShot = 0.5f;
        float timeBetweenShots = 0.5f;

        public Boss2_Bigtower(Vector2 position)
        {
            this.position = position;
            
            color = Color.White;
            texture = Texture2DLibrary.boss2_biggun;
            size = new Point(texture.Bounds.Width, texture.Bounds.Height);
            angle = (float)Math.PI * (1f / 2f);
            maxHealth = 2000;
            health = maxHealth;
            this.position.X -= size.X / 2;
            killable = true;
        }

        public override void Update(GameTime gameTime, Vector2 speed)
        {
            base.Update(gameTime, speed);
            if(reallyActivated)
            {
                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeUntilNextShot >= timeBetweenShots)
                {
                    Vector2 towerCenter = new Vector2((float)Position.X + (float)Size.X / (float)2, (float)Position.Y + (float)Size.Y / (float)2);
                    Vector2 shotOrigin = new Vector2(0, 0);
                    shotOrigin.X = (float)Math.Cos(angle) * size.Y / 2 + towerCenter.X;
                    shotOrigin.Y = (float)Math.Sin(angle) * size.Y / 2 + towerCenter.Y;
                    Objects.enemyProjectileList.Add(new Boss2_BigShot(shotOrigin, angle));
                    timeUntilNextShot -= timeBetweenShots;
                }

                if (phase == 0)
                {
                    angle += (float)Math.PI / 120f;

                    if (angle >= (float)Math.PI)
                    {
                        angle = (float)Math.PI;
                        phase = 1;
                    }
                }
                else if (phase == 1)
                {
                    angle -= (float)Math.PI / 120f;

                    if (angle <= 0)
                    {
                        angle = 0;
                        phase = 0;
                    }
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
                    new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height*(2f/3f)),
                    spriteEffect, layerDepth);
        }
    }
}
