using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    class PowerUpManager
    {
        PowerUpDamage mPowerUpDamage;
        PowerUpHealth mPowerUpHealth;
        PowerUpShield mPowerUpShield;
        Random random = new Random();

        public List<BasePowerUp> PowerUps;
        List<BasePowerUp> ReferencePowerUp;
        // PowerUpSpeed
        float mPowerUpSpeed = 3f;

        // PowerUpTimer
        float mTimeToSpawn = 5f;
        float resetSpawnTimer = 5f;
        


        public PowerUpManager()
        {
            PowerUps = new List<BasePowerUp>();
            ReferencePowerUp = new List<BasePowerUp>();
            ReferencePowerUp.Add(new PowerUpDamage(random));
            ReferencePowerUp.Add(new PowerUpHealth(random));
            ReferencePowerUp.Add(new PowerUpShield(random));
            for (int i = 0; i < ReferencePowerUp.Count; i++)
            {
                ReferencePowerUp[i].IsAlive = false;
            }

        }

        public void Update(GameTime gameTime)
        {
            mTimeToSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mTimeToSpawn < 0)
            {
                int temp;
                temp = random.Next(0, 3);
                PowerUps.Add(ReferencePowerUp[temp]);
                PowerUps[0].IsAlive = true;
                PowerUps[0].Position = new Vector2(random.Next(0, Settings.window.ClientBounds.Width), 0);
                mTimeToSpawn = resetSpawnTimer;
            }



            for (int i = 0; i < PowerUps.Count; i++)
            {
                PowerUps[i].PosY += mPowerUpSpeed;
                
                for (int j = 0; j < Objects.playerList.Count; j++)
                {
                    if (PowerUps.Count > 0 && PowerUps[i] != null)
                    {
                        if (PowerUps[i].GetRectangle.Intersects(Objects.playerList[j].Rectangle) && PowerUps[i].IsAlive == true)
                        {
                            if (PowerUps[i] is PowerUpDamage)
                            {
                                Objects.playerList[j].PowerUpDamage = true;
                                
                            }
                            if (PowerUps[i] is PowerUpHealth)
                            {
                                Objects.playerList[j].PowerUpHealth = true;
                                Objects.playerList[j].Health += 20;
                            }
                            if (PowerUps[i] is PowerUpShield)
                            {
                                Objects.playerList[j].PowerUpShield = true;
                            }
                            PowerUps.Remove(PowerUps[i]);
                        }
                    }
                    if (Objects.playerList[j].PowerUpDamage == true)
                    {
                        Objects.playerList[j].Damage += 10;
                    }
                    else
                    {
                        Objects.playerList[j].Damage = 10;
                    }
                }
            }
            for (int i = 0; i < PowerUps.Count; i++)
            {
                if (PowerUps[i].PosY > Settings.window.ClientBounds.Height)
                {
                    PowerUps.Remove(PowerUps[i]);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < PowerUps.Count; i++)
            {
                PowerUps[i].Draw(spriteBatch);
            }
        }
    }
}
