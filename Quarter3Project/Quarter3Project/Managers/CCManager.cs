using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Quarter3Project.Managers
{
    public class CCManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D ccImage, ccStats, ccBase, ccClass, ccArrows, menu, menuOpts;
        Game1 myGame;
        GameManager gm;
        float timer;
        Keys[] lastKeys;
        KeyboardState keyboardState, lastKeyboardState;
        SpriteFont consolas, consolassmall;

        Keys[] k = { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.Space, Keys.Back };

        public CCUI[] ccui;
        public Home[] home;

        double timer2;

        public CCManager(Game1 game, GameManager g)
            : base(game)
        {
            myGame = game;
            gm = g;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            ccClass = Game.Content.Load<Texture2D>(@"Images/ccClass");
            ccImage = Game.Content.Load<Texture2D>(@"Images/ccImage");
            ccArrows = Game.Content.Load<Texture2D>(@"Images/ccArrows");
            ccStats = Game.Content.Load<Texture2D>(@"Images/ccStats");
            ccBase = Game.Content.Load<Texture2D>(@"Images/ccBase");
            consolas = Game.Content.Load<SpriteFont>(@"Fonts/consolas");
            consolassmall = Game.Content.Load<SpriteFont>(@"Fonts/consolassmall");
            menu = Game.Content.Load<Texture2D>(@"Images/Menu");
            menuOpts = Game.Content.Load<Texture2D>(@"Images/menuOpts");

            ccui = new CCUI[2];
            home = new Home[3];

            ccui[0] = new CCUI(myGame, new Vector2(327, 130), ccClass);
            
            home[0] = new Home(myGame, ccArrows, new Vector2(305, 145), "7");
            home[1] = new Home(myGame, ccArrows, new Vector2(370, 145), "8");
            home[2] = new Home(myGame, menuOpts, new Vector2(600, 460), "9");
            
            base.LoadContent();
        }

        private void HandleKey(GameTime gameTime, Keys currentKey)
        {
            string keyString = currentKey.ToString();
            if (gm.name.Length < 20)
            {
                if (currentKey == Keys.Space)
                    gm.name += " ";
                else if ((currentKey == Keys.Back || currentKey == Keys.Delete) && gm.name.Length > 0)
                    gm.name = gm.name.Remove(gm.name.Length - 1);
                else if (currentKey == Keys.Enter)
                    return;
                for (int i = 0; i < 26; i++)
                {
                    if (currentKey == k[i])
                        gm.name += keyString;
                }
            }
            //Set the timer to the current time
            timer2 = gameTime.TotalGameTime.TotalMilliseconds;
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            for (int i = 0; i < home.Length; i++)
            {
                home[i].Update(gameTime);
            }

            //Get the current keyboard state and keys that are pressed
            Keys[] keys = keyboardState.GetPressedKeys();

            foreach (Keys currentKey in keys)
            {
                if (currentKey != Keys.None)
                {
                    if (lastKeys.Contains(currentKey))
                    {
                        if ((gameTime.TotalGameTime.TotalMilliseconds - timer2 > 15000))
                            HandleKey(gameTime, currentKey);
                    }

                    else if (!lastKeys.Contains(currentKey))
                        HandleKey(gameTime, currentKey);
                }
            }

            //Save the last keys and pressed keys array
            lastKeyboardState = keyboardState;
            lastKeys = keys;


            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= .1)
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    myGame.buttonPressed = 6;
                    myGame.SetCurrentLevel(Game1.GameLevels.UI);
                }
                timer = 0;
            }

            ccui[0].Update(gameTime);

            if (myGame.currentChar == 0)
            {
                myGame.currentChar = 3;
            }
            else if (myGame.currentChar == 4)
            {
                myGame.currentChar = 1;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(menu, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ccImage, new Vector2(300, 110), Color.White);
            spriteBatch.Draw(ccBase, new Vector2(405, 110), Color.White);
            spriteBatch.Draw(ccStats, new Vector2(300, 215), Color.White);
            spriteBatch.DrawString(consolas, myGame.buttonPressed.ToString(), new Vector2(50, 110), Color.Cyan);
            spriteBatch.DrawString(consolas, "Stats", new Vector2(305, 220), Color.Blue);
            spriteBatch.DrawString(consolassmall, "Enter your name: ", new Vector2(410, 400), Color.Blue);
            spriteBatch.DrawString(consolas, "" + gm.name, new Vector2(530, 395), Color.Red);
            if (myGame.currentChar == 3)
            {
                spriteBatch.DrawString(consolassmall, "\n\nWep Atk: 30\n\nMag Atk: 10\n\nMag Def: 5\n\nWep Def: 15", new Vector2(305, 220), Color.Blue);
                spriteBatch.DrawString(consolassmall, "This is a Knight. Overpowered tank.", new Vector2(415, 140), Color.Blue);
                spriteBatch.DrawString(consolas, "Knight", new Vector2(410, 120), Color.Blue);
            }
            else if (myGame.currentChar == 2)
            {
                spriteBatch.DrawString(consolassmall, "\n\nWep Atk: 10\n\nMag Atk: 20\n\nMag Def: 15\n\nWep Def: 10", new Vector2(305, 220), Color.Blue);
                spriteBatch.DrawString(consolassmall, "This is a Cleric. Complete garbage.", new Vector2(415, 140), Color.Blue);
                spriteBatch.DrawString(consolas, "Cleric", new Vector2(410, 120), Color.Blue);
            }
            else if (myGame.currentChar == 1)
            {
                spriteBatch.DrawString(consolassmall, "\n\nWep Atk: 5\n\nMag Atk: 25\n\nMag Def: 15\n\nWep Def: 10", new Vector2(305, 220), Color.Blue);
                spriteBatch.DrawString(consolassmall, "This is a Wizard. Overpowered.", new Vector2(415, 140), Color.Blue);
                spriteBatch.DrawString(consolas, "Wizard", new Vector2(410, 120), Color.Blue);
            }
            ccui[0].Draw(gameTime, spriteBatch);

            for (int i = 0; i < home.Length; i++)
            {
                home[i].Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}
