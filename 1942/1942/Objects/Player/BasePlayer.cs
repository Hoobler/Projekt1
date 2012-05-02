using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    class BasePlayer: BaseObject
    {
        protected float speedHor;
        protected float speedUp;
        protected float speedDown;
        protected int health = 100;
        protected float timeUntilNextShot;
        protected int playerID;

        public BasePlayer(): base()
        {
            angle = 0;
            color = Color.White;
            layerDepth = 0f;
            size = Settings.size_player;
            speedHor = 4f;
            speedUp = 4f;
            speedDown = 4f;
            position.X = Settings.window.ClientBounds.Width / 2 - size.X /2;
            position.Y = Settings.window.ClientBounds.Height - size.Y;
            texture = Texture2DLibrary.player;
            
        }

        public virtual void Update(KeyboardState keyState, GameTime gameTime)
        {
            if(timeUntilNextShot > 0)
                timeUntilNextShot -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            
            animationFrame.Y = 0;
            spriteEffect = SpriteEffects.None;

            animationFrame.X++;
            if (animationFrame.X > 2)
                animationFrame.X = 0;
            
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                Rectangle,
                new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                    (animationFrame.Y * (texture.Bounds.Height - 1) / 3) + 1,
                    ((texture.Bounds.Width - 1) / 3) - 1,
                    ((texture.Bounds.Height - 1) / 3) - 1),
                color,
                0,
                new Vector2 (0,0),
                spriteEffect,
                0.0f);
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public void GoLeft()
        {
            position.X -= speedHor;
            animationFrame.Y = 1;
        }

        public void GoRight()
        {
            position.X += speedHor;
            animationFrame.Y = 1;
            spriteEffect = SpriteEffects.FlipHorizontally;
        }

        public void GoUp()
        {
            position.Y -= speedUp;
        }

        public void GoDown()
        {
            position.Y += speedDown;
        }

        public void Fire()
        {

            if (timeUntilNextShot <= 0f)
            {
                if (animationFrame.Y == 1)
                {
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 - size.X / 6, position.Y + size.Y / 3), playerID));
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 + size.X / 6, position.Y + size.Y / 3), playerID));
                }
                else
                {
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 - size.X / 4, position.Y + size.Y / 3), playerID));
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 + size.X / 4, position.Y + size.Y / 3), playerID));
                }
                timeUntilNextShot = Settings.player_projectile_frequency;
            }

            
        }
        

    }
}
