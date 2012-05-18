using System;
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

        bool playerOneAdd = false;
        bool playerTwoAdd = false;
        string playerName = String.Empty;
        KeyboardState oldKeyState;
        KeyboardState myKeyState;

        PowerUpManager mPowerUpManager;
        LevelLoader levelLoader;
        ContentManager Content;
        HighScore highscore;
        Hud hud;

        GameTime gameTime;

        Random random = new Random();

        public Logic(ContentManager Content)
        {
            this.Content = Content;
            NewGame();
        }


        public void NewGame()
        {

            mPowerUpManager = new PowerUpManager();

            hud = new Hud();

            levelLoader = new LevelLoader(Settings.currentLevel.ToString(), this.Content);

            highscore = new HighScore(Settings.currentLevel.ToString());



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
                else if (levelLoader.MapSpawnList[i].Formation == "PowerUp_Health")
                    mPowerUpManager.PowerUps.Add(new PowerUpHealth(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "PowerUp_Armor")
                    mPowerUpManager.PowerUps.Add(new PowerUpShield(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "PowerUp_Damage")
                    mPowerUpManager.PowerUps.Add(new PowerUpDamage(levelLoader.MapSpawnList[i].Position)); 
            }

            if (Settings.nr_of_players >= 1 && Objects.playerList.Count <= 0)
                Objects.playerList.Add(new Player1());

            if (Settings.nr_of_players >= 2 && Objects.playerList.Count <= 1)
                Objects.playerList.Add(new Player2());

            for (int i = 0; i < Objects.playerList.Count; i++)
                Objects.playerList[i].Health = 100;
          
        }


        public void Update(KeyboardState keyState, GameTime gameTime)
        {
            this.gameTime = gameTime;
            oldKeyState = myKeyState;
            myKeyState = Keyboard.GetState();
            levelLoader.Update(gameTime);

            if (levelLoader.LevelHasEnded())
            {
                Settings.currentLevel++;
                NewGame();
            }

            #region HighScoreUpdate

            if (levelLoader.HighScoreScreen())
            {
                levelLoader.ScoreLoop = true;
            }

            if (levelLoader.ScoreLoop)
            {
                playerName = KeyBoardInput.TextInput(5, false);
                highscore.SetPlayerName = playerName;

                if (Objects.playerList.Count == 2)
                {
                    if (!playerOneAdd)
                    {
                        highscore.SetCurrentPlayer = "Player 1";
                        if (oldKeyState.IsKeyUp(Keys.Enter))
                        {
                            if (KeyBoardInput.KeyState().IsKeyDown(Keys.Enter))
                            {
                                highscore.AddHighScore(playerName, Objects.playerList[0].MyScore);
                                playerOneAdd = true;
                                KeyBoardInput.EmptyWord = "";
                                highscore.RetreiveHighScore();
                            }
                        }
                    }
                    else if (!playerTwoAdd)
                    {
                        highscore.SetCurrentPlayer = "Player 2";
                        if (oldKeyState.IsKeyUp(Keys.Enter))
                        {
                            if (KeyBoardInput.KeyState().IsKeyDown(Keys.Enter))
                            {
                                highscore.AddHighScore(playerName, Objects.playerList[1].MyScore);
                                playerTwoAdd = true;
                                KeyBoardInput.EmptyWord = "";
                                highscore.RetreiveHighScore();
                            }
                        }
                    }
                    else if (playerOneAdd && playerTwoAdd)
                    {
                        //Temp stuff
                        highscore.SetCurrentPlayer = "Press space to continue the next level";
                        if (oldKeyState.IsKeyUp(Keys.Enter))
                        {
                            if (KeyBoardInput.KeyState().IsKeyDown(Keys.Space))
                            {
                                levelLoader.EndLevel = true; 
                            }
                        }
                    }
                }
            }
            #endregion

            CollisionRemoval();
            levelLoader.MoveCamera(Settings.level_speed);
            Objects.Update(keyState, gameTime);

            mPowerUpManager.Update(gameTime);
            Objects.DeadRemoval();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            levelLoader.Draw(spriteBatch);
            if (!levelLoader.ScoreLoop)
            {
                Objects.Draw(spriteBatch);
                mPowerUpManager.Draw(spriteBatch);
                hud.Draw(spriteBatch, gameTime);
            }
            if (levelLoader.ScoreLoop)
            {
                highscore.Draw(spriteBatch);
            }
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
            for (int j = 0; j < Objects.enemyList.Count; j++)
            {
                for (int i = 0; i < Objects.playerList.Count; i++)
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
                                Objects.formationList[j].enemyInFormationList[k].SetDead();
                                Objects.playerList[i].MyScore += Objects.formationList[j].enemyInFormationList[k].MyScore;
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

                            if (Objects.formationList[j].enemyInFormationList[k].Rectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                            {
                                Objects.formationList[j].enemyInFormationList[k].Health -= Objects.playerProjectileList[i].Damage;
                                if (Objects.formationList[j].enemyInFormationList[k].Health <= 0 && !check)
                                {
                                    check = true;
                                    Objects.playerList[Objects.playerProjectileList[i].PlayerID].MyScore += Objects.formationList[j].enemyInFormationList[k].MyScore;
                                }

                                Objects.playerProjectileList[i].SetDead();
                            }
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

