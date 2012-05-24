using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Projectile_Enemy_Zero: BaseProjectile
    {

        public Projectile_Enemy_Zero(Vector2 startingPos, Vector2 speed)
        {
            position = new Vector2(startingPos.X, startingPos.Y);
            size = Settings.zero_projectile_size;
            layerDepth = 1.0f;
            angle = 0;
            this.speed = speed + Settings.zero_projectile_speed;
            this.speed.Y += Settings.level_speed;
            
            color = Color.Yellow;
            texture = Texture2DLibrary.projectile_enemy_zero;
            
            spriteEffect = SpriteEffects.FlipVertically;
            damage = Settings.zero_projectile_damage;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (position.Y > Settings.window.ClientBounds.Height+size.Y)
                dead = true;
        }


    }
}
