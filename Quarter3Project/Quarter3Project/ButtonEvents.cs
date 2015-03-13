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
                case 1:
                    sw.WriteLine(1);
                    break;
                case 2:
                    sw.WriteLine(2);
                    break;
                case 3:
                    sw.WriteLine(3);
                    break;
            }
            sw.WriteLine(myGame.gameManager.player.getPos().X);
            sw.WriteLine(myGame.gameManager.player.getPos().Y);
             
            sw.Close();
            closePop();
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
                            case 1:
                                sw.WriteLine(1);
                                break;
                            case 2:
                                sw.WriteLine(2);
                                break;
                            case 3:
                                sw.WriteLine(3);
                                break;
                        }
                        break;
                }
            }
            sw.Close();
            closePop();
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        public void Quit()
        {
            myGame.showPop(2000); //Confirms quit game
        }

        public void closePop()
        {
            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].getVis == true)
                {
                    myGame.popManager.popList[i] = new ItemData.pop(myGame.popManager.popList[i].getBGTex, myGame.popManager.popList[i].getBDTex, myGame.popManager.popList[i].getPos, myGame.popManager.popList[i].getSize, myGame.popManager.popList[i].getBoxType, myGame.popManager.popList[i].getText, false, myGame.popManager.popList[i].getHoverState, myGame.popManager.popList[i].getPrevHoverState, myGame.popManager.popList[i].getID);

                    myGame.popManager.prevIDN = myGame.popManager.idn;
                    myGame.popManager.idn = 0;
                }
            }
        }

        public void exitGame()
        {
            myGame.Exit();
        }
    }
}
