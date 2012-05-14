﻿using System;
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

        

        float timer;
        




        public Boss1(Vector2 startingPos)
        {
            position = startingPos;
            position.X = Settings.window.ClientBounds.Width + size.X;
            color = Color.White;
            size = new Point(400, 200);
            speed = new Vector2(-2, 0);
            texture = Texture2DLibrary.boss1;
            layerDepth = 0.5f;
            maxHealth = 1000;
            health = maxHealth;


            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            targetableRectangles.Clear();
            targetableRectangles.Add(new Rectangle((int)position.X, (int)position.Y + 65, 149, 120));
            

            if (phase == 0)
            {
                speed = new Vector2(0, 5);
                    if (position.Y >= 20)
                    {
                        position.Y = 20;
                        phase = 1;
                        speed = new Vector2(-2, 0);
                    }
                }
            else if (phase == 1)
            {
                if (position.X <= Settings.window.ClientBounds.Width / 2 - size.X / 2)
                {
                    phase = 2;
                    for (int i = 0; i < accessoryList.Count; i++)
                    {
                        if (!accessoryList[i].ReallyActivated)
                            accessoryList[i].ReallyActivated = true;
                    }
                }

            }
            else if (phase == 2)
            {
                if (position.X <= 0)
                {
                    phase = 3;
                    speed = new Vector2(2, 0);
                }
            }
            else if (phase == 3)
            {
                if (position.X > Settings.window.ClientBounds.Width - size.X)
                {
                    speed = new Vector2(-2, 0);
                    phase = 2;
                }
            }
                
                
            

            if (accessoryList.Count == 0 && activated)
                killable = true;
                
            if (dead)
            {
                Objects.particleList.Add(new Particle_Explosion(new Vector2(position.X + size.X / 2, position.Y + size.Y / 2)));
            }
            

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss1_Gun(new Vector2(position.X + 75, position.Y + 115), 0.0f));
            accessoryList.Add(new Boss1_Gun(new Vector2(position.X + 180, position.Y + 55), 0.1f));
            accessoryList.Add(new Boss1_Gun(new Vector2(position.X + size.X - 116, position.Y + 115), 0.2f));
        }
        
    }
}
