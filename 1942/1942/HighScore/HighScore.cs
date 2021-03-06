﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    class HighScore
    {

        #region Variables

        private bool textDraw = false;
        private bool levelPromt = false;

        private string playerName;
        private string currentPlayer;
        private string nextLevelText;

        private KeyboardState keyState;
        private KeyboardState oldKeyState;

        private string _path = String.Empty;
        XmlDocument doc;

        //Implementing of Icomparer sorting it in ascending order
        SortHighScore_Ascending sAscend = new SortHighScore_Ascending();

        //The list that holds the scores from the xml, before we add the new score.
        List<ScoreObj> list = new List<ScoreObj>();

        #endregion

        public HighScore(string ListFile)
        {
            _path = @"./HighScore/HighScore" + ListFile + ".xml";
            DataAccess();
            RetreiveHighScore();
        }

        #region HighScoreMethods
        private void DataAccess()
        {
            doc = new XmlDocument();
            doc.Load(_path);
        }

        private void SortHighScore()
        {
            list.Sort(sAscend);
        }

        public void AddHighScore(string name, int score)
        
        {
            RetreiveHighScore();
            list.Add(new ScoreObj(name, score, 0));
            SortHighScore();

            if (list.Count >= 10)
            {
                int remove = list.Count - 10;
                list.RemoveRange(10, remove);
            }

            DeleteHighScore();

            for (int i = 0; i < list.Count; i++)
            {
                WriteHighScore(list[i].PlayerName, list[i].PlayerScore, i+1);
            }
        }

        private Vector2 TextLenght(string String)
        {
            Vector2 tempVector;
            tempVector = FontLibrary.highscore_font.MeasureString(String);

            return tempVector;
        }

        /// <summary>
        /// Takes the inputed highscore and name of the player and add it's to the high scorelist
        /// </summary>
        private void WriteHighScore(string _name, int _score, int _placement)
        {
            XmlNode newHighScore = doc.CreateElement("highscore");

            XmlNode name = doc.CreateElement("Name");
            XmlNode score = doc.CreateElement("Score");
            XmlNode placement = doc.CreateElement("Placement");

            name.InnerText = _name;
            score.InnerText = _score.ToString();
            placement.InnerText = _placement.ToString();

            newHighScore.AppendChild(placement);
            newHighScore.AppendChild(name);
            newHighScore.AppendChild(score);

            doc.SelectSingleNode("//highscorelist").AppendChild(newHighScore);
            doc.Save(_path);
        }

        /// <summary>
        /// Retreives the Highscore list from the .xml file that contains the top 10 high scores
        /// </summary>
        /// <returns></returns>
        public void RetreiveHighScore()
        {
            list.Clear();
            XmlNode root = doc.SelectSingleNode("//highscorelist");
            XmlNodeList nodeList = root.SelectNodes("highscore");

            foreach (XmlNode n in nodeList)
            {
                ScoreObj c = new ScoreObj();
                c.PlayerName = n.SelectSingleNode("Name").InnerText;
                c.PlayerScore = int.Parse(n.SelectSingleNode("Score").InnerText);
                c.PlayerPlacement = int.Parse(n.SelectSingleNode("Placement").InnerText);

                list.Add(c);
            }
        }

        /// <summary>
        /// Used to delete a highscore based on it's placement in the highscore list
        /// </summary>
        /// <param name="placement"></param>
        private void DeleteHighScore()
        {
            XmlNode root = doc.SelectSingleNode("//highscorelist");
            XmlNodeList nodes = root.SelectNodes("highscore");

            root.RemoveAll();
            doc.Save(_path);
        }

        #endregion

        #region HighScoreProperties

        public string SetPlayerName
        {
            set { playerName = value; }
        }

        public string SetCurrentPlayer
        {
            set { currentPlayer = value; }
        }

        public string NextLevelText
        {
            set { nextLevelText = value; }
        }

        public bool NextLevelPrompt
        {
            set { levelPromt = value; }
        }

        public bool DrawText
        {
            set { textDraw = value; }
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();

            playerName = KeyBoardInput.TextInput(5, false);

            if (oldKeyState.IsKeyUp(Keys.Enter))
            {
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    AddHighScore(playerName, 5);
                }
            }
            RetreiveHighScore();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textcenter = new Vector2(Settings.windowBounds.X / 2, 50);

            if (textDraw)
            {
                spriteBatch.DrawString(FontLibrary.highscore_font, "HIGH SCORE", textcenter - ((TextLenght("HIGH SCORE") / 2) * 0.75f), Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0);
                spriteBatch.DrawString(FontLibrary.highscore_font, "" + currentPlayer.ToUpper(), new Vector2(0, 200), Color.White, 0, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0);
                //spriteBatch.DrawString(FontLibrary.highscore_font, "" + currentPlayer.ToUpper(), new Vector2(1f, (FontLibrary.debug.LineSpacing * 10)), Color.Red);
                spriteBatch.DrawString(FontLibrary.highscore_font, "" + playerName.ToUpper(), new Vector2(0, 250), Color.White, 0, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0);
                //spriteBatch.DrawString(FontLibrary.highscore_font, "" + playerName.ToUpper(), new Vector2(1f, (FontLibrary.debug.LineSpacing * 11)), Color.Red);
            }
            if (levelPromt)
            {
                spriteBatch.DrawString(FontLibrary.highscore_font, "" + nextLevelText.ToUpper(), textcenter - ((TextLenght(nextLevelText.ToUpper()) / 2) * 0.30f), Color.White, 0, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0);
                //spriteBatch.DrawString(FontLibrary.debug, "" + nextLevelText, new Vector2(1f, (FontLibrary.debug.LineSpacing * 11)), Color.Red);
            }
            for (int i = 0; i < list.Count ; i++)
            {
                spriteBatch.DrawString(FontLibrary.highscore_font, "" + list[i].PlayerPlacement.ToString().ToUpper() + " " + list[i].PlayerName.ToUpper() + " " + list[i].PlayerScore.ToString().ToUpper(), new Vector2(1000, 300 + (FontLibrary.testfont.LineSpacing * 1 * i)) * 0.30f, Color.White, 0f, new Vector2(0,0), 0.30f, SpriteEffects.None, 0);
            }
        }  
    }
}
