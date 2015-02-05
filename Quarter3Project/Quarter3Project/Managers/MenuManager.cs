using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Quarter3Project
{
    public class MenuManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D background;
        Game1 myGame;
        KeyboardState keyboardState, prevKeyboardState;

        public MenuManager(Game1 game) : base(game)
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
            background = Game.Content.Load<Texture2D>(@"Images/Menu");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyUp(Keys.Enter) && prevKeyboardState.IsKeyDown(Keys.Enter))
            {
                myGame.SetCurrentLevel(Game1.GameLevels.PLAY);
            }

            prevKeyboardState = keyboardState;
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
