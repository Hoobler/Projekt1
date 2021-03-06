﻿using System;
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
            position = new Vector2(Settings.windowBounds.X / 2 - size.X *1.5f, Settings.windowBounds.Y - size.Y);
            playerID = 0;
            damage = 10;
        }

        public override void Update(KeyboardState keyState, GameTime gameTime)
        {
            if (!killed)
            {
                base.Update(keyState, gameTime);
                myScore = Settings.score_player1;

                if (keyState.IsKeyDown(Keys.Left) && position.X > 0)
                    GoLeft();

                else if (keyState.IsKeyDown(Keys.Right) && position.X < Settings.windowBounds.X - size.X)
                    GoRight();

                if (keyState.IsKeyDown(Keys.Up) && position.Y > 0)
                    GoUp();
                else if (keyState.IsKeyDown(Keys.Down) && position.Y < Settings.windowBounds.Y - size.Y)
                    GoDown();
                
                if (keyState.IsKeyDown(Keys.RightControl))
                {
                    Fire();
                }
            }
        }
    }
}
