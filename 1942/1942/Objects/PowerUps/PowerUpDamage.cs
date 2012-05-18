﻿using System;
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
    class PowerUpDamage : BasePowerUp
    {

        public PowerUpDamage(Vector2 mySpawnPosition)
        {
            Position = mySpawnPosition;
        }

        public void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Texture2DLibrary.texture_PowerUp_Damage, new Rectangle((int)Position.X, (int)Position.Y, size, size), Color.White);
        }
    }
}
