using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    

    class Enemy_Boat_Tower : BaseObject
    {
        float timeUntilNextShot;
        float timeBetweenShots;

        public Enemy_Boat_Tower(Vector2 position)
        {
            this.position = position;
            texture = Texture2DLibrary.enemy_boat_tower;
            size = Settings.boat_tower_size;
            timeBetweenShots = Settings.boat_tower_projectile_frequency;

        }
        public void Update(GameTime gameTime, Vector2 position)
        {
            this.position.X = position.X - size.X/2f;
            this.position.Y = position.Y - size.Y/2f;

            
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
                    Objects.enemyProjectileList.Add(new Enemy_Boat_Tower_Projectile(
                        shotOrigin, angle)
                        );
                }
            

        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
            
            spriteBatch.Draw(texture,
                    new Rectangle((int)Position.X + (Size.X / 2), (int)Position.Y + (Size.Y / 2), (int)(Size.X * (2f / 3f)), Size.Y),
                    new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                    color,
                    angle + (float)Math.PI / 2,
                    new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                    spriteEffect,
                    layerDepth);
        }
    }
}
