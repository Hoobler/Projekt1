using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Escort : BaseObject
    {
        int maxHealth;
        int health;
        int phase;
        Point lifebarSizeFull;
        Point lifebarSize;
        bool animationDelay;
        protected List<Rectangle> targetableRectangles = new List<Rectangle>();

        public Escort(Vector2 startingPos)
        {
            
            this.speed = Settings.escort_speed;
            this.maxHealth = Settings.escort_health;
            health = maxHealth;

            texture = Texture2DLibrary.escort;
            size = new Point((texture.Bounds.Width - 1) / 3 - 3, texture.Bounds.Height - 2);
            lifebarSizeFull = new Point(Settings.window.ClientBounds.Width - 80, 40);
            lifebarSize = lifebarSizeFull;
            color = Color.White;
            position.X = 50;
            position.Y = Settings.window.ClientBounds.Height/2f;
            
        }
        public override void Update(GameTime gameTime)
        {
            targetableRectangles.Clear();
            targetableRectangles.Add(new Rectangle((int)position.X + 220, (int)position.Y + 151, 54, 76));
            targetableRectangles.Add(new Rectangle((int)Position.X+ 298, (int)Position.Y+ 150, 54, 76));
            targetableRectangles.Add(new Rectangle((int)Position.X+528, (int)Position.Y+150, 54, 76));
            targetableRectangles.Add(new Rectangle((int)Position.X+607, (int)Position.Y+151, 54, 76));

            if (animationDelay)
            {
                animationDelay = false;
                animationFrame.X++;
            }
            else
                animationDelay = true;

            if (animationFrame.X > 2)
                animationFrame.X = 0;

            if (phase == 0)
            {
                position -= speed;
                if (Center.X <= 0)
                {
                    position.X = -size.X/2f;
                    phase = 1;
                }
            }
            else if (phase == 1)
            {
                position += speed;
                if (Center.X >= Settings.window.ClientBounds.Width)
                {
                    position.X = Settings.window.ClientBounds.Width - size.X/2f;
                    phase = 0;
                }
            }

            lifebarSize.X = (int)((float)health / (float)maxHealth * (float)lifebarSizeFull.X);
            if (dead)
                Settings.gameOver = true;


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                new Rectangle(35, 35, lifebarSizeFull.X+10, lifebarSizeFull.Y+10),
                Color.Gray);
            spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                new Rectangle(40, 40, lifebarSize.X, lifebarSize.Y),
                Color.Red);
            spriteBatch.Draw(texture,
                    new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y),
                    new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                        (animationFrame.Y * (texture.Bounds.Height - 1) / 1) + 1,
                        ((texture.Bounds.Width - 1) / 3) - 1,
                        ((texture.Bounds.Height - 1) / 1) - 1),
                    color,
                    0,
                    new Vector2(0, 0),
                    spriteEffect,
                    0.0f);
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public List<Rectangle> TargetRectangles
        {
            get { return targetableRectangles; }
        }
    }
}
