using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    class ShotManager
    {
        List<Splittershot> splitterShots;
        List<MainShot> mainShots;
        List<Timer> timers;

        Random random = new Random();
       

        public ShotManager(GameWindow window)
        {
            splitterShots = new List<Splittershot>();
            mainShots = new List<MainShot>();
            timers = new List<Timer>();
        }

        public void Update(GameTime gameTime, GameWindow window)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                mainShots.Add(new MainShot(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Objects.bossList[0].Position));
                timers.Add(new Timer(2f));
            }

            for (int i = 0; i < mainShots.Count; i++)
            {
                mainShots[i].Active = true;
                for (int j = 0; j < timers.Count; j++)
                {
                    if (mainShots[i].Active)
                    {
                        timers[j].Time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (timers[j].Time < 0)
                    {
                        if (mainShots[i].Active == true)
                        {
                            splitterShots.Add(new Splittershot((float)Math.PI*2,mainShots[i].Position));
                            splitterShots.Add(new Splittershot((float)Math.PI*2*(1f/5f),mainShots[i].Position));
                            splitterShots.Add(new Splittershot((float)Math.PI*2*(2f/5f),mainShots[i].Position));
                            splitterShots.Add(new Splittershot((float)Math.PI*2*(3f/5f),mainShots[i].Position));
                            splitterShots.Add(new Splittershot((float)Math.PI*2*(4f/5f),mainShots[i].Position));
                        }
                        mainShots[i].Active = false;
                        timers.Remove(timers[j]);
                    }
                }
                if (mainShots[i].Active == false)
                {
                    mainShots.Remove(mainShots[i]); 
                }
            }

            for (int i = 0; i < splitterShots.Count; i++)
            {
                splitterShots[i].Active = true;
                splitterShots[i].Update(gameTime);
            }
            for (int i = 0; i < mainShots.Count; i++)
            {
                mainShots[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < splitterShots.Count; i++)
            {
                splitterShots[i].Draw(spriteBatch);
            }
            for (int i = 0; i < mainShots.Count; i++)
            {
                if (mainShots[i].Active)
                {
                    mainShots[i].Draw(spriteBatch);
                }
            }
            
        }
    }
}
