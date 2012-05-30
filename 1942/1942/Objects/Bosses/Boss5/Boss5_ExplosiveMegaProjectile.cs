using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss5_ExplosiveMegaProjectile : BaseProjectile
    {
        float timer;
        public Boss5_ExplosiveMegaProjectile(Vector2 startingPos)
        {
            size = new Point(1, 1);
            color = Color.White;
            texture = Texture2DLibrary.boss5_megaProjectile;
            position = startingPos;
            damage = Settings.boss5_megaProjectile_damage;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            size.X+=2;
            size.Y+=2;
            position.X--;
            position.Y--;
            color.G-=2;
            color.B-=2;

            if (timer >= 2f)
                dead = true;
        }
    }
}
