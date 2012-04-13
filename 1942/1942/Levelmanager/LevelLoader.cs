using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    class LevelLoader
    {
        private int nrOfRows = 0;
        private const int topMargin = 0;
        private static int tilesize = 80;
        private string levelName = string.Empty;
        private string description = string.Empty;
        private Vector2 cameraPosition = new Vector2(0, 0);

        //Spara in alla texturer där dom stämmer övernes symbolen man får från mapList så att rätt texture ritas ut.
        Dictionary<char, TileTexture> textureDictionary = new Dictionary<char, TileTexture>();
        //List för all tiles i "mapen" den håller x,y pos och vilken symbol som ska vara på den posen.
        List<Tile> mapList = new List<Tile>();

        public LevelLoader(string TheLevelFile, ContentManager content)
        {
            LoadLevelFile(TheLevelFile, content);
        }

        public void UnLoadLevel()
        {
            textureDictionary.Clear();
        }

        public void LoadLevelFile(string LevelFile, ContentManager content)
        {
            XmlReader reader = XmlReader.Create(LevelFile);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "tileset":
                            {
                                var texfilelocation = reader.ReadElementContentAsString();
                                LoadTile(texfilelocation, content);
                                break;
                            }
                        case "level":
                            {
                                LoadLevel(reader, content);
                                break;
                            }
                    }
                }
            }
        }

        public void LoadTile(string texturefile, ContentManager content)
        {
            XmlReader texReader = XmlReader.Create("./Levels/" + texturefile + ".xml");

            string aCurrentElement = string.Empty;
            char tempSymbol = 'A';

            while (texReader.Read())
            {
                if (texReader.NodeType == XmlNodeType.EndElement &&
                    texReader.Name.Equals("tiletexture", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                else if (texReader.NodeType == XmlNodeType.Element)
                {
                    aCurrentElement = texReader.Name;

                    if (aCurrentElement == "symbol")
                    {
                        tempSymbol = texReader.ReadElementContentAsString().ToCharArray()[0];
                    }
                    else if (aCurrentElement == "texture")
                    {
                        LoadTexture(texReader, content, tempSymbol);
                    }                    
                }
            }
        }

        public void LoadTexture(XmlReader reader, ContentManager content, char tempChar)
        {
            string aCurrentElement = string.Empty;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement &&
                    reader.Name.Equals("tiletexture", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                else if (reader.NodeType == XmlNodeType.Element)
                {
                    aCurrentElement = reader.Name;

                    if (aCurrentElement == "name")
                    {
                        var assetName = reader.ReadElementContentAsString();
                        textureDictionary.Add(tempChar, new TileTexture(content.Load<Texture2D>(assetName), tempChar));
                    }
                }
            }
        }

        public void LoadLevel(XmlReader reader, ContentManager content)
        {
            int aPositionX = 0;
            int aPositionY = 0;

            string aCurrentElement = string.Empty;
             
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.EndElement &&
                    reader.Name.Equals("tiletexture", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                else if (reader.NodeType == XmlNodeType.Element)
                {
                    aCurrentElement = reader.Name;

                    switch (aCurrentElement)
                    {
                        case "name":
                            {
                                levelName = reader.ReadElementContentAsString();
                                break;
                            }
                        case "tileset":
                            {
                                var texfilelocation = reader.ReadElementContentAsString();
                                LoadTile(texfilelocation, content);
                                break;
                            }
                    }

                }

                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (aCurrentElement == "row")
                    {
                        aPositionY += 1;
                        aPositionX = 0;
                    }
                }

                else if (reader.NodeType == XmlNodeType.Text)
                {
                    if (aCurrentElement == "row")
                    {
                        string aRow = reader.Value;
                        nrOfRows += 1;

                        for (int counter = 0; counter < aRow.Length; counter++)
                        {                          
                            var tempChar = aRow[counter].ToString().ToCharArray()[0];

                            mapList.Add(new Tile(new Vector2(aPositionX, aPositionY), tempChar));
                            aPositionX += 1;
                        }
                    }
                }                 
            }
            StartingCameraPos();
        }

        public int TileSize()
        {
            var tempSize = Settings.window.ClientBounds.Width;
            return tilesize = tempSize / 10; 
        }

        public int StartingCameraPos()
        {
            cameraPosition.Y = (float)nrOfRows * TileSize() - Settings.window.ClientBounds.Height;
            var tempInt = nrOfRows * TileSize() - Settings.window.ClientBounds.Height;
            return tempInt;
        }

        public void MoveCamera(float moved)
        {
            cameraPosition.Y -= moved;
            if (cameraPosition.Y < topMargin)
                cameraPosition.Y = StartingCameraPos();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Tile tile in mapList)
            {
                int left = (int)tile.Position.X * TileSize();
                int top = (int)tile.Position.Y * TileSize() - (int)cameraPosition.Y;
                spritebatch.Draw(textureDictionary[tile.Symbol].Texture, new Rectangle(left, top, TileSize(), TileSize()), Color.White);
            }
        }
    }
}   
