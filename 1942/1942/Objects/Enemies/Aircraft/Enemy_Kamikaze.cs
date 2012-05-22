using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Enemy_Kamikaze : FlyingObject
    {

        bool soundPlayed;
        public Enemy_Kamikaze(Vector2 position)
        {

            this.position = position;
            this.speed = Settings.kamikaze_speed;

            angle = (float)Math.PI;
            layerDepth = 1f;
            color = Color.White;
            size = Settings.size_zero;
            spriteEffect = SpriteEffects.None;
            texture = Texture2DLibrary.enemy_zero;
            health = Settings.kamikaze_health;
            maxHealth = Settings.kamikaze_health;
            score = 30;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated)
            {
                if (!soundPlayed)
                {
                    SoundLibrary.Kamikaze.Play();
                    soundPlayed = true;
                }

                position += speed;

                if (position.Y > Settings.window.ClientBounds.Height)
                    dead = true;

                if (dead)
                    Objects.particleList.Add(new Particle_Explosion(Center, size));



            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
                spriteBatch.Draw(texture,
                    new Rectangle((int)Position.X - size.X / 2, (int)Position.Y - size.Y / 2, Size.X, Size.Y),
                    new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                        (animationFrame.Y * (texture.Bounds.Height - 1) / 3) + 1,
                        ((texture.Bounds.Width - 1) / 3) - 1,
                        ((texture.Bounds.Height - 1) / 3) - 1),
                    color,
                    angle,
                    new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                    spriteEffect,
                    0.0f);
                spriteBatch.Draw(Texture2DLibrary.kamikaze, new Rectangle((int)Center.X + 20, (int)Center.Y - 20, 80, 49), Color.White);
            }

        }


    }
}
