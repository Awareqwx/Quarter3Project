using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Quarter3Project.Managers
{
    public class CCManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D ccImage, ccStats, ccBase, ccClass, ccArrows;
        Game1 myGame;

        public CCUI[] ccui;
        public Home[] home;

        public CCManager(Game1 game)
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

            ccClass = Game.Content.Load<Texture2D>(@"Images/ccClass");
            ccImage = Game.Content.Load<Texture2D>(@"Images/ccImage");
            ccArrows = Game.Content.Load<Texture2D>(@"Images/ccArrows");
            
            ccui = new CCUI[1];
            home = new Home[2];

            ccui[0] = new CCUI(myGame, new Vector2(127, 70), ccClass);

            home[0] = new Home(myGame, ccArrows, new Vector2(105, 90), "7");
            home[1] = new Home(myGame, ccArrows, new Vector2(170, 90), "8");
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < home.Length; i++)
            {
                home[i].Update(gameTime);
            }

            ccui[0].Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(ccImage, new Vector2(100, 50), Color.White);
            ccui[0].Draw(gameTime, spriteBatch);

            for (int i = 0; i < home.Length; i++)
            {
                home[i].Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
