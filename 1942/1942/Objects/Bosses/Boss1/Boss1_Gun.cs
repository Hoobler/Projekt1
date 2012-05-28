using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss1_Gun : Boss_Accessory
    {
        float timeUntilNextShot;
        float timeBetweenShots = Settings.boss1_projectile_frequency;
        
        

        public Boss1_Gun(Vector2 position, float timeUntilNextShot)
        {
            texture = Texture2DLibrary.boss1_gun;
            angle = (float)Math.PI*0.5f;
            color = Color.White;
            layerDepth = 0.0f;
            this.position = position;
            this.timeUntilNextShot = timeUntilNextShot;
            size = new Point(texture.Bounds.Width, texture.Bounds.Height);
            maxHealth = 400;
            health = maxHealth;
            this.position.X -= size.X / 2;
            this.position.Y -= size.Y / 2;
            killable = false;
            
        }

        public override void Update(GameTime gameTime, Vector2 speed)
        {
            base.Update(gameTime, speed);

                if(reallyActivated)
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

                
                    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeUntilNextShot >= timeBetweenShots)
                    {
                        timeUntilNextShot -= timeBetweenShots;
                        Objects.enemyProjectileList.Add(new Boss1_Projectile(
                            shotOrigin, angle)
                            );
                    }
                }
                if (dead)
                    Objects.particleList.Add(new Particle_Explosion(Center, size));
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                    new Rectangle((int)Center.X, (int)Center.Y, Size.X, Size.Y),
                    new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                    color,
                    angle + (float)Math.PI / 2,
                    new Vector2(texture.Bounds.Width / 2, 52),
                    spriteEffect, layerDepth);
            
        }

        public override Rectangle Rectangle
        {
            get { return new Rectangle(-30, -30, 0, 0); }
        }
    }
}
