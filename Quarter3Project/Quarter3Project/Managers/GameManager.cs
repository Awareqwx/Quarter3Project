using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
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
        Random RNG;

        Texture2D[] testTexture;
        Texture2D[] bT;
        public SpriteFont consolas;

        public TestEntity[] tests;
        TestEnemy[] mooks;
        public BuildingEntity[] bE;
        public ButtonEvents bE2;

        public List<Collision.mapSegment> buildingSegments;
        public List<Collision.mapSegment> mapSegments;

        public List<Attack> enemyShots;
        public List<Attack> friendlyShots;

        public Collision.mapSegment[] playerSegments = new Collision.mapSegment[4];

        KeyboardState keyBoardState, prevKeyState;

        public string name;        

        public GameManager(Game1 game)
            : base(game)
        {
            myGame = game;
            bE2 = new ButtonEvents(game);
        }

        public override void Initialize()
        {
            buildingSegments = new List<Collision.mapSegment>();

            name = "";

            RNG = new Random();

            mapSegments = new List<Collision.mapSegment>();
            mapSegments.Add(new Collision.mapSegment(new Point(960, 0), new Point(0, 0)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 0), new Point(0, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 619), new Point(960, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(960, 618), new Point(959, 0)));

            enemyShots = new List<Attack>();
            friendlyShots = new List<Attack>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            
            testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
            
            bT = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/PotionShopBase"), Game.Content.Load<Texture2D>(@"Images/PotionShopShadow") };
            consolas = Game.Content.Load<SpriteFont>(@"Fonts/consolas");

            tests = new TestEntity[1];
            bE = new BuildingEntity[1];

            for (int i = 0; i < tests.Length; i++)
                tests[i] = new TestEntity(this, testTexture, new Vector2(10, 10));

            mooks = new TestEnemy[1];

            Texture2D enemy = Game.Content.Load<Texture2D>(@"Images/EnemyTest");
            for (int i = 0; i < mooks.Length; i++)
            {
                mooks[i] = new TestEnemy(this, enemy, new Vector2(RNG.Next(0, 540), RNG.Next(0, 380)));
            }

            bE[0] = new BuildingEntity(this, bT, new Vector2(650, 450));
            /*
            bE[1] = new BuildingEntity(this, bT, new Vector2(250, 350));
            bE[2] = new BuildingEntity(this, bT, new Vector2(450, 150));
             */            

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            keyBoardState = Keyboard.GetState();

            if (keyBoardState.IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            {
                bE2.showPop(2002);
            }

            for (int i = 0; i < tests.Length; i++)
                tests[i].Update(gameTime);

            for (int i = 0; i < bE.Length; i++)
                bE[i].Update(gameTime);

            for (int i = 0; i < mooks.Length; i++)
                mooks[i].Update(gameTime);

            for (int i = 0; i < enemyShots.Count; i++)
            {
                enemyShots[i].Update(gameTime);
                if (enemyShots[i].getPos().X > 960 || enemyShots[i].getPos().X < 0 || enemyShots[i].getPos().Y > 620 || enemyShots[i].getPos().Y < 0)
                {
                    enemyShots.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < friendlyShots.Count; i++)
            {
                friendlyShots[i].Update(gameTime);
                if (friendlyShots[i].getPos().X > 960 || friendlyShots[i].getPos().X < 0 || friendlyShots[i].getPos().Y > 620 || friendlyShots[i].getPos().Y < 0)
                {
                    friendlyShots.RemoveAt(i);
                    i--;
                }
            }
            prevKeyState = keyBoardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            for (int i = 0; i < tests.Length; i++)
                tests[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < bE.Length; i++)
                bE[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < mooks.Length; i++)
                mooks[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < enemyShots.Count; i++)
                enemyShots[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < friendlyShots.Count; i++)
                friendlyShots[i].Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}