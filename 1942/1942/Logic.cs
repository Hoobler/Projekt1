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

        PowerUpManager mPowerUpManager;

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

            mPowerUpManager = new PowerUpManager();
        }


        public void Update(KeyboardState keyState, GameTime gameTime)
        {
            timer++;
            CollisionRemoval();

            Objects.Update(keyState, gameTime);

            timeUntilNextZero += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeUntilNextTower += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeUntilNextZero >= timeBetweenZero)
            {
                Objects.formationList.Add(new Formation2(new Vector2(random.Next(0, Settings.window.ClientBounds.Width-Settings.size_zero.X), -Settings.size_zero.X), false));
                

                timeUntilNextZero -= timeBetweenZero;
            }

            if (timeUntilNextTower >= timeBetweenTower)
            {
                Objects.enemyList.Add(new Enemy_Tower(new Vector2(random.Next(0, Settings.window.ClientBounds.Width - Settings.size_tower.X), -Settings.size_tower.Y)));

                timeUntilNextTower -= timeBetweenTower;
            }
            

            //looping purposes
            if (timer >= 600)
                timer -= 600;

            mPowerUpManager.Update(gameTime);
            Objects.DeadRemoval();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Objects.Draw(spriteBatch);
            mPowerUpManager.Draw(spriteBatch);
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

            for (int j = 0; j < Objects.formationList.Count; j++)
            {
                for (int k = 0; k < Objects.formationList[j].list_Zero.Count; k++)
                {
                    for (int i = 0; i < Objects.playerList.Count; i++)
                    {
                        if (Objects.formationList[j].list_Zero[k].Flying == true)
                        {
                            if (Objects.formationList[j].list_Zero[k].Rectangle.Intersects(Objects.playerList[i].Rectangle))
                            {
                                Objects.playerList[i].Health -= Settings.damage_collision;
                                Objects.formationList[j].list_Zero[k].SetDead();
                            }
                        }
                    }
                }
            }

            //allows players to shoot the shit out of formations
            for (int j = 0; j < Objects.formationList.Count; j++)
            {
                for (int k = 0; k < Objects.formationList[j].list_Zero.Count; k++)
                {
                    for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                    {
                        
                            if (Objects.formationList[j].list_Zero[k].Rectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                            {
                                Objects.formationList[j].list_Zero[k].Health -= Objects.playerProjectileList[i].Damage;
                                Objects.playerProjectileList[i].SetDead();
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
