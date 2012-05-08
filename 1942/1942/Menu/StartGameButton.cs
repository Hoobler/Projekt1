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
    class StartGameButton : BaseButton
    {
        Texture2D texture;
        Rectangle rectangle;

        public StartGameButton(Texture2D texture_StartGame, Vector2 position, int rectangle_Size_Height, int rectangle_Size_width)
        {
            this.texture = texture_StartGame;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, rectangle_Size_width, rectangle_Size_Height);
        }

        public void Update(Vector2 button_position, int rectangle_Size_Height, int rectangle_Size_width)
        {
            this.rectangle = new Rectangle((int)button_position.X, (int)button_position.Y, rectangle_Size_width, rectangle_Size_Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }

        public Rectangle GetRectangle()
        {
            return this.rectangle;
        }

        public void SetTexture(Texture2D newtex)
        {
            this.texture = newtex;
        }
    }
}
