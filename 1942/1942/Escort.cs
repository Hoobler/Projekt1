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

        public Escort(Vector2 startingPos)
        {
            
            this.speed = Settings.escort_speed;
            this.maxHealth = Settings.escort_health;
            health = maxHealth;
            this.size = Settings.escort_size;
            texture = Texture2DLibrary.escort;
            lifebarSizeFull = new Point(Settings.window.ClientBounds.Width - 80, 40);
            lifebarSize = lifebarSizeFull;
            color = Color.White;
            position.X = 50;
            position.Y = Settings.window.ClientBounds.Height - size.Y;
        }
        public override void Update(GameTime gameTime)
        {
            if (phase == 0)
            {
                position -= speed;
                if (position.X <= 0)
                {
                    position.X = 0;
                    phase = 1;
                }
            }
            else if (phase == 1)
            {
                position += speed;
                if (position.X >= Settings.window.ClientBounds.Width - size.X)
                {
                    position.X = Settings.window.ClientBounds.Width - size.X;
                    phase = 0;
                }
            }

            lifebarSize.X = (int)((float)health / (float)maxHealth * (float)lifebarSizeFull.X);


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                new Rectangle(35, 35, lifebarSizeFull.X+10, lifebarSizeFull.Y+10),
                Color.Gray);
            spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                new Rectangle(40, 40, lifebarSize.X, lifebarSize.Y),
                Color.Red);
            spriteBatch.Draw(texture, Rectangle, color);
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
    }
}
