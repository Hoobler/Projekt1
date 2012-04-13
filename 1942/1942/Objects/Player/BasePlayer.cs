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
        protected int health = 100;

        public BasePlayer(): base()
        {
            angle = 0;
            color = Color.White;
            layerDepth = 0f;
            size.X = Settings.window.ClientBounds.Width/40;
            size.Y = Settings.window.ClientBounds.Width / 40;
            speedHor = 4f;
            speedUp = 4f;
            speedDown = 4f;
            position.X = Settings.window.ClientBounds.Width / 2 - size.X /2;
            position.Y = Settings.window.ClientBounds.Height - size.Y;
            texture = Texture2DLibrary.player;
        }

        public virtual void Update(KeyboardState keyState, GameTime gameTime)
        { }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        

    }
}
