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

        KeyboardState keyState;
        Logic logic;
        MenuManager menu;

        Hud hud;


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

            Texture2DLibrary.player = Content.Load<Texture2D>(@"Enemies/Player");
            Texture2DLibrary.projectile_player = Content.Load<Texture2D>(@"Enemies/square1");
            Texture2DLibrary.enemy_zero = Content.Load<Texture2D>(@"Enemies/Zero");
            Texture2DLibrary.projectile_enemy_zero = Content.Load<Texture2D>(@"Enemies/square1");

            Texture2DLibrary.enemy_heavy = Content.Load<Texture2D>(@"Enemies/spaceship");
            Texture2DLibrary.projectile_enemy_heavy = Content.Load<Texture2D>(@"Enemies/Spaceship");

            Texture2DLibrary.enemy_tower = Content.Load<Texture2D>(@"Enemies/AATower");
            Texture2DLibrary.projectile_enemy_tower = Content.Load<Texture2D>(@"Enemies/square1");
            Texture2DLibrary.enemy_tower_base = Content.Load<Texture2D>(@"Enemies/AABase");
            Texture2DLibrary.enemy_tower_dead = Content.Load<Texture2D>(@"Enemies/AABaseDead");

            Texture2DLibrary.boss1 = Content.Load<Texture2D>(@"boss1");
            Texture2DLibrary.boss1_projectile = Content.Load<Texture2D>(@"Enemies/square1");

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
            Texture2DLibrary.texture_PowerUp_Damage = Content.Load<Texture2D>(@"PowerUps/PowerUp");
            Texture2DLibrary.texture_PowerUp_Health = Content.Load<Texture2D>(@"PowerUps/PowerUp");

            //Level Select
            Texture2DLibrary.texture_Level1 = Content.Load<Texture2D>(@"Menu/Level1");
            Texture2DLibrary.texture_Level2 = Content.Load<Texture2D>(@"Menu/Level2");
            Texture2DLibrary.texture_Level3 = Content.Load<Texture2D>(@"Menu/Level3");
            Texture2DLibrary.texture_Level4 = Content.Load<Texture2D>(@"Menu/Level4");
            Texture2DLibrary.texture_Level5 = Content.Load<Texture2D>(@"Menu/Level5");

            //fonts
            FontLibrary.debug = Content.Load<SpriteFont>(@"debugFont");
            FontLibrary.Hud_Font = Content.Load<SpriteFont>(@"Hud_Font");

            //Sounds
            SoundLibrary.Menu_Song = Content.Load<Song>(@"Ride_of_the_Valkyries");

            Texture2DLibrary.particle_zero_explosion = Content.Load<Texture2D>(@"Particles/explosion");
            

            Settings.window = Window;
            

            logic = new Logic(this.Content);
            menu = new MenuManager();

            hud = new Hud();

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
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();
            if (menu.GetExitProgram())
                this.Exit();

            keyState = Keyboard.GetState();

            //Just to test Orvar take it easy!
            if (menu.GetStartGame())
            {
                gameState = GameStates.Playing;
            }
            
            switch (gameState)
            {
                case GameStates.MainMenu:
                    {
                        menu.Update(new Point(Mouse.GetState().X, Mouse.GetState().Y), new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));
                        menu.PlayMusic();
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
                        menu.StopMusic();
                        logic.Update(keyState, gameTime);
                        break;
                    }
            }

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
                        spriteBatch.DrawString(FontLibrary.debug, "Screen resolution: " + Window.ClientBounds.Width + "x" + Window.ClientBounds.Height, new Vector2(1f, Window.ClientBounds.Height - (FontLibrary.debug.LineSpacing * 2)), Color.Red);
                        spriteBatch.DrawString(FontLibrary.debug, "Number of projectiles on screen: " + (Objects.playerProjectileList.Count + Objects.enemyProjectileList.Count), new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 3), Color.Red);
                        spriteBatch.DrawString(FontLibrary.debug, "Number of dead objects on screen: " + (Objects.deadList.Count), new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 4), Color.Red);
                        spriteBatch.DrawString(FontLibrary.debug, "Player 1 health: " + Objects.playerList[0].Health + "%", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 5), Color.Red);
                        if (Settings.nr_of_players >= 2)
                        {
                            spriteBatch.DrawString(FontLibrary.debug, "Player 2 health: " + Objects.playerList[1].Health + "%", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 6), Color.Red);
                        }
                        spriteBatch.DrawString(FontLibrary.debug, "Active particles on screen: " + Objects.particleList.Count + "", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 7), Color.Red);
                        spriteBatch.DrawString(FontLibrary.debug, "Active enemies on screen: " + Objects.ActiveObjects() + "", new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 8), Color.Red);


                        hud.Draw(spriteBatch, gameTime);
   
                        break;
                    }
            }
            

            

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
