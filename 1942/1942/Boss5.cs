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

        public Boss5(Vector2 position)
        {
            
            size = new Point(400, 300);
            texture = Texture2DLibrary.boss5;
            timeBetweenShips = 0.6f;
            shipBarrageLength = timeBetweenShips * 10;
            timeTotalPhase3 = 6f;
            timeTotalPhase5 = 6f;
            color = Color.White;
            maxHealth = 1000;
            health = maxHealth;
            this.position = position;
            this.position.X = Settings.window.ClientBounds.Width / 2f - size.X / 2f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            targetableRectangles.Clear();
            targetableRectangles.Add(new Rectangle((int)Center.X - 25, (int)Center.Y - 50, 50, 100));
            if (!accessorised && activated)
                Accessorize();
            if (activated && phase == 0)
                phase = 1;

            if(phase >= 2 && !killable)
                killable = true;
            if (!killed)
            {
                if (phase == 1)
                {
                    MusicManager.SetMusic(SoundLibrary.Boss1);
                    speed = new Vector2(0, 1);

                    if (Position.Y >= Settings.window.ClientBounds.Height * (1f / 8f))
                    {
                        position.Y = Settings.window.ClientBounds.Height * (1f / 8f);
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
                        Objects.enemyList.Add(new Boss5_MiniAirplane(Center, false));
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
                        Objects.enemyList.Add(new Boss5_MiniAirplane(Center, true));
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
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (activated)
                spriteBatch.Draw(texture, Rectangle, color);
            for(int i = 0; i < targetableRectangles.Count; i++)
                spriteBatch.Draw(texture, targetableRectangles[i], Color.Red);
            base.Draw(spriteBatch);
        }

        public override void Accessorize()
        {
            //accessoryList.Add(new Boss5_Cannon(Center));
        }
    }
}
