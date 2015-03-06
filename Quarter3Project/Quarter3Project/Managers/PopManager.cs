using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.Managers
{
    public class PopManager : DrawableGameComponent
    {
        Game1 g;
        Texture2D popFG, popBG, bgBorder;
        SpriteBatch spriteBatch;
        MouseState mouseState, prevMouseState;
        Vector2 mousePos;
        SpriteFont Consolas;

        public List<PopUp.pop> popList;
        public List<Button.btn> btnList;
        int idn = 0;
        ButtonEvents bE;

        public PopManager(Game1 G) : base(G)
        {
            g = G;
            popList = new List<PopUp.pop>();
            btnList = new List<Button.btn>();
            bE = new ButtonEvents(G);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bgBorder = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            bgBorder.SetData<Color>(new Color[] { new Color(0, 0, 0) });
            popBG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popBG.SetData<Color>(new Color[] { new Color(255, 255, 255) });
            popFG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popFG.SetData<Color>(new Color[] { new Color(0, 136, 255) });

            popList.Add(new PopUp.pop(popFG, new Vector2(0, 0), new Point(600, 250), false, "Do you want to quit the game?", 2000));
            popList.Add(new PopUp.pop(popFG, new Vector2(0, 0), new Point(600, 250), false, "Any previous games that have been created will be \ndeleted if you continue.", 2001));           

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            base.LoadContent();
        }

        public void showPop(int id)
        {
            idn = id;
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            foreach (PopUp.pop b in popList)
            {
                if (b.visible == true)
                {
                    btnList.Add(new Button.btn(popBG, new Vector2(((GraphicsDevice.Viewport.Width / 2) + b.size.X / 2) - 20, ((GraphicsDevice.Viewport.Height / 2) - b.size.Y / 2)), new Point(20, 20), "X", Color.Red, false, false));
                    btnList.Add(new Button.btn(popBG, new Vector2(((GraphicsDevice.Viewport.Width / 2) + (b.size.X / 2)) - 80, ((GraphicsDevice.Viewport.Height / 2) + ((b.size.Y / 2) - 40))), new Point(60, 30), "Yes", Color.Blue, false, false));
                    btnList.Add(new Button.btn(popBG, new Vector2(((GraphicsDevice.Viewport.Width / 2) + (b.size.X / 2)) - 160, ((GraphicsDevice.Viewport.Height / 2) + ((b.size.Y / 2) - 40))), new Point(60, 30), "No", Color.Red, false, false));
                }
            }

            for (int i = 0; i < popList.Count; i++)
            {
                if(popList[i].id == idn)
                    popList[i] = new PopUp.pop(popList[i].texture, popList[i].position, popList[i].size, true, popList[i].text, popList[i].id);
            }

            if (btnList.Count > 0)
            {
                foreach (Button.btn b in btnList)
                {
                    if (b.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                        {                            
                            switch (b.text.ToUpper())
                            {
                                case "X":
                                    for (int i = 0; i < popList.Count; i++)
                                    {
                                        switch (popList[i].id)
                                        {
                                            case 2000:
                                                idn = 0;
                                                bE.closePop();
                                                break;
                                            case 2001:
                                                idn = 0;
                                                bE.closePop();
                                                break;
                                        }
                                    }                                    
                                    break;
                                case "YES":
                                    for (int i = 0; i < popList.Count; i++)
                                    {
                                        if (popList[i].visible == true)
                                        {
                                            switch (popList[i].id.ToString())
                                            {
                                                case "2000":
                                                    bE.exitGame();
                                                    break;
                                                case "2001":
                                                    idn = 0;
                                                    bE.saveNewChr();
                                                    bE.closePop();
                                                    break;
                                            }
                                        }
                                    }  
                                    
                                    break;
                                case "NO":
                                    for (int i = 0; i < popList.Count; i++)
                                    {
                                        switch (popList[i].id)
                                        {
                                            case 2000:
                                                idn = 0;
                                                bE.closePop();
                                                break;
                                            case 2001:
                                                idn = 0;
                                                bE.closePop();
                                                break;
                                        }
                                    }  
                                    break;
                            }
                        }
                    }
                }
            }

            if (idn == 0)
            {
                btnList.Clear();
            }

            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (PopUp.pop p in popList)
            {
                if (p.visible == true) {
                    spriteBatch.Draw(popBG, new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((p.size.X + 4) / 2), (GraphicsDevice.Viewport.Height / 2) - ((p.size.Y + 4) / 2), p.size.X + 4, p.size.Y + 4), Color.White);
                    spriteBatch.Draw(p.texture, new Rectangle((GraphicsDevice.Viewport.Width / 2) - (p.size.X / 2), (GraphicsDevice.Viewport.Height / 2) - (p.size.Y / 2), p.size.X, p.size.Y), Color.White);
                    spriteBatch.Draw(bgBorder, new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((p.size.X + 4) / 2), (GraphicsDevice.Viewport.Height / 2) - ((p.size.Y + 4) / 2), 1, p.size.Y + 4), Color.White);
                    spriteBatch.Draw(bgBorder, new Rectangle(((GraphicsDevice.Viewport.Width / 2) + ((p.size.X + 2) / 2)), (GraphicsDevice.Viewport.Height / 2) - ((p.size.Y + 4) / 2), 1, p.size.Y + 4), Color.White);
                    spriteBatch.Draw(bgBorder, new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((p.size.X + 4) / 2), (GraphicsDevice.Viewport.Height / 2) - ((p.size.Y + 4) / 2), p.size.X + 4, 1), Color.White);
                    spriteBatch.Draw(bgBorder, new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((p.size.X + 4) / 2), (GraphicsDevice.Viewport.Height / 2) + ((p.size.Y + 2) / 2), p.size.X + 4, 1), Color.White);
                    spriteBatch.DrawString(Consolas, p.text, new Vector2((GraphicsDevice.Viewport.Width / 2) - (p.size.X / 2) + 11, (GraphicsDevice.Viewport.Height / 2) - (p.size.Y / 2) + 11), Color.Black);
                    spriteBatch.DrawString(Consolas, p.text, new Vector2((GraphicsDevice.Viewport.Width / 2) - (p.size.X / 2) + 10, (GraphicsDevice.Viewport.Height / 2) - (p.size.Y / 2) + 10), Color.White);
                }
            }

            

            foreach (Button.btn b in btnList)
            {
                if (b.text == "X")
                {
                    spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X - 1, (int)b.position.Y, b.size.X + 1, b.size.Y + 1), new Color(151, 0, 0));
                    spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);
                }
                else
                {
                    spriteBatch.Draw(b.btnTexture, new Rectangle((int)b.position.X, (int)b.position.Y, b.size.X, b.size.Y), b.color);
                }

                if (b.text.Length == 1)
                {
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 4), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.Black);
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 4), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 3), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.Black);
                    spriteBatch.DrawString(Consolas, b.text, new Vector2(((int)b.position.X + (b.size.X / 2)) - (Consolas.MeasureString(b.text).Length() / 3), ((int)b.position.Y + b.size.Y / 2) - (Consolas.MeasureString(b.text).Y / 2)), Color.White);
                }             
                
            }      
            


                spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
