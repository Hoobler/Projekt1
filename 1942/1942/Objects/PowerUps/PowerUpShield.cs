using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _1942
{
    class PowerUpShield : BasePowerUp
    {
        public PowerUpShield(Vector2 mySpawnPosition)
        {
            texture = Texture2DLibrary.texture_PowerUp_Armor;
            Position = mySpawnPosition;
        }

        public void Update()
        {
            
        }
    }
}
