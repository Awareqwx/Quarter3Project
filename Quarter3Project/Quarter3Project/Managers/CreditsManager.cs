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
using Quarter3Project.EventManagers;


namespace Quarter3Project.Managers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CreditsManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Game1 myGame;

        SpriteBatch spriteBatch;
        SpriteFont consolasLarge;

        KeyboardState keyBoardState,
                      prevKeyState;

        MouseState mouseState,
                   prevMouseState;

        Vector2 mousePos;

        Texture2D background;

        ButtonEvents bE;

        List<ItemData.btn> btnList;
        public GlobalEvents gE;
        private Texture2D btn;

        public CreditsManager(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here
            myGame = game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            bE = new ButtonEvents(myGame);

            gE = new GlobalEvents(myGame);

            btnList = new List<ItemData.btn>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            consolasLarge = Game.Content.Load<SpriteFont>(@"Fonts/ConsolasLarge");
            background = myGame.Content.Load<Texture2D>(@"Images/credits_screen");

            btn = Game.Content.Load<Texture2D>(@"Images/btnTex");

            btnList.Add(new ItemData.btn(btn, new Vector2(100, 450), new Point(100, 50), "Back", Color.White, false, false, true, 200, 210));

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            btnClick();
            btnHoverEvent();

            prevMouseState = mouseState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 960, 620), Color.White);
            spriteBatch.DrawString(consolasLarge, "CREDITS", new Vector2(450, 100), Color.Black);
            spriteBatch.DrawString(consolasLarge, "Backgrounds and Sprites\n    Cheryl Swanberg, Maddison Moore,\n    Kohl Johnson, and Trinity\n\nProgramming\n    Ben Peters, Gavin Hobbs, and Ryan\n\nProject Management\n    Gavin Hobbs\n", new Vector2(150, 175), Color.Black);
            
            foreach (ItemData.btn b in btnList)
            {
                spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X - 3, (int)b.position.Y - 3, b.size.X + 6, b.size.Y + 6), new Color(0, 0, 0, .7f));
                spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);

                spriteBatch.DrawString(consolasLarge, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (consolasLarge.MeasureString(b.text).Length() / 2) + 1, ((int)b.position.Y + b.size.Y / 2) - (consolasLarge.MeasureString(b.text).Y / 2) + 1), Color.Black);
                spriteBatch.DrawString(consolasLarge, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (consolasLarge.MeasureString(b.text).Length() / 2), ((int)b.position.Y + b.size.Y / 2) - (consolasLarge.MeasureString(b.text).Y / 2)), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void btnClick()
        {
            foreach (ItemData.btn b in btnList)
            {
                if (b.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        switch (b.text.ToUpper())
                        {
                            case "BACK":
                                bE.setMenu();
                                break;
                        }
                    }
                }
            }
        }

        public void btnHoverEvent()
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

                if (btnList[i].hover == true && btnList[i].phover == false)
                {
                    btnList[i] = new ItemData.btn(btnList[i].btnTexture, new Vector2(btnList[i].position.X + 10, btnList[i].position.Y), btnList[i].size, btnList[i].text, btnList[i].color, btnList[i].hover, btnList[i].phover, btnList[i].visible, btnList[i].id, btnList[i].uniqueid);
                }
                else if (btnList[i].hover == false && btnList[i].phover == true)
                {
                    btnList[i] = new ItemData.btn(btnList[i].btnTexture, new Vector2(btnList[i].position.X - 10, btnList[i].position.Y), btnList[i].size, btnList[i].text, btnList[i].color, btnList[i].hover, btnList[i].phover, btnList[i].visible, btnList[i].id, btnList[i].uniqueid);
                }
            }
        }

    }
}
