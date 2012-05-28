using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss_Base : BaseObject
    {
        protected int phase;
        protected int maxHealth;
        protected int health;
        protected bool activated;
        protected bool killable = false;
        protected bool killed;
        protected bool accessorised;
        protected float deathTimer = 240;
        public List<Boss_Accessory> accessoryList = new List<Boss_Accessory>();
        
        protected List<Rectangle> targetableRectangles = new List<Rectangle>();
        protected int score;
        Point lifebarSizeFull;
        Point lifebarSize;

        public Boss_Base()
        {
            lifebarSizeFull = new Point(Settings.window.ClientBounds.Width - 200, 30);
            lifebarSize = lifebarSizeFull;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            LifeBarCalc();

            for (int i = 0; i < accessoryList.Count; i++)
            {
                accessoryList[i].Update(gameTime, speed);
            }

            if (activated && accessorised)
            {
                int killables = 0;
                for (int i = 0; i < accessoryList.Count; i++)
                    if (accessoryList[i].IsKillable && !accessoryList[i].Killed)
                        killables++;
                if (killables == 0)
                    killed = true;
            }

            if (!activated)
            {
                position.Y += Settings.level_speed;
            }

            if (!activated && position.Y >= -1000)
            {
                position.Y = -size.Y;
                if (!accessorised)
                {
                    Accessorize();
                    activated = true;
                }
                for (int i = 0; i < accessoryList.Count; i++)
                {
                    if (!accessoryList[i].Activated)
                        accessoryList[i].Activated = true;
                }
            }

            if (activated)
            {
                position += speed;
                if (health <= 0)
                {
                    health = 0;
                    killed = true;
                }
                color.B = (byte)((float)255 * ((float)health / (float)maxHealth));
                color.G = (byte)((float)255 * ((float)health / (float)maxHealth));
            }

            if (killed)
            {
                accessoryList.Clear();
                
                deathTimer --;
                position += new Vector2(1, 1);

                if (deathTimer == 239)
                    Objects.particleList.Add(new Particle_Explosion(position + new Vector2(size.X * (1f / 5f), size.Y * (1f / 5f)), new Point(40, 40)));
                if (deathTimer == 200)
                    Objects.particleList.Add(new Particle_Explosion(position + new Vector2(size.X * (5f / 5f), size.Y * (3f / 5f)), new Point(60, 60)));
                if (deathTimer == 150)
                    Objects.particleList.Add(new Particle_Explosion(position + new Vector2(size.X * (3f / 5f), size.Y * (1f / 5f)), new Point(50, 50)));
                if (deathTimer == 100)
                {
                    Objects.particleList.Add(new Particle_Explosion(position + new Vector2(size.X * (2f / 5f), size.Y * (3f / 5f)), new Point(80, 80)));
                    Objects.particleList.Add(new Particle_Explosion(position + new Vector2(size.X * (5f / 5f), size.Y * (5f / 5f)), new Point(50, 50)));
                }
                if (deathTimer == 50)
                    Objects.particleList.Add(new Particle_Explosion(position + new Vector2(size.X * (3f / 5f), size.Y * (2f / 5f)), new Point(60, 60)));
                if (deathTimer == 0)
                {
                    Objects.particleList.Add(new Particle_Explosion(Center, new Point(1200,1200)));
                    SoundLibrary.Explosion_Big.Play();
                    dead = true;
                }
            }

            if (killed)
            {
                Objects.enemyList.Clear();
                Objects.enemyProjectileList.Clear();
                Objects.powerUpList.Clear();
            }
            
            DeadRemoval();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (activated)
            {
                for (int i = 0; i < accessoryList.Count; i++)
                {
                    accessoryList[i].Draw(spriteBatch);
                }
                if (phase == 1)
                    spriteBatch.DrawString(FontLibrary.Hud_Font, "BOSS APPROACHING", new Vector2(200, 200), Color.Red);
                if (!killed)
                {
                    spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                    new Rectangle(100, 20, lifebarSizeFull.X + 10, lifebarSizeFull.Y + 10),
                    Color.Gray);
                    spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                        new Rectangle(105, 25, lifebarSize.X, lifebarSize.Y),
                        Color.Red);
                }
            }
        }

        public virtual void Accessorize()
        {
            accessorised = true;
        }

        public int Health
        { 
            get { return health; }
            set { health = value; }
        }

        public bool IsActivated()
        {
            return activated;
        }

        public bool IsKillable()
        {
            return killable;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public List<Rectangle> TargetRectangles
        {
            get { return targetableRectangles; }
        }

        public void DeadRemoval()
        {

            for (int j = accessoryList.Count - 1; j >= 0; j--)
            {
                if (accessoryList[j].IsDead())
                    accessoryList.RemoveAt(j);
            }

        }
        public void LifeBarCalc()
        {
            int maxTotalHealth = 0;
            if (killable)
                maxTotalHealth += maxHealth;
            for (int i = 0; i < accessoryList.Count; i++)
            {
                if (accessoryList[i].IsKillable)
                    maxTotalHealth += accessoryList[i].HealthMax;
            }

            int totalHealth = 0;
            if (killable)
                totalHealth += health;
            for (int i = 0; i < accessoryList.Count; i++)
            {

                if (accessoryList[i].IsKillable)
                    totalHealth += accessoryList[i].Health;
            }
            lifebarSize.X = (int)((float)totalHealth / (float)maxTotalHealth * (float)lifebarSizeFull.X);
        }
    }
}
