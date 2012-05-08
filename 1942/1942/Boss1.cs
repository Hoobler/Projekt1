using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss1 : Boss_Base
    {
        int phase = 0;

        float timeUntilSpeedChange;
        float timeBetweenSpeedChange = 2f;

        float timer;
        float timePhase1;
        float timePhase2;
        float timePhase3;
        float timePhase4;

        float angleLeft;
        float angleRight;

        

        public Boss1(Vector2 startingPos)
        {
            position = startingPos;
            //position.X = Settings.window.ClientBounds.Width;
            color = Color.White;
            size = new Point(400, 200);
            speed = new Vector2(-2, 0);
            texture = Texture2DLibrary.boss1;
            layerDepth = 0.5f;
            angleLeft = (float)Math.PI *(3f/2f);
            angleRight = (float)Math.PI * (3f / 2f);
            maxHealth = 1000;
            health = maxHealth;

            gunList.Add(new Boss1_Gun(new Vector2(position.X + (float)size.X * (1f / 4f), position.Y + (float)size.Y * (3f / 4f)), 0.0f));
            gunList.Add(new Boss1_Gun(new Vector2(position.X + (float)size.X * (3f / 4f), position.Y + (float)size.Y * (3f / 4f)), 0.1f));
            gunList.Add(new Boss1_Gun(new Vector2(position.X + (float)size.X * (2f / 4f), position.Y + (float)size.Y * (1f / 4f)), 0.2f));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            

            if (position.Y > 20 && !activated)
            {
                for (int i = 0; i < gunList.Count; i++)
                {
                    gunList[i].Activated = true;
                }
                activated = true;
                
            }
            if (activated)
            {
                //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if (phase == 0)
                //{
                //    if (position.X >= Settings.window.ClientBounds.Width / 2 - size.X / 2)
                //    {
                //        position.X -= 5f;
                //    }
                //    else
                //    {
                //        speed = new Vector2(0.5f, 0);
                //        phase = 1;
                //    }
                //}
                //else if (phase == 1)
                //{
                //    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeUntilSpeedChange += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    

                    position += speed;

                    if (timeUntilSpeedChange >= timeBetweenSpeedChange)
                    {
                        speed = -speed;
                        timeUntilSpeedChange -= timeBetweenSpeedChange;
                    }

                    

                //    if (timer >= timePhase1 )
                //    {
                //        speed = new Vector2(0.5f, 0);
                //        phase = 2;
                //    }
                //}
                //else if (phase == 2)
                //{
                //    if (timer > 13)
                //    {
                //        phase = 3;
                //    }
                //}
                //else if (phase == 3)
                //{
                //    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //    timeUntilSpeedChange += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //    position += speed;

                //    if (timeUntilSpeedChange >= timeBetweenSpeedChange)
                //    {
                //        speed = -speed;
                //        timeUntilSpeedChange -= timeBetweenSpeedChange;
                //    }

                //    if (timeUntilNextShot >= timeBetweenShots)
                //    {
                //        timeUntilNextShot -= timeBetweenShots;
                //        angleLeft += (float)Math.PI / 12;
                //        angleRight -= (float)Math.PI / 12;

                //        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X, position.Y + size.Y / 2), (float)Math.PI + angleLeft));
                //        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X, position.Y + size.Y / 2), angleLeft));
                //        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X + size.X, position.Y + size.Y / 2), angleRight));
                //        Objects.enemyProjectileList.Add(new Boss1_Projectile2(new Vector2(position.X + size.X, position.Y + size.Y / 2), (float)Math.PI + angleRight));

                //    }

                //    if (timer > 37)
                //    {
                //        phase = 4;
                //        timeUntilNextShot = 0;
                //        timeBetweenShots = 0.2f;
                //        angleLeft = (float)Math.PI * (3f / 2f);
                //        angleRight = (float)Math.PI * (3f / 2f);

                //    }
                //}
                //else if (phase == 4)
                //{
                //    if (timer > 41)
                //    {
                //        timer -= 38;
                //        speed = new Vector2(2, 0);
                //        phase = 1;

                //    }
                //}

                    if (gunList.Count == 0)
                        killable = true;
                    DeadRemoval();
                if (dead)
                {
                    Objects.particleList.Add(new Particle_Explosion(new Vector2(position.X + size.X / 2, position.Y + size.Y / 2)));
                }
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
        }

        public void DeadRemoval()
        {

            for (int j = gunList.Count - 1; j >= 0; j--)
            {
                if (gunList[j].IsDead())
                    gunList.RemoveAt(j);
            }

        }
    }
}
