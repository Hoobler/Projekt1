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

        public Projectile_Enemy_Zero(Vector2 startingPos)
        {
            position = new Vector2(startingPos.X, startingPos.Y);
            size = Settings.zero_projectile_size;
            layerDepth = 1.0f;
            angle = 0;
            speed = new Vector2(Settings.zero_projectile_speed.X + Settings.zero_speed.X, Settings.zero_projectile_speed.Y + Settings.zero_speed.Y + Settings.level_speed);
            
            color = Color.PaleVioletRed;
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
