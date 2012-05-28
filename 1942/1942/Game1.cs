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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameStates { MainMenu, AudioScreen, VideoScreen, ControlScreen, Playing, GameOver };
        GameStates gameState = GameStates.MainMenu;

        Logic logic;
        MenuManager menu;
        bool debugText;
        bool paused;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2DLibrary.spaceship = Content.Load<Texture2D>(@"Enemies/spaceship");
            Texture2DLibrary.arrow = Content.Load<Texture2D>(@"Extra/arrow");
            Texture2DLibrary.kamikaze = Content.Load<Texture2D>(@"Kamikaze");

            Texture2DLibrary.escort = Content.Load<Texture2D>(@"Enemies/spaceship");
            Texture2DLibrary.escort_lifebar = Content.Load<Texture2D>(@"Extra/square1");

            Texture2DLibrary.player = Content.Load<Texture2D>(@"Enemies/Player");
            Texture2DLibrary.projectile_player = Content.Load<Texture2D>(@"Extra/square1");
            Texture2DLibrary.enemy_zero = Content.Load<Texture2D>(@"Enemies/Zero");
            Texture2DLibrary.projectile_enemy_zero = Content.Load<Texture2D>(@"Extra/square1");
            Texture2DLibrary.enemy_zeke = Content.Load<Texture2D>(@"Enemies/Zeke");
            Texture2DLibrary.projectile_enemy_zeke = Content.Load<Texture2D>(@"Extra/square1");
            Texture2DLibrary.enemy_todjo = Content.Load<Texture2D>(@"Enemies/Todjo");
            Texture2DLibrary.projectile_enemy_todjo = Content.Load<Texture2D>(@"Extra/square1");
            Texture2DLibrary.enemy_boat = Content.Load<Texture2D>(@"Boat");
            Texture2DLibrary.enemy_boat_tower = Content.Load<Texture2D>(@"Enemies/Boat_Tower");
            Texture2DLibrary.enemy_boat_tower_projectile = Content.Load<Texture2D>(@"Extra/square1");

            Texture2DLibrary.enemy_heavy = Content.Load<Texture2D>(@"Enemies/spaceship");
            Texture2DLibrary.projectile_enemy_heavy = Content.Load<Texture2D>(@"Enemies/Spaceship");

            Texture2DLibrary.enemy_tower = Content.Load<Texture2D>(@"Enemies/AATower");
            Texture2DLibrary.projectile_enemy_tower = Content.Load<Texture2D>(@"Extra/square1");
            Texture2DLibrary.enemy_tower_base = Content.Load<Texture2D>(@"Enemies/AABase");
            Texture2DLibrary.enemy_tower_dead = Content.Load<Texture2D>(@"Enemies/AABaseDead");

            Texture2DLibrary.boss1 = Content.Load<Texture2D>(@"Bosses/Boss1/Boss1");
            Texture2DLibrary.boss1_gun = Content.Load<Texture2D>(@"Bosses/Boss1/Boss1_Gun");
            Texture2DLibrary.boss1_projectile = Content.Load<Texture2D>(@"Extra/square1");

            Texture2DLibrary.boss2 = Content.Load<Texture2D>(@"Bosses/Boss2/Boss2");
            Texture2DLibrary.boss2_bigshot = Content.Load<Texture2D>(@"Bosses/Boss2/BigBomb");
            Texture2DLibrary.boss2_splitterbomb = Content.Load<Texture2D>(@"Bosses/Boss2/SplitterBombs");
            Texture2DLibrary.boss2_biggun = Content.Load<Texture2D>(@"Bosses/Boss2/Boss2_BigGun");
            Texture2DLibrary.boss2_smallgun = Content.Load<Texture2D>(@"Bosses/Boss2/Boss2_SmallGun");
            Texture2DLibrary.boss2_wall = Content.Load<Texture2D>(@"Bosses/Boss2/Boss2_Wall");

            Texture2DLibrary.boss3 = Content.Load<Texture2D>(@"Bosses/Boss3/Boss3");
            Texture2DLibrary.boss3_gun = Content.Load<Texture2D>(@"Bosses/Boss3/Boss3_Gun");

            Texture2DLibrary.boss5 = Content.Load<Texture2D>(@"Enemies/spaceship");
            //menu
            Texture2DLibrary.texture_MainMenu = Content.Load<Texture2D>(@"Menu/MainMenu");
            Texture2DLibrary.texture_OptionsButton = Content.Load<Texture2D>(@"Menu/Options");
            Texture2DLibrary.texture_StartGameButton = Content.Load<Texture2D>(@"Menu/StartGame");
            Texture2DLibrary.texture_ExitGameButton = Content.Load<Texture2D>(@"Menu/ExitGame");
            Texture2DLibrary.texture_ExitGameButtonShadow = Content.Load<Texture2D>(@"Menu/ExitGame_Shadow");
            Texture2DLibrary.texture_OptionsButtonShadow = Content.Load<Texture2D>(@"Menu/Options_Shadow");
            Texture2DLibrary.texture_StartGameButtonShadow = Content.Load<Texture2D>(@"Menu/StartGame_Shadow");
            Texture2DLibrary.texture_AdjustVideo = Content.Load<Texture2D>(@"Menu/AdjustVideo"); 
            Texture2DLibrary.texture_AdjustVolume = Content.Load<Texture2D>(@"Menu/AdjustVolume"); 
            Texture2DLibrary.texture_AudioOptions = Content.Load<Texture2D>(@"Menu/AudioOptions"); 
            Texture2DLibrary.texture_AudioOptions_Shadow = Content.Load<Texture2D>(@"Menu/AudioOptions_Shadow"); 
            Texture2DLibrary.texture_Back = Content.Load<Texture2D>(@"Menu/Back"); 
            Texture2DLibrary.texture_Back_Shadow = Content.Load<Texture2D>(@"Menu/Back_Shadow"); 
            Texture2DLibrary.texture_Control_Options = Content.Load<Texture2D>(@"Menu/ControlOptions"); 
            Texture2DLibrary.texture_Control_Options_Shadow = Content.Load<Texture2D>(@"Menu/ControlOptions_Shadow"); 
            Texture2DLibrary.texture_Controls = Content.Load<Texture2D>(@"Menu/Controls");
            Texture2DLibrary.texture_VideoOptions = Content.Load<Texture2D>(@"Menu/VideoOptions"); 
            Texture2DLibrary.texture_VideoOptions_Shadow = Content.Load<Texture2D>(@"Menu/VideoOptions_Shadow");
            Texture2DLibrary.texture_OptionScreen_Default = Content.Load<Texture2D>(@"Menu/OptionScreenDefault");
            Texture2DLibrary.texture_AddVolume = Content.Load<Texture2D>(@"Menu/AddVolume");
            Texture2DLibrary.texture_MinusVolume = Content.Load<Texture2D>(@"Menu/MinusVolume");

            //PowerUps
            Texture2DLibrary.texture_PowerUp_Damage = Content.Load<Texture2D>(@"PowerUps/2xDamage");
            Texture2DLibrary.texture_PowerUp_Health = Content.Load<Texture2D>(@"PowerUps/Health");
            Texture2DLibrary.texture_PowerUp_Armor = Content.Load<Texture2D>(@"PowerUps/Shield");

            //Level Select
            Texture2DLibrary.texture_Level1 = Content.Load<Texture2D>(@"Menu/Level1");
            Texture2DLibrary.texture_Level2 = Content.Load<Texture2D>(@"Menu/Level2");
            Texture2DLibrary.texture_Level3 = Content.Load<Texture2D>(@"Menu/Level3");
            Texture2DLibrary.texture_Level4 = Content.Load<Texture2D>(@"Menu/Level4");
            Texture2DLibrary.texture_Level5 = Content.Load<Texture2D>(@"Menu/Level5");

            //fonts
            FontLibrary.debug = Content.Load<SpriteFont>(@"debugFont");
            FontLibrary.Hud_Font = Content.Load<SpriteFont>(@"Hud_Font");
            FontLibrary.highscore_font = Content.Load<SpriteFont>(@"1942font3");

            //Music
            SoundLibrary.Menu_Song = Content.Load<Song>(@"Music/Ride_of_the_Valkyries");
            SoundLibrary.Twilight = Content.Load<Song>(@"Music/Twilight");
            SoundLibrary.Boss1 = Content.Load<Song>(@"Music/Boss1");
            SoundLibrary.Level1 = Content.Load<Song>(@"Music/Level1");
            SoundLibrary.Level2 = Content.Load<Song>(@"Music/Level2");
            SoundLibrary.Level3 = Content.Load<Song>(@"Music/Level3");
            SoundLibrary.Level4 = Content.Load<Song>(@"Music/Level4");
            SoundLibrary.Level5 = Content.Load<Song>(@"Music/Level5");

            //Sounds
            SoundLibrary.Explosion = Content.Load<SoundEffect>(@"Sounds/Explosion");
            SoundLibrary.Explosion_Big = Content.Load<SoundEffect>(@"Sounds/Explosion_Big");
            SoundLibrary.Kamikaze = Content.Load<SoundEffect>(@"Sounds/Kamikaze");
            SoundLibrary.Player_Shot = Content.Load<SoundEffect>(@"Sounds/Player_Shot");
            //SoundLibrary.Tower_Shot = Content.Load<SoundEffect>(@"Sounds/Tower_Shot");

            Texture2DLibrary.particle_explosion = Content.Load<Texture2D>(@"Particles/explosion");
            Texture2DLibrary.particle_smoke = Content.Load<Texture2D>(@"Particles/smoke");
            Texture2DLibrary.shielded = Content.Load<Texture2D>(@"shielded");
            Settings.window = Window;
            

            logic = new Logic(this.Content);
            menu = new MenuManager();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (KeyBoardInput.KeyState.IsKeyDown(Keys.Escape))
                this.Exit();
            if (menu.GetExitProgram())
                this.Exit();

            KeyBoardInput.KeyState = Keyboard.GetState();

            if (KeyBoardInput.KeyState.IsKeyDown(Keys.Pause) && KeyBoardInput.OldKeyState.IsKeyUp(Keys.Pause))
            {
                if (paused)
                    paused = false;
                else
                    paused = true;
            }
            if (KeyBoardInput.KeyState.IsKeyDown(Keys.M))
                debugText = true;

            //Just to test Orvar take it easy!
            if (menu.GetStartGame())
            {
                gameState = GameStates.Playing;
            }
            if (!paused)
            {
                switch (gameState)
                {
                    case GameStates.MainMenu:
                        {
                            logic.Update(KeyBoardInput.KeyState, gameTime);
                            menu.Update(new Point(Mouse.GetState().X, Mouse.GetState().Y), new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));
                            MusicManager.SetMusic(SoundLibrary.Menu_Song);
                            break;
                        }
                    case GameStates.AudioScreen:
                        {
                            break;
                        }
                    case GameStates.ControlScreen:
                        {
                            break;
                        }
                    case GameStates.VideoScreen:
                        {
                            break;
                        }
                    case GameStates.Playing:
                        {
                            logic.Update(KeyBoardInput.KeyState, gameTime);
                            break;
                        }
                }
            }

            KeyBoardInput.OldKeyState = KeyBoardInput.KeyState;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend, SamplerState.PointWrap,DepthStencilState.None,RasterizerState.CullNone);
           
            
            switch (gameState)
            {
                case GameStates.MainMenu:
                    {
                        logic.Draw(spriteBatch);
                        menu.Draw(spriteBatch);
                        break;
                    }
                case GameStates.AudioScreen:
                    {
                        break;
                    }
                case GameStates.ControlScreen:
                    {
                        break;
                    }
                case GameStates.VideoScreen:
                    {
                        break;
                    }
                case GameStates.Playing:
                    {
                        
                        logic.Draw(spriteBatch);
                        if (debugText)
                        {

                            spriteBatch.DrawString(FontLibrary.debug, "Screen resolution: " + Window.ClientBounds.Width + "x" + Window.ClientBounds.Height, new Vector2(1f, Window.ClientBounds.Height - (FontLibrary.debug.LineSpacing * 2)), Color.Red);
                            spriteBatch.DrawString(FontLibrary.debug, "Number of projectiles on screen: " + (Objects.playerProjectileList.Count + Objects.enemyProjectileList.Count), new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 3), Color.Red);
                            spriteBatch.DrawString(FontLibrary.debug, "Number of dead objects on screen: " + (Objects.deadList.Count), new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 4), Color.Red);
                            spriteBatch.DrawString(FontLibrary.debug, "Player 1 health: " + Objects.playerList[0].Health + "%", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 5), Color.Red);
                            if (Objects.playerList.Count >= 2)
                            {
                                spriteBatch.DrawString(FontLibrary.debug, "Player 2 health: " + Objects.playerList[1].Health + "%", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 6), Color.Red);
                            }
                            spriteBatch.DrawString(FontLibrary.debug, "Active particles on screen: " + Objects.particleList.Count + "", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 7), Color.Red);
                            spriteBatch.DrawString(FontLibrary.debug, "Active enemies on screen: " + Objects.ActiveObjects() + "", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 8), Color.Red);

                            spriteBatch.DrawString(FontLibrary.debug, "Current cameraposition: " + (145 - (int)logic.levelLoader.cameraPosition.Y / logic.levelLoader.TileSize()) + "", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 9), Color.White);
                        }
                        break;
                    }
            }
            

            

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        internal Logic Logic
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        

    }
}
