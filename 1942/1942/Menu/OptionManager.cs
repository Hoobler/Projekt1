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
        AddVolumeButton mAddVolumeButton;
        MinusVolumeButton mMinusVolumeButton;


        MouseState previousmouse = Mouse.GetState();
        //buttons
        int buttonWidth = (int)Settings.windowBounds.X / 4;
        int buttonHeight = (int)Settings.windowBounds.Y / 8;

        // Button Distance Between eachother
        int mButtonDistance = (int)Settings.windowBounds.X / 20;


        //OptionScreen
        float screenWidth = (float)Settings.windowBounds.X/1.4f;
        float screenHeight = (float)Settings.windowBounds.Y/4.5f;

        // Bool to get back to the Menu Screen
        bool back = false;

        // Volume button sizes
        int volumeButtonHeight = (int)Settings.windowBounds.Y / 10;
        int volumebuttonWidth = (int)Settings.windowBounds.X / 10;

        public OptionManager()
        {
            mAudioOptionButton = new AudioOptionButton();
            mVideoOptionButton = new VideoOptionButton();
            mControlsOptionButton = new ControlsOptionButton();
            mOptionScreen = new OptionScreen();
            mBackButton = new BackButton();
            mAddVolumeButton = new AddVolumeButton();
            mMinusVolumeButton = new MinusVolumeButton();
            mAddVolumeButton.IsVisible = false;
            mMinusVolumeButton.IsVisible = false;
            //Positions
            mAudioOptionButton.Position = new Rectangle((int)Settings.windowBounds.X / 20, (int)Settings.windowBounds.Y / 20, buttonWidth, buttonHeight);
            mVideoOptionButton.Position = new Rectangle(mAudioOptionButton.Position.Right + mButtonDistance, mAudioOptionButton.Position.Y, buttonWidth, buttonHeight);
            mControlsOptionButton.Position = new Rectangle(mVideoOptionButton.Position.Right + mButtonDistance, mVideoOptionButton.Position.Y, buttonWidth, buttonHeight);
            mOptionScreen.Position = new Rectangle((int)Settings.windowBounds.X / 6, (int)Settings.windowBounds.Y / 3, (int)screenWidth, (int)screenHeight);
            mBackButton.Position = new Rectangle((int)Settings.windowBounds.X - buttonWidth, (int)Settings.windowBounds.Y - buttonHeight, buttonWidth, buttonHeight);
            mMinusVolumeButton.Position = new Rectangle(mOptionScreen.Position.Center.X, mOptionScreen.Position.Bottom, volumebuttonWidth, volumeButtonHeight);
            mAddVolumeButton.Position = new Rectangle(mMinusVolumeButton.Position.Right + mButtonDistance, mOptionScreen.Position.Bottom, volumebuttonWidth, volumeButtonHeight);
            //Textures
            mAudioOptionButton.Texture = Texture2DLibrary.texture_AudioOptions;
            mVideoOptionButton.Texture = Texture2DLibrary.texture_VideoOptions;
            mControlsOptionButton.Texture = Texture2DLibrary.texture_Control_Options;
            mBackButton.Texture = Texture2DLibrary.texture_Back;
            mOptionScreen.Texture = Texture2DLibrary.texture_OptionScreen_Default;
            mAddVolumeButton.Texture = Texture2DLibrary.texture_AddVolume;
            mMinusVolumeButton.Texture = Texture2DLibrary.texture_MinusVolume;
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
                mBackButton.Texture = Texture2DLibrary.texture_Back_Shadow;
            }
            else
            {
                mBackButton.Texture = Texture2DLibrary.texture_Back;
            }

            if (mAudioOptionButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                mOptionScreen.Texture = Texture2DLibrary.texture_AdjustVolume;
                mAddVolumeButton.IsVisible = true;
                mMinusVolumeButton.IsVisible = true;
            }
            if (mVideoOptionButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                mOptionScreen.Texture = Texture2DLibrary.texture_AdjustVideo;
                mAddVolumeButton.IsVisible = false;
                mMinusVolumeButton.IsVisible = false;
            }
            if (mControlsOptionButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                mOptionScreen.Texture = Texture2DLibrary.texture_Controls;
                mAddVolumeButton.IsVisible = false;
                mMinusVolumeButton.IsVisible = false;
            }
            if (mBackButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
            {
                back = false;
            }

            if (mMinusVolumeButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && previousmouse.LeftButton == ButtonState.Released)
            {
                MediaPlayer.Volume -= 0.01f;
            }
            if (mAddVolumeButton.Position.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed && previousmouse.LeftButton == ButtonState.Released)
            {
                MediaPlayer.Volume += 0.01f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mAudioOptionButton.Draw(spriteBatch);
            mControlsOptionButton.Draw(spriteBatch);
            mVideoOptionButton.Draw(spriteBatch);
            mOptionScreen.Draw(spriteBatch);
            mBackButton.Draw(spriteBatch);
            if (mAddVolumeButton.IsVisible == true)
            {
                mAddVolumeButton.Draw(spriteBatch);
                spriteBatch.DrawString(FontLibrary.Hud_Font, ((int)(MediaPlayer.Volume*100f)).ToString() + "%", new Vector2(mOptionScreen.Position.X, mOptionScreen.Position.Bottom), Color.White);
            }
            if (mMinusVolumeButton.IsVisible == true)
            {
                mMinusVolumeButton.Draw(spriteBatch);
            }
        }

        public bool Back
        {
            get { return back; }
            set { back = value; }
        }
    }
}
