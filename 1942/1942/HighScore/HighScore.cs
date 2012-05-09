using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _1942
{
    class HighScore
    {
        //private string test = "1";
        private string _path = @"./HighScore/HighScoreList.xml";
        XmlDocument doc;

        //Implementing of Icomparer sorting it in ascending order
        SortHighScore_Ascending sAscend = new SortHighScore_Ascending();

        //The list that holds the scores from the xml, before we add the new score.
        List<ScoreObj> list = new List<ScoreObj>();

        public HighScore()
        {
            DataAccess();      
        }

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

            DeleteHighScore("1");

            for (int i = 0; i < list.Count; i++)
            {
                WriteHighScore(list[i].PlayerName, list[i].PlayerScore, i+1);
            }
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
        private void RetreiveHighScore()
        {
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
        private void DeleteHighScore(string placement)
        {
            XmlNode root = doc.SelectSingleNode("//highscorelist");
            XmlNodeList nodes = root.SelectNodes("highscore");

            root.RemoveAll();
            doc.Save(_path);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < list.Count ; i++)
            {
            spriteBatch.DrawString(FontLibrary.debug, " " + list[i].PlayerPlacement + " " + list[i].PlayerName + " " + list[i].PlayerScore, new Vector2(1f, Settings.window.ClientBounds.Height - (FontLibrary.debug.LineSpacing * 10 + i)), Color.Red);
            }
        }  
    }
}
