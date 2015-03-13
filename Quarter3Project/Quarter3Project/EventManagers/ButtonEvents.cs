using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Quarter3Project
{
    public class ButtonEvents
    {
        Game1 myGame;
        FileStream fs;

        public ButtonEvents(Game1 G)
        {
            myGame = G;
        }

        public void setMenu()
        {
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        public void CreateNew()
        {
            myGame.SetCurrentLevel(GameLevels.GameLevels.CREATE);
        }

        public void showPop(int id)
        {
            myGame.showPop(id); 
        }

        public void exitToMenu()
        {
            saveGame();
            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].getVis)
                {
                    closePop2(myGame.popManager.popList[i].getID);
                }
            }
                myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        public void saveGame()
        {
            if (!Directory.Exists(@"Save"))
            {
                Directory.CreateDirectory(@"Save");
            }

            fs = new FileStream(@"Save/Save.txt", FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            fs = new FileStream(@"Save/Save.txt", FileMode.Truncate, FileAccess.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(@"Save/Save.txt", true, Encoding.ASCII);
            switch (myGame.gameManager.classType)
            {
                case 100:
                    sw.WriteLine(100);
                    break;
                case 200:
                    sw.WriteLine(200);
                    break;
                case 300:
                    sw.WriteLine(300);
                    break;
            }
            sw.WriteLine(myGame.gameManager.tests[0].getPos().X);
            sw.WriteLine(myGame.gameManager.tests[0].getPos().Y);
             
            sw.Close();
        }

        public void loadGame()
        {
            if (File.Exists(@"Save/Save.txt"))
            {
                StreamReader sr = new StreamReader(@"Save/Save.txt");
                myGame.gameManager.classType = Int32.Parse(sr.ReadLine());
                myGame.gameManager.LoadContent2();
                sr.Close();
            }
        }

        public void saveNewChr2()
        {
            myGame.showPop(2001); //Confirm Character Creation
        }

        public void StartGame()
        {
            if (File.Exists(@"Save/Save.txt"))
            {
                loadGame();
                myGame.SetCurrentLevel(GameLevels.GameLevels.GAME);
            }
            else
            {
                myGame.showPop(2003); //Notifies no game saved
            }
        }

        public void saveNewChr()
        {
            if (!Directory.Exists(@"Save"))
            {
                Directory.CreateDirectory(@"Save");
            }

            fs = new FileStream(@"Save/Save.txt", FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            fs = new FileStream(@"Save/Save.txt", FileMode.Truncate, FileAccess.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(@"Save/Save.txt", true, Encoding.ASCII);
            for (int i = 0; i < myGame.createManager.chrSelection.Count; i++)
            {
                switch (myGame.createManager.chrSelection[i].visible)
                {
                    case true:
                        switch (myGame.createManager.chrSelection[i].id)
                        {
                            case 100:
                                sw.WriteLine(100);
                                break;
                            case 200:
                                sw.WriteLine(200);
                                break;
                            case 300:
                                sw.WriteLine(300);
                                break;
                        }
                        break;
                }
            }
            sw.Close();
            closePop(2001);
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        public void Quit()
        {
            myGame.showPop(2000); //Confirms quit game
        }

        public void closePop2(int id)
        {
            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].getVis == true)
                {
                    if (myGame.popManager.popList[i].getID == id)
                    {
                        switch (myGame.popManager.popList[i].getBoxType)
                        {
                            case 0: // Yes No
                                myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getOPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                break;
                            case 1: // Ok
                                myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getOPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                break;
                            case 2: // Next or Prev
                                break;
                            case 3: // Accept or Decline
                                break;
                            case 4: // Inventory
                                myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                break;
                            case 5: // Test
                                myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                break;
                        }
                    }

                    myGame.popManager.prevIDN = myGame.popManager.idn;
                    myGame.popManager.idn = 0;
                }
            }
        }

        public void closePop(int id)
        {
            int pOrder = int.MinValue;
            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].getVis == true)
                {
                    if (myGame.popManager.popList[i].getID == id)
                    {
                        pOrder = myGame.popManager.popList[i].getDrawOrder;
                        switch (myGame.popManager.popList[i].getBoxType)
                        {
                            case 0: // Yes No
                                for (int c = 0; c < myGame.popManager.btnList.Count; c++)
                                {
                                    if (myGame.popManager.btnList[c].hover == true)
                                    {
                                        if (myGame.popManager.popList[i].getID == myGame.popManager.btnList[c].id)
                                        {
                                            myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getOPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                            case 1: // Ok
                                for (int c = 0; c < myGame.popManager.btnList.Count; c++)
                                {
                                    if (myGame.popManager.btnList[c].hover == true)
                                    {
                                        if (myGame.popManager.popList[i].getID == myGame.popManager.btnList[c].id)
                                        {
                                            myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getOPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                            case 2: // Next or Prev
                                break;
                            case 3: // Accept or Decline
                                break;
                            case 4: // Inventory
                                for (int c = 0; c < myGame.popManager.btnList.Count; c++)
                                {
                                    if (myGame.popManager.btnList[c].hover == true)
                                    {
                                        if (myGame.popManager.popList[i].getID == myGame.popManager.btnList[c].id)
                                        {
                                            myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                            case 5: // Test
                                for (int c = 0; c < myGame.popManager.btnList.Count; c++)
                                {
                                    if (myGame.popManager.btnList[c].hover == true)
                                    {
                                        if (myGame.popManager.popList[i].getID == myGame.popManager.btnList[c].id)
                                        {
                                            myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, false, false, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, 0);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    

                    myGame.popManager.prevIDN = myGame.popManager.idn;
                    myGame.popManager.idn = 0;
                }
            }

            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].getID != id && myGame.popManager.popList[i].getDrawOrder > 0 && myGame.popManager.popList[i].getDrawOrder < pOrder)
                {
                    myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, myGame.popManager.popList[i].getVis, myGame.popManager.popList[i].getHoverState, myGame.popManager.popList[i].getPrevHoverState, myGame.popManager.popList[i].getID, myGame.popManager.popList[i].getOPos, myGame.popManager.popList[i].getDrawOrder + 1);
                }
            }

                switch (myGame.CurrentLevel)
                {
                    case GameLevels.GameLevels.CREATE:
                        myGame.createManager.Enabled = true;
                        break;
                    case GameLevels.GameLevels.GAME:
                        myGame.gameManager.Enabled = true;
                        break;
                    case GameLevels.GameLevels.MENU:
                        myGame.menuManager.Enabled = true;
                        break;
                }
        }

        public void exitGame()
        {
            myGame.Exit();
        }
    }
}
