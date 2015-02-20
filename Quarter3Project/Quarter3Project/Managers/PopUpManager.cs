using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.IO;

namespace Quarter3Project.Managers
{
    public class PopUpManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont consolas;
        Texture2D DialogBox, buttons;
        Game1 myGame;
        public YesNo[] YesNoDialog;
        public Home[] home;

        public PopUpManager(Game1 game)
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
            consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");
            DialogBox = Game.Content.Load <Texture2D>(@"Images/DialogBox");
            buttons = Game.Content.Load<Texture2D>(@"Images/MenuOpts");

            YesNoDialog = new YesNo[5];
            home = new Home[3];

            YesNoDialog[0] = new YesNo(myGame, DialogBox, new Vector2((myGame.GraphicsDevice.Viewport.Width / 2) - 300, (myGame.GraphicsDevice.Viewport.Height / 2) - 100), this, "Do you want to quit the game?");
            YesNoDialog[1] = new YesNo(myGame, DialogBox, new Vector2((myGame.GraphicsDevice.Viewport.Width / 2) - 300, (myGame.GraphicsDevice.Viewport.Height / 2) - 100), this, "Do you want to return to the menu?");
            YesNoDialog[2] = new YesNo(myGame, DialogBox, new Vector2((myGame.GraphicsDevice.Viewport.Width / 2) - 300, (myGame.GraphicsDevice.Viewport.Height / 2) - 100), this, "Continue previous save game?");
            YesNoDialog[3] = new YesNo(myGame, DialogBox, new Vector2((myGame.GraphicsDevice.Viewport.Width / 2) - 300, (myGame.GraphicsDevice.Viewport.Height / 2) - 100), this, "Create new character? This will delete any previous\ncharacters.");
            YesNoDialog[4] = new YesNo(myGame, DialogBox, new Vector2((myGame.GraphicsDevice.Viewport.Width / 2) - 300, (myGame.GraphicsDevice.Viewport.Height / 2) - 100), this, "There is no previous save game detected. Create a \ncharacter before starting the game.");

            home[0] = new Home(myGame, buttons, new Vector2((YesNoDialog[0].getPos().X + YesNoDialog[0].getFrameSize().X - 110),
                                                            (YesNoDialog[0].getPos().Y + YesNoDialog[0].getFrameSize().Y) - 55), "4");
            home[1] = new Home(myGame, buttons, new Vector2((YesNoDialog[0].getPos().X + YesNoDialog[0].getFrameSize().X - 220),
                                                            (YesNoDialog[0].getPos().Y + YesNoDialog[0].getFrameSize().Y) - 55), "5");
            home[2] = new Home(myGame, buttons, new Vector2((YesNoDialog[0].getPos().X + YesNoDialog[0].getFrameSize().X - 110),
                                                            (YesNoDialog[0].getPos().Y + YesNoDialog[0].getFrameSize().Y) - 55), "10");
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < YesNoDialog.Length; i++)
                YesNoDialog[i].Update(gameTime);

            for (int i = 0; i < 2; i++)
                home[i].Update(gameTime);

            if (!File.Exists(@"Save/Save.txt"))
            {
                home[2].Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if(myGame.prevButtonPressed == 3)
            {
                YesNoDialog[0].Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(consolas, YesNoDialog[0].ss, new Vector2(YesNoDialog[0].getPos().X + 10, YesNoDialog[0].getPos().Y + 10), Color.Blue);
                home[0].Draw(gameTime, spriteBatch);
                home[1].Draw(gameTime, spriteBatch);
            }
            else if (myGame.prevButtonPressed == 6)
            {
                YesNoDialog[1].Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(consolas, YesNoDialog[1].ss, new Vector2(YesNoDialog[1].getPos().X + 10, YesNoDialog[1].getPos().Y + 10), Color.Blue);
                home[0].Draw(gameTime, spriteBatch);
                home[1].Draw(gameTime, spriteBatch);
            }
            else if (myGame.prevButtonPressed == 2 && File.Exists(@"Save/Save.txt"))
            {
                YesNoDialog[2].Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(consolas, YesNoDialog[2].ss, new Vector2(YesNoDialog[2].getPos().X + 10, YesNoDialog[2].getPos().Y + 10), Color.Blue);
                home[0].Draw(gameTime, spriteBatch);
                home[1].Draw(gameTime, spriteBatch);
            } else if(!File.Exists(@"Save/Save.txt")) {
                YesNoDialog[4].Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(consolas, YesNoDialog[4].ss, new Vector2(YesNoDialog[4].getPos().X + 10, YesNoDialog[4].getPos().Y + 10), Color.Blue);
                home[2].Draw(gameTime, spriteBatch);
            }
            if (myGame.prevButtonPressed == 9)
            {
                YesNoDialog[3].Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(consolas, YesNoDialog[3].ss, new Vector2(YesNoDialog[3].getPos().X + 10, YesNoDialog[3].getPos().Y + 10), Color.Blue);
                home[0].Draw(gameTime, spriteBatch);
                home[1].Draw(gameTime, spriteBatch);
            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
