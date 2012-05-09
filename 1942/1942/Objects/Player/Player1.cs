using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Player1 : BasePlayer
    {

        public Player1(): base()
        {
            color = Color.White;
            position = new Vector2(Settings.window.ClientBounds.Width / 2 + size.X / 2, Settings.window.ClientBounds.Height - size.Y);
            playerID = 0;
            damage = 10;
        }

        public override void Update(KeyboardState keyState, GameTime gameTime)
        {
            

            base.Update(keyState, gameTime);

            if (keyState.IsKeyDown(Keys.Left) && position.X > 0)
                GoLeft();
            
            else if (keyState.IsKeyDown(Keys.Right) && position.X < Settings.window.ClientBounds.Width - size.X)
                GoRight();
            
            if (keyState.IsKeyDown(Keys.Up) && position.Y > 0)
                GoUp();
            else if (keyState.IsKeyDown(Keys.Down) && position.Y < Settings.window.ClientBounds.Height - size.Y)
                GoDown();
            //if (timeUntilNextShot > Settings.player_projectile_frequency)
            //{
             //   timeUntilNextShot -= Settings.player_projectile_frequency;
                if (keyState.IsKeyDown(Keys.RightControl))
                {
                    Fire();
                }
            //}
            
        }
    }
}
