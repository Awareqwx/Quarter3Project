#region File Description
// GlobalEvents.cs
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Quarter3Project.EventManagers
{
    public class GlobalEvents
    {

        #region Fields

        Game1 g;

        #endregion

        #region Initialization

        public GlobalEvents(Game1 G)
        {
            g = G;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void useItem(int id)
        {
            for (int i = 0; i < g.popManager.invList.Count; i++)
            {
                if (g.popManager.invList[i].itemID == id)
                {
                    g.popManager.invList[i] = new ItemData.itemSpace(g.popManager.invList[i].bgTex, g.popManager.invList[i].itemTex, g.popManager.invList[i].pos, g.popManager.invList[i].s, g.popManager.invList[i].itemID, g.popManager.invList[i].itemSpaceID, g.popManager.invList[i].qty - 1);
                    switch (id)
                    {
                        case 20000000: // Health Potion
                            g.gameManager.player.health += 5;
                            break;
                        case 20000001: // Mana Potion
                            g.gameManager.player.mana += 5;
                            break;
                        case 20000002: // Super Health Potion
                            g.gameManager.player.health += 50;
                            break;
                        case 20000003: // Super Mana Potion
                            g.gameManager.player.mana += 50;
                            break;
                        case 20000004: // Exp Potion
                            g.gameManager.player.exp += 5;
                            break;
                        case 20000005: // Super Exp Potion
                            g.gameManager.player.exp += 50;
                            break;
                    }
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="qty"></param>
        public void addItemToInv(int id, int qty)
        {
            if (g.popManager.invList.Count(p => p.itemID == id) >= 1)
            {
                g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)] = new ItemData.itemSpace(g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)].bgTex, g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)].itemTex, g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)].pos, g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)].s, id, g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)].itemSpaceID, g.popManager.invList[g.popManager.invList.FindIndex(p => p.itemID == id)].qty + qty);
            }
            else
            {
                for (int i = 0; i < g.popManager.invList.Count; i++)
                {
                    if (g.popManager.invList[i].itemID == 0)
                    {
                        for (int z = 0; z < g.gameManager.itemList.Count; z++)
                        {
                            if (g.gameManager.itemList[z].iID == id)
                            {
                                g.popManager.invList[i] = new ItemData.itemSpace(g.popManager.invList[i].bgTex, g.gameManager.itemList[z].iTex, g.popManager.invList[i].pos, g.popManager.invList[i].s, id, g.popManager.invList[i].itemSpaceID, qty);
                                i = Int32.MaxValue - 1;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void showPop(int id)
        {
            g.showPop(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void closePop2(int id)
        {
            for (int i = 0; i < g.popManager.popList.Count; i++)
            {
                if (g.popManager.popList[i].getVis == true)
                {
                    if (g.popManager.popList[i].getID == id)
                    {
                        switch (g.popManager.popList[i].getBoxType)
                        {
                            case 0: // Yes No
                                g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getOPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                break;
                            case 1: // Ok
                                g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getOPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                break;
                            case 2: // Next or Prev
                                break;
                            case 3: // Accept or Decline
                                break;
                            case 4: // Inventory
                                g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                break;
                            case 5: // Test
                                g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                break;
                        }
                    }

                    g.popManager.setIDN(0);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void closePop(int id)
        {
            int pOrder = int.MinValue;
            for (int i = 0; i < g.popManager.popList.Count; i++)
            {
                if (g.popManager.popList[i].getVis == true)
                {
                    if (g.popManager.popList[i].getID == id)
                    {
                        pOrder = g.popManager.popList[i].getDrawOrder;
                        switch (g.popManager.popList[i].getBoxType)
                        {
                            case 0: // Yes No
                                for (int c = 0; c < g.popManager.btnList.Count; c++)
                                {
                                    if (g.popManager.btnList[c].hover == true)
                                    {
                                        if (g.popManager.popList[i].getID == g.popManager.btnList[c].id)
                                        {
                                            g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getOPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                            case 1: // Ok
                                for (int c = 0; c < g.popManager.btnList.Count; c++)
                                {
                                    if (g.popManager.btnList[c].hover == true)
                                    {
                                        if (g.popManager.popList[i].getID == g.popManager.btnList[c].id)
                                        {
                                            g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getOPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                            case 2: // Next or Prev
                                break;
                            case 3: // Accept or Decline
                                break;
                            case 4: // Inventory
                                for (int c = 0; c < g.popManager.btnList.Count; c++)
                                {
                                    if (g.popManager.btnList[c].hover == true)
                                    {
                                        if (g.popManager.popList[i].getID == g.popManager.btnList[c].id)
                                        {
                                            g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                            case 5: // Test
                                for (int c = 0; c < g.popManager.btnList.Count; c++)
                                {
                                    if (g.popManager.btnList[c].hover == true)
                                    {
                                        if (g.popManager.popList[i].getID == g.popManager.btnList[c].id)
                                        {
                                            g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, false, false, false, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                        }
                    }

                    g.popManager.setIDN(0);
                }
            }

            for (int i = 0; i < g.popManager.popList.Count; i++)
            {
                if (g.popManager.popList[i].getID != id && g.popManager.popList[i].getDrawOrder > 0 && g.popManager.popList[i].getDrawOrder < pOrder)
                {
                    g.popManager.popList[i] = new ItemData.pop(g.popManager.popList[i].getBGTex, g.popManager.popList[i].getBDTex, g.popManager.popList[i].getPos, g.popManager.popList[i].getSize, g.popManager.popList[i].getBoxType, g.popManager.popList[i].getText, g.popManager.popList[i].getVis, g.popManager.popList[i].getHoverState, g.popManager.popList[i].getPrevHoverState, g.popManager.popList[i].getID, g.popManager.popList[i].getOPos, g.popManager.popList[i].getDrawOrder + 1);
                }
            }

            switch (g.cL)
            {
                case GameLevels.GameLevels.CREATE:
                    g.createManager.Enabled = true;
                    break;
                case GameLevels.GameLevels.GAME:
                    g.gameManager.Enabled = true;
                    break;
                case GameLevels.GameLevels.MENU:
                    g.menuManager.Enabled = true;
                    break;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void exitGame()
        {
            g.Exit();
        }
             
        #endregion

    }
}
