using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    class BasePowerUp
    {
        protected Vector2 position;
        protected bool isalive;
        protected int size = 25;

        public BasePowerUp()
        {
 
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float PosX
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float PosY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Rectangle GetRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, size, size); }
        }

        public bool IsAlive
        {
            get { return isalive; }
            set
            {
                isalive = value;
            }
        }
    }
}
