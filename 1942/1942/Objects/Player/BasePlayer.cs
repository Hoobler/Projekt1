using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    class BasePlayer: BaseObject
    {
        protected float speedHor;
        protected float speedUp;
        protected float speedDown;

        public BasePlayer(): base()
        {
            angle = 0;
            color = Color.White;
            layerDepth = 0f;
            size.X = 16;
            size.Y = 16;
            speedHor = 4f;
            speedUp = 4f;
            speedDown = 4f;
            position.X = Settings.window.ClientBounds.Width / 2 - size.X /2;
            position.Y = Settings.window.ClientBounds.Height - size.Y;
            texture = Texture2DLibrary.player;
        }

        public virtual void Update(KeyboardState keyState, GameTime gameTime)
        { }
        
        

    }
}
