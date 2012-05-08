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
        Random random = new Random();

 

        // PowerUpSpeed
        float mPowerUpSpeed = 3f;

        // PowerUpTimer
        float mTimeToSpawn = 2f;
        float resetTimer = 2f;

        public PowerUpManager()
        {
            mPowerUpDamage = new PowerUpDamage(random);
            mPowerUpHealth = new PowerUpHealth(random);
            mPowerUpHealth.IsAlive = false;
            mPowerUpDamage.IsAlive = false;
        }

        public void Update(GameTime gameTime)
        {
            mTimeToSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mTimeToSpawn < 0)
            {
                if (mPowerUpDamage.IsAlive == false)
                {
                    mPowerUpDamage = new PowerUpDamage(random);
                    mPowerUpDamage.IsAlive = true;
                }
                if (mPowerUpHealth.IsAlive == false)
                {
                    mPowerUpHealth = new PowerUpHealth(random);
                    mPowerUpHealth.IsAlive = true;
                }
                mTimeToSpawn = resetTimer;
            }

            if (mPowerUpDamage.IsAlive == true)
            {
                for (int i = 0; i < Objects.playerList.Count; i++)
                {
                    if (Objects.playerList[i].Rectangle.Intersects(mPowerUpDamage.GetRectangle))
                    {
                        mPowerUpDamage.IsAlive = false;
                    }
                }
            }

            if (mPowerUpHealth.IsAlive == true)
            {
                for (int i = 0; i < Objects.playerList.Count; i++)
                {
                    if (Objects.playerList[i].Rectangle.Intersects(mPowerUpHealth.GetRectangle))
                    {
                        mPowerUpHealth.IsAlive = false;
                    }
                }
            }
            if (mPowerUpDamage.IsAlive == true)
            {
                mPowerUpDamage.PosY += mPowerUpSpeed;
            }
            if (mPowerUpHealth.IsAlive == true)
            {
                mPowerUpHealth.PosY += mPowerUpSpeed;
            }

            if (mPowerUpDamage.PosY == Settings.window.ClientBounds.Height)
            {
                mPowerUpDamage.IsAlive = false;
            }
            if (mPowerUpHealth.PosY == Settings.window.ClientBounds.Height)
            {
                mPowerUpHealth.IsAlive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mPowerUpHealth.IsAlive == true)
            {
                mPowerUpHealth.Draw(spriteBatch);
            }
            if (mPowerUpDamage.IsAlive == true)
            {
                mPowerUpDamage.Draw(spriteBatch);
            }
        }
    }
}
