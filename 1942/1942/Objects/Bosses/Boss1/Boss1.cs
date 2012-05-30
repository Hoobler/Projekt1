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
        
        bool animationDelay;

        
        float timer;



        public Boss1(Vector2 startingPos)
        {
            position = startingPos;
            position.X = Settings.windowBounds.X + size.X+30;
            color = Color.White;
            
            speed = new Vector2(-2, 0);
            texture = Texture2DLibrary.boss1;
            layerDepth = 0.5f;
            maxHealth = 1;
            health = maxHealth;
            //size = new Point(400, 200);
            size = new Point((texture.Bounds.Width-1)/3 -3 , texture.Bounds.Height -2);
            score = 1000;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated)
            {
                if (phase >= 2 && !killed)
                {
                    timer++;
                    if (timer == 1)
                        Objects.powerUpList.Add(new PowerUpHealth(new Vector2(400, -50)));
                    if (timer == 300)
                        Objects.powerUpList.Add(new PowerUpShield(new Vector2(100, -50)));
                    if (timer == 600)
                        Objects.powerUpList.Add(new PowerUpDamage(new Vector2(400, -50)));
                    if (timer == 900)
                        Objects.powerUpList.Add(new PowerUpHealth(new Vector2(400, -50)));
                    if (timer >= 1200)
                        timer = 0;
                }
                targetableRectangles.Clear();
                if (animationDelay)
                {
                    animationDelay = false;
                    animationFrame.X++;
                }
                else
                    animationDelay = true;


                if (animationFrame.X > 2)
                    animationFrame.X = 0;

                if (phase == 0)
                {

                    speed = new Vector2(0, 5);
                    if (position.Y >= -30)
                    {
                        position.Y = -30;
                        phase = 1;
                        speed = new Vector2(-2, 0);
                    }
                }
                else if (phase == 1)
                {
                    MusicManager.SetMusic(SoundLibrary.Boss1);
                    if (position.X <= Settings.windowBounds.X / 2 - size.X / 2)
                    {
                        phase = 2;
                        for (int i = 0; i < accessoryList.Count; i++)
                        {
                            if (!accessoryList[i].ReallyActivated)
                            {
                                accessoryList[i].ReallyActivated = true;
                            }
                        }
                    }

                }
                else if (phase == 2)
                {
                    if (Center.X <= 200)
                    {
                        phase = 3;
                        speed = new Vector2(2, 0);
                    }
                }
                else if (phase == 3)
                {
                    if (Center.X > Settings.windowBounds.X - 200)
                    {
                        speed = new Vector2(-2, 0);
                        phase = 2;
                    }
                }
            }
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
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
                
            }
            base.Draw(spriteBatch);
        }

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss1_Gun(new Vector2(position.X + 143, position.Y + 271), 0.0f));
            accessoryList.Add(new Boss1_Gun(new Vector2(position.X + size.X/2, position.Y + 276), 0.1f));
            accessoryList.Add(new Boss1_Gun(new Vector2(position.X + size.X - 143, position.Y + 271), 0.0f));

            accessoryList.Add(new Boss1_Engine(new Vector2(position.X + 215, position.Y + 163)));
            accessoryList.Add(new Boss1_Engine(new Vector2(position.X + 296, position.Y + 163)));
            accessoryList.Add(new Boss1_Engine(new Vector2(position.X + 528, position.Y + 163)));
            accessoryList.Add(new Boss1_Engine(new Vector2(position.X + 609, position.Y + 163)));
        }
        
    }
}
