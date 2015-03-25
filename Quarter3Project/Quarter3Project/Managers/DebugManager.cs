#region File Description
// DebugManager.cs
#endregion

#region Using Statements
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Quarter3Project.Managers
{
    public class DebugManager : DrawableGameComponent
    {

        #region Fields

        Game1 g;

        MenuManager m;

        SpriteBatch spriteBatch;
        SpriteFont Consolas;

        Texture2D debugBG;

        KeyboardState keyboardState, 
                      prevKeyboardState;

        List<ItemData.text> debugList;

        bool debugMode = false;

        #endregion

        #region Initialization

        public DebugManager(Game1 G)
            : base(G)
        {
            g = G;
        }

        public override void Initialize()
        {
            debugList = new List<ItemData.text>();

            base.Initialize();

            //Add items into the debugList to be displayed
            debugList.Add(new ItemData.text(Consolas, "Current Level: " + g.cL.ToString(), new Vector2(20, 20), Color.White));
            debugList.Add(new ItemData.text(Consolas, "Previous Level: " + g.pL.ToString(), new Vector2(20, 35), Color.White));
            debugList.Add(new ItemData.text(Consolas, "Class Type: " + g.gameManager.classType.ToString(), new Vector2(20, 50), Color.White));

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loads and sets the Consolas Font
            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            //Gives debugBG a 1x1px texture
            debugBG = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            //Sets debugBG texture color to black with alpha level at .7f
            debugBG.SetData<Color>(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.7f) });
           
            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F2) && prevKeyboardState.IsKeyUp(Keys.F2))
                debugMode = !debugMode;

            for (int i = 0; i < debugList.Count; i++)
            {
                debugList[0] = new ItemData.text(debugList[0].font, "Current Level: " + g.cL.ToString(), debugList[0].position, debugList[0].textColor);
                debugList[1] = new ItemData.text(debugList[1].font, "Previous Level: " + g.pL.ToString(), debugList[1].position, debugList[1].textColor);
                debugList[2] = new ItemData.text(debugList[2].font, "Class Type: " + g.gameManager.classType.ToString(), debugList[2].position, debugList[2].textColor);
            }

            prevKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (debugMode == true)
            {

                spriteBatch.Draw(debugBG, new Rectangle(0, 0, 960, 620), Color.White);

                for (int i = 0; i < debugList.Count; i++)
                {
                    spriteBatch.DrawString(debugList[i].font, debugList[i].msg, new Vector2(debugList[i].position.X + 1, debugList[i].position.Y + 1), Color.Black);
                    spriteBatch.DrawString(debugList[i].font, debugList[i].msg, debugList[i].position, debugList[i].textColor);
                }

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

    }
}
