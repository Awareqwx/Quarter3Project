using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.Managers
{
    class MenuManager : DrawableGameComponent
    {
        ButtonEvents bE;
        float[] RotationAngle = { 0, 0, 0 };
        Game1 g;
        List<ItemData.btn> buttonList;
        MouseState mouseState, prevMouseState;
        Random r;
        SpriteBatch spriteBatch;
        SpriteFont Consolas;
        Texture2D[] r1;
        Texture2D btn, gameBG;        
        Vector2 mousePos;

        public MenuManager(Game1 G)
            : base(G)
        {
            g = G;
            r = new Random();
            bE = new ButtonEvents(g);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            buttonList = new List<ItemData.btn>();
            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");
            btn = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            r1 = new Texture2D[] { new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color), new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color), new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color) };
            gameBG = Game.Content.Load<Texture2D>(@"Images/gameBG");

            btn.SetData<Color>(new Color[] { new Color(r.Next(0, 255) / 100, r.Next(0, 255) / 100, r.Next(0, 255) / 100, 0.5f) });

            for (int i = 0; i < r1.Length; i++)
                r1[i].SetData<Color>(new Color[] { new Color(r.Next(0, 255) / 101, r.Next(0, 255) / 101, r.Next(0, 255) / 101, 0.5f) });

            buttonList.Add(new ItemData.btn(btn, new Vector2(100, 100), new Point(100, 50), "Create New", Color.White, false, false, true, 200, 201));
            buttonList.Add(new ItemData.btn(btn, new Vector2(100, 150), new Point(100, 50), "Start", Color.White, false, false, true, 200, 202));
            buttonList.Add(new ItemData.btn(btn, new Vector2(100, 200), new Point(100, 50), "Credits", Color.White, false, false, true, 200, 203));
            buttonList.Add(new ItemData.btn(btn, new Vector2(100, 250), new Point(100, 100), "Quit", Color.White, false, false, true, 200, 204));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            btnClick();
            btnHoverEvent();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            RotationAngle[0] += elapsed - .02f;
            RotationAngle[1] += elapsed - .05f;
            RotationAngle[2] += elapsed + .01f;
            float circle = MathHelper.Pi * 2;
            RotationAngle[0] = RotationAngle[0] % circle;
            RotationAngle[1] = RotationAngle[1] % circle;
            RotationAngle[2] = RotationAngle[2] % circle;

            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(gameBG, new Rectangle(0, 0, 960, 620), Color.White);

            DrawMenu();
            FancyShit();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Moves buttons in the buttonlist to the right by 10 pixels while the mouse is hovered over them
        /// </summary>
        public void btnHoverEvent()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    buttonList[i] = new ItemData.btn(buttonList[i].btnTexture, buttonList[i].position, buttonList[i].size, buttonList[i].text, buttonList[i].color, true, buttonList[i].hover, buttonList[i].visible, buttonList[i].id, buttonList[i].uniqueid);
                }
                else if (!buttonList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    buttonList[i] = new ItemData.btn(buttonList[i].btnTexture, buttonList[i].position, buttonList[i].size, buttonList[i].text, buttonList[i].color, false, buttonList[i].hover, buttonList[i].visible, buttonList[i].id, buttonList[i].uniqueid);
                }

                if (buttonList[i].hover == true && buttonList[i].phover == false)
                {
                    buttonList[i] = new ItemData.btn(buttonList[i].btnTexture, new Vector2(buttonList[i].position.X + 10, buttonList[i].position.Y), buttonList[i].size, buttonList[i].text, buttonList[i].color, buttonList[i].hover, buttonList[i].phover, buttonList[i].visible, buttonList[i].id, buttonList[i].uniqueid);
                }
                else if (buttonList[i].hover == false && buttonList[i].phover == true)
                {
                    buttonList[i] = new ItemData.btn(buttonList[i].btnTexture, new Vector2(buttonList[i].position.X - 10, buttonList[i].position.Y), buttonList[i].size, buttonList[i].text, buttonList[i].color, buttonList[i].hover, buttonList[i].phover, buttonList[i].visible, buttonList[i].id, buttonList[i].uniqueid);
                }
            }
        }

        /// <summary>
        /// If a button is pressed then it will launch a function under buttonEvents class.
        /// </summary>
        public void btnClick()
        {
            foreach (ItemData.btn b in buttonList)
            {
                if (b.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        switch (b.text.ToUpper())
                        {
                            case "QUIT":
                                bE.Quit();
                                break;
                            case "CREATE NEW":
                                bE.CreateNew();
                                break;
                            case "START":
                                bE.StartGame();
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws out the buttons in the buttonlist from the ItemData.btn struct
        /// </summary>
        public void DrawMenu()
        {
            foreach (ItemData.btn b in buttonList)
            {
                spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X + (b.size.X / 4), (int)b.position.Y + (b.size.Y / 4), b.size.X / (4 / 3), b.size.Y / (4 / 3)), new Color(0, 0, 0, .7f));
                spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);

                spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2) + 1, ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2) + 1), Color.Black);
                spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.White);
            }
        }


        /// <summary>
        /// Draws out the spinning rectangles stored in the r1 texture2d array
        /// </summary>
        public void FancyShit()
        {
            for (int i = 0; i < r1.Length; i++)
            {
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0), RotationAngle[i], new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0), RotationAngle[i] + 1, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0), RotationAngle[i] + 2, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0), RotationAngle[i] + 3, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0), RotationAngle[i] + 4, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0), RotationAngle[i] + 5, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }

            for (int i = 0; i < r1.Length; i++)
            {
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2), 100, 20), new Rectangle(0, 0, 1, 1), Color.White, RotationAngle[i], new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2), 100, 20), new Rectangle(0, 0, 1, 1), Color.White, RotationAngle[i] + 1, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2), 100, 20), new Rectangle(0, 0, 1, 1), Color.White, RotationAngle[i] + 2, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2), 100, 20), new Rectangle(0, 0, 1, 1), Color.White, RotationAngle[i] + 3, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2), 100, 20), new Rectangle(0, 0, 1, 1), Color.White, RotationAngle[i] + 4, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2), 100, 20), new Rectangle(0, 0, 1, 1), Color.White, RotationAngle[i] + 5, new Vector2(0, 0), SpriteEffects.None, 0.0f);

            }

        }
    }
}
