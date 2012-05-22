using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Projectile_Player : BaseProjectile
    {

        int playerID;
        public Projectile_Player(Vector2 startingPos, int playerID, int damage, Color myColor)
            : base()
        {
            position = new Vector2(startingPos.X, startingPos.Y);
            size = Settings.player_projectile_size;
            layerDepth = 1.0f;
            angle = 0;
            speed = Settings.player_projectile_speed;
            color = myColor;
            texture = Texture2DLibrary.projectile_player;
            this.damage = damage;
            this.playerID = playerID;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            if (position.Y < -size.Y)
                dead = true;
        }

        public int PlayerID
        {
            get { return playerID; }
        }


    }
}
