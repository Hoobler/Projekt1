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
    class Boss2_BigShot : BaseProjectile
    {
        float timeUntilExplode = 2f;

        public Boss2_BigShot(Vector2 origin, float angle)
        {
            Position = origin;
            size = new Point(15, 15);
            texture = Texture2DLibrary.boss2_bigshot;
            color = Color.White;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 2;
            damage = 20;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeUntilExplode -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeUntilExplode <= 0)
            {
                Objects.enemyProjectileList.Add(new Boss2_Splittershot((float)Math.PI * 2, position));
                Objects.enemyProjectileList.Add(new Boss2_Splittershot((float)Math.PI * 2 * (1f / 5f), position));
                Objects.enemyProjectileList.Add(new Boss2_Splittershot((float)Math.PI * 2 * (2f / 5f), position));
                Objects.enemyProjectileList.Add(new Boss2_Splittershot((float)Math.PI * 2 * (3f / 5f), position));
                Objects.enemyProjectileList.Add(new Boss2_Splittershot((float)Math.PI * 2 * (4f / 5f), position));
                dead = true;
            }

        }
    }
}
