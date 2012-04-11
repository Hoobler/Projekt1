using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Enemy_Tower_Dead: StationaryObject
    {
        public Enemy_Tower_Dead(Vector2 startingPos, Point size)
        {
            texture = Texture2DLibrary.enemy_tower_dead;
            position = startingPos;
            this.size = new Point (size.Y, size.Y);
            color = Color.Black;
            layerDepth = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
