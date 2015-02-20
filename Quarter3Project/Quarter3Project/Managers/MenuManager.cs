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
        Game1 g;
        SpriteBatch spriteBatch;
        SpriteFont Consolas;
        Texture2D[] r1;
        Texture2D btn;
        Random r;
        float[] RotationAngle = { 0, 0, 0 };
        MouseState mouseState, prevMouseState;
        Vector2 mousePos;

        List<Button.btn> buttonList;
        ButtonEvents bE;

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
            buttonList = new List<Button.btn>();

            btn = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            btn.SetData<Color>(new Color[] { new Color(r.Next(0, 255) / 100, r.Next(0, 255) / 100, r.Next(0, 255) / 100, 0.5f) });

            r1 = new Texture2D[] { new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color), new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color), new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color) };

            for (int i = 0; i < r1.Length; i++)
                r1[i].SetData<Color>(new Color[] { new Color(r.Next(0, 255) / 101, r.Next(0, 255) / 101, r.Next(0, 255) / 101, 0.5f) });

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            buttonList.Add(new Button.btn(btn, new Vector2(100, 100), new Point(100, 50), "Create New", Color.White, false, false));
            buttonList.Add(new Button.btn(btn, new Vector2(100, 150), new Point(100, 50), "Start", Color.White, false, false));
            buttonList.Add(new Button.btn(btn, new Vector2(100, 200), new Point(100, 50), "Credits", Color.White, false, false));
            buttonList.Add(new Button.btn(btn, new Vector2(100, 250), new Point(100, 100), "Quit", Color.White, false, false));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            foreach (Button.btn b in buttonList)
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

            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    buttonList[i] = new Button.btn(buttonList[i].btnTexture, buttonList[i].position, buttonList[i].size, buttonList[i].text, buttonList[i].color, true, buttonList[i].hover);
                }
                else if (!buttonList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    buttonList[i] = new Button.btn(buttonList[i].btnTexture, buttonList[i].position, buttonList[i].size, buttonList[i].text, buttonList[i].color, false, buttonList[i].hover);
                }

                if (buttonList[i].hover == true && buttonList[i].phover == false)
                {
                    buttonList[i] = new Button.btn(buttonList[i].btnTexture, new Vector2(buttonList[i].position.X + 10, buttonList[i].position.Y), buttonList[i].size, buttonList[i].text, buttonList[i].color, buttonList[i].hover, buttonList[i].phover);
                }
                else if (buttonList[i].hover == false && buttonList[i].phover == true)
                {
                    buttonList[i] = new Button.btn(buttonList[i].btnTexture, new Vector2(buttonList[i].position.X - 10, buttonList[i].position.Y), buttonList[i].size, buttonList[i].text, buttonList[i].color, buttonList[i].hover, buttonList[i].phover);
                }
            }

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

        public Color getBtn()
        {
            return buttonList[1].color;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawMenu();
            FancyShit();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawMenu()
        {
            foreach (Button.btn b in buttonList)
            {
                spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X + (b.size.X / 4), (int)b.position.Y + (b.size.Y / 4), b.size.X / (4 / 3), b.size.Y / (4 / 3)), new Color(0, 0, 0, .7f));
                spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);

                spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2) + 1, ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2) + 1), Color.Black);
                spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.White);
            }
        }

        public void FancyShit()
        {
            for (int i = 0; i < r1.Length; i++)
            {
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0, .7f), RotationAngle[i], new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0, .7f), RotationAngle[i] + 1, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0, .7f), RotationAngle[i] + 2, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0, .7f), RotationAngle[i] + 3, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0, .7f), RotationAngle[i] + 4, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(r1[i], new Rectangle((GraphicsDevice.Viewport.Width / 2) + (100 / 2), (GraphicsDevice.Viewport.Height / 2) + (20 / 2), 100, 20), new Rectangle(0, 0, 1, 1), new Color(0, 0, 0, .7f), RotationAngle[i] + 5, new Vector2(0, 0), SpriteEffects.None, 0.0f);
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
