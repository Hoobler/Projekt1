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
        bool killed;
        protected List<Rectangle> targetableRectangles = new List<Rectangle>();

        public Escort(Vector2 startingPos)
        {
            
            this.speed = Settings.escort_speed;
            this.maxHealth = Settings.escort_health;
            health = maxHealth;

            texture = Texture2DLibrary.escort;
            size = new Point((texture.Bounds.Width - 1) / 3 - 3, texture.Bounds.Height - 2);
            lifebarSizeFull = new Point((int)Settings.windowBounds.X - 80, 40);
            lifebarSize = lifebarSizeFull;
            color = Color.White;
            position.X = 0;
            position.Y = Settings.windowBounds.Y;
            
        }
        public override void Update(GameTime gameTime)
        {
            targetableRectangles.Clear();
            targetableRectangles.Add(new Rectangle((int)position.X + 220, (int)position.Y + 151, 54, 76));
            targetableRectangles.Add(new Rectangle((int)Position.X+ 298, (int)Position.Y+ 150, 54, 76));
            targetableRectangles.Add(new Rectangle((int)Position.X+528, (int)Position.Y+150, 54, 76));
            targetableRectangles.Add(new Rectangle((int)Position.X+607, (int)Position.Y+151, 54, 76));

            if (health <= 0)
            {
                health = 0;
                killed = true;
            }
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
                position.Y -= 1f;
                if (position.Y <= Settings.windowBounds.Y / 2f)
                {
                    position.Y = Settings.windowBounds.Y / 2f;
                    phase = 1;
                }

            }
            if (phase == 1)
            {
                position -= speed;
                if (Center.X <= 225)
                {
                    position.X = -size.X/2f + 225;
                    phase = 2;
                }
            }
            else if (phase == 2)
            {
                position += speed;
                if (Center.X >= Settings.windowBounds.X-225)
                {
                    position.X = Settings.windowBounds.X - size.X/2f-225;
                    phase = 1;
                }
            }

            lifebarSize.X = (int)((float)health / (float)maxHealth * (float)lifebarSizeFull.X);
            if (dead)
                Settings.gameOver = true;


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (phase >= 1)
            {
                spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                            new Rectangle(100, 20, (int)Settings.windowBounds.X - 190, 40),
                            Color.Gray);
                spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                new Rectangle(105, 25, ((int)((float)lifebarSize.X / (float)lifebarSizeFull.X * (float)(Settings.windowBounds.X - 200))), 30),
                Color.Red);
            }
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
            if (phase == 0)
                spriteBatch.DrawString(FontLibrary.Hud_Font, "ESCORT THE BOMBER", new Vector2(Settings.windowBounds.X / 2f, Settings.windowBounds.Y / 2f), Color.Red);
            
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public bool Killed
        {
            get { return killed; }
        }
        public List<Rectangle> TargetRectangles
        {
            get { return targetableRectangles; }
        }
    }
}
