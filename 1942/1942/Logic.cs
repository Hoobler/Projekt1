﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace _1942
{
    class Logic
    {

        /*private List<BasePlayer> playerList = new List<BasePlayer>();
        private List<BaseEnemy> enemyList = new List<BaseEnemy>();

        private List<BaseProjectile> projectileList = new List<BaseProjectile>();*/

        PowerUpManager mPowerUpManager;

        LevelLoader levelLoader;
        ContentManager Content;

        Random random = new Random();

        
        public Logic(ContentManager Content)
        {
            this.Content = Content;
            NewGame();
        }
        

        public void NewGame()
        {
            
            

            levelLoader = new LevelLoader(Settings.currentLevel.ToString(), this.Content);



            for (int i = 0; i < levelLoader.MapSpawnList.Count; i++)
            {
                if (levelLoader.MapSpawnList[i].Formation == "formation1")
                    Objects.formationList.Add(new Formation1(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation2")
                    Objects.formationList.Add(new Formation2(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "boss1")
                    Objects.bossList.Add(new Boss1(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "tower")
                    Objects.enemyList.Add(new Enemy_Tower(levelLoader.MapSpawnList[i].Position));
            }

            if (Settings.nr_of_players >= 1 && Objects.playerList.Count <= 0)
                Objects.playerList.Add(new Player1());

            if (Settings.nr_of_players >= 2 && Objects.playerList.Count <= 1)
                Objects.playerList.Add(new Player2());

            
            mPowerUpManager = new PowerUpManager();
        }


        public void Update(KeyboardState keyState, GameTime gameTime)
        {

            if (levelLoader.LevelHasEnded())
            {
                Settings.currentLevel++;
                NewGame();

            }
            
            CollisionRemoval();
            levelLoader.MoveCamera(Settings.level_speed);
            Objects.Update(keyState, gameTime);

            
            

            

            mPowerUpManager.Update(gameTime);
            Objects.DeadRemoval();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            levelLoader.Draw(spriteBatch);
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
                    if (Objects.enemyList[j].IsFlying == true)
                    {
                        if (Objects.enemyList[j].Rectangle.Intersects(Objects.playerList[i].Rectangle))
                        {
                            if (Objects.playerList[i].PowerUpShield == true)
                            { }
                            else
                            {
                                Objects.playerList[i].Health -= Settings.damage_collision;
                            }
                            Objects.enemyList[j].SetDead();
                            Objects.playerList[i].MyScore += Objects.enemyList[j].MyScore;
                        }
                    }
                }
            }

            for (int j = 0; j < Objects.formationList.Count; j++)
            {
                for (int k = 0; k < Objects.formationList[j].enemyInFormationList.Count; k++)
                {
                    for (int i = 0; i < Objects.playerList.Count; i++)
                    {
                        if (Objects.formationList[j].enemyInFormationList[k].IsFlying == true)
                        {
                            if (Objects.formationList[j].enemyInFormationList[k].Rectangle.Intersects(Objects.playerList[i].Rectangle))
                            {
                                Objects.formationList[j].enemyInFormationList[k].SetDead();

                                if (Objects.playerList[i].PowerUpShield == true)
                                { }
                                else
                                {
                                    Objects.playerList[i].Health -= Settings.damage_collision;
                                }
                                Objects.formationList[j].list_Zero[k].SetDead();
                                Objects.playerList[i].MyScore += Objects.formationList[j].list_Zero[k].MyScore;
                            }
                        }
                    }
                }
            }

            //allows players to shoot the shit out of formations
            for (int j = 0; j < Objects.formationList.Count; j++)
            {
                for (int k = 0; k < Objects.formationList[j].enemyInFormationList.Count; k++)
                {
                    bool check = false;
                    for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                    {                        
                            if (Objects.formationList[j].enemyInFormationList[k].Rectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                            {
                                Objects.formationList[j].enemyInFormationList[k].Health -= Objects.playerProjectileList[i].Damage;

                            if (Objects.formationList[j].list_Zero[k].Rectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                            {
                                Objects.formationList[j].list_Zero[k].Health -= Objects.playerProjectileList[i].Damage;
                                if (Objects.formationList[j].list_Zero[k].Health <= 0 && !check)
                                {
                                    check = true;
                                    Objects.playerList[Objects.playerProjectileList[i].PlayerID].MyScore += Objects.formationList[j].list_Zero[k].MyScore;
                                }

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
                        if (Objects.playerList[j].PowerUpShield == true)
                        { }
                        else
                        {
                            Objects.playerList[j].Health -= Objects.enemyProjectileList[i].Damage;
                        }
                        Objects.enemyProjectileList[i].SetDead();
                    }
                }
            }

            //bosses
            for (int j = 0; j < Objects.playerProjectileList.Count; j++)
            {
                for (int i = 0; i < Objects.bossList.Count; i++)
                {
                    if (Objects.bossList[i].IsKillable())
                    {
                        if (Objects.playerProjectileList[j].Rectangle.Intersects(Objects.bossList[i].Rectangle))
                        {
                            Objects.bossList[i].Health -= Objects.playerProjectileList[j].Damage;
                            Objects.playerProjectileList[j].SetDead();
                        }
                    }
                }
            }
            for (int j = 0; j < Objects.bossList.Count; j++)
            {
                for (int k = 0; k < Objects.bossList[j].gunList.Count; k++)
                {
                    if (Objects.bossList[j].gunList[k].Activated)
                    {
                        for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                        {

                            if (Objects.bossList[j].gunList[k].Rectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                            {
                                Objects.bossList[j].gunList[k].Health -= Objects.playerProjectileList[i].Damage;
                                Objects.playerProjectileList[i].SetDead();
                            }

                        }
                    }
                }
            }
        }

        
    }
}
