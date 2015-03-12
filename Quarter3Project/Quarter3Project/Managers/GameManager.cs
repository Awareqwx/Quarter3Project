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

namespace Quarter3Project
{

    public class GameManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public BuildingEntity[] bE;
        public ButtonEvents bE2;
        public GlobalEvents gE;
        public Collision.mapSegment[] playerSegments = new Collision.mapSegment[4];
        Game1 myGame;
        public int classType, prevClassType, health, mana, exp;
        KeyboardState keyBoardState, prevKeyState;
        public List<Attack> enemyShots, friendlyShots;
        public List<Collision.mapSegment> buildingSegments, mapSegments;
        Random RNG;
        SpriteBatch spriteBatch;   
        public SpriteFont consolas;
        TestEnemy[] mooks;
        public TestEntity[] tests;
        Texture2D[] testTexture, mage, cler, warr, bT;
        Texture2D enemy, uiBG, redUI, blueUI, yellowUI, blackUI, transparentTex, blueTUI, redPotion, bluePotion;
        List<ItemData.btn> btnList;
        MouseState mouseState, prevMouseState;
        Vector2 mousePos;
        public List<ItemData.item> itemList;
        bool loadOnce = true;        

        public GameManager(Game1 game)
            : base(game)
        {
            myGame = game;
        }

        public override void Initialize()
        {
            buildingSegments = new List<Collision.mapSegment>();
            RNG = new Random();
            mapSegments = new List<Collision.mapSegment>();
            enemyShots = new List<Attack>();
            friendlyShots = new List<Attack>();
            bE2 = new ButtonEvents(myGame); //ButtonEvents bE2, BuildingEntity bE
            gE = new GlobalEvents(myGame);
            btnList = new List<ItemData.btn>();
            itemList = new List<ItemData.item>();

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

                if (loadOnce == true)
                {                   

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

                    tests = new TestEntity[1];
                    bE = new BuildingEntity[1];
                    mooks = new TestEnemy[1];  

                    for (int i = 0; i < tests.Length; i++)
                        tests[i] = new TestEntity(this, testTexture, new Vector2(10, 10));

                    for (int i = 0; i < mooks.Length; i++)
                        mooks[i] = new TestEnemy(this, enemy, new Vector2(RNG.Next(0, 540), RNG.Next(0, 380)));

                    for (int i = 0; i < bE.Length; i++)
                        bE[i] = new BuildingEntity(this, bT, new Vector2(650, 450));

                    btnList.Add(new ItemData.btn(transparentTex, new Vector2(875, 67), new Point(25, 25), "X", Color.White, false, false, true, 300, 300));
                    btnList.Add(new ItemData.btn(transparentTex, new Vector2(853, 67), new Point(25, 25), "Y", Color.White, false, false, true, 300, 301));

                    itemList.Add(new ItemData.item(redPotion, 20000000, "Potion of Healing", blueTUI, "This is a Potion of Healing. It will \nheal you for 5 Health.", false));
                    itemList.Add(new ItemData.item(bluePotion, 20000001, "Potion of Mana", blueTUI, "This is a Potion of Mana. It will \ngive you 5 Mana.", false));
                    itemList.Add(new ItemData.item(redPotion, 20000002, "Potion of Superior Healing", blueTUI, "This is a Potion of Superior Healing. It will \nheal you for 50 Health.", false));
                    itemList.Add(new ItemData.item(bluePotion, 20000003, "Potion of Superior Mana", blueTUI, "This is a Potion of Superior Mana. It will \ngive you for 50 Mana.", false));

                    gE.addItemToInv(20000000, 5);
                    gE.addItemToInv(20000001, 8);
                    gE.addItemToInv(20000002, 1);
                    gE.addItemToInv(20000003, 1);

                    loadOnce = false;
                }                
                                
            }



            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyBoardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y; 
                  
            if (keyBoardState.IsKeyDown(Keys.D1) && prevKeyState.IsKeyUp(Keys.D1))
            {
                gE.addItemToInv(20000000, 1); // health potion
            }
            else if (keyBoardState.IsKeyDown(Keys.D2) && prevKeyState.IsKeyUp(Keys.D2))
            {
                gE.addItemToInv(20000001, 1); // mana potion
            }
            else if (keyBoardState.IsKeyDown(Keys.D3) && prevKeyState.IsKeyUp(Keys.D3))
            {
                gE.addItemToInv(20000002, 1); // super health potion
            }
            else if (keyBoardState.IsKeyDown(Keys.D4) && prevKeyState.IsKeyUp(Keys.D4))
            {
                gE.addItemToInv(20000003, 1); // super mana potion
            }


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
                                bE2.showPop(2004);
                                break;
                            case 301:
                                bE2.showPop(2005);
                                break;
                        }
                    }
                }
            }

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

            prevMouseState = mouseState;
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
            spriteBatch.Draw(uiBG, new Rectangle((GraphicsDevice.Viewport.Width - 204), 10, 194, 81), Color.White);

            for(int i = 0; i < btnList.Count; i++)
            {
                if (btnList[i].visible == true)
                {
                    spriteBatch.Draw(btnList[i].btnTexture, new Rectangle((int)btnList[i].position.X, (int)btnList[i].position.Y, btnList[i].size.X, btnList[i].size.Y), Color.White);
                    spriteBatch.DrawString(consolas, btnList[i].text, new Vector2(((int)btnList[i].position.X + (btnList[i].size.X / 2)) - (consolas.MeasureString(btnList[i].text).Length() / 4), ((int)btnList[i].position.Y + (btnList[i].size.Y / 2)) - (consolas.MeasureString(btnList[i].text).Y / 2)), Color.White);

                }
            }

        }
    }
}