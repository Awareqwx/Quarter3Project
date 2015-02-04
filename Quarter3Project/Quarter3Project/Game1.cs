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
using Quarter3Project.EntityTypes;

namespace Quarter3Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D testTexture;
        Texture2D bT;

        TestEntity[] tests;
        BuildingEntity[] bE;

        public List<Collision.mapSegment> buildingSegments;
        public List<Collision.mapSegment> mapSegments;

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
            buildingSegments = new List<Collision.mapSegment>();

            mapSegments = new List<Collision.mapSegment>();
            mapSegments.Add(new Collision.mapSegment(new Point(960, 0), new Point(0, 0)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 0), new Point(0, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 619), new Point(960, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(960, 618), new Point(959, 0)));

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
            testTexture = Content.Load<Texture2D>(@"Images/PlayerTest");
            bT = Content.Load<Texture2D>(@"Images/PlayerTest");
            tests = new TestEntity[1];
            bE = new BuildingEntity[6];
            for (int i = 0; i < tests.Length; i++)
                tests[i] = new TestEntity(this, testTexture, new Vector2(500, 150));
            
            for (int i = 0; i < bE.Length; i++) 
                bE[i] = new BuildingEntity(this, bT, new Vector2(i*50, i*50));
            
            // TODO: use this.Content to load your game content here
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
            for (int i = 0; i < tests.Length; i++)
                tests[i].Update(gameTime);

            // TODO: Add your update logic here
            for (int i = 0; i < bE.Length; i++)
                bE[i].Update(gameTime);

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
            for (int i = 0; i < tests.Length; i++)
                tests[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < bE.Length; i++)
                bE[i].Draw(gameTime, spriteBatch);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
