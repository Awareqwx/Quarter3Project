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
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        public void saveNewChr2()
        {
            myGame.showPop(2001);
        }

        public void StartGame()
        {
            myGame.SetCurrentLevel(GameLevels.GameLevels.GAME);
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
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        public void saveGame()
        {
        }

        public void LoadGame()
        {
        }

        public void Quit()
        {
            myGame.showPop(2000);
        }

        public void closePop()
        {
            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].visible == true)
                {
                    myGame.popManager.popList[i] = new PopUp.pop(myGame.popManager.popList[i].texture, myGame.popManager.popList[i].position, myGame.popManager.popList[i].size, false, myGame.popManager.popList[i].text, myGame.popManager.popList[i].id);
                }
            }               
        }

        public void exitGame()
        {
            myGame.Exit();
        }
    }
}
