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
    class OptionManager
    {
        AudioOptionButton mAudioOptionButton;
        VideoOptionButton mVideoOptionButton;
        ControlsOptionButton mControlsOptionButton;
        BackButton mBackButton;
        OptionScreen mOptionScreen;

        //buttons
        int buttonWidth = 100;
        int buttonHeight = 50;

        //OptionScreen
        int screenWidth = 300;
        int screenHeight = 200;

        public OptionManager(GameWindow window)
        {
            mAudioOptionButton = new AudioOptionButton();
            mVideoOptionButton = new VideoOptionButton();
            mControlsOptionButton = new ControlsOptionButton();
            mOptionScreen = new OptionScreen();
            mBackButton = new BackButton();
            mAudioOptionButton.Position = new Rectangle(50, 50, buttonWidth, buttonHeight);
            mVideoOptionButton.Position = new Rectangle(200, 50, buttonWidth, buttonHeight);
            mControlsOptionButton.Position = new Rectangle(350, 50, buttonWidth, buttonHeight);
            mBackButton.Position = new Rectangle(window.ClientBounds.Width - buttonWidth, window.ClientBounds.Height - buttonHeight, buttonWidth, buttonHeight);
        }

        public void Update(Point mouseLocation)
        {
            MouseState mouse = Mouse.GetState();
            if (mAudioOptionButton.Position.Contains(mouseLocation))
            {
                mAudioOptionButton.Texture = Texture2DLibrary.texture_AudioOptions_Shadow;
            }
            else 
            {
                mAudioOptionButton.Texture = Texture2DLibrary.texture_AudioOptions;
            }
                     
            if (mVideoOptionButton.Position.Contains(mouseLocation))
            {
                mVideoOptionButton.Texture = Texture2DLibrary.texture_VideoOptions_Shadow;
            }
            else
            {
                mVideoOptionButton.Texture = Texture2DLibrary.texture_VideoOptions;
            }

            if (mControlsOptionButton.Position.Contains(mouseLocation))
            {
                mControlsOptionButton.Texture = Texture2DLibrary.texture_Control_Options_Shadow;
            }
            else
            {
                mControlsOptionButton.Texture = Texture2DLibrary.texture_Control_Options;
            }

            if (mBackButton.Position.Contains(mouseLocation))
            {
                mAudioOptionButton.Texture = Texture2DLibrary.texture_Back_Shadow;
            }
            else
            {
                mAudioOptionButton.Texture = Texture2DLibrary.texture_Back;
            }

            if (mAudioOptionButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                mOptionScreen.Texture = Texture2DLibrary.texture_AdjustVolume;
            }
            if (mVideoOptionButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                mOptionScreen.Texture = Texture2DLibrary.texture_AdjustVideo;
            }
            if (mControlsOptionButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                mOptionScreen.Texture = Texture2DLibrary.texture_Controls;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mAudioOptionButton.Draw(spriteBatch);
            mControlsOptionButton.Draw(spriteBatch);
            mVideoOptionButton.Draw(spriteBatch);
        }

    }
}
