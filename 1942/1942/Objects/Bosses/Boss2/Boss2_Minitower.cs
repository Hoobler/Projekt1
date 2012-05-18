using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss2_Minitower : Boss_Accessory
    {
        float timeUntilNextBarrage = 5f;
        float timeBetweenBarrages = 10f;

        bool barraging;

        float timeUntilNextShot;
        float timeBetweenShots = 0.1f;

        float timeUntilBarrageEnds;
        float timeBarrageLength = 5f;

        public Boss2_Minitower(Vector2 position)
        {
            this.position = position;
            texture = Texture2DLibrary.boss2_smallgun;
            size = new Point(Texture2DLibrary.boss2_smallgun.Bounds.Width, Texture2DLibrary.boss2_smallgun.Bounds.Height);
            color = Color.White;
            maxHealth = 50;
            health = maxHealth;
            
        }

        public override void Update(GameTime gameTime, Vector2 speed)
        {
            layerDepth = 0.0f;
            if (activated)
            {
                if (health <= 0)
                {
                    health = 0;
                    dead = true;
                }
            }

            if (!activated)
                position.Y += Settings.level_speed;
            
            else if (activated)
                position += speed;
            


            if (reallyActivated)
            {
                timeUntilNextBarrage += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeUntilNextBarrage + 1.5f >= timeBetweenBarrages)
                {
                    color.B -= 5;
                }
                else
                    color = Color.White;

                if (timeUntilNextBarrage >= timeBetweenBarrages)
                {
                    barraging = true;
                    timeUntilNextBarrage -= timeBetweenBarrages;
                    timeUntilBarrageEnds = 0;
                }

                if (barraging)
                {
                    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeUntilBarrageEnds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeUntilNextShot >= timeBetweenShots)
                    {
                        Objects.enemyProjectileList.Add(new Boss2_SmallShot(new Vector2(position.X + size.X / 2, position.Y + size.Y)));
                        timeUntilNextShot -= timeBetweenShots;
                    }
                    

                    if (timeUntilBarrageEnds >= timeBarrageLength)
                    {
                        barraging = false;
                    }

                }
            }
        }
    }
}
