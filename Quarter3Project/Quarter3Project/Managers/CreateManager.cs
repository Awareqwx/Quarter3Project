using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.Managers
{
    public class CreateManager : DrawableGameComponent
    {
        Game1 g;
        SpriteBatch spriteBatch;
        Texture2D bg, btn, btn2, arrows, h, k, w;
        public List<ChrS.chr> chrSelection;
        List<Button.btn> btnList2;
        SpriteFont Consolas, ConsolasLarge;
        MouseState mouseState, prevMouseState;
        Vector2 mousePos;
        ButtonEvents bE;
        float supermansafetytimer = 0.0f;
        bool supermansafetylock = false;

        public CreateManager(Game1 G)
            : base(G)
        {
            g = G;
            chrSelection = new List<ChrS.chr>();
            btnList2 = new List<Button.btn>();
            bE = new ButtonEvents(G);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            bg.SetData<Color>(new Color[] { new Color(255, 255, 255, 1) });
            btn = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            btn.SetData<Color>(new Color[] { new Color(255, 0, 0) });
            btn2 = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            btn2.SetData<Color>(new Color[] { new Color(50, 155, 50) });

            arrows = Game.Content.Load<Texture2D>(@"Images/ccArrows");

            btnList2.Add(new Button.btn(arrows, new Vector2(0, 0), new Vector2(40, (GraphicsDevice.Viewport.Height / 2) - (200 / 2)), new Point(30, 200), "1", Color.White, false, false));
            btnList2.Add(new Button.btn(arrows, new Vector2(30, 0), new Vector2(880, (GraphicsDevice.Viewport.Height / 2) - (200 / 2)), new Point(30, 200), "2", Color.White, false, false));
            btnList2.Add(new Button.btn(btn, new Vector2(0, 0), new Point(50, 35), "Back", Color.White, false, false));
            btnList2.Add(new Button.btn(btn2, new Vector2(375, 420), new Point(200, 40), "Start", Color.White, false, false));

            h = Game.Content.Load<Texture2D>(@"Images/Healer");
            k = Game.Content.Load<Texture2D>(@"Images/Knight");
            w = Game.Content.Load<Texture2D>(@"Images/Wizard");

            chrSelection.Add(new ChrS.chr(h, new Vector2(0, 0), "Healer", "This is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.\nThis is the Healer.", 1, true));
            chrSelection.Add(new ChrS.chr(k, new Vector2(0, 0), "Knight", "This is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.\nThis is the Knight.", 2, false));
            chrSelection.Add(new ChrS.chr(w, new Vector2(0, 0), "Wizard", "This is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.\nThis is the Wizard.", 3, false));

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");
            ConsolasLarge = Game.Content.Load<SpriteFont>(@"Fonts/ConsolasLarge");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            foreach (Button.btn b in btnList2)
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
                            case "START":
                                bE.saveNewChr2();
                                break;
                        }
                    }
                }
            }

            if (supermansafetylock == true)
            {
                if (supermansafetytimer > 0)
                {
                    supermansafetytimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (supermansafetytimer <= 0)
                {
                    supermansafetylock = false;
                }
            }

            for (int i = 0; i < btnList2.Count; i++)
            {
                if (btnList2[i].collisionRect2().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        switch (btnList2[i].text.ToString())
                        {
                            case "1":
                                for (int s = 0; s < chrSelection.Count; s++)
                                {
                                    if (chrSelection[s].visible == true)
                                    {
                                        if (chrSelection[s].id == 1 && supermansafetylock == false)
                                        {
                                            chrSelection[0] = new ChrS.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, false);
                                            chrSelection[1] = new ChrS.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                        else if (chrSelection[s].id == 2 && supermansafetylock == false)
                                        {
                                            chrSelection[1] = new ChrS.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, false);
                                            chrSelection[2] = new ChrS.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                        else if (chrSelection[s].id == 3 && supermansafetylock == false)
                                        {
                                            chrSelection[2] = new ChrS.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, false);
                                            chrSelection[0] = new ChrS.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, true);
                                            supermansafetylock = true;
                                            supermansafetytimer = 0.4f;
                                        }
                                    }
                                }
                                    break;
                            case "2":
                                    for (int s = 0; s < chrSelection.Count; s++)
                                    {
                                        if (chrSelection[s].visible == true)
                                        {
                                            if (chrSelection[s].id == 1 && supermansafetylock == false)
                                            {
                                                chrSelection[2] = new ChrS.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, true);
                                                chrSelection[0] = new ChrS.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                            else if (chrSelection[s].id == 2 && supermansafetylock == false)
                                            {
                                                chrSelection[0] = new ChrS.chr(chrSelection[0].tex, chrSelection[0].pos, chrSelection[0].name, chrSelection[0].descr, chrSelection[0].id, true);
                                                chrSelection[1] = new ChrS.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                            else if (chrSelection[s].id == 3 && supermansafetylock == false)
                                            {
                                                chrSelection[1] = new ChrS.chr(chrSelection[1].tex, chrSelection[1].pos, chrSelection[1].name, chrSelection[1].descr, chrSelection[1].id, true);
                                                chrSelection[2] = new ChrS.chr(chrSelection[2].tex, chrSelection[2].pos, chrSelection[2].name, chrSelection[2].descr, chrSelection[2].id, false);
                                                supermansafetylock = true;
                                                supermansafetytimer = 0.4f;
                                            }
                                        }
                                    }
                                break;
                        }
                    }
                }
            }

                base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(bg, new Rectangle((GraphicsDevice.Viewport.Width / 2) - 400, (GraphicsDevice.Viewport.Height / 2) - 250, 800, 500), Color.White);

            foreach (Button.btn b in btnList2)
            {
                if (b.text == "" || b.text == "1" || b.text == "2")
                {
                    spriteBatch.Draw(bg, new Rectangle((int)b.position2.X, (int)b.position2.Y, 40, 200), Color.White);
                    spriteBatch.Draw(b.btnTexture, new Vector2(b.position2.X + 5, b.position2.Y), new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);
                }
                else
                {
                    spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2) + 1, ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2) + 1), Color.Black);
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 2), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.White);
                }               
            }



            foreach (ChrS.chr c in chrSelection)
            {
                if (c.visible == true)
                {
                    spriteBatch.DrawString(ConsolasLarge, c.name, new Vector2((GraphicsDevice.Viewport.Width / 2) - (Consolas.MeasureString(c.name).Length() / 2), (GraphicsDevice.Viewport.Height / 2) - 240), Color.Blue);
                    spriteBatch.DrawString(Consolas, c.descr, new Vector2(600, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Blue);
                    spriteBatch.DrawString(Consolas, "Skills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\nSkills & Stats\n", new Vector2(220, (GraphicsDevice.Viewport.Height / 2) - 200), Color.Blue);

                    spriteBatch.Draw(c.tex, new Rectangle(((GraphicsDevice.Viewport.Width / 2) + (int)c.pos.X) - (c.tex.Bounds.Width / 2), ((GraphicsDevice.Viewport.Height / 2) + (int)c.pos.Y) - (c.tex.Bounds.Height / 2), c.tex.Bounds.Width, c.tex.Bounds.Height), Color.White);
                }
            }

            spriteBatch.End();
 	         base.Draw(gameTime);
        }
    }
}
