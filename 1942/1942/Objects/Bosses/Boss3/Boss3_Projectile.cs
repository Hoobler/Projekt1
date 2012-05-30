using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    


    class Boss3_Projectile : BaseProjectile
    {

        public Boss3_Projectile(Vector2 position, float angle)
        {
            this.position = position;
            size = new Point(4, 4);

            texture = Texture2DLibrary.spaceship;
            color = Color.White;
            damage = 5;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 2;

        }

    }
}
