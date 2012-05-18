using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Enemy_Tower: StationaryObject
    {

        float timeUntilNextShot;
        Texture2D textureBase;

        public Enemy_Tower(Vector2 startingPos)
        {
            
            angle = 0;
            color = Color.White;
            layerDepth = 0.9f;
            size = Settings.size_tower;
            position = startingPos;
            texture = Texture2DLibrary.enemy_tower;
            textureBase = Texture2DLibrary.enemy_tower_base;
            health = Settings.tower_health;
            maxHealth = Settings.tower_health;
            score = 10;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated)
            {

                
                //Checks for the currently nearest player
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

                //float a = (playerCenter.X - towerCenter.X);
                //float b = (playerCenter.Y - towerCenter.Y);
                //float c = (float)Math.Sqrt(a * a + b * b);

                //angle = (float)Math.Acos(a / c);


                
                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //calculate origin of bullets
                Vector2 shotOrigin = new Vector2(0, 0);
                shotOrigin.X = (float)Math.Cos(angle) * size.Y / 2 + towerCenter.X;
                shotOrigin.Y = (float)Math.Sin(angle) * size.Y / 2 + towerCenter.Y;

                Vector2 shot1Origin = new Vector2(0, 0);
                Vector2 shot2Origin = new Vector2(0, 0);
                float angleChange = (float)Math.PI / 8;
                shot1Origin.X = (shotOrigin.X - towerCenter.X) * (float)Math.Cos(angleChange) + (shotOrigin.Y - towerCenter.Y) * (float)Math.Sin(angleChange) + towerCenter.X;
                shot1Origin.Y = -(shotOrigin.X - towerCenter.X) * (float)Math.Sin(angleChange) + (shotOrigin.Y - towerCenter.Y) * (float)Math.Cos(angleChange) + towerCenter.Y;
                shot2Origin.X = (shotOrigin.X - towerCenter.X) * (float)Math.Cos(-angleChange) + (shotOrigin.Y - towerCenter.Y) * (float)Math.Sin(-angleChange) + towerCenter.X;
                shot2Origin.Y = -(shotOrigin.X - towerCenter.X) * (float)Math.Sin(-angleChange) + (shotOrigin.Y - towerCenter.Y) * (float)Math.Cos(-angleChange) + towerCenter.Y;

                if (timeUntilNextShot >= Settings.tower_projectile_frequency)
                {
                    timeUntilNextShot -= Settings.tower_projectile_frequency;
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Tower(
                        shot1Origin, angle)
                        );
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Tower(
                       shot2Origin, angle)
                       );

                }

                if (dead)
                {
                    Objects.deadList.Add(new Enemy_Tower_Dead(position, size));
                    Objects.particleList.Add(new Particle_Explosion(Center, size));
                }
                
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
                spriteBatch.Draw(Texture2DLibrary.enemy_tower_base,
                    Rectangle,
                    new Rectangle(0, 0, textureBase.Bounds.Width, textureBase.Bounds.Height),
                    color,
                    0,
                    new Vector2(0, 0),
                    spriteEffect,
                    layerDepth);
                spriteBatch.Draw(Texture2DLibrary.enemy_tower,
                    new Rectangle((int)Position.X + (Size.X / 2), (int)Position.Y + (Size.Y / 2), (int)(Size.X * (2f / 3f)), Size.Y),
                    new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                    color,
                    angle + (float)Math.PI / 2,
                    new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height/2),
                    spriteEffect, layerDepth);
            }
        }
    }
}
