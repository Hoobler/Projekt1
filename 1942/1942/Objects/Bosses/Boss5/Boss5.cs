using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss5 : Boss_Base
    {
        float timeUntilNextShip;
        float timeBetweenShips;
        float shipBarrageLength;
        float timeUntilShipBarrageEnds;
        float timeUntilEndPhase3;
        float timeTotalPhase3;
        float timeUntilEndPhase5;
        float timeTotalPhase5;
        bool animationDelay;
        Vector2 rotationPoint;
        int timer;

        public Boss5(Vector2 position)
        {
            texture = Texture2DLibrary.boss5;
            size = new Point((texture.Bounds.Width - 1) / 3 - 3, texture.Bounds.Height - 2);
            timeBetweenShips = 0.6f;
            shipBarrageLength = timeBetweenShips * 10;
            timeTotalPhase3 = 6f;
            timeTotalPhase5 = 6f;
            color = Color.White;
            maxHealth = 10000;
            health = maxHealth;
            this.position = position;
            this.position.X = Settings.windowBounds.X / 2f - size.X / 2f;
            killable = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            rotationPoint = new Vector2(Center.X, position.X + 102);
            targetableRectangles.Clear();
            targetableRectangles.Add(new Rectangle((int)position.X+313, (int)position.Y+2, 49, 133));
            if (!accessorised && activated)
            {
                accessorised = true;
                Accessorize();
            }
            if (activated && phase == 0)
                phase = 1;

            if(phase >= 2 && !killable)
                killable = true;
            if (activated && !killed)
            {
                if (phase == 1)
                {
                    MusicManager.SetMusic(SoundLibrary.Boss1);
                    speed = new Vector2(0, 1);


                    if (Position.Y >= Settings.windowBounds.Y * (1f / 8f))
                    {
                        position.Y = Settings.windowBounds.Y * (1f / 8f);
                        speed = new Vector2(0, 0);
                        phase = 2;
                        for (int i = 0; i < accessoryList.Count; i++)
                            accessoryList[i].ReallyActivated = true;
                    }
                }
                if (phase == 2)
                {
                    timeUntilShipBarrageEnds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeUntilNextShip += (float)gameTime.ElapsedGameTime.TotalSeconds;


                    if (timeUntilNextShip >= timeBetweenShips)
                    {
                        timeUntilNextShip -= timeBetweenShips;
                        Objects.enemyList.Add(new Boss5_MiniAirplane(rotationPoint, false));
                    }
                    if (timeUntilShipBarrageEnds >= shipBarrageLength)
                    {
                        timeUntilShipBarrageEnds = 0;
                        timeUntilNextShip = 0;
                        phase = 3;
                    }

                }
                if (phase == 3)
                {
                    timeUntilEndPhase3 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeUntilEndPhase3 >= timeTotalPhase3)
                    {
                        timeUntilEndPhase3 = 0;
                        phase = 4;
                    }
                }
                if (phase == 4)
                {
                    timeUntilShipBarrageEnds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    timeUntilNextShip += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeUntilNextShip >= timeBetweenShips)
                    {
                        timeUntilNextShip -= timeBetweenShips;
                        Objects.enemyList.Add(new Boss5_MiniAirplane(rotationPoint, true));
                    }
                    if (timeUntilShipBarrageEnds >= shipBarrageLength)
                    {
                        timeUntilShipBarrageEnds = 0;
                        timeUntilNextShip = 0;
                        phase = 5;
                    }
                }
                if (phase == 5)
                {
                    timeUntilEndPhase5 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeUntilEndPhase5 >= timeTotalPhase5)
                    {
                        timeUntilEndPhase5 = 0;
                        phase = 2;
                    }
                }
                if (animationDelay)
                {
                    animationDelay = false;
                    animationFrame.X++;
                }
                else
                    animationDelay = true;


                if (animationFrame.X > 2)
                    animationFrame.X = 0;

                if (phase >= 2 && !killed)
                {
                    timer++;
                    if (timer == 1)
                        Objects.powerUpList.Add(new PowerUpHealth(new Vector2(Settings.windowBounds.X/2f, -50)));
                    if (timer == 300)
                        Objects.powerUpList.Add(new PowerUpDamage(new Vector2(Settings.windowBounds.X / 2f, -50)));
                    if (timer == 600)
                        Objects.powerUpList.Add(new PowerUpHealth(new Vector2(Settings.windowBounds.X / 2f, -50)));
                    if (timer == 900)
                        Objects.powerUpList.Add(new PowerUpDamage(new Vector2(Settings.windowBounds.X / 2f, -50)));
                    if (timer >= 1200)
                        timer = 0;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (activated)
                spriteBatch.Draw(texture,
                    new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y),
                    new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                        (animationFrame.Y * (texture.Bounds.Height - 1) / 1) + 1,
                        ((texture.Bounds.Width - 1) / 3) - 1,
                        ((texture.Bounds.Height - 1) / 1) - 1),
                    color,
                    0,
                    new Vector2(0, 0),
                    spriteEffect,
                    0.0f);
            for(int i = 0; i < targetableRectangles.Count; i++)
                spriteBatch.Draw(texture, targetableRectangles[i], Color.Red);
            base.Draw(spriteBatch);
        }

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss5_Cannon(new Vector2(position.X+338, position.Y+234)));
        }
    }
}
