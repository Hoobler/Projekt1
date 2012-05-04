﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Particle_Explosion : Particle_Base
    {
        bool delay;
        public Particle_Explosion(Vector2 startingPos)
        {
            position = startingPos;
            size = new Point(39, 39);
            layerDepth = 0.0f;
            texture = Texture2DLibrary.particle_zero_explosion;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            //12
            if(animationFrame.X < 4 && delay)
            animationFrame.X++;

            if (delay)
                delay = false;
            else
                delay = true;

            if (animationFrame.X >= 4)
                dead = true;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                Rectangle,
                new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 4) + 1,
                    1,
                    38,
                    38),
                color,
                0,
                new Vector2(0, 0),
                spriteEffect,
                layerDepth);
        }

    }
}