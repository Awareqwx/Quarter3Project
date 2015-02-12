﻿using System;
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

        public List<Collision.mapSegment> buildingSegments;
        public List<Collision.mapSegment> mapSegments;

        public List<Attack> enemyShots;
        public List<Attack> friendlyShots;

        public Collision.mapSegment[] playerSegments = new Collision.mapSegment[4];

        KeyboardState keyBoardState;

        float timer;
        public string name;

        FileStream fs;

        

        public GameManager(Game1 game)
            : base(game)
        {
            myGame = game;
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

            if (File.Exists(@"Save/Save.txt"))
            {
                load();
            
            
            if (myGame.currentChar == 1)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C") };
            }
            else if (myGame.currentChar == 2)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Cleric"), Game.Content.Load<Texture2D>(@"Images/Cleric_C") };
            }
            else if (myGame.currentChar == 3)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Knight"), Game.Content.Load<Texture2D>(@"Images/Knight_C") };
            }

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
            
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            keyBoardState = Keyboard.GetState();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= .1)
            {
                if (keyBoardState.IsKeyDown(Keys.Escape))
                {
                    myGame.buttonPressed = 6;
                    myGame.SetCurrentLevel(Game1.GameLevels.UI);
                }
                timer = 0;
            }

            for (int i = 0; i < tests.Length; i++)
                tests[i].Update(gameTime);

            for (int i = 0; i < bE.Length; i++)
                bE[i].Update(gameTime);

            for (int i = 0; i < mooks.Length; i++)
                mooks[i].Update(gameTime);

            for (int i = 0; i < enemyShots.Count; i++)
                enemyShots[i].Update(gameTime);

            for (int i = 0; i < friendlyShots.Count; i++)
                friendlyShots[i].Update(gameTime);

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

        public void save()
        {
            fs = new FileStream(@"Save/Save.txt", FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            fs = new FileStream(@"Save/Save.txt", FileMode.Truncate, FileAccess.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(@"Save/Save.txt", true, Encoding.ASCII);
            string names = name;
            string chr = myGame.currentChar.ToString();
            sw.WriteLine(names);
            sw.WriteLine(chr);            
            sw.Close();
            LoadContent();
        }

        public void load()
        {
            StreamReader sr = new StreamReader(@"Save/Save.txt", Encoding.ASCII);
            if (!File.Exists(@"Save/Save.txt"))
            {
                myGame.SetCurrentLevel(Game1.GameLevels.UI);
            }
            name = sr.ReadLine();
            myGame.currentChar = Int32.Parse(sr.ReadLine());
            sr.Close();
        }
    }
}