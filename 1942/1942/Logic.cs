using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    class Logic
    {

        bool playerOneAdd = false;
        bool playerTwoAdd = false;
        string playerName = String.Empty;
        KeyboardState oldKeyState;
        KeyboardState myKeyState;


        PowerUpManager mPowerUpManager;
        public LevelLoader levelLoader;

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
                if (levelLoader.MapSpawnList[i].Formation == "formation1a")
                    Objects.formationList.Add(new Formation1a(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation2a")
                    Objects.formationList.Add(new Formation2a(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation3a")
                    Objects.formationList.Add(new Formation3a(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation1b")
                    Objects.formationList.Add(new Formation1b(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation2b")
                    Objects.formationList.Add(new Formation2b(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation3b")
                    Objects.formationList.Add(new Formation3b(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation1c")
                    Objects.formationList.Add(new Formation1c(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation2c")
                    Objects.formationList.Add(new Formation2c(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "formation3c")
                    Objects.formationList.Add(new Formation3c(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "boat")
                    Objects.enemyList.Add(new Enemy_Boat(levelLoader.MapSpawnList[i].Position, levelLoader.MapSpawnList[i].IsMirrored()));
                else if (levelLoader.MapSpawnList[i].Formation == "kamikaze")
                    Objects.enemyList.Add(new Enemy_Kamikaze(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "boss1")
                    Objects.bossList.Add(new Boss1(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "boss2")
                    Objects.bossList.Add(new Boss2(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "boss3")
                {
                    Objects.bossList.Add(new Boss3(levelLoader.MapSpawnList[i].Position, 0f));
                    Objects.bossList.Add(new Boss3(levelLoader.MapSpawnList[i].Position, 3f));
                    Objects.bossList.Add(new Boss3(levelLoader.MapSpawnList[i].Position, 6f));
                    Objects.bossList.Add(new Boss3(levelLoader.MapSpawnList[i].Position, 9f));
                }
                else if (levelLoader.MapSpawnList[i].Formation == "boss5")
                    Objects.bossList.Add(new Boss5(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "escort")
                    Objects.escortList.Add(new Escort(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "tower")
                    Objects.enemyList.Add(new Enemy_Tower(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "PowerUp_Health")
                    Objects.powerUpList.Add(new PowerUpHealth(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "PowerUp_Armor")
                    Objects.powerUpList.Add(new PowerUpShield(levelLoader.MapSpawnList[i].Position));
                else if (levelLoader.MapSpawnList[i].Formation == "PowerUp_Damage")
                    Objects.powerUpList.Add(new PowerUpDamage(levelLoader.MapSpawnList[i].Position));
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

            if (Settings.currentLevel == Settings.CurrentLevel.Level1)
            {
                for (int i = 0; i < Objects.bossList.Count; i++)
                    if (!Objects.bossList[i].IsActivated())
                        MusicManager.SetMusic(SoundLibrary.Level1);
            }

            if (levelLoader.LevelHasEnded())
            {
                Settings.currentLevel++;
                NewGame();
                if (Settings.currentLevel == Settings.CurrentLevel.Level2)
                    MusicManager.SetMusic(SoundLibrary.Level2);
                if (Settings.currentLevel == Settings.CurrentLevel.Level3)
                    MusicManager.SetMusic(SoundLibrary.Level3);
            }

            if (Objects.bossList.Count == 0)
                MusicManager.SetMusic(SoundLibrary.Level3);

            CollisionRemoval();
            levelLoader.MoveCamera(Settings.level_speed);
            Objects.Update(keyState, gameTime);

            mPowerUpManager.Update(gameTime);

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
                                playerOneAdd = false;
                                playerTwoAdd = false;
                            }
                        }
                    }
                }
            }
            #endregion
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
            #region PlayerProjectile_Collision
            //if a player's projectile hits an enemy, they die
            for (int i = 0; i < Objects.playerProjectileList.Count; i++)
            {
                //player projectiles vs enemyList
                for (int j = 0; j < Objects.enemyList.Count; j++)
                    if (Objects.enemyList[j].IsActivated && Objects.enemyList[j].IsKillable())
                    {
                        bool check = false;
                        if (Objects.playerProjectileList[i].Rectangle.Intersects(Objects.enemyList[j].Rectangle))
                        {
                            Objects.enemyList[j].Health -= Objects.playerProjectileList[i].Damage;
                            if (Objects.enemyList[j].Health <= 0 && !check)
                            {
                                check = true;
                                Objects.playerList[Objects.playerProjectileList[i].PlayerID].MyScore += Objects.enemyList[j].MyScore;
                            }
                            Objects.playerProjectileList[i].SetDead();
                        }
                    }

                //player projectiles vs formationList
                for (int j = 0; j < Objects.formationList.Count; j++)
                    for (int k = 0; k < Objects.formationList[j].enemyInFormationList.Count; k++)
                        if (Objects.formationList[j].enemyInFormationList[k].IsActivated)
                        {
                            bool check = false;
                            if (Objects.formationList[j].enemyInFormationList[k].TargetingRectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                            {
                                Objects.formationList[j].enemyInFormationList[k].Health -= Objects.playerProjectileList[i].Damage;
                                if (Objects.formationList[j].enemyInFormationList[k].TargetingRectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
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

                //player projectiles vs bossList
                for (int j = 0; j < Objects.bossList.Count; j++)
                    if (Objects.bossList[j].IsActivated())
                        if (Objects.bossList[j].IsKillable()) //actual boss
                            for (int k = 0; k < Objects.bossList[j].TargetRectangles.Count; k++)
                                if (Objects.playerProjectileList[i].Rectangle.Intersects(Objects.bossList[j].TargetRectangles[k]))
                                {
                                    Objects.bossList[j].Health -= Objects.playerProjectileList[i].Damage;
                                    Objects.playerProjectileList[i].SetDead();
                                    break;
                                }
                                else { }
                        else //accessories
                        {
                            for (int k = 0; k < Objects.bossList[j].accessoryList.Count; k++)
                                if (Objects.bossList[j].accessoryList[k].ReallyActivated && Objects.bossList[j].accessoryList[k].IsKillable())
                                    if (Objects.bossList[j].accessoryList[k].Rectangle.Intersects(Objects.playerProjectileList[i].Rectangle))
                                    {
                                        Objects.bossList[j].accessoryList[k].Health -= Objects.playerProjectileList[i].Damage;
                                        Objects.playerProjectileList[i].SetDead();
                                        break;
                                    }
                        }

            }
            #endregion

            #region Player_Collision

            for (int i = 0; i < Objects.playerList.Count; i++)
            {
                //Player vs Flying enemy
                for (int j = 0; j < Objects.enemyList.Count; j++)
                    if (Objects.enemyList[j].IsActivated && Objects.enemyList[j].IsFlying && Objects.enemyList[j].IsKillable())
                        if (Objects.enemyList[j].TargetingRectangle.Intersects(Objects.playerList[i].Rectangle))
                        {
                            if (!Objects.playerList[i].PowerUpShield)
                                Objects.playerList[i].Health -= Settings.damage_collision;
                            Objects.enemyList[j].SetDead();
                            Objects.playerList[i].MyScore += Objects.enemyList[j].MyScore;
                        }
                //Player vs Enemies in Formations
                for (int j = 0; j < Objects.formationList.Count; j++)
                    if (Objects.formationList[j].IsActivated())
                        for (int k = 0; k < Objects.formationList[j].enemyInFormationList.Count; k++)
                            if (Objects.formationList[j].enemyInFormationList[k].TargetingRectangle.Intersects(Objects.playerList[i].Rectangle))
                            {
                                if (!Objects.playerList[i].PowerUpShield)
                                    Objects.playerList[i].Health -= Settings.damage_collision;

                                Objects.formationList[j].enemyInFormationList[k].SetDead();
                                Objects.playerList[i].MyScore += Objects.formationList[j].enemyInFormationList[k].MyScore;
                            }
                //Player vs Enemy bullets
                for (int j = 0; j < Objects.enemyProjectileList.Count; j++)
                    if (Objects.enemyProjectileList[j].Rectangle.Intersects(Objects.playerList[i].Rectangle))
                    {
                        if (!Objects.playerList[i].PowerUpShield)
                            Objects.playerList[i].Health -= Objects.enemyProjectileList[j].Damage;
                        Objects.enemyProjectileList[j].SetDead();
                    }
            }
            #endregion

            #region Escort_Collision
            for (int i = 0; i < Objects.escortList.Count; i++)
            {
                for (int j = 0; j < Objects.enemyProjectileList.Count; j++)
                    if (Objects.escortList[i].Rectangle.Intersects(Objects.enemyProjectileList[j].Rectangle))
                    {
                        Objects.escortList[i].Health -= Objects.enemyProjectileList[j].Damage;
                        Objects.enemyProjectileList[j].SetDead();
                        break;
                    }
                for (int j = 0; j < Objects.formationList.Count; j++)
                    for (int k = 0; k < Objects.formationList[j].enemyInFormationList.Count; k++)
                        if (Objects.escortList[i].Rectangle.Intersects(Objects.formationList[j].enemyInFormationList[k].Rectangle))
                        {
                            Objects.escortList[i].Health -= Settings.damage_collision;
                            Objects.formationList[j].enemyInFormationList[k].SetDead();
                            break;
                        }
            }

            #endregion
        }
    }
}

