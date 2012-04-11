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

        
        KeyboardState keyState;
        Logic logic;
        LevelLoader levelLoader;


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
            Texture2DLibrary.player = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.projectile_player = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.enemy_zero = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.projectile_enemy_zero = Content.Load<Texture2D>(@"spaceship");

            Texture2DLibrary.enemy_heavy = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.projectile_enemy_heavy = Content.Load<Texture2D>(@"spaceship");

            Texture2DLibrary.enemy_tower = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.projectile_enemy_tower = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.enemy_tower_base = Content.Load<Texture2D>(@"spaceship");
            Texture2DLibrary.enemy_tower_dead = Content.Load<Texture2D>(@"spaceship");

            FontLibrary.debug = Content.Load<SpriteFont>(@"debugFont");

            Settings.nr_of_players = 2;
            Settings.level_speed = 1f;
            Settings.window = Window;
            

            logic = new Logic();
            levelLoader = new LevelLoader("./Levels/level1.xml", this.Content);
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

            keyState = Keyboard.GetState();

            //Just to test Orvar take it easy!
            levelLoader.MoveCamera(0.5F);

            logic.Update(keyState, gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            levelLoader.Draw(spriteBatch);
            logic.Draw(spriteBatch);

            spriteBatch.DrawString(FontLibrary.debug, "Number of enemies on screen: " + Objects.enemyList.Count, new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing), Color.Red);
            spriteBatch.DrawString(FontLibrary.debug, "Screen resolution: " + Window.ClientBounds.Width + "x" + Window.ClientBounds.Height, new Vector2(1f, Window.ClientBounds.Height - (FontLibrary.debug.LineSpacing * 2)), Color.Red);
            spriteBatch.DrawString(FontLibrary.debug, "Number of projectiles on screen: " + (Objects.playerProjectileList.Count + Objects.enemyProjectileList.Count), new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 3), Color.Red);
            spriteBatch.DrawString(FontLibrary.debug, "Number of dead objects on screen: " + (Objects.deadList.Count), new Vector2(1f, Window.ClientBounds.Height - FontLibrary.debug.LineSpacing * 4), Color.Red);
            

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
