using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Player2: BasePlayer
    {

        public Player2(): base()
        {
            color = Color.Pink;
            position = new Vector2(Settings.window.ClientBounds.Width / 2 + size.X * 0.8f, Settings.window.ClientBounds.Height - size.Y);
            playerID = 1;
            damage = 10;
        }

        public override void Update(KeyboardState keyState, GameTime gameTime)
        {
            if (!killed)
            {
                base.Update(keyState, gameTime);
                myScore = Settings.score_player2;
                if (keyState.IsKeyDown(Keys.A) && position.X > 0)
                    GoLeft();
                else if (keyState.IsKeyDown(Keys.D) && position.X < Settings.window.ClientBounds.Width - size.X)
                    GoRight();

                if (keyState.IsKeyDown(Keys.W) && position.Y > 0)
                    GoUp();
                else if (keyState.IsKeyDown(Keys.S) && position.Y < Settings.window.ClientBounds.Height - size.Y)
                    GoDown();

                if (keyState.IsKeyDown(Keys.LeftControl))
                {
                    Fire();
                }

            }
            
        }

    }
}
