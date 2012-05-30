using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss1_Projectile : BaseProjectile
    {
        
        public Boss1_Projectile(Vector2 startingPos, float angle)
        {
            position = startingPos;
            this.angle = angle;
            this.size = new Point(4, 4);
            layerDepth = 0.1f;
            color = Color.White;
            texture = Texture2DLibrary.boss1_projectile;
            damage = Settings.boss1_projectile_damage;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Settings.boss1_projectile_speed;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
