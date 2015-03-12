using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter3Project.EventManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.Managers
{
    public class PopManager : DrawableGameComponent
    {
        Game1 g;
        Texture2D popFG, popBG, bgBorder, red, empty;
        SpriteBatch spriteBatch;
        MouseState mouseState, prevMouseState;
        Vector2 mousePos, posDisplacement, sizeDisplacement, newPos;
        SpriteFont Consolas;

        public List<ItemData.pop> popList;
        public List<ItemData.btn> btnList;
        public List<ItemData.itemSpace> invList;
        public int idn = 0, prevIDN, popID;
        ButtonEvents bE;
        GlobalEvents gE;
        bool lockMove;
        private const int invSpace = 25;
        double doubleClickTime = 110;

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
            gE = new GlobalEvents(g);
            invList = new List<ItemData.itemSpace>();

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
            empty = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            empty.SetData<Color>(new Color[] { new Color(0, 0, 0, 0.3f) });
            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Are you sure you want to quit?" }, false, false, false, 2000, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Any previous games that have been created will be \ndeleted if you continue." }, false, false, false, 2001, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Do you want to return to the menu?" }, false, false, false, 2002, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 1, new string[] { "There is no save game detected, create a character \nfirst before starting the game." }, false, false, false, 2003, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (310 / 2), (GraphicsDevice.Viewport.Height / 2) - (360 / 2)), new Point(310, 360), 4, new string[] { "" }, false, false, false, 2004, new Vector2((GraphicsDevice.Viewport.Width / 2) - (155 / 2), (GraphicsDevice.Viewport.Height / 2) - (180 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (310 / 2), (GraphicsDevice.Viewport.Height / 2) - (360 / 2)), new Point(310, 360), 5, new string[] { "" }, false, false, false, 2005, new Vector2((GraphicsDevice.Viewport.Width / 2) - (155 / 2), (GraphicsDevice.Viewport.Height / 2) - (180 / 2)), 0));

            for (int c = 0; c < popList.Count; c++)
            {
                if (popList[c].getBoxType == 4)
                {
                    int p = 0;
                    int i = 1;
                    while (p < 25)
                    {
                        Vector2 pos = new Vector2(0, 0);
                        

                        if (i > 5)
                        {
                            i = 1;
                        }

                        if (i <= 5)
                        {
                            pos = new Vector2(popList[i].getPos.X + (i * 50), popList[i].getPos.Y + (i * 100));
                        }

                        invList.Add(new ItemData.itemSpace(empty, empty, pos, new Point(30, 30), 0, p, 0));
                        p += 1;
                        i += 1;
                    }
                }
            }

            for (int i = 0; i < popList.Count; i++)
            {
                switch (popList[i].getBoxType)
                {
                    case 0: // Yes or No
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
                    case 1: // Ok
                        switch (popList[i].getID)
                        {
                            case 2003:
                                btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Ok", Color.White, false, false, true, 2003, 2003));
                                break;
                        }
                        break;
                    case 2: // Next or Prev
                        break;
                    case 3: //Accept or Decline
                        break;
                    case 4: // Inventory
                        switch (popList[i].getID)
                        {
                            case 2004:
                                btnList.Add(new ItemData.btn(red, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X, (int)popList[i].getPos.Y), new Point(20, 20), "X", Color.White, false, false, true, 2004, 2004));
                                break;
                        }                    
                        break;
                    case 5: switch (popList[i].getID)
                        {
                            case 2005:
                                btnList.Add(new ItemData.btn(red, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X, (int)popList[i].getPos.Y), new Point(20, 20), "X", Color.White, false, false, true, 2005, 2004));
                                break;
                        }
                        break;
                }
            }
               
            base.LoadContent();
        }

        public void showPop(int id, List<ItemData.pop> popList)
        {
            idn = id;
            int pOrder = int.MinValue;

            for (int i = 0; i < popList.Count; i++)
            {
                if (id == popList[i].getID)
                {
                    pOrder = popList[i].getDrawOrder;
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList.Count - 1);
                }
            }

            if (pOrder == int.MinValue)
                return;

            for (int i = 0; i < popList.Count; i++)
            {
                if (popList[i].getID != id && popList[i].getDrawOrder > 0 && popList[i].getDrawOrder >= pOrder)
                {
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder - 1);
                }
            }

            for (int i = 0; i < popList.Count; i++)
            {
                for (int c = 0; c < btnList.Count; c++)
                {
                    if (btnList[c].id == popList[i].getID)
                    {
                        switch (btnList[c].uniqueid)
                        {
                            case 2000: // Yes
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 65, (int)popList[i].getPos.Y + popList[i].getSize.Y - 35), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2001: // No
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 130, (int)popList[i].getPos.Y + popList[i].getSize.Y - 35), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2002: // Cancel
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + 15, (int)popList[i].getPos.Y + popList[i].getSize.Y - 35), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2003: // Ok
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 65, (int)popList[i].getPos.Y + popList[i].getSize.Y - 35), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2004: // X
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 20, (int)popList[i].getPos.Y + 1), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                        }
                    }
                }
            }
        }

        public void inventoryUpdate()
        {
            for (int i = 0; i < g.popManager.invList.Count; i++)
            {
                if (invList[i].qty <= 0)
                {
                    invList[i] = new ItemData.itemSpace(empty, empty, invList[i].pos, new Point(30, 30), 0, invList[i].itemSpaceID, 0);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            inventoryUpdate();

            for (int i = 0; i < invList.Count; i++)
            {
                for (int c = 0; c < popList.Count; c++)
                {
                    if (popList[c].getBoxType == 4)
                    {
                        if (i <= 4)
                        {
                            invList[i] = new ItemData.itemSpace(invList[i].bgTex, invList[i].itemTex, new Vector2((int)popList[c].getPos.X + (i * 50) + 35, (int)popList[c].getPos.Y + (1 * 60)), invList[i].s, invList[i].itemID, invList[i].itemSpaceID, invList[i].qty);
                        }
                        else if (i <= 9)
                        {
                            invList[i] = new ItemData.itemSpace(invList[i].bgTex, invList[i].itemTex, new Vector2((int)popList[c].getPos.X + ((i - 5) * 50) + 35, (int)popList[c].getPos.Y + (2 * 60)), invList[i].s, invList[i].itemID, invList[i].itemSpaceID, invList[i].qty);
                        }
                        else if (i <= 14)
                        {
                            invList[i] = new ItemData.itemSpace(invList[i].bgTex, invList[i].itemTex, new Vector2((int)popList[c].getPos.X + ((i - 10) * 50) + 35, (int)popList[c].getPos.Y + (3 * 60)), invList[i].s, invList[i].itemID, invList[i].itemSpaceID, invList[i].qty);
                        }
                        else if (i <= 19)
                        {
                            invList[i] = new ItemData.itemSpace(invList[i].bgTex, invList[i].itemTex, new Vector2((int)popList[c].getPos.X + ((i - 15) * 50) + 35, (int)popList[c].getPos.Y + (4 * 60)), invList[i].s, invList[i].itemID, invList[i].itemSpaceID, invList[i].qty);
                        }
                        else if (i <= 24)
                        {
                            invList[i] = new ItemData.itemSpace(invList[i].bgTex, invList[i].itemTex, new Vector2((int)popList[c].getPos.X + ((i - 20) * 50) + 35, (int)popList[c].getPos.Y + (5 * 60)), invList[i].s, invList[i].itemID, invList[i].itemSpaceID, invList[i].qty);
                        }
                    }                    
                }
            }

            switch (g.CurrentLevel)
            {
                case GameLevels.GameLevels.CREATE:
                    for (int i = 0; i < popList.Count; i++)
                    {
                        if (popList[i].getVis == true)
                        {
                            g.createManager.Enabled = false;
                        }
                    }
                    break;
                case GameLevels.GameLevels.GAME:
                    for (int i = 0; i < popList.Count; i++)
                    {
                        if (popList[i].getVis == true)
                        {
                            switch (popList[i].getID)
                            {
                                case 2002:
                                    g.gameManager.Enabled = false;
                                    break;
                            }
                        }
                    }

                    break;
                case GameLevels.GameLevels.MENU:
                    for (int i = 0; i < popList.Count; i++)
                    {
                        if (popList[i].getVis == true)
                        {
                            g.menuManager.Enabled = false;
                        }
                    }
                    break;
            }
                
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
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder);
                }
            }

            for(int i = 0; i < popList.Count; i++) {
                if (popList[i].getVis == true)
                {
                    if (popList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                    {
                        popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, popList[i].getVis, true, popList[i].getHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder);
                    }
                    else if (!popList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                    {
                        popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, popList[i].getVis, false, popList[i].getHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder);
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && popList[i].getHoverState == true && popList.Count(p => p.getHoverState == true) == 1)
                    {
                        showPop(popList[i].getID, popList);
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && popList[i].getHoverState == true && popList.Any(p => p.getDrawOrder == popList.Count - 1 && p.getID != popList[i].getID && p.getHoverState == false) && popList.Any(p => p.getDrawOrder != popList.Count - 1 && p.getDrawOrder < popList[i].getDrawOrder) && popList.Count(p => p.getHoverState == true) > 1 && popList.Any(p => p.getHoverState == true && p.getID != popList[i].getID && popList[i].getDrawOrder > p.getDrawOrder))
                    {
                        if (popList.Count(p => p.getHoverState == true) > 1)
                        {
                            showPop(popList[i].getID, popList);
                        }
                    }
                }
            }

            for(int i = 0; i < popList.Count; i++) {
                if (popList[i].getVis == true && popList.Count(p => p.getDrawOrder > popList[i].getDrawOrder) == 0)
                {
                    for (int b = 0; b < btnList.Count; b++)
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

                            popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, newPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, popList[i].getVis, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder);

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
                                    case 2003: // Ok
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 65, newPos.Y + popList[i].getSize.Y - 35), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                    case 2004: // X
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 20, newPos.Y + 1), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                }
                            }
                        }

                        if (popList[i].getHoverState == true && btnList[b].hover == true && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                        {
                            switch (popList[i].getID)
                            {
                                case 2000: // Are you sure you want to quit the game?
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2000: // Yes
                                            bE.exitGame();
                                            break;
                                        case 2001: // No
                                            bE.closePop(2000);
                                            break;
                                        case 2002: // Cancel
                                            bE.closePop(2000);
                                            break;
                                    }
                                    break;
                                case 2001: // Any current save games will be overwritten if you continue.
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2000: // Yes
                                            bE.saveNewChr();
                                            break;
                                        case 2001: // No
                                            bE.closePop(2001);
                                            break;
                                        case 2002: // Cancel
                                            bE.closePop(2001);
                                            break;
                                    }
                                    break;
                                case 2002: // Are you sure you want to go back to the main menu?
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2000: // Yes
                                            bE.exitToMenu();
                                            break;
                                        case 2001: // No
                                            bE.closePop(2002);
                                            break;
                                        case 2002: // Cancel
                                            bE.closePop(2002);
                                            break;
                                    }
                                    break;
                                case 2003: // You haven't created any characters yet....
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2003: // Ok
                                            bE.closePop(2003);
                                            break;
                                    }
                                    break;
                                case 2004: // Inventory
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2004: // X
                                            bE.closePop(2004);
                                            break;
                                    }
                                    break;
                                case 2005:
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2004: // X
                                            bE.closePop(2005);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            doubleClickTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        const double doubleClickDelay = 138;
                        if (doubleClickTime < doubleClickDelay)
                        {
                            for (int i = 0; i < invList.Count; i++)
                            {
                                if (invList[i].itemID > 0)
                                {
                                    for (int z = 0; z < g.gameManager.itemList.Count; z++)
                                    {
                                        if (g.gameManager.itemList[z].iID == invList[i].itemID)
                                        {
                                            g.gameManager.itemList[z] = new ItemData.item(g.gameManager.itemList[z].iTex, g.gameManager.itemList[z].iID, g.gameManager.itemList[z].iName, g.gameManager.itemList[z].pBG, g.gameManager.itemList[z].pDesc, false);
                                        }
                                    }
                                }
                            }

                            for (int i = 0; i < invList.Count; i++)
                            {
                                if (invList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    gE.useItem(invList[i].itemID);
                                }
                            }
                        }
                        doubleClickTime = 0;
                    }

                    if (popList.Count(p => p.getBoxType == 4 && p.getVis == true && p.getDrawOrder == popList.Count - 1) == 1)
                    {
                        for (int i = 0; i < invList.Count; i++)
                        {
                            if (invList[i].itemID > 0)
                            {
                                if (invList[i].collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1)))
                                {
                                    for (int z = 0; z < g.gameManager.itemList.Count; z++)
                                    {
                                        if (g.gameManager.itemList[z].iID == invList[i].itemID)
                                        {
                                            g.gameManager.itemList[z] = new ItemData.item(g.gameManager.itemList[z].iTex, g.gameManager.itemList[z].iID, g.gameManager.itemList[z].iName, g.gameManager.itemList[z].pBG, g.gameManager.itemList[z].pDesc, true);
                                        }
                                    }
                                }

                                if (invList.Count(p => p.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1))) == 0)
                                {
                                    for (int z = 0; z < g.gameManager.itemList.Count; z++)
                                    {
                                        if (g.gameManager.itemList[z].iID == invList[i].itemID)
                                        {
                                            g.gameManager.itemList[z] = new ItemData.item(g.gameManager.itemList[z].iTex, g.gameManager.itemList[z].iID, g.gameManager.itemList[z].iName, g.gameManager.itemList[z].pBG, g.gameManager.itemList[z].pDesc, false);
                                            
                                        }
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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.DrawString(Consolas, popList.Count(p => p.getHoverState == true).ToString(), new Vector2(10, 10), Color.White);
            
            foreach (var p in popList.OrderBy(i => i.getDrawOrder))
            {
                if (p.getVis == true)
                {
                    spriteBatch.Draw(p.getBGTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, p.getSize.X, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, 1, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, p.getSize.X, 1), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X + p.getSize.X, (int)p.getPos.Y, 1, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y + p.getSize.Y, p.getSize.X, 1), Color.White);
                    spriteBatch.DrawString(Consolas, p.getDrawOrder.ToString(), new Vector2((int)p.getPos.X + 10, p.getPos.Y + 10), Color.White);
                    foreach (String s in p.getText)
                    {
                        spriteBatch.DrawString(Consolas, s, new Vector2(p.getPos.X + 10, p.getPos.Y + 50), Color.White);
                    }

                    for (int i = 0; i < btnList.Count; i++)
                    {
                        if (btnList[i].id == p.getID && btnList[i].visible == true)
                        {
                            spriteBatch.Draw(btnList[i].btnTexture, new Rectangle((int)btnList[i].position.X, (int)btnList[i].position.Y, btnList[i].size.X, btnList[i].size.Y), Color.White);
                            if (btnList[i].text.Length == 1)
                            {
                                spriteBatch.DrawString(Consolas, btnList[i].text, new Vector2(((int)btnList[i].position.X + (btnList[i].size.X / 2)) - (Consolas.MeasureString(btnList[i].text).Length() / 4), ((int)btnList[i].position.Y + (btnList[i].size.Y / 2)) - (Consolas.MeasureString(btnList[i].text).Y / 2)), Color.White);
                            }
                            else
                            {
                                spriteBatch.DrawString(Consolas, btnList[i].text, new Vector2(((int)btnList[i].position.X + (btnList[i].size.X / 2)) - (Consolas.MeasureString(btnList[i].text).Length() / 2), ((int)btnList[i].position.Y + (btnList[i].size.Y / 2)) - (Consolas.MeasureString(btnList[i].text).Y / 2)), Color.White);
                            }

                        }
                    }

                    if (p.getBoxType == 4 && p.getVis == true)
                    {
                        for (int c = 0; c < invList.Count; c++)
                        {
                            spriteBatch.Draw(invList[c].bgTex, new Rectangle((int)invList[c].pos.X, (int)invList[c].pos.Y, invList[c].s.X, invList[c].s.Y), Color.White);
                            spriteBatch.Draw(invList[c].itemTex, new Rectangle((int)invList[c].pos.X - (24 / 2) + (invList[c].s.X / 2), (int)invList[c].pos.Y - (24 / 2) + (invList[c].s.Y / 2), 24, 24), Color.White);
                            if (invList[c].qty > 0)
                            {
                                spriteBatch.DrawString(Consolas, invList[c].qty.ToString(), new Vector2(invList[c].pos.X + invList[c].s.X - 5, invList[c].pos.Y + invList[c].s.Y - (Consolas.MeasureString(invList[c].qty.ToString()).Y / 1.5f)), Color.White);
                            }                            
                        }
                    }
                    
                    for (int c = 0; c < invList.Count; c++)
                    {
                        for (int z = 0; z < g.gameManager.itemList.Count; z++)
                        {
                            if (g.gameManager.itemList[z].iID == invList[c].itemID)
                            {
                                if (g.gameManager.itemList[z].pVis == true)
                                {
                                    spriteBatch.Draw(g.gameManager.itemList[z].pBG, new Rectangle((int)mousePos.X, (int)mousePos.Y, (int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() + 12, 125), Color.White * .8f);
                                    spriteBatch.Draw(g.gameManager.itemList[z].iTex, new Rectangle((int)mousePos.X + 10, (int)mousePos.Y + 10, 24, 24), Color.White);
                                    spriteBatch.DrawString(Consolas, g.gameManager.itemList[z].iName, new Vector2((int)mousePos.X + 50, (int)mousePos.Y + 5 + ((int)Consolas.MeasureString(g.gameManager.itemList[z].iName).Y / 2)), Color.White);
                                    spriteBatch.DrawString(Consolas, g.gameManager.itemList[z].pDesc, new Vector2((int)mousePos.X + 12, (int)mousePos.Y + 45), Color.White);
                                }
                            }
                        }
                    }
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
