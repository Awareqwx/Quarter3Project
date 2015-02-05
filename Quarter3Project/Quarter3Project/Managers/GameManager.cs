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
using Quarter3Project.EntityTypes;

namespace Quarter3Project
{

    public class GameManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Game1 myGame;

        Texture2D testTexture;
        Texture2D bT;

        TestEntity[] tests;
        BuildingEntity[] bE;

        public List<Collision.mapSegment> buildingSegments;
        public List<Collision.mapSegment> mapSegments;

        public Collision.Circle Building;
        public float buildingRadius;
        public Collision.Circle Player;
        public float playerRadius;

        public enum GameLevels { SPLASH, MENU, PLAY };
        public GameLevels currentLevel = GameLevels.SPLASH;

        public GameManager(Game1 game) : base(game)
        {
            myGame = game;
        }

        public override void Initialize() 
        {
            buildingSegments = new List<Collision.mapSegment>();

            mapSegments = new List<Collision.mapSegment>();
            mapSegments.Add(new Collision.mapSegment(new Point(960, 0), new Point(0, 0)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 0), new Point(0, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 619), new Point(960, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(960, 618), new Point(959, 0)));
            
            base.Initialize(); 
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            testTexture = Game.Content.Load<Texture2D>(@"Images/PlayerTest");
            bT = Game.Content.Load<Texture2D>(@"Images/PlayerTest");

            tests = new TestEntity[1];
            bE = new BuildingEntity[6];

            for (int i = 0; i < tests.Length; i++)
                tests[i] = new TestEntity(this, testTexture, new Vector2(500, 150));

            for (int i = 0; i < bE.Length; i++)
                bE[i] = new BuildingEntity(this, bT, new Vector2(450, 450));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < tests.Length; i++)
                tests[i].Update(gameTime);

            for (int i = 0; i < bE.Length; i++)
                bE[i].Update(gameTime);


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            for (int i = 0; i < tests.Length; i++)
                tests[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < bE.Length; i++)
                bE[i].Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}