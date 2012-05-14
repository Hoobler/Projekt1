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

        
        // PowerUpSpeed
        float mPowerUpSpeed = Settings.level_speed;

        public PowerUpManager()
        {
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Objects.powerUpList.Count; i++)
            {
                Objects.powerUpList[i].PosY += mPowerUpSpeed;
                Objects.powerUpList[i].IsAlive = true;
                
                for (int j = 0; j < Objects.playerList.Count; j++)
                {
                    if (Objects.powerUpList[i] != null)
                    {
                        if (Objects.powerUpList[i].GetRectangle.Intersects(Objects.playerList[j].Rectangle) && Objects.powerUpList[i].IsAlive == true)
                        {
                            if (Objects.powerUpList[i] is PowerUpDamage)
                            {
                                Objects.playerList[j].PowerUpDamage = true;
                                if (Objects.playerList[j].TimeLeftOnDamagePowerUp < 10)
                                {
                                    Objects.playerList[j].TimeLeftOnDamagePowerUp = 10;
                                   
                                }
                            }
                            if (Objects.powerUpList[i] is PowerUpHealth)
                            {
                                Objects.playerList[j].PowerUpHealth = true;
                                Objects.playerList[j].Health += 20;
                                
                            }
                            if (Objects.powerUpList[i] is PowerUpShield)
                            {
                                Objects.playerList[j].PowerUpShield = true;
                                if (Objects.playerList[j].TimeLeftOnArmorPowerUp < 10)
                                {
                                    Objects.playerList[j].TimeLeftOnArmorPowerUp = 10;
                                    
                                }
                            }
                            Objects.powerUpList.RemoveAt(i);
                            break;
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
            for (int i = 0; i < Objects.powerUpList.Count; i++)
            {
                if (Objects.powerUpList[i].PosY > Settings.window.ClientBounds.Height)
                {
                    Objects.powerUpList.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Objects.powerUpList.Count; i++)
            {
                Objects.powerUpList[i].Draw(spriteBatch);
            }
        }
    }
}
