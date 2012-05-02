using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Particle_Base
    {
        protected List<ParticlePiece_Smoke> particles = new List<ParticlePiece_Smoke>();
        protected Vector2 position;
        protected Random random = new Random();
        bool dead = false;

        public virtual void Update(GameTime gameTime)
        {
            position.Y += Settings.level_speed;
            if (position.Y > Settings.window.ClientBounds.Height)
                dead = true;
            for (int i = 0; i < particles.Count; i++)
                particles[i].Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
                particles[i].Draw(spriteBatch);
        }

        public void DeadCheck()
        {
            for (int i = particles.Count; i >= 0; i--)
            {
                if (particles[i].IsDead())
                    particles.RemoveAt(i);
            }
        }
    }
}
