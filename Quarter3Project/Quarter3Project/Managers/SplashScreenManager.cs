#region File Description
// SplashScreenManager.cs
#endregion 

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Quarter3Project.Managers
{
    public class SplashScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        #region Constants

        // How long until splash goes away in Milliseconds?
        private const int countdown = 1000;

        #endregion

        #region Fields

        Game1 myGame;

        SpriteBatch spriteBatch;

        Texture2D background;

        int countdown2 = countdown;

        #endregion

        #region Initialization

        public SplashScreenManager(Game1 game)
            : base(game)
        {
            myGame = game;
        }
        
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            background = Game.Content.Load<Texture2D>(@"Images/Splash");

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            countdown2 -= gameTime.ElapsedGameTime.Milliseconds;
            if (countdown2 <= 0)
            {
                myGame.SetCurrentLevel(GameLevels.GameLevels.MENU);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

    }
}