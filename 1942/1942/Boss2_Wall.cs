using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss2_Wall : Boss_Accessory
    {

        public Boss2_Wall(Vector2 position)
        {
            this.position = position;
            texture = Texture2DLibrary.spaceship;
            size = new Point(300, 50);
            color = Color.White;
            maxHealth = 2000;
            health = maxHealth;

        }
    }
}
