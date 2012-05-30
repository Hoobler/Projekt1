using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss5_Projectile : BaseProjectile
    {
        Vector2 targetPosition;
        public Boss5_Projectile(Vector2 startingPos, float angle, Vector2 targetPos)
        {
            position = startingPos;
            this.angle = angle;
            this.size = new Point(4, 4);
            layerDepth = 0.1f;
            color = Color.White;
            texture = Texture2DLibrary.boss5_projectile;
            damage = Settings.boss5_projectile_damage;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Settings.boss5_projectile_speed;
            targetPosition = targetPos;
            
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(Center.X >= targetPosition.X - 10 && Center.X <= targetPosition.X + 10)
                if (Center.Y >= targetPosition.Y - 10 && Center.Y <= targetPosition.Y + 10)
                {
                    dead = true;
                    Objects.enemyProjectileList.Add(new Boss5_ExplosiveMegaProjectile(Center));
                }
            

        }

    }
}
