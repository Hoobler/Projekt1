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
        private const int tileWidht = 50;
        private const int tileHight = 50;

        private const int topMargin = 0;

        private int tilesize = 60;
        private string levelName = string.Empty;


        Vector2 cameraPosition = new Vector2(0, 14 * tileHight);

        List<TileTexture> textureList = new List<TileTexture>();

        char[,] map2 = new char [11,20];

        public LevelLoader(string TheLevelFile, ContentManager content)
        {
            LoadLevelFile(TheLevelFile, content);
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
                        textureList.Add(new TileTexture(content.Load<Texture2D>(assetName), tempChar));                       
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

                        for (int counter = 0; counter < aRow.Length; counter++)
                        {
                            var tempNr = aRow[counter].ToString().ToCharArray()[0];
                            map2[aPositionX, aPositionY] = tempNr;

                            aPositionX += 1;
                        }
                    }
                }
                    
            }
        }

        public int Width
        {
            get { return map2.GetLength(0); }
        }

        public int Hight
        {
            get { return map2.GetLength(1); }
        }

        public void MoveCamera(float moved)
        {
            cameraPosition.Y -= moved;

            if (cameraPosition.Y < topMargin)
                cameraPosition.Y = topMargin;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Texture2D texture = null; 
            for(int x = 0; x < Width; x++)
                for (int y = 0; y < Hight; y++)
                {
                    var left = x * tilesize;
                    var top = y * tilesize - (int)cameraPosition.Y;

                    var textureIndex = (char)map2[x,y];
                    //if (textureIndex == -1) continue;

                    foreach (TileTexture aTexture in textureList)
                    {
                        if (aTexture.Symbol == textureIndex)
                        {
                            texture = aTexture.Texture;
                        }
                    }

                    spritebatch.Draw(texture, new Rectangle(left, top, tilesize, tilesize), Color.White);
                }
        }
    }
}   
