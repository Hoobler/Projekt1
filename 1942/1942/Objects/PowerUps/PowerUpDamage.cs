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
    class PowerUpDamage : BasePowerUp
    {

        public PowerUpDamage(Vector2 mySpawnPosition)
        {
            texture = Texture2DLibrary.texture_PowerUp_Damage;
            Position = mySpawnPosition;
        }

        public void Update()
        {
            
        }
    }
}
