using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Quarter3Project.Managers
{
    public class MenuManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D background, menu;
        SpriteFont consolas;
        Game1 myGame;
        KeyboardState ke;        

        public Home[] Home;
        
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
            menu = Game.Content.Load<Texture2D>(@"Images/MenuOpts");
            consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            Home = new Home[3];

            Home[0] = (new Home(myGame, menu, new Vector2(100, 200), "1"));
            Home[1] = (new Home(myGame, menu, new Vector2(100, 300), "2"));
            Home[2] = (new Home(myGame, menu, new Vector2(100, 400), "3"));
            

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            ke = Keyboard.GetState();

            for (int i = 0; i < Home.Length; i++)
                Home[i].Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            for (int i = 0; i < Home.Length; i++)
            {
                Home[i].Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(consolas, myGame.buttonPressed.ToString(), new Vector2(120, 120), Color.Cyan);
            }            

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
