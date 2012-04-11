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
        }

        public override void Update(KeyboardState keyState, GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.Left) && position.X > 0)
                position.X -= speedHor;
            else if (keyState.IsKeyDown(Keys.Right) && position.X < Settings.window.ClientBounds.Width - size.X)
                position.X += speedHor;

            if (keyState.IsKeyDown(Keys.Up) && position.Y > 0)
                position.Y -= speedUp;
            else if (keyState.IsKeyDown(Keys.Down) && position.Y < Settings.window.ClientBounds.Height - size.Y)
                position.Y += speedDown;

            if (keyState.IsKeyDown(Keys.RightControl))
            {
                Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X, position.Y)));
                Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X+size.X, position.Y)));
            }

        }
    }
}
