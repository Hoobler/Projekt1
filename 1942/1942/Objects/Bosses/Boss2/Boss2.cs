using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss2 : Boss_Base
    {
        
        public Boss2(Vector2 position)
        {

            size = new Point(300, 800);
            this.position.Y = position.Y;
            
            this.position.X = Settings.window.ClientBounds.Width / 2f - size.X / 2f;
            
            texture = Texture2DLibrary.boss2;
            color = Color.White;
            maxHealth = 1;
            health = maxHealth;
            score = 20000;

            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (accessoryList.Count <= 0)
                dead = true;
            if(!dead && activated)
                for (int i = 0; i < Objects.playerList.Count; i++)
                {
                    if (Objects.playerList[i].PosY <= Settings.window.ClientBounds.Height / 2)
                        Objects.playerList[i].PosY += 5f;
                }
            

            if (dead)
            {
                Objects.particleList.Add(new Particle_Explosion(new Vector2(position.X + size.X, position.Y + size.Y / 2)));
            }

            if (activated)
            {
                speed = new Vector2(0, 0.5f);
                
            }

            if (position.Y >= -size.Y / 2f-50)
            {
                
                speed = new Vector2(0, 0);
                position.Y = -size.Y / 2f-50;
                for (int i = 0; i < accessoryList.Count; i++)
                    if (!accessoryList[i].ReallyActivated)
                        accessoryList[i].ReallyActivated = true;
            }

        }

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss2_Wall(new Vector2(this.position.X, this.position.Y + 581)));
            accessoryList.Add(new Boss2_Wall(new Vector2(this.position.X, this.position.Y + 509)));

            for (int i = 0; i < 13; i++)
                accessoryList.Add(new Boss2_Minitower(new Vector2(this.position.X + 20 * (i + 1), this.position.Y + 560)));
            accessoryList.Add(new Boss2_Bigtower(new Vector2(this.position.X + size.X / 2 - 50, this.position.Y + 410)));
        }
    }
}
