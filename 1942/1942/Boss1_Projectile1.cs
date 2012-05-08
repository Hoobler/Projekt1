using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss1_Projectile1 : BaseProjectile
    {
        public Boss1_Projectile1(Vector2 position)
        {
            this.position = position;
            damage = 1;
            size = new Point(2, 2);
            color = Color.White;
            speed = new Vector2(0, 5);
            texture = Texture2DLibrary.projectile_enemy_zero;
        }

    }
}
