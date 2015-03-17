#region File Description
// GameManager.cs
#endregion

#region Using Statements
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
using Quarter3Project.EventManagers;
#endregion

namespace Quarter3Project
{
    public class GameManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        #region Fields

        Game1 myGame;

        SpriteBatch spriteBatch;
        SpriteFont consolas;

        KeyboardState keyBoardState,
                      prevKeyState;

        MouseState mouseState,
                   prevMouseState;
        Vector2 mousePos;

        Texture2D[] testTexture,
                    mage,
                    cler,
                    warr;

        public Collision.mapSegment[] playerSegments = new Collision.mapSegment[4];
        public int classType,
                   prevClassType,
                   health,
                   mana,
                   exp;

        Texture2D[] bT;
        public BuildingEntity[] bE;
        public List<Collision.mapSegment> buildingSegments,
                                          mapSegments;

        Texture2D enemy;
        //TestEnemy[] mooks;

        Texture2D atk;
        public TestEntity[] tests;
        public List<Attack> enemyShots,
                            friendlyShots;
        int timer;

        public List<ItemData.item> itemList;
        Texture2D redPotion,
                  bluePotion;

        Texture2D uiBG,
                  redUI,
                  blueUI,
                  yellowUI,
                  blackUI,
                  transparentTex,
                  blueTUI;

        List<ItemData.btn> btnList;

        public ButtonEvents bE2;
        public GlobalEvents gE;

        Random RNG = new Random();

        #endregion

        #region Initialization

        public GameManager(Game1 game)
            : base(game)
        {
            myGame = game;
        }

        public override void Initialize()
        {

            buildingSegments = new List<Collision.mapSegment>();

            mapSegments = new List<Collision.mapSegment>();

            enemyShots = new List<Attack>();

            friendlyShots = new List<Attack>();

            bE2 = new ButtonEvents(myGame); //ButtonEvents bE2, BuildingEntity bE

            gE = new GlobalEvents(myGame);

            btnList = new List<ItemData.btn>();

            itemList = new List<ItemData.item>();

            tests = new TestEntity[1];

            bE = new BuildingEntity[1];

            mapSegments.Add(new Collision.mapSegment(new Point(960, 0), new Point(0, 0)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 0), new Point(0, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(0, 619), new Point(960, 620)));
            mapSegments.Add(new Collision.mapSegment(new Point(960, 618), new Point(959, 0)));

            bE2.loadGame();

            base.Initialize();

            if (File.Exists(@"Save/Save.txt"))
            {

                loadPlayer();

                loadEnemy();

                loadBuildings();

                loadItems();

                loadButtons();

                loadMissiles();

                gE.addItemToInv(20000000, 5);
                gE.addItemToInv(20000001, 8);
                gE.addItemToInv(20000002, 1231);
                gE.addItemToInv(20000003, 1123);

            }

        }

        public void LoadContent2()
        {
            LoadContent();
        }

        /// <summary>
        /// Load the player texture.
        /// </summary>
        public void loadPlayer()
        {
            if (classType == 200)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
                cler = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
            }
            else if (classType == 100)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
                mage = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
            }
            else if (classType == 300)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/knight") };
                warr = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/knight") };
            }
        }

        /// <summary>
        /// Add enemies
        /// </summary>
        public void loadEnemy()
        {
            //mooks = new TestEnemy[1];  

            /*
            for (int i = 0; i < mooks.Length; i++)
                mooks[i] = new TestEnemy(this, enemy, new Vector2(RNG.Next(0, 540), RNG.Next(0, 380)));
            */
        }

        /// <summary>
        /// Load buildings.
        /// </summary>
        public void loadBuildings()
        {
            for (int i = 0; i < bE.Length; i++)
                bE[i] = new BuildingEntity(this, bT, new Vector2(650, 450));
        }

        /// <summary>
        /// add items to itemList.
        /// </summary>
        public void loadItems()
        {
            itemList.Add(new ItemData.item(redPotion, 20000000, "Potion of Healing", blueTUI, "This is a Potion of Healing. It will \nheal you for 5 Health.", false));
            itemList.Add(new ItemData.item(bluePotion, 20000001, "Potion of Mana", blueTUI, "This is a Potion of Mana. It will \ngive you 5 Mana.", false));
            itemList.Add(new ItemData.item(redPotion, 20000002, "Potion of Superior Healing", blueTUI, "This is a Potion of Superior Healing. It will \nheal you for 50 Health.", false));
            itemList.Add(new ItemData.item(bluePotion, 20000003, "Potion of Superior Mana", blueTUI, "This is a Potion of Superior Mana. It will \ngive you for 50 Mana.", false));
        }

        /// <summary>
        /// add buttons.
        /// </summary>
        public void loadButtons()
        {
            btnList.Add(new ItemData.btn(transparentTex, new Vector2(875, 67), new Point(25, 25), "X", Color.White, false, false, true, 300, 300));
            btnList.Add(new ItemData.btn(transparentTex, new Vector2(853, 67), new Point(25, 25), "Y", Color.White, false, false, true, 300, 301));
        }

        /// <summary>
        /// add missiles.
        /// </summary>
        public void loadMissiles()
        {
            for (int i = 0; i < tests.Length; i++)
                tests[i] = new TestEntity(this, testTexture, new Vector2(10, 10));
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            bT = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/PotionShopBase"), Game.Content.Load<Texture2D>(@"Images/PotionShopShadow") };
            enemy = Game.Content.Load<Texture2D>(@"Images/EnemyTest");
            consolas = Game.Content.Load<SpriteFont>(@"Fonts/consolas");
            uiBG = Game.Content.Load<Texture2D>(@"Images/health_bar");
            redPotion = Game.Content.Load<Texture2D>(@"Images/red_potion");
            bluePotion = Game.Content.Load<Texture2D>(@"Images/blue_potion");
            redUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            redUI.SetData<Color>(new Color[] { new Color(255, 0, 0) });
            blueUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blueUI.SetData<Color>(new Color[] { new Color(0, 0, 255) });
            yellowUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            yellowUI.SetData<Color>(new Color[] { new Color(232, 205, 0) });
            blackUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blackUI.SetData<Color>(new Color[] { new Color(0, 0, 0) });
            transparentTex = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transparentTex.SetData<Color>(new Color[] { new Color(0, 0, 0, 0f) });
            blueTUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blueTUI.SetData<Color>(new Color[] { new Color(0, 55, 122) });
            atk = Game.Content.Load<Texture2D>(@"Images/Spell");

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        private void setStates()
        {
            keyBoardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Update buttons.
        /// </summary>
        private void updateButtons()
        {
            for (int i = 0; i < btnList.Count; i++)
            {
                if (btnList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    btnList[i] = new ItemData.btn(btnList[i].btnTexture, btnList[i].position, btnList[i].size, btnList[i].text, btnList[i].color, true, btnList[i].hover, btnList[i].visible, btnList[i].id, btnList[i].uniqueid);
                }
                else if (!btnList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    btnList[i] = new ItemData.btn(btnList[i].btnTexture, btnList[i].position, btnList[i].size, btnList[i].text, btnList[i].color, false, btnList[i].hover, btnList[i].visible, btnList[i].id, btnList[i].uniqueid);
                }
            }

            foreach (ItemData.btn b in btnList)
            {
                if (b.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        switch (b.uniqueid)
                        {
                            case 300:
                                gE.showPop(2004);
                                break;
                            case 301:
                                gE.showPop(2005);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update class textures.
        /// </summary>
        private void updateClassTexture()
        {
            if (prevClassType != classType)
            {
                if (classType == 200)
                {
                    testTexture = cler;
                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") }, new Vector2(10, 10));
                }
                else if (classType == 100)
                {
                    testTexture = mage;
                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") }, new Vector2(10, 10));
                }
                else if (classType == 300)
                {
                    testTexture = warr;
                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/knight") }, new Vector2(10, 10));
                }
            }
        }

        /// <summary>
        /// Update projectiles.
        /// </summary>
        /// <param name="gameTime"></param>
        private void updateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < tests.Length; i++)
                tests[i].Update(gameTime);

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
        }

        /// <summary>
        /// Update and executes events on user input.
        /// </summary>
        private void updateUserInput()
        {
            if (keyBoardState.IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            {
                gE.showPop(2002);
            }
        }

        /// <summary>
        /// Update Buildings.
        /// </summary>
        /// <param name="gameTime">Timing values</param>
        private void updateBuildings(GameTime gameTime)
        {
            for (int i = 0; i < bE.Length; i++)
                bE[i].Update(gameTime);
        }

        /// <summary>
        /// Update Enemies.
        /// </summary>
        /// <param name="gameTime">Timing values</param>
        private void updateEnemies(GameTime gameTime)
        {
            /*
            for (int i = 0; i < mooks.Length; i++)
                mooks[i].Update(gameTime);
            */
        }

        /// <summary>
        /// Set mouse and keyboard states.
        /// </summary>
        private void setPreviousStates()
        {
            prevMouseState = mouseState;
            prevKeyState = keyBoardState;
            prevClassType = classType;
        }

        /// <summary>
        /// Update user interface.
        /// </summary>
        private void updateUI()
        {
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
        }

        public override void Update(GameTime gameTime)
        {
            //Sets keyboard and mouse state.
            setStates();

            //updates buttons in the game.
            updateButtons();

            //updates the texture based on class being used.
            updateClassTexture();

            //updates position of projectiles and removes them.
            updateProjectiles(gameTime);

            //Checks if buttons are being pressed and starts an event
            updateUserInput();

            //updates enemies
            updateEnemies(gameTime);

            //updates buildings
            updateBuildings(gameTime);

            //updates user interface
            updateUI();

            //set previous keyboard and mouse state
            setPreviousStates();
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws all user interface.
        /// </summary>
        private void DrawUI()
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
            spriteBatch.Draw(uiBG, new Rectangle((GraphicsDevice.Viewport.Width - 204), 10, 194, 81), Color.White);

            for (int i = 0; i < btnList.Count; i++)
            {
                if (btnList[i].visible == true)
                {
                    spriteBatch.Draw(btnList[i].btnTexture, new Rectangle((int)btnList[i].position.X, (int)btnList[i].position.Y, btnList[i].size.X, btnList[i].size.Y), Color.White);
                    spriteBatch.DrawString(consolas, btnList[i].text, new Vector2(((int)btnList[i].position.X + (btnList[i].size.X / 2)) - (consolas.MeasureString(btnList[i].text).Length() / 4), ((int)btnList[i].position.Y + (btnList[i].size.Y / 2)) - (consolas.MeasureString(btnList[i].text).Y / 2)), Color.White);

                }
            }

        }

        /// <summary>
        /// Draws all projectiles
        /// </summary>
        /// <param name="gameTime">Timing values</param>
        private void drawProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < tests.Length; i++)
                tests[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < enemyShots.Count; i++)
                enemyShots[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < friendlyShots.Count; i++)
                friendlyShots[i].Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draws all buildings.
        /// </summary>
        /// <param name="gameTime"></param>
        private void drawBuildings(GameTime gameTime)
        {
            for (int i = 0; i < bE.Length; i++)
                bE[i].Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draws all enemies.
        /// </summary>
        private void drawEnemies()
        {
            /*
            for (int i = 0; i < mooks.Length; i++)
                mooks[i].Draw(gameTime, spriteBatch);
            */
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            drawProjectiles(gameTime);

            drawBuildings(gameTime);

            drawEnemies();

            DrawUI();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}