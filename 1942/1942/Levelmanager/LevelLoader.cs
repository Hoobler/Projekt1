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
        private int nrOfTilesShown = 0;
        private int nrOfRows = 0;
        private const int topMargin = 0;
        private static int tilesize = 80;
        private string levelName = string.Empty;
        private string nextLevel = string.Empty;
        private string description = string.Empty;
        private Vector2 cameraPosition = new Vector2(0, 0);

        //Spara in alla texturer där dom stämmer övernes symbolen man får från mapList så att rätt texture ritas ut.
        Dictionary<char, TileTexture> textureDictionary = new Dictionary<char, TileTexture>();
        //List för all tiles i "mapen" den håller x,y pos och vilken symbol som ska vara på den posen.
        List<Tile> mapList = new List<Tile>();
        //List for all the spawns that should happen in the map position X/Y, formation, spawn time
        List<LevelSpawnObj> mapSpawnList = new List<LevelSpawnObj>();

        public LevelLoader(string TheLevelFile, ContentManager content)
        {
            LoadLevelFile(TheLevelFile, content);
        }

        private void UnLoadLevel()
        {
            mapList.Clear();
            textureDictionary.Clear();
            mapSpawnList.Clear();
        }

        /// <summary>
        /// The method used for loading a level, this method also calls the UnLoadLevel metod that clears all the lists prior to loading in the new level
        /// </summary>
        /// <param name="levelName">The name of the level to be loaded</param>
        /// <param name="content"></param>
        public void LoadMap(string levelName, ContentManager content)
        {
            LoadLevelFile(levelName, content);
        }

        private void LoadLevelFile(string LevelFile, ContentManager content)
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

        private void LoadTile(string texturefile, ContentManager content)
        {
            XmlReader texReader = XmlReader.Create("./Levels/" + texturefile + ".xml");

            string aCurrentElement = string.Empty;
            char tempSymbol = 'W';

            //SpriteEffect vars used for fliping the tiles
            bool hFlip = false;
            bool vFlip = false;

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

                    else if (aCurrentElement == "hFlip")
                    {
                        hFlip = texReader.ReadElementContentAsBoolean();
                    }

                    else if (aCurrentElement == "vFlip")
                    {
                        vFlip = texReader.ReadElementContentAsBoolean();
                    }

                    else if (aCurrentElement == "texture")
                    {
                        LoadTexture(texReader, content, tempSymbol, hFlip, vFlip);
                    }                    
                }
            }
        }

        private void LoadTexture(XmlReader reader, ContentManager content, char tempChar, bool hFlip, bool vFlip)
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
                        textureDictionary.Add(tempChar, new TileTexture(content.Load<Texture2D>(assetName), tempChar, hFlip, vFlip));
                    }
                }
            }
        }

        private void LoadLevel(XmlReader reader, ContentManager content)
        {
            string aCurrentElement = string.Empty;

            int aPositionX = 0;
            int aPositionY = 0;

            //vars for the spawning of objects
            int aSpawnPosX = 0;
            int aSpawnPosY = 0;
            string formation = string.Empty;
            bool mirrored = false;
             
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.EndElement &&
                    reader.Name.Equals("level", StringComparison.OrdinalIgnoreCase))
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
                        case "nextlevel":
                            {
                                nextLevel = reader.ReadElementContentAsString();
                                break;
                            }
                        case "description":
                            {
                                description = reader.ReadElementContentAsString();
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
                    else if (aCurrentElement == "positionX")
                    {
                        aSpawnPosX = reader.ReadContentAsInt();
                        aSpawnPosX *= TileSize();
                    }
                    else if (aCurrentElement == "positionY")
                    {
                        aSpawnPosY = reader.ReadContentAsInt();
                        aSpawnPosY *= TileSize();
                        aSpawnPosY = -aSpawnPosY;
                    }
                    else if (aCurrentElement == "formation")
                    {
                        formation = reader.Value;
                    }
                    else if (aCurrentElement == "mirrored")
                    {
                        mirrored = reader.ReadContentAsBoolean();
                        mapSpawnList.Add(new LevelSpawnObj(new Vector2(aSpawnPosX, aSpawnPosY), formation, mirrored));
                    }
                    
                    
                }
            }
            StartingCameraPos();
        }

        public List<LevelSpawnObj> MapSpawnList
        {
            get { return mapSpawnList; }
        }

        public String LevelName
        {
            get { return levelName; }
        }

        public String NextLevel
        {
            get { return NextLevel; }
        }

        public String Description
        {
            get { return Description; }
        }

        public int TilesOnScreen
        {
            get { return nrOfTilesShown; }
        }

        public int TileSize()
        {
            var tempSize = Settings.window.ClientBounds.Width;
            return tilesize = tempSize / 10; 
        }

        private int StartingCameraPos()
        {
            cameraPosition.Y = (float)nrOfRows * TileSize() - Settings.window.ClientBounds.Height;
            var tempInt = nrOfRows * TileSize() - Settings.window.ClientBounds.Height;
            return tempInt;
        }

        public void MoveCamera(float moved)
        {
            cameraPosition.Y -= moved;
            //for (int i = 0; i < Objects.bossList.Count; i++)
            //{
            //    while (Objects.bossList[i].IsActivated)
            //    {
            //        if (cameraPosition.Y < 6 * TileSize())
            //        {
            //            cameraPosition.Y = 12 * TileSize();
            //        }
            //    }
            //}
            if (cameraPosition.Y < topMargin)
                cameraPosition.Y = StartingCameraPos();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            nrOfTilesShown = 0;
            for (int i = 0; i < mapList.Count; i++)
            {
                int left = (int)mapList[i].Position.X * TileSize();
                int top = (int)mapList[i].Position.Y * TileSize() - (int)cameraPosition.Y;

                if (top >= -TileSize() && top < TileSize()*7)
                {
                    spritebatch.Draw(textureDictionary[mapList[i].Symbol].Texture,
                        new Rectangle(left, top, TileSize(), TileSize()),
                        new Rectangle(0, 0, textureDictionary[mapList[i].Symbol].Texture.Bounds.Width, textureDictionary[mapList[i].Symbol].Texture.Bounds.Height),
                        Color.White,
                        0,
                        new Vector2(0, 0),
                        textureDictionary[mapList[i].Symbol].SpriteEffect,
                        1f);
                    nrOfTilesShown++;
                }
            }
        }
    }
}   
