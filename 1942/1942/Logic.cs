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
        private bool playerOneAdd = false;
        private bool playerTwoAdd = false;

        private bool gameOver = false;

        string playerName;
        KeyboardState oldKeyState;
        KeyboardState myKeyState;

        int Timer = 0;
        bool LevelNameActive = false;

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
            Objects.ClearAll();
            Settings.gameOver = false;
            LevelNameActive = true;
            
            mPowerUpManager = new PowerUpManager();
            hud = new Hud();
            levelLoader = new LevelLoader(Settings.currentLevel.ToString(), this.Content);
 

            //Highscore stuff, DUH!
            if (Settings.currentLevel.ToString() == "Level0")
            {
            }
            else
            {
                highscore = new HighScore(Settings.currentLevel.ToString());
                playerName = String.Empty;
                playerOneAdd = false;
                playerTwoAdd = false;
            }
            #region levelLoaderReader
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
            #endregion
            if (Settings.currentLevel != Settings.CurrentLevel.Level0)
            {
                if (Settings.nr_of_players >= 1 && Objects.playerList.Count <= 0)
                    Objects.playerList.Add(new Player1());

                if (Settings.nr_of_players >= 2 && Objects.playerList.Count <= 1)
                    Objects.playerList.Add(new Player2());
                if (Objects.playerList.Count == 1)
                {
                    playerTwoAdd = true;
                }
            }
            else
            {
                Objects.playerList.Add(new MenuPlayer());
            }

            for (int i = 0; i < Objects.playerList.Count; i++)
                Objects.playerList[i].Health = 100;
            #region Music
            if (Settings.currentLevel == Settings.CurrentLevel.Level1)
                MusicManager.SetMusic(SoundLibrary.Level1);
            if (Settings.currentLevel == Settings.CurrentLevel.Level2)
                MusicManager.SetMusic(SoundLibrary.Level2);
            if (Settings.currentLevel == Settings.CurrentLevel.Level3)
                MusicManager.SetMusic(SoundLibrary.Level3);
            if (Settings.currentLevel == Settings.CurrentLevel.Level4)
                MusicManager.SetMusic(SoundLibrary.Level4);
            if (Settings.currentLevel == Settings.CurrentLevel.Level5)
                MusicManager.SetMusic(SoundLibrary.Level5);
            #endregion
        }

        private void HighScoreUpdate()
        {
            if (Settings.currentLevel != Settings.CurrentLevel.Level0)
            {
                if (levelLoader.HighScoreScreen())
                {
                    levelLoader.ScoreLoop = true;
                }

                if (levelLoader.ScoreLoop || gameOver)
                {
                    highscore.DrawText = true;
                    MusicManager.SetMusic(SoundLibrary.Twilight);
                    if (!playerOneAdd || !playerTwoAdd || !playerTwoAdd && !playerOneAdd)
                    {
                        playerName = KeyBoardInput.TextInput(5, false);
                        highscore.SetPlayerName = playerName;
                    }

                    if (Objects.playerList.Count == 2)
                    {
                        if (!playerOneAdd)
                        {
                            highscore.SetCurrentPlayer = "Player 1";
                            if (oldKeyState.IsKeyUp(Keys.Enter))
                            {
                                if (KeyBoardInput.KeyState.IsKeyDown(Keys.Enter))
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
                                if (KeyBoardInput.KeyState.IsKeyDown(Keys.Enter))
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
                            if (gameOver)
                            {
                                highscore.NextLevelText = "Press space to restart or Escape to exit the game";
                            }
                            else
                            {
                                highscore.NextLevelText = "Press space to go to the next level";
                            }
                            highscore.NextLevelPrompt = true;
                            highscore.DrawText = false;
                            if (oldKeyState.IsKeyUp(Keys.Enter))
                            {
                                if (KeyBoardInput.KeyState.IsKeyDown(Keys.Space))
                                {
                                    if (gameOver)
                                    {
                                        NewGame();
                                        gameOver = false;
                                    }
                                    else
                                    {
                                        levelLoader.EndLevel = true;
                                    }
                                }
                            }
                        }
                    }
                    if (Objects.playerList.Count == 1)
                    {
                        if (!playerOneAdd)
                        {
                            highscore.SetCurrentPlayer = "Player 1";
                            if (oldKeyState.IsKeyUp(Keys.Enter))
                            {
                                if (KeyBoardInput.KeyState.IsKeyDown(Keys.Enter))
                                {
                                    highscore.AddHighScore(playerName, Objects.playerList[0].MyScore);
                                    playerOneAdd = true;
                                    KeyBoardInput.EmptyWord = "";
                                    highscore.RetreiveHighScore();
                                }
                            }
                        }
                        else if (playerOneAdd)
                        {
                            if (gameOver)
                            {
                                highscore.NextLevelText = "Press space to restart or Escape to exit the game";
                            }
                            else
                            {
                                highscore.NextLevelText = "Press space to go to the next level";
                            }
                            highscore.NextLevelPrompt = true;
                            highscore.DrawText = false;
                            if (oldKeyState.IsKeyUp(Keys.Enter))
                            {
                                if (KeyBoardInput.KeyState.IsKeyDown(Keys.Space))
                                {
                                    if (gameOver)
                                    {
                                        NewGame();
                                        gameOver = false;
                                    }
                                    else
                                    {
                                        levelLoader.EndLevel = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Update(KeyboardState keyState, GameTime gameTime)
        {
            this.gameTime = gameTime;
            myKeyState = Keyboard.GetState();
            levelLoader.Update(gameTime);
            int temp = 0;
            for(int i = 0; i < Objects.playerList.Count; i++)
            {
                if (Objects.playerList[i].Killed)
                {
                    temp++;
                    if (temp == Objects.playerList.Count)
                    {
                        gameOver = true;
                        //Settings.currentLevel = Settings.CurrentLevel.GameOver;
                    }
                } 
            }
            if(Objects.escortList.Count >= 1)
                if (Objects.escortList[0].Killed)
                {
                    temp = Objects.playerList.Count;
                }

            if (levelLoader.LevelHasEnded())
            {
                if (Settings.currentLevel == Settings.CurrentLevel.Level0)
                {
                    NewGame();
                }
                else
                {
                    Settings.currentLevel++;
                    NewGame();
                }
            }

            if (Settings.LevelHasChanged)
            {
                NewGame();
                Settings.LevelHasChanged = false;
            }

            if (Timer < 540)
            {
                Timer++;
                if (Timer >= 300)
                {
                    Timer = 0;
                    LevelNameActive = false;
                }
            }

            CollisionRemoval();

            if (temp != Objects.playerList.Count)
            {
                hud.Update(gameTime);
                levelLoader.MoveCamera(Settings.level_speed);
                Objects.Update(keyState, gameTime);
                levelLoader.Update(gameTime);
                mPowerUpManager.Update(gameTime);
            }
            
            HighScoreUpdate();

            //post-boss
            if (Settings.currentLevel != Settings.CurrentLevel.Level4 &&  Settings.currentLevel != Settings.CurrentLevel.Level0)
                if(Objects.bossList.Count <= 0 && !levelLoader.ScoreLoop)
                    levelLoader.cameraPosition.Y = ((145 - 144)* levelLoader.TileSize());

            //escort
            if(Settings.currentLevel == Settings.CurrentLevel.Level4)
                if(Objects.escortList.Count >= 1)
                    if (levelLoader.cameraPosition.Y <= ((145 - 135)* levelLoader.TileSize))
                    {
                        Objects.escortList[0].PosY -= 5f;
                    }
  
            oldKeyState = myKeyState;
        }

        private Vector2 TextLenght(string String)
        {
            Vector2 tempVector;
            tempVector = FontLibrary.Hud_Font.MeasureString(String);

            return tempVector;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textcenter = new Vector2(Settings.windowBounds.X / 2, 240);
            
            levelLoader.Draw(spriteBatch);
            
            if (LevelNameActive)
            {
                spriteBatch.DrawString(FontLibrary.Hud_Font, "" + levelLoader.LevelName, textcenter - (TextLenght(levelLoader.LevelName) / 2), Color.White);
                //spriteBatch.DrawString(FontLibrary.Hud_Font, "" + levelLoader.LevelName, new Vector2(1f, 200f), Color.White);
            }
            if (Objects.bossList.Count >= 1)
                if (Objects.bossList[0].IsActivated() && !Objects.bossList[0].Killed)
                {
                    if (Objects.bossList[0].Phase >= 1)
                    {
                        spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                        new Rectangle(100, 20, (int)Settings.windowBounds.X - 190, 40),
                        Color.Gray);
                        spriteBatch.Draw(Texture2DLibrary.escort_lifebar,
                        new Rectangle(105, 25, ((int)((float)bossCurrentLifeBarCalc() / (float)bossTotalLifeBarCalc() * (float)(Settings.windowBounds.X - 200))), 30),
                        Color.Red);
                    }

                }
            if (!levelLoader.ScoreLoop)
            {
                Objects.Draw(spriteBatch);
                mPowerUpManager.Draw(spriteBatch);
                if (Settings.currentLevel == Settings.CurrentLevel.Level0)
                { }
                else { hud.Draw(spriteBatch); }
                
            }
            if (levelLoader.ScoreLoop || gameOver)
            {
                highscore.Draw(spriteBatch);
            }

            if (gameOver)
            {
                spriteBatch.Draw(Texture2DLibrary.GameOverScreen, new Rectangle(0, 0, (int)Settings.windowBounds.X, (int)Settings.windowBounds.Y), Color.White);
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
                    if (Objects.enemyList[j].IsActivated && Objects.enemyList[j].IsKillable)
                    {
                        bool check = false;
                        if (Objects.playerProjectileList[i].Rectangle.Intersects(Objects.enemyList[j].Rectangle))
                        {
                            Objects.enemyList[j].Health -= Objects.playerProjectileList[i].Damage;
                            if (Objects.enemyList[j].Health <= 0 && !check)
                            {
                                check = true;
                                if(Objects.playerProjectileList[i].PlayerID == 0)
                                    Settings.score_player1 += Objects.enemyList[j].MyScore;
                                else if(Objects.playerProjectileList[i].PlayerID == 1)
                                    Settings.score_player2 += Objects.enemyList[j].MyScore;
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
                                        if (Objects.playerProjectileList[i].PlayerID == 0)
                                            Settings.score_player1 += Objects.formationList[j].enemyInFormationList[k].MyScore;
                                        else if (Objects.playerProjectileList[i].PlayerID == 1)
                                            Settings.score_player2 += Objects.formationList[j].enemyInFormationList[k].MyScore;
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
                                if (Objects.bossList[j].accessoryList[k].ReallyActivated && Objects.bossList[j].accessoryList[k].IsKillable)
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
                if (Objects.playerList[i].Killed == false)
                {
                    //Player vs Flying enemy
                    for (int j = 0; j < Objects.enemyList.Count; j++)
                        if (Objects.enemyList[j].IsActivated && Objects.enemyList[j].IsFlying && Objects.enemyList[j].IsKillable)
                            if (Objects.enemyList[j].TargetingRectangle.Intersects(Objects.playerList[i].Rectangle))
                            {
                                if (!Objects.playerList[i].PowerUpShield)
                                    Objects.playerList[i].Health -= Settings.damage_collision;
                                Objects.enemyList[j].SetDead();
                                if (i == 0)
                                    Settings.score_player1 += Objects.enemyList[j].MyScore;
                                else if (i == 1)
                                    Settings.score_player2 += Objects.enemyList[j].MyScore;
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
                                    if (i == 0)
                                        Settings.score_player1 += Objects.formationList[j].enemyInFormationList[k].MyScore;
                                    else if (i == 1)
                                        Settings.score_player2 += Objects.formationList[j].enemyInFormationList[k].MyScore;
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
            }
            #endregion

            #region Escort_Collision
            for (int i = 0; i < Objects.escortList.Count; i++)
            {
                for (int j = 0; j < Objects.enemyProjectileList.Count; j++)
                    for (int k = 0; k < Objects.escortList[i].TargetRectangles.Count; k++)

                        if (Objects.escortList[i].TargetRectangles[k].Intersects(Objects.enemyProjectileList[j].Rectangle))
                        {
                            Objects.escortList[i].Health -= Objects.enemyProjectileList[j].Damage;
                            Objects.enemyProjectileList[j].SetDead();
                            break;
                        }
                for (int j = 0; j < Objects.formationList.Count; j++)
                    for (int k = 0; k < Objects.formationList[j].enemyInFormationList.Count; k++)
                        for (int l = 0; l < Objects.escortList[i].TargetRectangles.Count; l++)
                            if (Objects.escortList[i].TargetRectangles[l].Intersects(Objects.formationList[j].enemyInFormationList[k].Rectangle))
                            {
                                Objects.escortList[i].Health -= Settings.damage_collision;
                                Objects.formationList[j].enemyInFormationList[k].SetDead();
                                break;
                            }
                for (int j = 0; j < Objects.enemyList.Count; j++)
                    if (Objects.enemyList[j].IsFlying)
                        for (int k = 0; k < Objects.escortList[i].TargetRectangles.Count; k++)

                            if (Objects.escortList[i].TargetRectangles[k].Intersects(Objects.enemyList[j].Rectangle))
                            {
                                Objects.escortList[i].Health -= Settings.damage_collision;
                                Objects.enemyList[j].SetDead();
                                break;
                            }
            }

            #endregion
        }

        public int bossTotalLifeBarCalc()
        {
            int totalMaxHealth = 0;
            for (int i = 0; i < Objects.bossList.Count; i++)
            {
                if (Objects.bossList[i].IsKillable())
                    totalMaxHealth += Objects.bossList[i].HealthMax;
                for (int j = 0; j < Objects.bossList[i].accessoryList.Count; j++)
                    if (Objects.bossList[i].accessoryList[j].IsKillable)
                        totalMaxHealth += Objects.bossList[i].accessoryList[j].HealthMax;
            }
            return totalMaxHealth;
        }
        public int bossCurrentLifeBarCalc()
        {
            int totalHealth = 0;
            for (int i = 0; i < Objects.bossList.Count; i++)
            {
                if (Objects.bossList[i].IsKillable())
                    totalHealth += Objects.bossList[i].Health;
                for (int j = 0; j < Objects.bossList[i].accessoryList.Count; j++)
                    if (Objects.bossList[i].accessoryList[j].IsKillable)
                        totalHealth += Objects.bossList[i].accessoryList[j].Health;
            }
            return totalHealth;
        }
    }
}

