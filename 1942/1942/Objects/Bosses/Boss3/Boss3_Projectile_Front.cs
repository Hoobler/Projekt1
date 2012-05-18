using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss3_Projectile_Front : BaseProjectile
    {
        public Boss3_Projectile_Front(Vector2 position, float angle)
        {
            this.position = position;
            size = new Point(4, 2);

            texture = Texture2DLibrary.projectile_enemy_zero;
            color = Color.Yellow;
            damage = 1;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 6;

        }


    }
}
