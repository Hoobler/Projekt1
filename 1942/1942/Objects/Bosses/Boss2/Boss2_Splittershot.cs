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
    class Boss2_Splittershot : BaseProjectile
    {
        

        public Boss2_Splittershot(float angle, Vector2 origin)
        {
            Position = origin;
            size = new Point(8, 8);
            texture = Texture2DLibrary.boss2_splitterbomb;
            color = Color.White;
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))* 2;
            damage = 5;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            angle += 0.2f;
        }

        
    }
}
