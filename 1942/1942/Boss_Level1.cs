using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss_Level1 : Boss_Base
    {
        int phase = 0;

        float timeUntilNextShot;
        float timeBetweenShots = 0.2f;

        float timeUntilSpeedChange;
        float timeBetweenSpeedChange = 2f;

        float timer;

        float angleLeft;
        float angleRight;

        public Boss_Level1(Vector2 startingPos)
        {
            position = startingPos;
            position.X = Settings.window.ClientBounds.Width;
            color = Color.White;
            size = new Point(400, 200);
            speed = new Vector2(0, 0);
            texture = Texture2DLibrary.boss1;
            layerDepth = 0.5f;
            angleLeft = (float)Math.PI *(3f/2f);
            angleRight = (float)Math.PI * (3f / 2f);
            maxHealth = 250;
            health = maxHealth;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (position.Y > 10 && !activated)
                activated = true;

            if (activated)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (phase == 0)
                {
                    if (position.X >= Settings.window.ClientBounds.Width / 2 - size.X / 2)
                    {
                        position.X -= 5f;
                    }
                    else
                    {
                        speed = new Vector2(0.5f, 0);
                        phase = 1;
                    }
                }
                else if (phase == 1)
                {
                    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeUntilSpeedChange += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    position += speed;

                    if (timeUntilSpeedChange >= timeBetweenSpeedChange)
                    {
                        speed = -speed;
                        timeUntilSpeedChange -= timeBetweenSpeedChange;
                    }

                    if (timeUntilNextShot >= timeBetweenShots)
                    {
                        timeUntilNextShot -= timeBetweenShots;
                        Objects.enemyProjectileList.Add(new Boss1_Projectile1(new Vector2(position.X, position.Y + size.Y)));
                        Objects.enemyProjectileList.Add(new Boss1_Projectile1(new Vector2(position.X + size.X, position.Y + size.Y)));
                    }

                    if (timer > 11)
                    {
                        speed = new Vector2(0.5f, 0);
                        phase = 2;
                        timeUntilNextShot = 0;
                        timeBetweenShots = 0.3f;
                    }
                }
                else if (phase == 2)
                {
                    if (timer > 13)
                    {
                        phase = 3;
                    }
                }
                else if (phase == 3)
                {
                    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeUntilSpeedChange += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    position += speed;

                    if (timeUntilSpeedChange >= timeBetweenSpeedChange)
                    {
                        speed = -speed;
                        timeUntilSpeedChange -= timeBetweenSpeedChange;
                    }

                    if (timeUntilNextShot >= timeBetweenShots)
                    {
                        timeUntilNextShot -= timeBetweenShots;
                        angleLeft += (float)Math.PI / 12;
                        angleRight -= (float)Math.PI / 12;

                        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X, position.Y + size.Y / 2), (float)Math.PI + angleLeft));
                        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X, position.Y + size.Y / 2), angleLeft));
                        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X + size.X, position.Y + size.Y / 2), angleRight));
                        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X + size.X, position.Y + size.Y / 2), (float)Math.PI + angleRight));

                    }

                    if (timer > 37)
                    {
                        phase = 4;
                        timeUntilNextShot = 0;
                        timeBetweenShots = 0.2f;
                        angleLeft = (float)Math.PI * (3f / 2f);
                        angleRight = (float)Math.PI * (3f / 2f);

                    }
                }
                else if (phase == 4)
                {
                    if (timer > 41)
                    {
                        timer -= 38;
                        speed = new Vector2(2, 0);
                        phase = 1;

                    }
                }

                if (dead)
                {
                    Objects.particleList.Add(new Particle_Explosion(new Vector2(position.X + size.X / 2, position.Y + size.Y / 2)));
                }
            }

        }

        

    }
}
