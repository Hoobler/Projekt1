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
            layerDepth = 0.5f;
            size = new Point(20, 20);
            position = startingPos;
            texture = Texture2DLibrary.enemy_tower;
            textureBase = Texture2DLibrary.enemy_tower_base;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int nearestPlayer = 0;

            for (int i = 1; i < Objects.playerList.Count; i++)
            {
                float distanceCurrent = (float)Math.Sqrt(
                    (Position.X - Objects.playerList[i].Position.X) * (Position.X - Objects.playerList[i].Position.X) +
                    (Position.Y - Objects.playerList[i].Position.Y) * (Position.Y - Objects.playerList[i].Position.Y)
                    );

                float distancePrevious = (float)Math.Sqrt(
                    (Position.X - Objects.playerList[i-1].Position.X) * (Position.X - Objects.playerList[i-1].Position.X) +
                    (Position.Y - Objects.playerList[i-1].Position.Y) * (Position.Y - Objects.playerList[i-1].Position.Y)
                    );

                if (distanceCurrent < distancePrevious)
                    nearestPlayer = i;

            }




            angle = (float)Math.Atan((position.Y - Objects.playerList[nearestPlayer].Position.Y - (Objects.playerList[nearestPlayer].Size.Y / 2)) /
                (position.X - Objects.playerList[nearestPlayer].Position.X - (Objects.playerList[nearestPlayer].Size.X / 2))) - (float)Math.PI / 2;

            if (Objects.playerList[nearestPlayer].Position.X + Objects.playerList[nearestPlayer].Size.X / 2 > position.X)
                    angle += (float)Math.PI;

                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;


                if (timeUntilNextShot >= Settings.tower_projectile_frequency)
                {
                    timeUntilNextShot -= Settings.tower_projectile_frequency;
                    Objects.enemyProjectileList.Add(new Projectile_Enemy_Tower(position, angle - (float)Math.PI / 2));
                }

                if (dead)
                    Objects.deadList.Add(new Enemy_Tower_Dead(position, size));
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.enemy_tower_base, Rectangle, new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height), Color.Red, 0, new Vector2(0, 0), spriteEffect, layerDepth);
            spriteBatch.Draw(Texture2DLibrary.enemy_tower, new Rectangle((int)Position.X + (Size.X / 2), (int)Position.Y + (Size.Y / 2), (int)(Size.X * (2f / 3f)), Size.Y), new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height), Color.White, angle,
                new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2), spriteEffect, layerDepth);
        }
    }
}
