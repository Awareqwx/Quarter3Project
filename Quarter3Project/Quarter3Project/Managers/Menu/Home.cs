
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Quarter3Project;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project.Managers
{
    public class Home : AnimatedSprite
    {
        Game1 myGame;
        MouseState mouseState;
        Point mousePos;
        string Name;

        public Home(Game1 g, Texture2D t, Vector2 v, string name)
            : base(t)
        {
            myGame = g;
            position = v;
            Name = name;
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;

            //mouseState.LeftButton == ButtonState.Pressed
            switch (Name)
            {
                case "1":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS");
                    }
                    else
                    {
                        setAnimation("IDLE");
                    }
                    break;
                case "2":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS2");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.SetCurrentLevel(Game1.GameLevels.PLAY);
                        }
                    }
                    else
                    {
                        setAnimation("IDLE2");
                    }
                    break;
                case "3":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS3");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.Exit();
                        }
                    }
                    else
                    {
                        setAnimation("IDLE3");
                    }
                    break;
            }
            

            base.Update(gameTime);
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(100, 45), new Point(1, 1), new Point(0, 0), 0, false);
            AnimationSet press = new AnimationSet("PRESS", new Point(100, 45), new Point(1, 1), new Point(1, 0), 0, false);
            AnimationSet idle2 = new AnimationSet("IDLE2", new Point(100, 45), new Point(1, 1), new Point(0, 1), 0, false);
            AnimationSet press2 = new AnimationSet("PRESS2", new Point(100, 45), new Point(1, 1), new Point(1, 1), 0, false);
            AnimationSet idle3 = new AnimationSet("IDLE3", new Point(100, 45), new Point(1, 1), new Point(0, 2), 0, false);
            AnimationSet press3 = new AnimationSet("PRESS3", new Point(100, 45), new Point(1, 1), new Point(1, 2), 0, false);
            sets.Add(idle);
            sets.Add(press);
            sets.Add(idle2);
            sets.Add(press2);
            sets.Add(idle3);
            sets.Add(press3);
            setAnimation("IDLE");
            base.addAnimations();
        }

    }
}