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
    class BaseShot
    {
        protected int damage;
        protected Rectangle rectangle;
        protected int size = 20;
        protected Vector2 position;
        protected bool active;
        protected float speed;

        public BaseShot()
        {
 
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float posY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        public float posX
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}
