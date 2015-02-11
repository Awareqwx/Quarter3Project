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

namespace Quarter3Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public enum GameLevels { SPLASH, MENU, PLAY, UI, CC };
        public GameLevels currentLevel = GameLevels.SPLASH, prevLevel = GameLevels.SPLASH;

        SplashScreenManager splashScreenManager;
        MenuManager menuManager;
        GameManager gameManager;
        PopUpManager popUpManager;
        CCManager ccManager;

        public int buttonPressed, prevButtonPressed, currentChar = 1;
        public string msg;


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
            // TODO: Add your initialization logic here     

            splashScreenManager = new SplashScreenManager(this);
            menuManager = new MenuManager(this);
            gameManager = new GameManager(this);
            ccManager = new CCManager(this);
            popUpManager = new PopUpManager(this);
            

            Components.Add(splashScreenManager);
            Components.Add(menuManager);
            Components.Add(gameManager);
            Components.Add(ccManager);
            Components.Add(popUpManager);
            

            SetCurrentLevel(GameLevels.SPLASH);

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

        public void SetCurrentLevel(GameLevels level)
        {
            splashScreenManager.Enabled = false;
            splashScreenManager.Visible = false;
            menuManager.Enabled = false;
            menuManager.Visible = false;
            gameManager.Enabled = false;
            gameManager.Visible = false;
            ccManager.Enabled = false;
            ccManager.Visible = false;
            popUpManager.Enabled = false;
            popUpManager.Visible = false;
            

            switch (level)
            {
                case GameLevels.SPLASH:
                    splashScreenManager.Enabled = true;
                    splashScreenManager.Visible = true;
                    currentLevel = GameLevels.SPLASH;
                    break;
                case GameLevels.MENU:
                    menuManager.Visible = true;
                    menuManager.Enabled = true;
                    currentLevel = GameLevels.MENU;
                    break;
                case GameLevels.PLAY:
                    gameManager.Visible = true;
                    gameManager.Enabled = true;
                    currentLevel = GameLevels.PLAY;
                    break;
                case GameLevels.CC:
                   ccManager.Enabled = true;
                   ccManager.Visible = true;
                   currentLevel = GameLevels.CC;
                   break;
                case GameLevels.UI:
                    switch (prevLevel)
                    {
                        case GameLevels.MENU:
                            menuManager.Visible = true;
                            break;
                        case GameLevels.PLAY:
                            gameManager.Visible = true;
                            break;
                        case GameLevels.SPLASH:
                            splashScreenManager.Visible = true;
                            break;
                        case GameLevels.CC:
                            ccManager.Visible = true;
                            break;
                    }
                    popUpManager.Enabled = true;
                    popUpManager.Visible = true;
                   break;
                
            }
            prevLevel = currentLevel;
        }
    }
}
