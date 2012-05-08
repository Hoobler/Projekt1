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

        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < Objects.playerList.Count; i++)
            {
                spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[0].Health.ToString() + "%", new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing), Objects.playerList[0].Color);
                spritebatch.DrawString(FontLibrary.Hud_Font, "Insert Score", new Vector2(1f, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 2), Objects.playerList[0].Color);
                if (Settings.nr_of_players >= 2)
                {
                    spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[1].Health.ToString() + "%", new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString(Objects.playerList[1].Health.ToString() + "%").X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing), Objects.playerList[1].Color);
                    spritebatch.DrawString(FontLibrary.Hud_Font, "Insert Score", new Vector2(Settings.window.ClientBounds.Width - FontLibrary.Hud_Font.MeasureString("Insert Score").X, Settings.window.ClientBounds.Height - FontLibrary.Hud_Font.LineSpacing * 2), Objects.playerList[1].Color);
                }
                if (Settings.nr_of_players >= 3)
                {
                    spritebatch.DrawString(FontLibrary.Hud_Font, Objects.playerList[2].Health.ToString() + "%", new Vector2(1f, 1f), Objects.playerList[2].Color);
                    spritebatch.DrawString(FontLibrary.Hud_Font, "Insert Score", new Vector2(1f, 1f + FontLibrary.Hud_Font.MeasureString("Insert Score").X), Objects.playerList[2].Color);
                }
            }
        }
    }
}
