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

namespace Quarter3Project.Managers
{

    public class SplashScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D background;
        int countdown = 1000;
        Game1 myGame;

        public SplashScreenManager(Game1 game)
            : base(game)
        {
            myGame = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            background = Game.Content.Load<Texture2D>(@"Images/Splash");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            countdown -= gameTime.ElapsedGameTime.Milliseconds;
            if (countdown <= 0)
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
    }
}