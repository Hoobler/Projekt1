using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss2 : Boss_Base
    {
        int phase;
        float timer;

        public Boss2(Vector2 position)
        {

            texture = Texture2DLibrary.boss2;
            size = new Point(Texture2DLibrary.boss2.Bounds.Width, Texture2DLibrary.boss2.Bounds.Height);
            this.position.Y = position.Y;
            
            this.position.X = Settings.window.ClientBounds.Width / 2f - size.X / 2f;
            
            
            color = Color.White;
            maxHealth = 1;
            health = maxHealth;
            score = 2000;

            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (accessoryList.Count <= 0 && activated)
                killed = true;
            if(!dead && activated)
                for (int i = 0; i < Objects.playerList.Count; i++)
                {
                    if (Objects.playerList[i].PosY <= Settings.window.ClientBounds.Height / 2)
                        Objects.playerList[i].PosY += 5f;
                }

            if (phase >= 2 && !killed)
            {
                timer++;
                if (timer == 300)
                    Objects.powerUpList.Add(new PowerUpDamage(new Vector2(100, -50)));
                if (timer == 600)
                    Objects.powerUpList.Add(new PowerUpHealth(new Vector2(400, -50)));
                if (timer == 900)
                    Objects.powerUpList.Add(new PowerUpHealth(new Vector2(400, -50)));
                if (timer >= 900)
                    timer = 0;
            }

            if (activated)
            {
                
                speed = new Vector2(0, 0.5f);
                phase = 1;
            }

            if (phase == 1)
            {
                MusicManager.SetMusic(SoundLibrary.Boss1);
            }
            if (position.Y >= -size.Y / 2f && !killed)
            {
                
                phase = 2;
                speed = new Vector2(0, 0);
                position.Y = -size.Y / 2f;
                for (int i = 0; i < accessoryList.Count; i++)
                    if (!accessoryList[i].ReallyActivated)
                        accessoryList[i].ReallyActivated = true;
            }

            if (killed)
                phase = 2;

            

        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (phase == 1)
                spriteBatch.DrawString(FontLibrary.Hud_Font, "SHOOT EVERYTHING\nAND DODGE THE BARRAGE", new Vector2(200, 200), Color.Red);
        }

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss2_Wall(new Vector2(Center.X, this.position.Y + 581)));
            accessoryList.Add(new Boss2_Wall(new Vector2(Center.X, this.position.Y + 459)));

            for (int i = 0; i < 4; i++)
                accessoryList.Add(new Boss2_Minitower(new Vector2(this.position.X + 30 * (i + 1)+10, this.position.Y + 530)));
            accessoryList.Add(new Boss2_Bigtower(new Vector2(Center.X, this.position.Y + 350)));
        }
    }
}
