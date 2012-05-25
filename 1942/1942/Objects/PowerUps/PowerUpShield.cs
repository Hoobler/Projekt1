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
            Position = mySpawnPosition;
        }

        public void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Texture2DLibrary.texture_PowerUp_Armor, new Rectangle((int)Position.X, (int)Position.Y, size.X, size.Y), Color.White);
        }
    }
}
