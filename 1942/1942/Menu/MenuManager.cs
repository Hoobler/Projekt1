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
    class MenuManager
    {
        StartGameButton mStartGameButton;
        OptionButton mOptionButton;
        ExitButton mExitButton;

        int distance;
        int button_Size_Height = 50;
        int button_Size_Width = 200;
        bool exitProgram = false;
        bool startGame = false;
        bool options = false;

        Texture2D original_texture_StartButton;
        Texture2D original_texture_ExitButton;
        Texture2D original_texture_OptionButton;
        Vector2 button_position;

        public MenuManager()
        {

            button_position = new Vector2(Settings.window.ClientBounds.Width / 2, Settings.window.ClientBounds.Height / 2);
            distance = Settings.window.ClientBounds.Height / 5;
            mStartGameButton = new StartGameButton(Texture2DLibrary.texture_StartGameButton, new Vector2(button_position.X - (button_Size_Width / 2), button_position.Y), this.button_Size_Height, this.button_Size_Width);
            mOptionButton = new OptionButton(Texture2DLibrary.texture_OptionsButton, new Vector2(mStartGameButton.GetRectangle().X, mStartGameButton.GetRectangle().Bottom + distance), this.button_Size_Height, this.button_Size_Width);
            mExitButton = new ExitButton(Texture2DLibrary.texture_ExitGameButton, new Vector2(mOptionButton.GetRectangle().X, mOptionButton.GetRectangle().Bottom + distance), this.button_Size_Height, this.button_Size_Width);
            this.original_texture_StartButton = Texture2DLibrary.texture_StartGameButton;
            this.original_texture_OptionButton = Texture2DLibrary.texture_OptionsButton;
            this.original_texture_ExitButton = Texture2DLibrary.texture_ExitGameButton;
        }

        public void Update(Point mouseLocation, Vector2 button_position)
        { 
            MouseState mouse = Mouse.GetState();
            if (mOptionButton.GetRectangle().Contains(mouseLocation))
            {
                mOptionButton.SetTexture(Texture2DLibrary.texture_OptionsButtonShadow);
            }
            else if (mExitButton.GetRectangle().Contains(mouseLocation))
            {
                mExitButton.SetTexture(Texture2DLibrary.texture_ExitGameButtonShadow);
            }
            else if (mStartGameButton.GetRectangle().Contains(mouseLocation))
            {
                mStartGameButton.SetTexture(Texture2DLibrary.texture_StartGameButtonShadow);
            }
            else
            {
                mStartGameButton.SetTexture(original_texture_StartButton);
                mOptionButton.SetTexture(original_texture_OptionButton);
                mExitButton.SetTexture(original_texture_ExitButton);
            }

            if (mExitButton.GetRectangle().Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                this.exitProgram = true;
            }
            if (mStartGameButton.GetRectangle().Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                this.startGame = true;
            }
            if (mOptionButton.GetRectangle().Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                this.options = true;
            }

            mStartGameButton.Update(new Vector2(button_position.X - (button_Size_Width /2), button_position.Y), button_Size_Height, button_Size_Width);
            mOptionButton.Update(new Vector2(mStartGameButton.GetRectangle().X, mStartGameButton.GetRectangle().Y + distance), button_Size_Height, button_Size_Width);
            mExitButton.Update(new Vector2(mOptionButton.GetRectangle().X, mOptionButton.GetRectangle().Y + distance), button_Size_Height, button_Size_Width);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mStartGameButton.Draw(spriteBatch);
            mOptionButton.Draw(spriteBatch);
            mExitButton.Draw(spriteBatch);
        }

        public bool GetExitProgram()
        {
            return this.exitProgram;
        }
        public bool GetStartGame()
        {
            return this.startGame;
        }
        public bool Options()
        {
            return options;
        }
    }
}
