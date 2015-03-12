using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.EventManagers
{
    public class GlobalEvents
    {
        Game1 g;

        public GlobalEvents(Game1 G)
        {
            g = G;
        }

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
                            g.gameManager.health += 5;
                            break;
                        case 20000001: // Mana Potion
                            g.gameManager.mana += 5;
                            break;
                        case 20000002: //Super Health Potion
                            g.gameManager.health += 50;
                            break;
                        case 20000003: // Super Mana Potion
                            g.gameManager.mana += 50;
                            break;
                    }
                }
            }
        }

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
    }
}
