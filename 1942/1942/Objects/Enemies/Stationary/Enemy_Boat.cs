using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Enemy_Boat : StationaryObject
    {

        Enemy_Boat_Tower tower;
        float towerShift;

        public Enemy_Boat(Vector2 position, bool mirrored)
        {
            texture = Texture2DLibrary.enemy_boat;
            size = Settings.boat_size;
            this.position = position;
            speed = Settings.boat_speed;
            angle = (float)Math.PI / 2f;
            towerShift = size.X * (1f / 6f);
            if (mirrored)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                speed.X = -speed.X;
                towerShift = -towerShift;
            }
            
            tower = new Enemy_Boat_Tower(new Vector2(Center.X+towerShift, Center.Y));
            maxHealth = Settings.boat_health;
            health = maxHealth;
            score = 50;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (activated)
            {
                position += speed;
                tower.Update(gameTime, new Vector2(Center.X + towerShift, Center.Y));
            }
            if (dead)
                Objects.particleList.Add(new Particle_Explosion(Center, new Point(size.X, size.X)));
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            if (activated)
            {
                spriteBatch.Draw(texture,
                    Rectangle,
                    null,
                    color,
                    0,
                    new Vector2(0, 0),
                    spriteEffect, layerDepth);
                tower.Draw(spriteBatch);
            }
        }
    }
}
