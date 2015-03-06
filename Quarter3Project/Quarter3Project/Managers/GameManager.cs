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
        public BuildingEntity[] bE;
        public ButtonEvents bE2;
        public Collision.mapSegment[] playerSegments = new Collision.mapSegment[4];
        Game1 myGame;
        public int classType, prevClassType, health, mana, exp;
        KeyboardState keyBoardState, prevKeyState;
        public List<Projectile> enemyShots, friendlyShots;
        public List<Collision.mapSegment> buildingSegments, mapSegments;
        Random RNG;
        SpriteBatch spriteBatch;   
        public SpriteFont consolas;
        TestEnemy[] mooks;
        public TestEntity[] tests;
        Texture2D[] testTexture, mage, cler, warr, bT;
        Texture2D enemy, uiBG, redUI, blueUI, yellowUI;

        public GameManager(Game1 game)
            : base(game)
        {
            myGame = game;
            bE2 = new ButtonEvents(game); //ButtonEvents bE2, BuildingEntity bE
        }

        public override void Initialize()
        {
            buildingSegments = new List<Collision.mapSegment>();
            RNG = new Random();
            mapSegments = new List<Collision.mapSegment>();
            enemyShots = new List<Projectile>();
            friendlyShots = new List<Projectile>();

            mapSegments.Add(new Collision.mapSegment(new Point(960, 0), new Point(0, 0)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 0), new Point(0, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 619), new Point(960, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(960, 618), new Point(959, 0)));

            bE2.loadGame();

            base.Initialize();
        }

        public void LoadContent2()
        {
            LoadContent();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            if (File.Exists(@"Save/Save.txt"))
            {

                if (classType == 1)
                {
                    testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
                    cler = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
                }
                else if (classType == 3)
                {
                    testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
                    mage = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
                }
                else if (classType == 2)
                {
                    testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Base"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Armor"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Sword") };
                    warr = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Base"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Armor"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Sword") };
                }

                bT = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/PotionShopBase"), Game.Content.Load<Texture2D>(@"Images/PotionShopShadow") };
                enemy = Game.Content.Load<Texture2D>(@"Images/EnemyTest");
                consolas = Game.Content.Load<SpriteFont>(@"Fonts/consolas");
                uiBG = Game.Content.Load<Texture2D>(@"Images/health_bar");
                redUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                redUI.SetData<Color>(new Color[] { new Color(255, 0, 0) });
                blueUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                blueUI.SetData<Color>(new Color[] { new Color(0, 0, 255) });
                yellowUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                yellowUI.SetData<Color>(new Color[] { new Color(232, 205, 0) });

                tests = new TestEntity[1];
                bE = new BuildingEntity[1];
                mooks = new TestEnemy[1];

                for (int i = 0; i < tests.Length; i++)
                    tests[i] = new TestEntity(this, testTexture, new Vector2(10, 10));
                
                for (int i = 0; i < mooks.Length; i++)
                    mooks[i] = new TestEnemy(this, enemy, new Vector2(RNG.Next(0, 540), RNG.Next(0, 380)));

                for (int i = 0; i < bE.Length; i++ )
                    bE[i] = new BuildingEntity(this, bT, new Vector2(650, 450));

            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyBoardState = Keyboard.GetState();

            if (prevClassType != classType)
            {
                if (classType == 1)
                {
                    testTexture = cler;
                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") }, new Vector2(10, 10));
                }
                else if (classType == 3)
                {
                    testTexture = mage;
                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") }, new Vector2(10, 10));
                }
                else if (classType == 2)
                {
                    testTexture = warr;
                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, testTexture, new Vector2(10, 10));
                }
            }

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

            if (keyBoardState.IsKeyDown(Keys.OemPlus))
            {
                health += 1;
                mana += 1;
                exp += 1;
            }

            if (keyBoardState.IsKeyDown(Keys.OemMinus))
            {
                health -= 1;
                mana -= 1;
                exp -= 1;
            }

            if (health <= 0)
            {
                health = 0;
            }
            else if (health >= 50)
            {
                health = 50;
            }

            if (mana <= 0)
            {
                mana = 0;
            }
            else if (mana >= 115)
            {
                mana = 115;
            }

            if (exp <= 0)
            {
                exp = 0;
            }
            else if (exp >= 115)
            {
                exp = 115;
            }

            prevKeyState = keyBoardState;
            prevClassType = classType;
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

            
            DrawUI();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawUI()
        {
            
            spriteBatch.Draw(blueUI, new Rectangle(830, 24, mana, 12), new Rectangle(0, 0, 1, 1), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, mana.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(mana.ToString()).Length() / 2) + 1, (24 - 5) + 1), Color.Black);
            spriteBatch.DrawString(consolas, mana.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(mana.ToString()).Length() / 2), (24 - 5)), Color.White);
            spriteBatch.Draw(yellowUI, new Rectangle(830, 43, exp, 12), new Rectangle(0, 0, 1, 1), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, exp.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(exp.ToString()).Length() / 2) + 1, (43 - 5) + 1), Color.Black);
            spriteBatch.DrawString(consolas, exp.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(exp.ToString()).Length() / 2), (43 - 5)), Color.White);
            spriteBatch.Draw(redUI, new Rectangle(825, 65, 50, health), new Rectangle(0, 0, 1, 1), Color.White, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, health.ToString(), new Vector2((825 - 23) - (consolas.MeasureString(health.ToString()).Length() / 2) + 1, (65 - 25) - (consolas.MeasureString(health.ToString()).Y / 2) + 1), Color.Black);
            spriteBatch.DrawString(consolas, health.ToString(), new Vector2((825 - 23) - (consolas.MeasureString(health.ToString()).Length() / 2), (65 - 25) - (consolas.MeasureString(health.ToString()).Y / 2)), Color.White);
            spriteBatch.Draw(uiBG, new Rectangle((GraphicsDevice.Viewport.Width - 204), 10, 194, 59), Color.White);
            
        }
    }
}