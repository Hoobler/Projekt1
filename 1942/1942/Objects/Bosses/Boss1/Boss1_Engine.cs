using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss1_Engine : Boss_Accessory
    {
        bool delay;
        bool exploded;
        float particle_delay_smoke;
        float particle_delay_fire;
        
        public Boss1_Engine(Vector2 position)
        {
            this.position = position;
            size = new Point(50, 59);
            texture = Texture2DLibrary.boss1_projectile;
            color = Color.Transparent;
            maxHealth = Settings.boss1_engine_health;
            health = maxHealth;
            killable = true;
        }

        public override void Update(GameTime gameTime, Vector2 speed)
        {
            particle_delay_smoke -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!delay)
                delay = true;

            if (health <= 0)
            {
                health = 0;
                killed = true;
            }
            
            if (!activated)
            {
                position.Y += Settings.level_speed;
            }
            else if(activated)
            {
                position += speed;
            }
            if (particle_delay_smoke <= 0)
            {
                particle_delay_smoke += 0.1f;
                if (health < (float)maxHealth * (4f / 5f))
                {
                    Objects.particleList.Add(new Particle_SmokeStream(new Vector2(position.X + 20, position.Y + 30)));
                }
                if (health < (float)maxHealth * (2f / 5f))
                {
                    Objects.particleList.Add(new Particle_SmokeStream(new Vector2(position.X + 40, position.Y + 40)));
                }
            }

            if (killed)
            {
                particle_delay_fire -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!exploded)
                {
                    exploded = true;
                    Objects.particleList.Add(new Particle_Explosion(Center, size));
                }
                if (particle_delay_fire <= 0)
                {
                    particle_delay_fire += 0.2f;
                    Objects.particleList.Add(new Particle_FireStream(new Vector2(position.X + 25, position.Y + 20)));
                    Objects.particleList.Add(new Particle_FireStream(new Vector2(position.X + 42, position.Y + 20)));
                }
            }
            

        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (reallyActivated && !killed)
            {
                spriteBatch.Draw(Texture2DLibrary.arrow, new Rectangle((int)position.X + size.X/2-15, (int)position.Y - 40, 30, 30), Color.Orange);
                
            }
        }
    }
}
