using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    class Logic
    {

        /*private List<BasePlayer> playerList = new List<BasePlayer>();
        private List<BaseEnemy> enemyList = new List<BaseEnemy>();

        private List<BaseProjectile> projectileList = new List<BaseProjectile>();*/


        Random random = new Random();
        float timeUntilNextZero;
        float timeBetweenZero = 2;

        float timeUntilNextTower;
        float timeBetweenTower = 3f;

        private int timer;

        public Logic()
        {
            if (Settings.nr_of_players >= 1)
                Objects.playerList.Add(new Player1());
            
            if (Settings.nr_of_players >= 2)
                Objects.playerList.Add(new Player2());

            
        }


        public void Update(KeyboardState keyState, GameTime gameTime)
        {
            timer++;
            CollisionRemoval();

            for (int i = 0; i < Objects.playerList.Count; i++)
                Objects.playerList[i].Update(keyState, gameTime);

            for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                Objects.playerProjectileList[i].Update(gameTime);

            for (int i = 0; i < Objects.deadList.Count; i++)
                Objects.deadList[i].Update(gameTime);

            for (int i = 0; i < Objects.enemyList.Count; i++)
                Objects.enemyList[i].Update(gameTime);

            for (int i = 0; i < Objects.enemyProjectileList.Count; i++)
                Objects.enemyProjectileList[i].Update(gameTime);

            timeUntilNextZero += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextTower += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeUntilNextZero >= timeBetweenZero)
            {
                Objects.enemyList.Add(new Enemy_Zero(new Vector2(random.Next(0, Settings.window.ClientBounds.Width-20),-200), new Vector2(0,0)));

                timeUntilNextZero -= timeBetweenZero;
            }

            if (timeUntilNextTower >= timeBetweenTower)
            {
                Objects.enemyList.Add(new Enemy_Tower(new Vector2(random.Next(0, Settings.window.ClientBounds.Width - 20), -30)));

                timeUntilNextTower -= timeBetweenTower;
            }
            

            //looping purposes
            if (timer >= 600)
                timer -= 600;

            
            DeadRemoval();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Objects.deadList.Count; i++)
                Objects.deadList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                Objects.playerProjectileList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.enemyProjectileList.Count; i++)
                Objects.enemyProjectileList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.enemyList.Count; i++)
                Objects.enemyList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.playerList.Count; i++)
                Objects.playerList[i].Draw(spriteBatch);

        }

        public void DeadRemoval()
        {
            for (int i = Objects.enemyList.Count-1; i >= 0; i--)
                if(Objects.enemyList[i].IsDead())
                    Objects.enemyList.RemoveAt(i);

            for (int i = Objects.deadList.Count - 1; i >= 0; i--)
                if (Objects.deadList[i].IsDead())
                    Objects.deadList.RemoveAt(i);

            for (int i = Objects.playerProjectileList.Count - 1; i >= 0; i--)
                if (Objects.playerProjectileList[i].IsDead())
                    Objects.playerProjectileList.RemoveAt(i);

            for (int i = Objects.enemyProjectileList.Count - 1; i >= 0; i--)
                if (Objects.enemyProjectileList[i].IsDead())
                    Objects.enemyProjectileList.RemoveAt(i);
            
        }

        public void CollisionRemoval()
        {
            //if a player's projectile hits an enemy, they die
            for (int j = 0; j < Objects.playerProjectileList.Count; j++)
            {
                for (int i = 0; i < Objects.enemyList.Count; i++)
                {
                    if (Objects.playerProjectileList[j].Rectangle.Intersects(Objects.enemyList[i].Rectangle))
                    {
                        Objects.enemyList[i].Health -= Objects.playerProjectileList[j].Damage;
                        Objects.playerProjectileList[j].SetDead();
                    }
                }
            }

            //if a player collides with a flying enemy, both die
            for(int j= 0; j < Objects.enemyList.Count; j++)
            {
                for(int i = 0; i < Objects.playerList.Count; i++)
                {
                    if (Objects.enemyList[j].Flying == true)
                    {
                        if (Objects.enemyList[j].Rectangle.Intersects(Objects.playerList[i].Rectangle))
                        {
                            Objects.playerList[i].Health -= Settings.damage_collision;
                            Objects.enemyList[j].SetDead();
                        }
                    }
                }
            }

            //if an enemy's projectile hits a player, the player dies
            for (int i = 0; i < Objects.enemyProjectileList.Count; i++)
            {
                for (int j = 0; j < Objects.playerList.Count; j++)
                {
                    if (Objects.enemyProjectileList[i].Rectangle.Intersects(Objects.playerList[j].Rectangle))
                    {
                        Objects.playerList[j].Health -= Objects.enemyProjectileList[i].Damage;
                        Objects.enemyProjectileList[i].SetDead();
                    }
                }
            }
        }

        
    }
}
