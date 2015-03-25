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
using Quarter3Project.Classes;
#endregion

namespace Quarter3Project
{
    public class GameManager : DrawableGameComponent
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
                    warr,
                    arch;

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

        Texture2D atk;
        public List<Attack> enemyShots,
                            friendlyShots;

        public List<ItemData.item> itemList;
        Texture2D redPotion,
                  bluePotion,
                  yellowPotion;

        Texture2D uiBG,
                  redUI,
                  blueUI,
                  yellowUI,
                  blackUI,
                  transparentTex,
                  blueTUI,
                  yellowTUI,
                  whiteRarity,
                  blueRarity,
                  greenRarity,
                  yellowRarity,
                  purpleRarity,
                  redRarity,
                  rainbowRarity;

        List<ItemData.btn> btnList;

        public ButtonEvents bE2;
        public GlobalEvents gE;

        public Location Test;
        public Location currentLoc;
        Texture2D mapBG;

        public Player player;

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

            RNG = new Random();

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

            }
            
            loadBuildings();

            loadItems();         

            loadButtons();

            addLocs();

            gE.addItemToInv(20000000, 5); // Health Potion
            gE.addItemToInv(20000001, 8); // Mana Potion
            gE.addItemToInv(20000002, 1231); // Superior Health Potion
            gE.addItemToInv(20000003, 1123); // Superior Mana Potion
            gE.addItemToInv(20000004, 23); // Experience Potion
            gE.addItemToInv(20000005, 2); // Superior Experience Potion
            
        }

        /// <summary>
        /// Load the player texture.
        /// </summary>
        public void loadPlayer()
        {
            if (classType == 200)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
                cler = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Cleric/Cleric")};
                player = new Cleric(cler, new Vector2(10, 10), this, myGame);
            }
            else if (classType == 100)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard/Wizard_Base"), Game.Content.Load<Texture2D>(@"Images/Wizard/Wizard_Cloak"), Game.Content.Load<Texture2D>(@"Images/Wizard/Wizard_Staff") };
                mage = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard/Wizard_Base"), Game.Content.Load<Texture2D>(@"Images/Wizard/Wizard_Cloak"), Game.Content.Load<Texture2D>(@"Images/Wizard/Wizard_Staff") };
                player = new Mage(mage, new Vector2(10, 10), this, myGame);
            }
            else if (classType == 300)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Base"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Armor"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Sword") };
                warr = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Base"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Armor"), Game.Content.Load<Texture2D>(@"Images/Knight/Knight_Sword") };
                player = new Knight(warr, new Vector2(10, 10), this, myGame);
            }
            else if(classType == 400)
            {
                testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Archer/Archer_Base"), Game.Content.Load<Texture2D>(@"Images/Archer/Archer_Feather") };
                arch = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Archer/Archer_Base"), Game.Content.Load<Texture2D>(@"Images/Archer/Archer_Feather") };
                player = new Archer(arch, new Vector2(10, 10), this, myGame);
            }
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
            itemList.Add(new ItemData.item(redPotion, 20000000, "Potion of Healing", blueTUI, "This is a Potion of Healing. It will \nheal you for 5 Health.", false, whiteRarity));
            itemList.Add(new ItemData.item(bluePotion, 20000001, "Potion of Mana", blueTUI, "This is a Potion of Mana. It will \ngive you 5 Mana.", false, whiteRarity));
            itemList.Add(new ItemData.item(redPotion, 20000002, "Potion of Superior Healing", blueTUI, "This is a Potion of Superior Healing. It will \nheal you for 50 Health.", false, whiteRarity));
            itemList.Add(new ItemData.item(bluePotion, 20000003, "Potion of Superior Mana", blueTUI, "This is a Potion of Superior Mana. It will \ngive you 50 Mana.", false, whiteRarity));
            itemList.Add(new ItemData.item(yellowPotion, 20000004, "Potion of Experience", yellowTUI, "This is a Potion of Experience. It will \ngive you 5 Experience.", false, whiteRarity));
            itemList.Add(new ItemData.item(yellowPotion, 20000005, "Potion of Superior Experience", yellowTUI, "This is a Potion of Superior Experience. It will \ngive you 50 Experience.", false, whiteRarity));
        }

        /// <summary>
        /// add buttons.
        /// </summary>
        public void loadButtons()
        {
            btnList.Add(new ItemData.btn(transparentTex, new Vector2(875, 67), new Point(25, 25), "X", Color.White, false, false, true, 300, 301));
            btnList.Add(new ItemData.btn(transparentTex, new Vector2(852, 67), new Point(25, 25), "I", Color.White, false, false, true, 300, 300));
        }
               
        /// <summary>
        /// 
        /// </summary>
        private void addLocs()
        {
            List<Collision.mapSegment> ml = new List<Collision.mapSegment>();
            List<Collision.Ellipse> el = new List<Collision.Ellipse>();
            List<Collision.Circle> cl = new List<Collision.Circle>();
            List<BuildingEntity> bl = new List<BuildingEntity>();
            List<Portal> p = new List<Portal>();
            List<Enemy> enemies = new List<Enemy>();

            for (int i = 0; i < 1; i++)
                enemies.Add(new TestEnemy(this, enemy, new Vector2(RNG.Next(0, 540), RNG.Next(0, 380))));

            Test = new Location(ml, cl, el, p, bl, enemies, mapBG);

            currentLoc = Test;
        }

        public void LoadContent2()
        {
            LoadContent();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            consolas = Game.Content.Load<SpriteFont>(@"Fonts/consolas");

            bT = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/PotionShopBase"), Game.Content.Load<Texture2D>(@"Images/PotionShopShadow") };

            enemy = Game.Content.Load<Texture2D>(@"Images/EnemyTest");

            atk = Game.Content.Load<Texture2D>(@"Images/Spell");

            redPotion = Game.Content.Load<Texture2D>(@"Images/red_potion");
            bluePotion = Game.Content.Load<Texture2D>(@"Images/blue_potion");
            yellowPotion = Game.Content.Load<Texture2D>(@"Images/yellow_potion");
            uiBG = Game.Content.Load<Texture2D>(@"Images/health_bar");
            mapBG = Game.Content.Load<Texture2D>(@"Images/bossmap");

            redUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blueUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            yellowUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blackUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transparentTex = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blueTUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            yellowTUI = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            whiteRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blueRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            greenRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            yellowRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            purpleRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            redRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            rainbowRarity = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                

            redUI.SetData<Color>(new Color[] { new Color(255, 0, 0) });
            blueUI.SetData<Color>(new Color[] { new Color(0, 0, 255) });
            yellowUI.SetData<Color>(new Color[] { new Color(232, 205, 0) });
            blackUI.SetData<Color>(new Color[] { new Color(0, 0, 0) });
            transparentTex.SetData<Color>(new Color[] { new Color(0, 0, 0, 0f) });
            blueTUI.SetData<Color>(new Color[] { new Color(0, 55, 122) });
            yellowTUI.SetData<Color>(new Color[] { new Color(255, 204, 0) });
            whiteRarity.SetData<Color>(new Color[] { new Color(255, 255, 255) });
            blueRarity.SetData<Color>(new Color[] { new Color(0, 166, 255) });
            greenRarity.SetData<Color>(new Color[] { new Color(0, 255, 21) });
            yellowRarity.SetData<Color>(new Color[] { new Color(255, 255, 64) });
            purpleRarity.SetData<Color>(new Color[] { new Color(187, 0, 194) });
            redRarity.SetData<Color>(new Color[] { new Color(194, 0, 26) });
            rainbowRarity = Game.Content.Load<Texture2D>(@"Images/rainbowRarity");

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
                                gE.showPop(2002);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update class textures.
        /// </summary>
        private void updatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
            currentLoc.Update(gameTime);
        }

        /// <summary>
        /// Update projectiles.
        /// </summary>
        /// <param name="gameTime"></param>
        private void updateProjectiles(GameTime gameTime)
        {           
            for (int i = 0; i < enemyShots.Count; i++)
            {
                enemyShots[i].Update(gameTime);
            }

            for (int i = 0; i < friendlyShots.Count; i++)
            {
                friendlyShots[i].Update(gameTime);
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
            if (player.health <= 0)
            {
                player.health = 0;
            }
            
            if (player.health >= 50)
            {
                player.health = 50;
            }

            if (player.mana <= 0)
            {
                player.mana = 0;
            }
            else if (player.mana >= 115)
            {
                player.mana = 115;
            }

            if (player.exp <= 0)
            {
                player.exp = 0;
            }
            else if (player.exp >= 115)
            {
                player.exp = 0;
                player.health = 50;
                player.mana = 115;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Sets keyboard and mouse state.
            setStates();

            //updates buttons in the game.
            updateButtons();

            //updates the texture based on class being used.
            updatePlayer(gameTime);

            //updates position of projectiles and removes them.
            updateProjectiles(gameTime);

            //Checks if buttons are being pressed and starts an event
            updateUserInput();

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
            spriteBatch.Draw(blueUI, new Rectangle(830, 24, player.mana, 12), new Rectangle(0, 0, 1, 1), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, player.mana.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(player.mana.ToString()).Length() / 2) + 1, (24 - 5) + 1), Color.Black);
            spriteBatch.DrawString(consolas, player.mana.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(player.mana.ToString()).Length() / 2), (24 - 5)), Color.White);
            spriteBatch.Draw(yellowUI, new Rectangle(830, 43, player.exp, 12), new Rectangle(0, 0, 1, 1), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, player.exp.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(player.exp.ToString()).Length() / 2) + 1, (43 - 5) + 1), Color.Black);
            spriteBatch.DrawString(consolas, player.exp.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(player.exp.ToString()).Length() / 2), (43 - 5)), Color.White);
            spriteBatch.Draw(redUI, new Rectangle(825, 65, 50, player.health), new Rectangle(0, 0, 1, 1), Color.White, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, player.health.ToString(), new Vector2((825 - 23) - (consolas.MeasureString(player.health.ToString()).Length() / 2) + 1, (65 - 25) - (consolas.MeasureString(player.health.ToString()).Y / 2) + 1), Color.Black);
            spriteBatch.DrawString(consolas, player.health.ToString(), new Vector2((825 - 23) - (consolas.MeasureString(player.health.ToString()).Length() / 2), (65 - 25) - (consolas.MeasureString(player.health.ToString()).Y / 2)), Color.White);
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

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            
            currentLoc.Draw(gameTime, spriteBatch);

            player.Draw(gameTime, spriteBatch);

            drawProjectiles(gameTime);

            drawBuildings(gameTime);

            DrawUI();

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
        
        #endregion

        #region Methods

        public void Teleport(Portal p)
        {
            currentLoc = p.dest;
        }

        public void deleteFriendly(int index)
        {
            
            friendlyShots.RemoveAt(index);
            for (int i = 0; i < friendlyShots.Count; i++)
            {
                friendlyShots[i].setIndex(i);
            }
        }

        public void deleteEnemy(int index)
        {
            enemyShots.RemoveAt(index);
            for (int i = 0; i < enemyShots.Count; i++)
            {
                enemyShots[i].setIndex(i);
            }
        }

        #endregion

    }
}