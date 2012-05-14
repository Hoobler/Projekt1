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
       
        Random random = new Random();

        public List<BasePowerUp> PowerUps = new List<BasePowerUp>(); 
        // PowerUpSpeed
        float mPowerUpSpeed = Settings.level_speed;

        public PowerUpManager()
        {
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < PowerUps.Count; i++)
            {
                PowerUps[i].PosY += mPowerUpSpeed;
                PowerUps[i].IsAlive = true;
                
                for (int j = 0; j < Objects.playerList.Count; j++)
                {
                    if (PowerUps.Count > 0 && PowerUps[i] != null)
                    {
                        if (PowerUps[i].GetRectangle.Intersects(Objects.playerList[j].Rectangle) && PowerUps[i].IsAlive == true)
                        {
                            if (PowerUps[i] is PowerUpDamage)
                            {
                                Objects.playerList[j].PowerUpDamage = true;
                                if (Objects.playerList[j].TimeLeftOnDamagePowerUp < 10)
                                {
                                    Objects.playerList[j].TimeLeftOnDamagePowerUp = 10;
                                   
                                }
                            }
                            if (PowerUps[i] is PowerUpHealth)
                            {
                                Objects.playerList[j].PowerUpHealth = true;
                                Objects.playerList[j].Health += 20;
                                
                            }
                            if (PowerUps[i] is PowerUpShield)
                            {
                                Objects.playerList[j].PowerUpShield = true;
                                if (Objects.playerList[j].TimeLeftOnArmorPowerUp < 10)
                                {
                                    Objects.playerList[j].TimeLeftOnArmorPowerUp = 10;
                                    
                                }
                            }
                            PowerUps.Remove(PowerUps[i]);
                        }
                    }
                    if (Objects.playerList[j].PowerUpDamage == true)
                    {
                        Objects.playerList[j].Damage += 10;
                        Objects.playerList[j].ProjectileColor = Color.Orange;
                    }
                    else
                    {
                        Objects.playerList[j].Damage = 10;
                        Objects.playerList[j].ProjectileColor = Color.Yellow;
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
