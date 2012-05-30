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
        #region Variables

        private int nrOfTilesShown = 0;
        private int nrOfRows = 0;
        private const int topMargin = 0;
        private static int tilesize = 0;
        private string levelName = string.Empty;
        private string nextLevel = string.Empty;
        private string description = string.Empty;
        public Vector2 cameraPosition = new Vector2(0, 0);
        private Vector2 bossCameraPosition = new Vector2(0, 0);
        private bool scoreLoop;
        private bool endLevel;

        //Spara in alla texturer där dom stämmer övernes symbolen man får från mapList så att rätt texture ritas ut.
        Dictionary<char, TileTexture> texDictionary = new Dictionary<char, TileTexture>();
        //List för all tiles i "mapen" den håller x,y pos och vilken symbol som ska vara på den posen.
        List<Tile> mapList = new List<Tile>();
        //List for all the spawns that should happen in the map position X/Y, formation, spawn time
        List<LevelSpawnObj> mapSpawnList = new List<LevelSpawnObj>();

        #endregion

        public LevelLoader(string TheLevelFile, ContentManager content)
        {
            LoadLevelFile(TheLevelFile, content);
            TileSize();
        }

        #region LoadingMethods

        private void UnLoadLevel()
        {
            mapList.Clear();
            texDictionary.Clear();
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
            XmlReader reader = XmlReader.Create("./Levels/" + LevelFile + ".xml");

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
            //How many frames of animation there is in the sprite
            int frames = 0;
            //Is the sprite animated
            bool animated = false;

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

                    else if (aCurrentElement == "animated")
                    {
                        animated = texReader.ReadElementContentAsBoolean();
                    }
                    else if (aCurrentElement == "frames")
                    {
                        frames = texReader.ReadElementContentAsInt();
                    }
                    else if (aCurrentElement == "texture")
                    {
                        LoadTexture(texReader, content, tempSymbol, hFlip, vFlip, animated, frames);
                    }                    
                }
            }
        }

        private void LoadTexture(XmlReader reader, ContentManager content, char tempChar, bool hFlip, bool vFlip , bool animated, int frames)
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
                        texDictionary.Add(tempChar, new TileTexture(content.Load<Texture2D>(assetName), tempChar, hFlip, vFlip , animated , frames));
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
            float aSpawnPosX = 0;
            float aSpawnPosY = 0;
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
                        aSpawnPosX = reader.ReadContentAsFloat();
                        aSpawnPosX *= tilesize;
                    }
                    else if (aCurrentElement == "positionY")
                    {
                        aSpawnPosY = reader.ReadContentAsFloat();
                        aSpawnPosY *= tilesize;
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

        #endregion

        #region HelperMethods

        public int TileSize()
        {
            var tempSize = Settings.windowBounds.X;
            tilesize = (int)tempSize / 10;
            return tilesize;
        }

        private int StartingCameraPos()
        {
            cameraPosition.Y = (float)nrOfRows * tilesize - Settings.windowBounds.Y;
            var tempInt = nrOfRows * tilesize - Settings.windowBounds.Y;
            return (int)tempInt;
        }

        public void MoveCamera(float moved)
        {
            cameraPosition.Y -= moved;
            
            for (int i = 0; i < Objects.bossList.Count; i++)
            {
                if (!Objects.bossList[i].IsActivated())
                    bossCameraPosition = cameraPosition;
                if (Objects.bossList[i].IsActivated())
                {
                    if (bossCameraPosition.Y > cameraPosition.Y)
                    {
                        cameraPosition.Y = cameraPosition.Y + 8 * tilesize;
                    }
                }
            }
            if (cameraPosition.Y < 2 * tilesize && scoreLoop && !endLevel)
            {
                cameraPosition.Y = cameraPosition.Y + 7 * tilesize;
            }
        }

        public bool HighScoreScreen()
        {
            if (cameraPosition.Y < 1 * tilesize)
                return true;
            else
                return false;
        }

        public bool LevelHasEnded()
        {
            if (cameraPosition.Y < topMargin)
                return true;
            else
                return false;
        }

        #endregion 

        #region Properties

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

        public bool ScoreLoop
        {
            get { return scoreLoop; }
            set { scoreLoop = value; }
        }

        public bool EndLevel
        {
            get { return endLevel; }
            set { endLevel = value; }
        }

        public int TileSizeInt
        {
            get { return tilesize; }
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (var Tile in texDictionary)
            {
                Tile.Value.AnimationFrame();
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            nrOfTilesShown = 0;
            for (int i = 0; i < mapList.Count; i++)
            {
                int left = (int)mapList[i].Position.X * tilesize;
                int top = (int)mapList[i].Position.Y * tilesize - (int)cameraPosition.Y;

                if (top >= -tilesize && top < tilesize * 7)
                {
                    if (texDictionary[mapList[i].Symbol].Animated)
                    {
                        spritebatch.Draw(
                        texDictionary[mapList[i].Symbol].Tex, //Texture
                        new Rectangle(left, top, tilesize, tilesize), //Size of the tile rectangle
                        new Rectangle((texDictionary[mapList[i].Symbol].CFrame * (texDictionary[mapList[i].Symbol].Tex.Bounds.Width -1 ) / texDictionary[mapList[i].Symbol].TFrame) +1,
                            (0 *(texDictionary[mapList[i].Symbol].Tex.Bounds.Width -1)) + 1 ,
                            ((texDictionary[mapList[i].Symbol].Tex.Bounds.Width - 1) / texDictionary[mapList[i].Symbol].TFrame) - 1,
                            ((texDictionary[mapList[i].Symbol].Tex.Bounds.Height - 1)) - 1),
                        Color.White, //Color of the tile
                        0,
                        new Vector2(0, 0),
                        texDictionary[mapList[i].Symbol].SpriteEffect,
                        1f);
                    }
                    else
                    {
                        spritebatch.Draw(texDictionary[mapList[i].Symbol].Tex,
                            new Rectangle(left, top, tilesize, tilesize),
                            new Rectangle(1, 1, texDictionary[mapList[i].Symbol].Tex.Bounds.Width -2, texDictionary[mapList[i].Symbol].Tex.Bounds.Height -2),
                            Color.White,
                            0,
                            new Vector2(0, 0),
                            texDictionary[mapList[i].Symbol].SpriteEffect,
                            1f);
                    }
                    nrOfTilesShown++;
                }
            }
        }
    }
}   
