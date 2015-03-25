#region File Description
// Game1.cs
#endregion

#region Using Statements
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
using Quarter3Project.Managers;
#endregion

namespace Quarter3Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        #region Fields

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        public CreateManager createManager;
        public DebugManager debugManager;
        public GameManager gameManager;
        public MenuManager menuManager;
        public PopManager popManager;
        public CreditsManager creditsManager;
        public SplashScreenManager splashScreenManager;

        public GameLevels.GameLevels cL { get; private set; }
        public GameLevels.GameLevels pL { get; private set; }

        #endregion

        #region Initialization

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 620;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //Highest Draw order is drawn over the lower draw numbers.
            menuManager = new MenuManager(this);
            Components.Add(menuManager);
            menuManager.DrawOrder = 1;

            createManager = new CreateManager(this);
            Components.Add(createManager);
            createManager.DrawOrder = 1;

            popManager = new PopManager(this);
            Components.Add(popManager);
            popManager.DrawOrder = 2;

            splashScreenManager = new SplashScreenManager(this);
            Components.Add(splashScreenManager);
            splashScreenManager.DrawOrder = 2;

            debugManager = new DebugManager(this);
            Components.Add(debugManager);
            debugManager.DrawOrder = 3;

            gameManager = new GameManager(this);
            Components.Add(gameManager);
            gameManager.DrawOrder = 1;

            creditsManager = new CreditsManager(this);
            Components.Add(creditsManager);
            creditsManager.DrawOrder = 1;

            SetCurrentLevel(GameLevels.GameLevels.SPLASH);

            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        #endregion

        #region Update and Draw

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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        public void showPop(int id)
        {
            popManager.showPop(id, popManager.popList);
        }

        public void SetCurrentLevel(GameLevels.GameLevels gL)
        {
            menuManager.Enabled = false;
            menuManager.Visible = false;
            createManager.Enabled = false;
            createManager.Visible = false;
            splashScreenManager.Enabled = false;
            splashScreenManager.Visible = false;
            gameManager.Enabled = false;
            gameManager.Visible = false;
            creditsManager.Enabled = false;
            creditsManager.Visible = false;

            switch (gL)
            {
                case GameLevels.GameLevels.MENU:
                    menuManager.Enabled = true;
                    menuManager.Visible = true;
                    pL = cL;
                    cL = GameLevels.GameLevels.MENU;
                    break;
                case GameLevels.GameLevels.CREATE:
                    createManager.Enabled = true;
                    createManager.Visible = true;
                    pL = cL;
                    cL = GameLevels.GameLevels.CREATE;
                    break;
                case GameLevels.GameLevels.GAME:
                    gameManager.Enabled = true;
                    gameManager.Visible = true;
                    pL = cL;
                    cL = GameLevels.GameLevels.GAME;
                    break;
                case GameLevels.GameLevels.SPLASH:
                    splashScreenManager.Enabled = true;
                    splashScreenManager.Visible = true;
                    pL = cL;
                    cL = GameLevels.GameLevels.SPLASH;
                    break;
                case GameLevels.GameLevels.CREDITS:
                    creditsManager.Enabled = true;
                    creditsManager.Visible = true;
                    pL = cL;
                    cL = GameLevels.GameLevels.CREDITS;
                    break;
            }
        }

        #endregion

    }
}
