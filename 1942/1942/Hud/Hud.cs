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
    class Hud
    {
      
        public Hud()
        {
 
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
            spritebatch.DrawString(FontLibrary.Hud_Font, gametime.TotalGameTime.Minutes.ToString() + ":" + gametime.TotalGameTime.Seconds.ToString(), new Vector2(Settings.window.ClientBounds.Width / 2 - (FontLibrary.Hud_Font.MeasureString(gametime.TotalGameTime.Minutes.ToString() + ":" + gametime.ElapsedGameTime.TotalSeconds.ToString()).X / 2), 0), Color.BlanchedAlmond);
            for (int i = 0; i < Objects.playerList.Count; i++)
            {
                spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[0].Health.ToString() + "%", new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString(Objects.playerList[0].Health.ToString() + "%").X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing), Objects.playerList[0].Color);
                spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[0].MyScore.ToString(), new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString(Objects.playerList[0].MyScore.ToString()).X, 0), Objects.playerList[0].Color);
                if (Objects.playerList[0].PowerUpDamage == true || Objects.playerList[0].PowerUpHealth == true || Objects.playerList[0].PowerUpShield == true)
                {
                    if (Objects.playerList[0].PowerUpDamage == true)
                    {
                        spritebatch.DrawString(FontLibrary.Hud_Font, Math.Round(Objects.playerList[0].TimeLeftOnDamagePowerUp).ToString(), new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString(Math.Round(Objects.playerList[0].TimeLeftOnDamagePowerUp).ToString()).X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 2), Objects.playerList[0].Color);
                        spritebatch.DrawString(FontLibrary.Hud_Font, "Double Damage", new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString("Double Damage").X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 3), Objects.playerList[0].Color);
                    }
                    if (Objects.playerList[0].PowerUpShield == true)
                    {
                        spritebatch.DrawString(FontLibrary.Hud_Font, Math.Round(Objects.playerList[0].TimeLeftOnArmorPowerUp).ToString(), new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString(Math.Round(Objects.playerList[1].TimeLeftOnArmorPowerUp).ToString()).X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 4), Objects.playerList[0].Color);
                        spritebatch.DrawString(FontLibrary.Hud_Font, "Armor", new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString("Armor").X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 5), Objects.playerList[0].Color);
                    }
                }
                if (Objects.playerList.Count >= 2)
                {
                    spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[1].Health.ToString() + "%", new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing), Objects.playerList[1].Color);
                    spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[1].MyScore.ToString(), new Vector2(1f, 0), Objects.playerList[1].Color);
                    if (Objects.playerList[1].PowerUpDamage == true || Objects.playerList[1].PowerUpShield == true)
                    {
                        if (Objects.playerList[1].PowerUpDamage == true)
                        {
                            spritebatch.DrawString(FontLibrary.Hud_Font, Math.Round(Objects.playerList[1].TimeLeftOnDamagePowerUp).ToString(), new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 2), Objects.playerList[1].Color);
                            spritebatch.DrawString(FontLibrary.Hud_Font, "Double Damage", new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 3), Objects.playerList[1].Color);
                        }
                        if (Objects.playerList[1].PowerUpShield == true)
                        {
                            spritebatch.DrawString(FontLibrary.Hud_Font, "Armor", new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 5), Objects.playerList[1].Color);
                            spritebatch.DrawString(FontLibrary.Hud_Font, Math.Round(Objects.playerList[1].TimeLeftOnArmorPowerUp).ToString(), new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 4), Objects.playerList[1].Color);
                        }
                    }
                }
            }
        }
    }
}
