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

        public Enemy_Tower(Vector2 startingPos)
        {
            
            angle = 0;
            color = Color.White;
            layerDepth = 0.5f;
            size = new Point(4, 20);
            position = startingPos;
            texture = Texture2DLibrary.enemy_tower;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
                angle = (float)Math.Atan((position.Y - Objects.playerList[0].Position.Y - (Objects.playerList[0].Size.Y / 2)) / (position.X - Objects.playerList[0].Position.X - (Objects.playerList[0].Size.X / 2))) - (float)Math.PI / 2;
                
                if (Objects.playerList[0].Position.X + Objects.playerList[0].Size.X / 2 > position.X)
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
            spriteBatch.Draw(Texture2DLibrary.enemy_tower_base, new Rectangle((int)position.X - Size.Y / 2, (int)position.Y - Size.Y/2, Size.Y, Size.Y), new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height), Color.Red, 0, new Vector2(0, 0), spriteEffect, 0.4f);
            base.Draw(spriteBatch);
        }
    }
}
