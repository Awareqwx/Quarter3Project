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
        Texture2D popFG, popBG, bgBorder, red;
        SpriteBatch spriteBatch;
        MouseState mouseState, prevMouseState;
        Vector2 mousePos, posDisplacement, sizeDisplacement, newPos;
        SpriteFont Consolas;

        public List<ItemData.pop> popList;
        public List<ItemData.btn> btnList;
        string stringList;
        public int idn = 0, prevIDN;
        ButtonEvents bE;
        bool lockMove;

        public PopManager(Game1 G)
            : base(G)
        {
            g = G;
        }

        public override void Initialize()
        {
            popList = new List<ItemData.pop>();
            btnList = new List<ItemData.btn>();
            bE = new ButtonEvents(g);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bgBorder = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            bgBorder.SetData<Color>(new Color[] { new Color(0, 0, 0) });
            popBG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popBG.SetData<Color>(new Color[] { new Color(255, 255, 255) });
            popFG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popFG.SetData<Color>(new Color[] { new Color(110, 110, 110) });
            red = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            red.SetData<Color>(new Color[] { new Color(255, 0, 0) });
            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            /*
            popList.Add(new ItemData.pop(popFG, new Vector2(0, 0), new Point(600, 250), false, "Do you want to quit the game?", 2000, 1));
            popList.Add(new ItemData.pop(popFG, new Vector2(0, 0), new Point(600, 250), false, "Any previous games that have been created will be \ndeleted if you continue.", 2001, 1));
            popList.Add(new ItemData.pop(popFG, new Vector2(0, 0), new Point(600, 250), false, "Do you want to return to the menu?", 2002, 1));
            popList.Add(new ItemData.pop(popFG, new Vector2(0, 0), new Point(600, 250), false, "There is no save game detected, create a character \nfirst before starting the game.", 2003, 2));
            */

            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Are you sure you want to quit?" }, false, false, false, 2000));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Any previous games that have been created will be \ndeleted if you continue." }, false, false, false, 2001));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Do you want to return to the menu?" }, false , false, false, 2002));

            for (int i = 0; i < popList.Count; i++)
            {
                switch (popList[i].getBoxType)
                {
                    case 0:
                        switch(popList[i].getID) {
                            case 2000:
                            btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Yes", Color.White, false, false, true, 2000, 2000));
                            btnList.Add(new ItemData.btn(red, new Vector2(650, 400), new Point(50, 25), "No", Color.White, false, false, true, 2000, 2001));
                            btnList.Add(new ItemData.btn(red, new Vector2(195, 400), new Point(50, 25), "Cancel", Color.White, false, false, true, 2000, 2002));
                            break;
                            case 2001:
                                btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Yes", Color.White, false, false, true, 2001, 2000));
                                btnList.Add(new ItemData.btn(red, new Vector2(650, 400), new Point(50, 25), "No", Color.White, false, false, true, 2001, 2001));
                                btnList.Add(new ItemData.btn(red, new Vector2(195, 400), new Point(50, 25), "Cancel", Color.White, false, false, true, 2001, 2002));
                            break;
                            case 2002:
                                btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Yes", Color.White, false, false, true, 2002, 2000));
                                btnList.Add(new ItemData.btn(red, new Vector2(650, 400), new Point(50, 25), "No", Color.White, false, false, true, 2002, 2001));
                                btnList.Add(new ItemData.btn(red, new Vector2(195, 400), new Point(50, 25), "Cancel", Color.White, false, false, true, 2002, 2002));
                                break;
                        }
                        break;
                }
            }
               
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

            for (int b = 0; b < btnList.Count; b++)
            {
                if (btnList[b].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    btnList[b] = new ItemData.btn(btnList[b].btnTexture, btnList[b].position, btnList[b].size, btnList[b].text, btnList[b].color, true, btnList[b].hover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                }
                else if (!btnList[b].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                {
                    btnList[b] = new ItemData.btn(btnList[b].btnTexture, btnList[b].position, btnList[b].size, btnList[b].text, btnList[b].color, false, btnList[b].hover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                }
            }

            for (int i = 0; i < popList.Count; i++)
            {
                if (popList[i].getID == idn && popList[i].getVis == false)
                {
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID);
                }

                if (popList[i].getID == idn && popList[i].getVis == true)
                {

                    if (popList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                    {
                        popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, popList[i].getVis, true, popList[i].getHoverState, popList[i].getID);
                    }
                    else if (!popList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                    {
                        popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, popList[i].getVis, false, popList[i].getHoverState, popList[i].getID);
                    }

                     for(int b = 0; b < btnList.Count; b++)
                     {
                         if (mouseState.LeftButton == ButtonState.Pressed && btnList[b].hover == true)
                         {
                             lockMove = true;
                         }
                         else if (mouseState.LeftButton == ButtonState.Released && btnList[b].hover == false)
                         {
                             lockMove = false;
                         }

                         if (mouseState.LeftButton == ButtonState.Pressed && popList[i].getHoverState == true && btnList[b].hover == false && lockMove == false)
                         {
                             if (prevMouseState.LeftButton == ButtonState.Released)
                             {
                                 posDisplacement = new Vector2(mousePos.X - popList[i].getPos.X, mousePos.Y - popList[i].getPos.Y);
                                 sizeDisplacement = new Vector2((popList[i].getPos.X + popList[i].getSize.X) - mousePos.X, (popList[i].getPos.Y + popList[i].getSize.Y) - mousePos.Y);
                             }

                             if ((int)mousePos.X - posDisplacement.X < 0 - (popList[i].getSize.X / 2))
                             {
                                 newPos.X = popList[i].getPos.X;
                             }
                             else if ((int)mousePos.X + sizeDisplacement.X > GraphicsDevice.Viewport.Width + (popList[i].getSize.X / 2))
                             {
                                 newPos.X = popList[i].getPos.X;
                             }
                             else
                             {
                                 newPos.X = mousePos.X - posDisplacement.X;
                             }

                             if ((int)mousePos.Y - posDisplacement.Y < 0 - (popList[i].getSize.Y / 2))
                             {
                                 newPos.Y = popList[i].getPos.Y;
                             }
                             else if ((int)mousePos.Y + sizeDisplacement.Y > GraphicsDevice.Viewport.Height + (popList[i].getSize.Y / 2))
                             {
                                 newPos.Y = popList[i].getPos.Y;
                             }
                             else
                             {
                                 newPos.Y = mousePos.Y - posDisplacement.Y;
                             }

                             popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, newPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, popList[i].getVis, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID);

                             if (btnList[b].id == popList[i].getID)
                             {
                                 switch (btnList[b].uniqueid)
                                 {
                                     case 2000: // Yes
                                         btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 65, newPos.Y + popList[i].getSize.Y - 35), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                         break;
                                     case 2001: // No
                                         btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 130, newPos.Y + popList[i].getSize.Y - 35), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                         break;
                                     case 2002: // Cancel
                                         btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + 15, newPos.Y + popList[i].getSize.Y - 35), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                         break;
                                 }
                             }                       
                         }

                         if (btnList[b].hover == true && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                         {
                             switch(popList[i].getID) {
                                 case 2000:
                                 switch (btnList[b].uniqueid)
                                 {
                                     case 2000: // Yes
                                         bE.exitGame();
                                         break;
                                     case 2001: // No
                                         bE.closePop();
                                         break;
                                     case 2002: // Cancel
                                         bE.closePop();
                                         break;
                                 }
                                 break;
                                 case 2001:
                                 switch (btnList[b].uniqueid)
                                 {
                                     case 2000: // Yes
                                         bE.saveNewChr();
                                         break;
                                     case 2001: // No
                                         bE.closePop();
                                         break;
                                     case 2002: // Cancel
                                         bE.closePop();
                                         break;
                                 }
                                 break;
                                case 2002:
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2000: // Yes
                                            bE.exitToMenu();
                                            break;
                                        case 2001: // No
                                            bE.closePop();
                                            break;
                                        case 2002: // Cancel
                                            bE.closePop();
                                            break;
                                    }
                                 break;
                            }
                         } 
                    }
                }
            }

            prevMouseState = mouseState;
            prevIDN = idn;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (ItemData.pop p in popList)
            {
                if (p.getVis)
                {
                    spriteBatch.Draw(p.getBGTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, p.getSize.X, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, 1, 250), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, 600, 1), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X + p.getSize.X, (int)p.getPos.Y, 1, 250), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y + p.getSize.Y, 600, 1), Color.White);
                    foreach (String s in p.getText)
                    {
                        spriteBatch.DrawString(Consolas, s, new Vector2(p.getPos.X + 10, p.getPos.Y + 10), Color.White);
                    }

                    for (int i = 0; i < btnList.Count; i++)
                    {
                        if (btnList[i].id == p.getID && btnList[i].visible == true)
                        {
                            spriteBatch.Draw(btnList[i].btnTexture, new Rectangle((int)btnList[i].position.X, (int)btnList[i].position.Y, btnList[i].size.X, btnList[i].size.Y), Color.White);
                            spriteBatch.DrawString(Consolas, btnList[i].text, new Vector2(((int)btnList[i].position.X + (btnList[i].size.X / 2)) - (Consolas.MeasureString(btnList[i].text).Length() / 2), ((int)btnList[i].position.Y + (btnList[i].size.Y / 2)) - (Consolas.MeasureString(btnList[i].text).Y / 2)), Color.White);

                        }
                    }
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
