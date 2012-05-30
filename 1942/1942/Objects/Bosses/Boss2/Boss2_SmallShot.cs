using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss2_SmallShot : BaseProjectile
    {

        public Boss2_SmallShot(Vector2 startingPos)
        {
            position = new Vector2(startingPos.X, startingPos.Y);
            size = Settings.zero_projectile_size;
            layerDepth = 1.0f;
            angle = 0;
            speed = new Vector2(0, 6);
            
            color = Color.Yellow;
            texture = Texture2DLibrary.projectile_enemy_zero;
            
            spriteEffect = SpriteEffects.FlipVertically;
            damage = 5;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (position.Y > Settings.windowBounds.Y+size.Y)
                dead = true;
        }
    }
}
