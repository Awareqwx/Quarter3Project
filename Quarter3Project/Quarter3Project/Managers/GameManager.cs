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
using Quarter3Project.Classes;

namespace Quarter3Project
{

    public class GameManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public ButtonEvents bE2;
        public Collision.mapSegment[] playerSegments = new Collision.mapSegment[4];
        Game1 myGame;
        public int classType, prevClassType, health, mana, exp;
        KeyboardState keyBoardState, prevKeyState;
        public List<Attack> enemyShots, friendlyShots;
        Random RNG;
        SpriteBatch spriteBatch;
        public SpriteFont consolas;
        public Player player;
        Texture2D[] testTexture, mage, cler, warr, bT;
        Texture2D enemy, uiBG, redUI, blueUI, yellowUI;

        public Location Test;

        public Location currentLoc;

        public GameManager(Game1 game)
            : base(game)
        {
            myGame = game;
            bE2 = new ButtonEvents(game); //ButtonEvents bE2, BuildingEntity bE
        }

        public override void Initialize()
        {
            RNG = new Random();
            enemyShots = new List<Attack>();
            friendlyShots = new List<Attack>();

            bE2.loadGame();

            addLocs();

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

                if (classType == 100)
                {
                    testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
                    cler = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") };
                }
                else if (classType == 200)
                {
                    testTexture = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
                    mage = new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") };
                }
                else if (classType == 300)
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

            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyBoardState = Keyboard.GetState();

            if (prevClassType != classType)
            {
                if (classType == 100)
                {
                    testTexture = cler;
                        player = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Healer") }, new Vector2(10, 10));
                }
                else if (classType == 200)
                {
                    testTexture = mage;
                        player = new TestEntity(this, new Texture2D[] { Game.Content.Load<Texture2D>(@"Images/Wizard"), Game.Content.Load<Texture2D>(@"Images/Wizard_C"), Game.Content.Load<Texture2D>(@"Images/Wizard_S") }, new Vector2(10, 10));
                }
                else if (classType == 300)
                {
                    testTexture = warr;
                        player = new Knight(testTexture, new Vector2(10, 10), this);
                }
            }

            if (keyBoardState.IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            {
                bE2.showPop(2002);
            }

            player.Update(gameTime);

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

            currentLoc.Update(gameTime);

            if (health <= 0)
            {
                player.health = 0;
            }
            else if (health >= 50)
            {
                player.health = 50;
            }

            if (mana <= 0)
            {
                player.mana = 0;
            }
            else if (mana >= 115)
            {
                player.mana = 115;
            }

            if (exp <= 0)
            {
                player.exp = 0;
            }
            else if (exp >= 115)
            {
                player.exp = 115;
            }

            prevKeyState = keyBoardState;
            prevClassType = classType;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            player.Draw(gameTime, spriteBatch);

            for (int i = 0; i < enemyShots.Count; i++)
                enemyShots[i].Draw(gameTime, spriteBatch);

            for (int i = 0; i < friendlyShots.Count; i++)
                friendlyShots[i].Draw(gameTime, spriteBatch);

            currentLoc.Draw(gameTime, spriteBatch);

            DrawUI();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawUI()
        {

            spriteBatch.Draw(blueUI, new Rectangle(830, 24, player.mana, 12), new Rectangle(0, 0, 1, 1), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, player.mana.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(mana.ToString()).Length() / 2) + 1, (24 - 5) + 1), Color.Black);
            spriteBatch.DrawString(consolas, player.mana.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(mana.ToString()).Length() / 2), (24 - 5)), Color.White);
            spriteBatch.Draw(yellowUI, new Rectangle(830, 43, player.exp, 12), new Rectangle(0, 0, 1, 1), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, player.exp.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(exp.ToString()).Length() / 2) + 1, (43 - 5) + 1), Color.Black);
            spriteBatch.DrawString(consolas, player.exp.ToString(), new Vector2((830 + 57) - (consolas.MeasureString(exp.ToString()).Length() / 2), (43 - 5)), Color.White);
            spriteBatch.Draw(redUI, new Rectangle(825, 65, 50, health), new Rectangle(0, 0, 1, 1), Color.White, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(consolas, player.health.ToString(), new Vector2((825 - 23) - (consolas.MeasureString(health.ToString()).Length() / 2) + 1, (65 - 25) - (consolas.MeasureString(health.ToString()).Y / 2) + 1), Color.Black);
            spriteBatch.DrawString(consolas, player.health.ToString(), new Vector2((825 - 23) - (consolas.MeasureString(health.ToString()).Length() / 2), (65 - 25) - (consolas.MeasureString(health.ToString()).Y / 2)), Color.White);
            spriteBatch.Draw(uiBG, new Rectangle((GraphicsDevice.Viewport.Width - 204), 10, 194, 59), Color.White);

        }

        public void Teleport(Portal p)
        {
            currentLoc = p.dest;

        }


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

            Test = new Location(ml, cl, el, p, bl, enemies);

            currentLoc = Test;
        }
    }
}