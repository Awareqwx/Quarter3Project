#region File Description
// PopUpManager.cs
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter3Project.EventManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Quarter3Project.Managers
{
    public class PopManager : DrawableGameComponent
    {

        #region Constants

        //How many spaces are there in the players inventory?
        const int invSpace = 25;

        //How many buttons are bindable?
        const int keybindSpace = 54;

        //How long does it take to handle input for a double-click in Milliseconds?
        const double doubleClickDelay = 155;

        #endregion
         
        #region Fields

        Game1 g;

        Vector2 mousePos;
        MouseState mouseState,
                   prevMouseState;

        SpriteBatch spriteBatch;
        SpriteFont Consolas;

        Vector2 posDisplacement,
                sizeDisplacement,
                newPos;

        public List<ItemData.pop> popList { get; private set; }
        Texture2D popFG,
                  popBG,
                  bgBorder,
                  popbg;
        public int idn { get; private set; }
        public bool lockMove { get; private set; }

        public List<ItemData.btn> btnList { get; private set; }
        Texture2D red;

        public List<ItemData.itemSpace> invList { get; private set; }
        Texture2D empty,
                  rarityBorder;
        double doubleClickTime = 0;

        public List<ItemData.itemSpace> keybindList { get; private set; }

        ButtonEvents bE;

        GlobalEvents gE;

        #endregion

        #region Initialization

        public PopManager(Game1 G)
            : base(G)
        {
            g = G;
        }

        /// <summary>
        /// Adds all pop ups to popList.
        /// </summary>
        private void addPopUps()
        {
            popList.Add(new ItemData.pop(popbg, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Are you sure you want to quit?" }, false, false, false, 2000, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popbg, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Any previous games that have been created will be \ndeleted if you continue." }, false, false, false, 2001, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popbg, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 0, new string[] { "Do you want to return to the menu?" }, false, false, false, 2002, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popbg, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), new Point(600, 250), 1, new string[] { "There is no save game detected, create a character \nfirst before starting the game." }, false, false, false, 2003, new Vector2((GraphicsDevice.Viewport.Width / 2) - (600 / 2), (GraphicsDevice.Viewport.Height / 2) - (250 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (310 / 2), (GraphicsDevice.Viewport.Height / 2) - (360 / 2)), new Point(310, 360), 4, new string[] { "" }, false, false, false, 2004, new Vector2((GraphicsDevice.Viewport.Width / 2) - (155 / 2), (GraphicsDevice.Viewport.Height / 2) - (180 / 2)), 0));
            popList.Add(new ItemData.pop(popFG, popBG, new Vector2((GraphicsDevice.Viewport.Width / 2) - (860 / 2), (GraphicsDevice.Viewport.Height / 2) - (420 / 2)), new Point(860, 420), 5, new string[] { "" }, false, false, false, 2005, new Vector2((GraphicsDevice.Viewport.Width / 2) - (155 / 2), (GraphicsDevice.Viewport.Height / 2) - (180 / 2)), 0));
        }

        /// <summary>
        /// Add all keybind and inventory slots.
        /// </summary>
        private void addInvAndKeybindSlots()
        {
            for (int c = 0; c < popList.Count; c++)
            {
                if (popList[c].getBoxType == 4) // Change to switch statement if using more boxTypes.
                {
                    int p = 0;
                    int i = 1;
                    while (p < invSpace)
                    {
                        Vector2 pos = new Vector2(0, 0);

                        if (i > 5)
                            i = 1;

                        if (i <= 5)
                            pos = new Vector2(popList[c].getPos.X + (i * 50), popList[c].getPos.Y + (i * 100));

                        invList.Add(new ItemData.itemSpace(empty, empty, pos, new Point(30, 30), 0, p, 0));

                        p += 1;
                        i += 1;
                    }
                }
                else if (popList[c].getBoxType == 5)
                {
                    int p = 0;
                    int i = 1;
                    int s = 0;
                    while (p < keybindSpace)
                    {
                        Vector2 pos = new Vector2(0, 0);

                        if (i < 13)
                        {
                            s = 2;
                            pos = new Vector2(popList[c].getPos.X + (i * 15), popList[c].getPos.Y + (s * 100));
                        }
                        else if (i < 26)
                        {
                            s = 3;
                            pos = new Vector2(popList[c].getPos.X + ((i - 13) * 50), popList[c].getPos.Y + (s * 100));
                        }
                        else if (i < 37)
                        {
                            s = 4;
                            pos = new Vector2(popList[c].getPos.X + ((i - 11) * 50), popList[c].getPos.Y + (s * 100));
                        }
                        else if (i < 49)
                        {
                            s = 5;
                            pos = new Vector2(popList[c].getPos.X + ((i - 12) * 50), popList[c].getPos.Y + (s * 100));
                        }
                        else if (i < 54)
                        {
                            s = 6;
                            pos = new Vector2(popList[c].getPos.X + ((i - 5) * 50), popList[c].getPos.Y + (s * 100));
                        }

                        keybindList.Add(new ItemData.itemSpace(empty, empty, pos, new Point(30, 30), 0, p, 0));

                        p += 1;
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// All all buttons to their associated pop ups.
        /// </summary>
        private void addPopUpButtons()
        {
            for (int i = 0; i < popList.Count; i++)
            {
                switch (popList[i].getBoxType)
                {
                    case 0: // Yes or No
                        switch (popList[i].getID)
                        {
                            case 2000:
                                btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Yes", Color.White, false, false, true, 2000, 2000));
                                btnList.Add(new ItemData.btn(red, new Vector2(650, 400), new Point(50, 25), "No", Color.White, false, false, true, 2000, 2001));
                                btnList.Add(new ItemData.btn(red, new Vector2(195, 400), new Point(60, 25), "Cancel", Color.White, false, false, true, 2000, 2002));
                                break;
                            case 2001:
                                btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Yes", Color.White, false, false, true, 2001, 2000));
                                btnList.Add(new ItemData.btn(red, new Vector2(650, 400), new Point(50, 25), "No", Color.White, false, false, true, 2001, 2001));
                                btnList.Add(new ItemData.btn(red, new Vector2(195, 400), new Point(60, 25), "Cancel", Color.White, false, false, true, 2001, 2002));
                            break;
                            case 2002:
                                btnList.Add(new ItemData.btn(red, new Vector2(715, 400), new Point(50, 25), "Yes", Color.White, false, false, true, 2002, 2000));
                                btnList.Add(new ItemData.btn(red, new Vector2(650, 400), new Point(50, 25), "No", Color.White, false, false, true, 2002, 2001));
                                btnList.Add(new ItemData.btn(red, new Vector2(195, 400), new Point(60, 25), "Cancel", Color.White, false, false, true, 2002, 2002));
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
                    case 3: // Accept or Decline
                        break;
                    case 4: // Inventory
                        switch (popList[i].getID)
                        {
                            case 2004:
                                btnList.Add(new ItemData.btn(red, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 30, (int)popList[i].getPos.Y + 11), new Point(20, 20), "X", Color.White, false, false, true, 2004, 2004));
                                break;
                        }
                        break;
                    case 5: // Keybind
                        switch (popList[i].getID)
                        {
                            case 2005:
                                btnList.Add(new ItemData.btn(red, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 30, (int)popList[i].getPos.Y + 30), new Point(20, 20), "X", Color.White, false, false, true, 2005, 2004));
                                break;
                        }
                        break;
                }
            }
        }

        public override void Initialize()
        {

            popList = new List<ItemData.pop>();

            btnList = new List<ItemData.btn>();

            invList = new List<ItemData.itemSpace>();

            keybindList = new List<ItemData.itemSpace>();

            bE = new ButtonEvents(g);

            gE = new GlobalEvents(g);

            base.Initialize();

            // Add all of the game pop ups to the popList.
            addPopUps();

            // Add all the textures and slots into inventory and keybind menus.
            addInvAndKeybindSlots();

            // Add all the buttons.
            addPopUpButtons();

        }

        protected override void LoadContent()
        {
            //Create spriteBatch and load all of the games textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");
            bgBorder = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            bgBorder.SetData(new Color[] { new Color(0, 0, 0) });
            popBG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popBG.SetData(new Color[] { new Color(255, 255, 255) });
            popFG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popFG.SetData(new Color[] { new Color(110, 110, 110) });
            red = Game.Content.Load<Texture2D>(@"Images/btnTex");
            empty = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            empty.SetData(new Color[] { new Color(0, 0, 0, 0.3f) });
            rarityBorder = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            popbg = Game.Content.Load<Texture2D>(@"Images/long_board_ui");

            base.LoadContent();
        }

        #endregion

        #region Mutators

        /// <summary>
        /// Sets the variable idn to the specified parameter.
        /// </summary>
        /// <param name="i">Any number within range may be passed through i and set into the idn.</param>
        public void setIDN(int i)
        {
            idn = i;
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Finds the pop up which id matches the id parameter
        /// then sets the visiblility of that pop up to true and
        /// the draw order to the highest value so that it draws
        /// in front of everything else. Any other visible pop ups
        /// will have their draw order reduced by one to make room
        /// for the new pop up that is being shown. Then loads
        /// the buttons associated with that pop up to make sure they
        /// are positioned correctly.
        /// </summary>
        /// <param name="id">Each pop up has its own unique ID, pass through the ID to show that specific pop up.</param>
        /// <param name="popList">This is the list of all of the pop ups that are added to the game.</param>
        public void showPop(int id, List<ItemData.pop> popList)
        {
            idn = id;
            int pOrder = int.MinValue;

            // Match the pop up id with the id parameter and set the draw order to the front and make the pop up visible.
            for (int i = 0; i < popList.Count; i++)
            {
                if (id == popList[i].getID)
                {
                    pOrder = popList[i].getDrawOrder;
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList.Count - 1);
                }
            }

            // If the pOrder is zero then the pop up is not visible.
            if (pOrder == int.MinValue)
                return;

            // Check any current visible pop ups and send them to the back so that the one opening is at the front.
            for (int i = 0; i < popList.Count; i++)
            {
                if (popList[i].getID != id && popList[i].getDrawOrder > 0 && popList[i].getDrawOrder >= pOrder)
                {
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder - 1);
                }
            }

            // Load the buttons associated with the pop up.
            for (int i = 0; i < popList.Count; i++)
            {
                for (int c = 0; c < btnList.Count; c++)
                {
                    if (btnList[c].id == popList[i].getID && popList[i].getID == id)
                    {
                        switch (btnList[c].uniqueid)
                        {
                            case 2000: // Yes
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 135, (int)popList[i].getPos.Y + popList[i].getSize.Y - 55), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2001: // No
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 210, (int)popList[i].getPos.Y + popList[i].getSize.Y - 55), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2002: // Cancel
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + 85, (int)popList[i].getPos.Y + popList[i].getSize.Y - 55), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2003: // Ok
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2((int)popList[i].getPos.X + popList[i].getSize.X - 135, (int)popList[i].getPos.Y + popList[i].getSize.Y - 55), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                            case 2004: // X
                                btnList[c] = new ItemData.btn(btnList[c].btnTexture, new Vector2(((int)popList[i].getPos.X + popList[i].getSize.X - 30), (int)popList[i].getPos.Y + 11), btnList[c].size, btnList[c].text, btnList[c].color, btnList[c].hover, btnList[c].phover, btnList[c].visible, btnList[c].id, btnList[c].uniqueid);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the states of any user input ie: mouse input.
        /// </summary>
        public void setStates()
        {
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Sets the previous states of any user input ie: previous mosue input.
        /// </summary>
        public void setPreviousStates()
        {
            prevMouseState = mouseState;
        }

        /// <summary>
        /// Checks if the number of items in the inventory is equal to zero.
        /// If that is true then it removes the item id and texture from the space.
        /// Then it updates the positioning of the inventory slots. Then it will
        /// check if you are double clicking an item and uses that item, and if you
        /// are hovering over an item it will show a description.
        /// </summary>
        /// <param name="gameTime">provides timing values</param>
        public void inventoryUpdate(GameTime gameTime)
        {
            for (int i = 0; i < invList.Count; i++)
            {
                if (invList[i].qty <= 0)
                {
                    invList[i] = new ItemData.itemSpace(empty, empty, invList[i].pos, new Point(30, 30), 0, invList[i].itemSpaceID, 0);
                }
            }

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

            doubleClickTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            for (int c = 0; c < popList.Count; c++)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && popList[c].getBoxType == 4 && popList[c].getDrawOrder == popList.Count - 1)
                {
                    if (doubleClickTime < doubleClickDelay)
                    {
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
                                    g.gameManager.itemList[z] = new ItemData.item(g.gameManager.itemList[z].iTex, g.gameManager.itemList[z].iID, g.gameManager.itemList[z].iName, g.gameManager.itemList[z].pBG, g.gameManager.itemList[z].pDesc, true, g.gameManager.itemList[z].rar);
                                }
                            }
                        }

                        if (invList.Count(p => p.collisionRect().Intersects(new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1))) == 0)
                        {
                            for (int z = 0; z < g.gameManager.itemList.Count; z++)
                            {
                                if (g.gameManager.itemList[z].iID == invList[i].itemID)
                                {
                                    g.gameManager.itemList[z] = new ItemData.item(g.gameManager.itemList[z].iTex, g.gameManager.itemList[z].iID, g.gameManager.itemList[z].iName, g.gameManager.itemList[z].pBG, g.gameManager.itemList[z].pDesc, false, g.gameManager.itemList[z].rar);

                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function will update the positioning of the keybind slots.
        /// </summary>
        public void keybindUpdate()
        {
            for (int i = 0; i < keybindList.Count; i++)
            {
                for (int c = 0; c < popList.Count; c++)
                {
                    if (popList[c].getBoxType == 5)
                    {
                        if (i < 13)
                        {
                            keybindList[i] = new ItemData.itemSpace(keybindList[i].bgTex, keybindList[i].itemTex, new Vector2(((int)popList[c].getPos.X + ((i) * 50) + 35) - ((((13) * 50) + 35) / 2) + (popList[c].getSize.X / 2), (int)popList[c].getPos.Y + (1 * 60)), keybindList[i].s, keybindList[i].itemID, keybindList[i].itemSpaceID, keybindList[i].qty);
                        }
                        else if (i < 26)
                        {
                            keybindList[i] = new ItemData.itemSpace(keybindList[i].bgTex, keybindList[i].itemTex, new Vector2(((int)popList[c].getPos.X + ((i - 13) * 50) + 35) - ((((26 - 13) * 50) + 35) / 2) + (popList[c].getSize.X / 2), (int)popList[c].getPos.Y + (2 * 60)), keybindList[i].s, keybindList[i].itemID, keybindList[i].itemSpaceID, keybindList[i].qty);
                        }
                        else if (i < 37)
                        {
                            keybindList[i] = new ItemData.itemSpace(keybindList[i].bgTex, keybindList[i].itemTex, new Vector2(((int)popList[c].getPos.X + ((i - 26) * 50) + 35) - ((((37 - 26) * 50) + 35) / 2) + (popList[c].getSize.X / 2), (int)popList[c].getPos.Y + (3 * 60)), keybindList[i].s, keybindList[i].itemID, keybindList[i].itemSpaceID, keybindList[i].qty);
                        }
                        else if (i < 49)
                        {
                            keybindList[i] = new ItemData.itemSpace(keybindList[i].bgTex, keybindList[i].itemTex, new Vector2(((int)popList[c].getPos.X + ((i - 37) * 50) + 35) - ((((49 - 37) * 50) + 35) / 2) + (popList[c].getSize.X / 2), (int)popList[c].getPos.Y + (4 * 60)), keybindList[i].s, keybindList[i].itemID, keybindList[i].itemSpaceID, keybindList[i].qty);
                        }
                        else if (i < 55)
                        {
                            keybindList[i] = new ItemData.itemSpace(keybindList[i].bgTex, keybindList[i].itemTex, new Vector2(((int)popList[c].getPos.X + ((i - 49) * 50) + 35) - ((((55 - 49) * 50) + 35) / 2) + (popList[c].getSize.X / 2), (int)popList[c].getPos.Y + (5 * 60)), keybindList[i].s, keybindList[i].itemID, keybindList[i].itemSpaceID, keybindList[i].qty);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if certain pop ups are open then it will pause the
        /// background so that interaction with anything other than 
        /// the pop up is not possible.
        /// </summary>
        public void updateGameState()
        {
            switch (g.cL)
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
        }

        /// <summary>
        /// Checks whether or not you are hovering over buttons and will
        /// set the hover state of that button to true or false. Then 
        /// checks if you are clicking the buttons and executes a button event.
        /// </summary>
        public void updateButtons()
        {
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
                for (int b = 0; b < btnList.Count; b++)
                {
                    if (popList.Count(s => s.getHoverState == true) == 1 || popList.Any(p => p.getDrawOrder == popList.Count - 1 && p.getID == popList[i].getID && popList[i].getDrawOrder == popList.Count - 1 && popList[i].getID == btnList[b].id))
                    {
                        if (popList[i].getHoverState == true && btnList[b].hover == true && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                        {
                            switch (popList[i].getID)
                            {
                                case 2000: // Are you sure you want to quit the game?
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2000: // Yes
                                            gE.exitGame();
                                            break;
                                        case 2001: // No
                                            gE.closePop(2000);
                                            break;
                                        case 2002: // Cancel
                                            gE.closePop(2000);
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
                                            gE.closePop(2001);
                                            break;
                                        case 2002: // Cancel
                                            gE.closePop(2001);
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
                                            gE.closePop(2002);
                                            break;
                                        case 2002: // Cancel
                                            gE.closePop(2002);
                                            break;
                                    }
                                    break;
                                case 2003: // You haven't created any characters yet....
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2003: // Ok
                                            gE.closePop(2003);
                                            break;
                                    }
                                    break;
                                case 2004: // Inventory
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2004: // X
                                            gE.closePop(2004);
                                            break;
                                    }
                                    break;
                                case 2005:
                                    switch (btnList[b].uniqueid)
                                    {
                                        case 2004: // X
                                            gE.closePop(2005);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }


        }

        /// <summary>
        /// This function will check if the idn is equal to a pop up id and set it to 
        /// visible and also check whether or not you are hovering over a pop up
        /// and will set the hover state of the pop up to true or false. It will then
        /// check if you have clicked on a seperate pop up while other pop ups are open
        /// and will set the draw order of the pop up you just click to the front.
        /// </summary>
        public void updatePopUp()
        {
            for (int i = 0; i < popList.Count; i++)
            {
                if (popList[i].getID == idn && popList[i].getVis == false)
                {
                    popList[i] = new ItemData.pop(popList[i].getBGTex, popList[i].getBDTex, popList[i].getPos, popList[i].getSize, popList[i].getBoxType, popList[i].getText, true, popList[i].getHoverState, popList[i].getPrevHoverState, popList[i].getID, popList[i].getOPos, popList[i].getDrawOrder);
                }
            }

            for (int i = 0; i < popList.Count; i++)
            {
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
        }

        /// <summary>
        /// This function will allow you to move pop ups around with the buttons.
        /// </summary>
        public void updateMovement()
        {
            for (int i = 0; i < popList.Count; i++)
            {
                if (popList[i].getVis == true && popList.Count(p => p.getDrawOrder > popList[i].getDrawOrder) == 0)
                {
                    for (int b = 0; b < btnList.Count; b++)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && btnList[b].hover == true && popList.Any(p => p.getDrawOrder == popList.Count - 1 && p.getID == popList[i].getID && popList[i].getDrawOrder == popList.Count - 1 && popList[i].getID == btnList[b].id))
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
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 135, newPos.Y + popList[i].getSize.Y - 55), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                    case 2001: // No
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 210, newPos.Y + popList[i].getSize.Y - 55), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                    case 2002: // Cancel
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + 85, newPos.Y + popList[i].getSize.Y - 55), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                    case 2003: // Ok
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 135, newPos.Y + popList[i].getSize.Y - 55), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                    case 2004: // X
                                        btnList[b] = new ItemData.btn(btnList[b].btnTexture, new Vector2(newPos.X + popList[i].getSize.X - 30, newPos.Y + 11), btnList[b].size, btnList[b].text, btnList[b].color, btnList[b].hover, btnList[b].phover, btnList[b].visible, btnList[b].id, btnList[b].uniqueid);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Sets the position and state of the mouse.
            setStates();

            // Updates the items and item spaces in the inventory and
            // handles events taken place in the inventory window.
            inventoryUpdate(gameTime);

            // Updates the keybind spaces and keybinded keys and
            // handles events taken place in the keybind window.
            keybindUpdate();

            // Updates the Manager to pause whether or not
            // certain pop ups are open.
            updateGameState();

            // Updates pop up button positions and handles
            // events when buttons are pressed.
            updateButtons();

            // Updates the pop up visibility and hover state
            // and the draw order of the pop ups.
            updatePopUp();

            // Updates the movement of the pop up and the
            // movement of the buttons in the pop up.
            updateMovement();

            // Sets the previous state of the mouse.
            setPreviousStates();
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the pop up with buttons and items.
        /// </summary>
        private void drawPopUp()
        {
            foreach (var p in popList.OrderBy(i => i.getDrawOrder))
            {
                // Draw Pop Up
                if (p.getVis == true)
                {
                    spriteBatch.Draw(p.getBGTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, p.getSize.X, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, 1, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y, p.getSize.X, 1), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X + p.getSize.X, (int)p.getPos.Y, 1, p.getSize.Y), Color.White);
                    spriteBatch.Draw(p.getBDTex, new Rectangle((int)p.getPos.X, (int)p.getPos.Y + p.getSize.Y, p.getSize.X, 1), Color.White);

                    foreach (String s in p.getText)
                    {
                        spriteBatch.DrawString(Consolas, s, new Vector2(p.getPos.X + 51, p.getPos.Y + 56), new Color(0, 0, 0, 1.0f));
                        spriteBatch.DrawString(Consolas, s, new Vector2(p.getPos.X + 50, p.getPos.Y + 55), new Color(255, 255, 173));
                    }
                }

                // Draw Buttons
                if (p.getVis == true)
                {
                    for (int i = 0; i < btnList.Count; i++)
                    {
                        if (btnList[i].id == p.getID && btnList[i].visible == true)
                        {
                            spriteBatch.Draw(btnList[i].btnTexture, new Rectangle((int)btnList[i].position.X - 3, (int)btnList[i].position.Y - 3, btnList[i].size.X + 6, btnList[i].size.Y + 6), new Color(0, 0, 0, .7f));
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
                }

                // Draw Pop Up Items
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

                if (p.getBoxType == 5 && p.getVis == true)
                {
                    for (int c = 0; c < keybindList.Count; c++)
                    {
                        spriteBatch.Draw(keybindList[c].bgTex, new Rectangle((int)keybindList[c].pos.X, (int)keybindList[c].pos.Y, keybindList[c].s.X, keybindList[c].s.Y), Color.White);
                        spriteBatch.Draw(keybindList[c].itemTex, new Rectangle((int)keybindList[c].pos.X - (24 / 2) + (keybindList[c].s.X / 2), (int)keybindList[c].pos.Y - (24 / 2) + (keybindList[c].s.Y / 2), 24, 24), Color.White);
                        if (keybindList[c].qty > 0)
                        {
                            spriteBatch.DrawString(Consolas, keybindList[c].qty.ToString(), new Vector2(keybindList[c].pos.X + keybindList[c].s.X - 5, keybindList[c].pos.Y + keybindList[c].s.Y - (Consolas.MeasureString(keybindList[c].qty.ToString()).Y / 1.5f)), Color.White);
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
                                spriteBatch.Draw(g.gameManager.itemList[z].pBG, new Rectangle((int)mousePos.X, (int)mousePos.Y, (int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() + 12, ((int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() / 2) + 12), Color.White * .8f);
                                spriteBatch.Draw(g.gameManager.itemList[z].iTex, new Rectangle((int)mousePos.X + 10, (int)mousePos.Y + 10, 24, 24), Color.White);
                                spriteBatch.DrawString(Consolas, g.gameManager.itemList[z].iName, new Vector2((int)mousePos.X + 51, (int)mousePos.Y + 6 + ((int)Consolas.MeasureString(g.gameManager.itemList[z].iName).Y / 2)), Color.Black);
                                spriteBatch.DrawString(Consolas, g.gameManager.itemList[z].iName, new Vector2((int)mousePos.X + 50, (int)mousePos.Y + 5 + ((int)Consolas.MeasureString(g.gameManager.itemList[z].iName).Y / 2)), Color.White);
                                spriteBatch.DrawString(Consolas, g.gameManager.itemList[z].pDesc, new Vector2((int)mousePos.X + 12, (int)mousePos.Y + 45), Color.White);
                                spriteBatch.Draw(g.gameManager.itemList[z].rar, new Rectangle((int)mousePos.X, (int)mousePos.Y, (int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() + 12, 1), Color.White);
                                spriteBatch.Draw(g.gameManager.itemList[z].rar, new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, ((int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() / 2) + 12), Color.White);
                                spriteBatch.Draw(g.gameManager.itemList[z].rar, new Rectangle((int)mousePos.X, (int)mousePos.Y + ((int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() / 2) + 12, ((int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length()) + 12, 1), Color.White);
                                spriteBatch.Draw(g.gameManager.itemList[z].rar, new Rectangle((int)mousePos.X + ((int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length()) + 12, (int)mousePos.Y, 1, ((int)Consolas.MeasureString(g.gameManager.itemList[z].pDesc).Length() / 2) + 12), Color.White);
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            // Draws the visible pop ups in order.
            drawPopUp();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

    }
}
