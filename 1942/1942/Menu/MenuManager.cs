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
        //Main Buttons
        StartGameButton mStartGameButton;
        OptionButton mOptionButton;
        ExitButton mExitButton;

        //Level Select Buttons
        Level1_Button mLevel1_Button;
        Level2_Button mLevel2_Button;
        Level3_Button mLevel3_Button;
        Level4_Button mLevel4_Button;
        Level5_Button mLevel5_Button;

        //Select Nr of Players
        _1Player_Button m1Player_Button;
        _2Player_Button m2Player_Button;

        OptionManager mOptionsManager;

        int distance;
        //Main buttons
        int button_Size_Height = 50;
        int button_Size_Width = 200;

        // Level Buttons
        int lvl_button_Size_Height = 25;
        int lvl_button_Size_Width = 100;
        // minidistance
        int miniDistance;

        bool exitProgram = false;
        bool startGame = false;
        bool options = false;

        MouseState prevMouse = Mouse.GetState();

        
        Vector2 button_position;

        public MenuManager()
        {
            mOptionsManager = new OptionManager(Settings.window);
            button_position = new Vector2(Settings.window.ClientBounds.Width / 2, Settings.window.ClientBounds.Height / 2);
            distance = Settings.window.ClientBounds.Height / 5;
            miniDistance = Settings.window.ClientBounds.Height / 10;
            mStartGameButton = new StartGameButton(Texture2DLibrary.texture_StartGameButton, new Vector2(button_position.X - (button_Size_Width / 2), button_position.Y), this.button_Size_Height, this.button_Size_Width);
            mOptionButton = new OptionButton(Texture2DLibrary.texture_OptionsButton, new Vector2(mStartGameButton.GetRectangle().X, mStartGameButton.GetRectangle().Bottom + distance), this.button_Size_Height, this.button_Size_Width);
            mExitButton = new ExitButton(Texture2DLibrary.texture_ExitGameButton, new Vector2(mOptionButton.GetRectangle().X, mOptionButton.GetRectangle().Bottom + distance), this.button_Size_Height, this.button_Size_Width);
            mStartGameButton.IsVisible = true;
            mOptionButton.IsVisible = true;
            mLevel1_Button = new Level1_Button();
            mLevel2_Button = new Level2_Button();
            mLevel3_Button = new Level3_Button();
            mLevel4_Button = new Level4_Button();
            mLevel5_Button = new Level5_Button();
            mLevel1_Button.IsVisible = false;
            mLevel2_Button.IsVisible = false;
            mLevel3_Button.IsVisible = false;
            mLevel4_Button.IsVisible = false;
            mLevel5_Button.IsVisible = false;
            mLevel1_Button.IsUnlocked= true;
            mLevel1_Button.Position = new Rectangle(Settings.window.ClientBounds.Width/2 - lvl_button_Size_Width /2, Settings.window.ClientBounds.Height / 3, lvl_button_Size_Width, lvl_button_Size_Height);
            mLevel2_Button.Position = new Rectangle(mLevel1_Button.Position.X, mLevel1_Button.Position.Y+ miniDistance, lvl_button_Size_Width, lvl_button_Size_Height);
            mLevel3_Button.Position = new Rectangle(mLevel2_Button.Position.X, mLevel2_Button.Position.Y + miniDistance, lvl_button_Size_Width, lvl_button_Size_Height);
            mLevel4_Button.Position = new Rectangle(mLevel3_Button.Position.X, mLevel3_Button.Position.Y + miniDistance, lvl_button_Size_Width, lvl_button_Size_Height);
            mLevel5_Button.Position = new Rectangle(mLevel4_Button.Position.X, mLevel4_Button.Position.Y + miniDistance, lvl_button_Size_Width, lvl_button_Size_Height);
            m1Player_Button = new _1Player_Button();
            m2Player_Button = new _2Player_Button();
            m1Player_Button.Position = new Rectangle(Settings.window.ClientBounds.Width / 4 - 20, Settings.window.ClientBounds.Height / 3+ 20, 30, 30);
            m2Player_Button.Position = new Rectangle(Settings.window.ClientBounds.Width / 4 - 20, Settings.window.ClientBounds.Height / 3 + 50, 30, 30);
            m1Player_Button.IsVisible = false;
            m2Player_Button.IsVisible = false;
        }

        public void Update(Point mouseLocation, Vector2 button_position)
        { 
            MouseState mouse = Mouse.GetState();
            switch (mOptionsManager.Back)
            {
                case true:
                    {
                        options = true;
                        break;
                    }
                case false:
                    {
                        options = false;
                        break;
                    }
            }

            switch (options)
            {
                case false:
                    {
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
                            mStartGameButton.SetTexture(Texture2DLibrary.texture_StartGameButton);
                            mOptionButton.SetTexture(Texture2DLibrary.texture_OptionsButton);
                            mExitButton.SetTexture(Texture2DLibrary.texture_ExitGameButton);
                        }

                        if (mExitButton.GetRectangle().Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            this.exitProgram = true;
                        }
                        if (mStartGameButton.GetRectangle().Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            mStartGameButton.IsVisible = false;
                            mOptionButton.IsVisible = false;
                            mLevel1_Button.IsVisible = true;
                            mLevel2_Button.IsVisible = true;
                            mLevel3_Button.IsVisible = true;
                            mLevel4_Button.IsVisible = true;
                            mLevel5_Button.IsVisible = true;
                            m1Player_Button.IsVisible = true;
                            m2Player_Button.IsVisible = true;
                        }
                        if (mOptionButton.GetRectangle().Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            if (mOptionButton.IsVisible)
                            mOptionsManager.Back = true;

                        }

                        // Level Select
                        if (mLevel1_Button.IsUnlocked)
                        {
                            if (mLevel1_Button.Position.Contains(mouseLocation))
                            {
                                mLevel1_Button.mColor = Color.Blue;
                            }
                            else
                            {
                                mLevel1_Button.mColor = Color.White;
                            }
                        }
                        else
                        {
                            mLevel1_Button.mColor = Color.MediumVioletRed;
                        }

                        if (mLevel2_Button.IsUnlocked)
                        {
                           if (mLevel2_Button.Position.Contains(mouseLocation))
                           {
                               mLevel2_Button.mColor = Color.Blue;
                           }
                               else
                               {
                                   mLevel2_Button.mColor = Color.White;
                               }
                        }
                        else
                        {
                            mLevel2_Button.mColor = Color.MediumVioletRed;
                        }

                        if (mLevel3_Button.IsUnlocked)
                        {
                            if (mLevel3_Button.Position.Contains(mouseLocation))
                            {
                                mLevel3_Button.mColor = Color.Blue;
                            }
                            else
                            {
                                mLevel3_Button.mColor = Color.White;
                            }
                        }
                        else
                        {
                            mLevel3_Button.mColor = Color.MediumVioletRed;
                        }

                        if (mLevel4_Button.IsUnlocked)
                        {
                            if (mLevel4_Button.Position.Contains(mouseLocation))
                            {
                                mLevel4_Button.mColor = Color.Blue;
                            }
                            else
                            {
                                mLevel4_Button.mColor = Color.White;
                            }
                        }
                        else
                        {
                            mLevel4_Button.mColor = Color.MediumVioletRed;
                        }

                        if (mLevel5_Button.IsUnlocked)
                        {
                            if (mLevel5_Button.Position.Contains(mouseLocation))
                            {
                                mLevel5_Button.mColor = Color.Blue;
                            }
                            else
                            {
                                mLevel5_Button.mColor = Color.White;
                            }
                        }
                        else
                        {
                            mLevel5_Button.mColor = Color.MediumVioletRed;
                        }
                        

                        if (mLevel1_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            if (mLevel1_Button.IsUnlocked)
                            {
                                Settings.currentLevel = Settings.CurrentLevel.Level1;
                                startGame = true;
                            }
                        }
                        if (mLevel2_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            if (mLevel2_Button.IsUnlocked)
                            {
                                Settings.currentLevel = Settings.CurrentLevel.Level2;
                                startGame = true;
                            }
                        }
                        if (mLevel3_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            if (mLevel3_Button.IsUnlocked)
                            {
                                Settings.currentLevel = Settings.CurrentLevel.Level3;
                                startGame = true;
                            }
                        }
                        if (mLevel4_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            if (mLevel4_Button.IsUnlocked)
                            {
                                Settings.currentLevel = Settings.CurrentLevel.Level4;
                                startGame = true;
                            }
                        }
                        if (mLevel5_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            if (mLevel5_Button.IsUnlocked)
                            {
                                Settings.currentLevel = Settings.CurrentLevel.Level5;
                                startGame = true;
                            }
                        }
                        if (m1Player_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            Settings.Nr_Of_Players = 1;
                            m1Player_Button.mColor = Color.Red;
                            m2Player_Button.mColor = Color.White;
                        }
                        else if (m2Player_Button.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                        {
                            Settings.Nr_Of_Players = 2;
                            m2Player_Button.mColor = Color.Red;
                            m1Player_Button.mColor = Color.White;
                        }

                        mStartGameButton.Update(new Vector2(button_position.X - (button_Size_Width / 2), button_position.Y), button_Size_Height, button_Size_Width);
                        mOptionButton.Update(new Vector2(mStartGameButton.GetRectangle().X, mStartGameButton.GetRectangle().Y + distance), button_Size_Height, button_Size_Width);
                        mExitButton.Update(new Vector2(mOptionButton.GetRectangle().X, mOptionButton.GetRectangle().Y + distance), button_Size_Height, button_Size_Width);
                        break;
                    }
                case true:
                    {
                        mOptionsManager.Update(mouseLocation);
                        break;
                    }     
            }
            prevMouse = mouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (options)
            {
                case false:
                    {
                        spriteBatch.Draw(Texture2DLibrary.texture_MainMenu, new Rectangle(0, 0, Settings.window.ClientBounds.Width, Settings.window.ClientBounds.Height), Color.White);
                         mExitButton.Draw(spriteBatch);
                         if (mLevel1_Button.IsVisible == true)
                         {
                             mLevel1_Button.Draw(spriteBatch);
                             mLevel2_Button.Draw(spriteBatch);
                             mLevel3_Button.Draw(spriteBatch);
                             mLevel4_Button.Draw(spriteBatch);
                             mLevel5_Button.Draw(spriteBatch);
                             m1Player_Button.Draw(spriteBatch);
                             m2Player_Button.Draw(spriteBatch);
                             spriteBatch.DrawString(FontLibrary.Hud_Font, "Players", new Vector2(Settings.window.ClientBounds.Width / 5, Settings.window.ClientBounds.Height / 3 - 20), Color.Azure);
                         }
                         else
                         {
                             mStartGameButton.Draw(spriteBatch);
                             mOptionButton.Draw(spriteBatch);
                         }
                         break;
                    }
                case true:
                    {
                        spriteBatch.Draw(Texture2DLibrary.texture_OptionScreen_Default, new Rectangle(0, 0, Settings.window.ClientBounds.Width, Settings.window.ClientBounds.Height), Color.Black);
                        mOptionsManager.Draw(spriteBatch);
                        break;
                    }
            }
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
