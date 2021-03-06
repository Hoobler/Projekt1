﻿using System;
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
    class BaseButton
    {
        Texture2D texture;
        Rectangle position;
        bool isvisible;
        bool isunlocked;
        Color color = Color.White;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsVisible
        {
            get { return isvisible; }
            set { isvisible = value; }
        }

        public bool IsUnlocked
        {
            get { return isunlocked; }
            set { isunlocked = value; }
        }
        public Color mColor
        {
            get { return color; }
            set { color = value; }
        }

    }
}
