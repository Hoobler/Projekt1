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
            texture = Texture2DLibrary.boss2_wall;
            size = new Point(Texture2DLibrary.boss2_wall.Bounds.Width, Texture2DLibrary.boss2_wall.Bounds.Height);
            color = Color.White;
            maxHealth = 1000;
            health = maxHealth;
            this.position.X -= size.X / 2;
            killable = true;
        }
    }
}
