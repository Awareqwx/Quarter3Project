#region File Description
// ButtonEvents.cs
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Quarter3Project.EventManagers;
#endregion

namespace Quarter3Project
{
    public class ButtonEvents
    {

        #region Fields

        Game1 myGame;
        GlobalEvents gE;
        FileStream fs;

        #endregion

        #region Initialization

        public ButtonEvents(Game1 G)
        {
            myGame = G;
            gE = new GlobalEvents(G);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void setMenu()
        {
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateNew()
        {
            myGame.SetCurrentLevel(GameLevels.GameLevels.CREATE);
        }

        /// <summary>
        /// 
        /// </summary>
        public void exitToMenu()
        {
            saveGame();
            for (int i = 0; i < myGame.popManager.popList.Count; i++)
            {
                if (myGame.popManager.popList[i].getVis)
                {
                    gE.closePop2(myGame.popManager.popList[i].getID);
                }
            }
                myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public void saveNewChr2()
        {
            myGame.showPop(2001); //Confirm Character Creation
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
            gE.closePop(2001);
            myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Quit()
        {
            myGame.showPop(2000); //Confirms quit game
        }

        #endregion

    }
}
