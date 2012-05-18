using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class BaseObject
    {
        protected Vector2 position;
        protected Vector2 speed;
        protected Color color;
        protected Point size;
        protected float layerDepth;
        protected float angle;
        protected bool dead;
        protected Texture2D texture;
        public SpriteEffects spriteEffect;
        public Point animationFrame;

        public BaseObject()
        {
            position = new Vector2(0, 0);
            speed = new Vector2(0, 0);
            color = Color.White;
            size = new Point(0, 0);
            layerDepth = 1f;
            angle = 0f;
            dead = false;
            animationFrame = new Point(0, 0);
        }

        public virtual Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, size.X, size.Y); }
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
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        public Point Size
        {
            get { return size; }
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                new Rectangle((int)Position.X+size.X/2,(int)Position.Y+size.Y/2, size.X, size.Y),
                new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                color,
                angle,
                new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                spriteEffect, layerDepth);
            
        }

        public Vector2 Center
        {
            get { return new Vector2(position.X + size.X / 2, position.Y + size.Y / 2); }
        }

        public bool IsDead()
        {
            return dead;
        }
        public void SetDead()
        {
            dead = true;
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

    }
}
