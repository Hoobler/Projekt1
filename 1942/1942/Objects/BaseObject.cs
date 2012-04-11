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
        protected SpriteEffects spriteEffect;

        public BaseObject()
        {
            position = new Vector2(0, 0);
            speed = new Vector2(0, 0);
            color = Color.White;
            size = new Point(0, 0);
            layerDepth = 1f;
            angle = 0f;
            dead = false;
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, size.X, size.Y); }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public float PosX
        {
            set { position.X = value; }
        }
        public float PosY
        {
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
            spriteBatch.Draw(texture, Rectangle, new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height), color, angle, new Vector2(Size.X, Size.Y), spriteEffect, layerDepth);
        }

        public bool IsDead()
        {
            return dead;
        }
        public void SetDead()
        {
            dead = true;
        }

    }
}
