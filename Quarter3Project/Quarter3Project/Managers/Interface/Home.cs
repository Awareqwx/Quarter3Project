using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Quarter3Project;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Buttons are as follows:
 * 1 - Create New Character(Menu)
 * 2 - Load from previous game(Menu)
 * 3 - Quit game(Menu)
 * 4 - Yes(Various Uses)
 * 5 - No(Various Uses)
 * 6 - Esc(Various Uses)
 * 7 - Left Arrow(Char Creation)
 * 8 - Right Arrow(Char Creation)
 * 9 - Ok(Char Creation)
*/

namespace Quarter3Project.Managers
{
    public class Home : AnimatedSprite
    {
        Game1 myGame;
        MouseState mouseState, prevMouseState;
        KeyboardState keyBoardState, prevKeyBoardState;
        Point mousePos;
        string Name;
        float timer;

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
            keyBoardState = Keyboard.GetState();
            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;

            switch (Name)
            {
                case "1":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS1");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.buttonPressed = 1;
                            myGame.SetCurrentLevel(Game1.GameLevels.CC);
                        }
                    }
                    else
                    {
                        setAnimation("IDLE1");
                    }
                    break;
                case "2":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS2");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.buttonPressed = 2;
                            myGame.SetCurrentLevel(Game1.GameLevels.UI);
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
                            myGame.buttonPressed = 3;
                            myGame.SetCurrentLevel(Game1.GameLevels.UI);                           
                        }
                    }
                    else
                    {
                        setAnimation("IDLE3");
                    }
                    break;
                case "4":
                    if (collisionRect().Contains(mousePos) && myGame.prevButtonPressed != 4)
                    {
                        setAnimation("PRESS4");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.buttonPressed = 4;
                            switch (myGame.currentLevel)
                            {
                                case Game1.GameLevels.PLAY:
                                case Game1.GameLevels.CC:
                                    myGame.SetCurrentLevel(Game1.GameLevels.MENU);
                                    myGame.gameManager.save();
                                    break;
                                case Game1.GameLevels.MENU:
                                    if (myGame.prevButtonPressed == 3)
                                    {
                                        myGame.Exit();
                                    }
                                    if (myGame.prevButtonPressed == 2)
                                    {
                                        myGame.SetCurrentLevel(Game1.GameLevels.PLAY);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        setAnimation("IDLE4");
                    }
                    break;
                case "5":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS5");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.buttonPressed = 5;
                            switch (myGame.currentLevel) { 
                                case Game1.GameLevels.MENU:
                                    myGame.SetCurrentLevel(Game1.GameLevels.MENU);
                                    break;
                                case Game1.GameLevels.PLAY:
                                    myGame.SetCurrentLevel(Game1.GameLevels.PLAY);
                                    break;
                                case Game1.GameLevels.CC:
                                    myGame.SetCurrentLevel(Game1.GameLevels.CC);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        setAnimation("IDLE5");
                    }
                    break;
                case "7":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("LEFT");
                        
                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (timer > .1F)
                                {
                                    myGame.currentChar -= 1;
                                    timer = 0;
                                }
                            }
                            
                        
                    }
                    else
                    {
                        setAnimation("LEFT");
                    }
                    break;
                case "8":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("RIGHT");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (timer > .1F)
                            {
                                myGame.currentChar += 1;
                                timer = 0;
                            }
                            
                        }
                    }
                    else
                    {
                        setAnimation("RIGHT");
                    }
                    break;
                case "9":
                    if (collisionRect().Contains(mousePos))
                    {
                        setAnimation("PRESS9");
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            myGame.buttonPressed = 9;
                            myGame.SetCurrentLevel(Game1.GameLevels.UI);
                        }
                    }
                    else
                    {
                        setAnimation("IDLE9");
                    }
                    break;
            }            
                        
            prevMouseState = mouseState;
            prevKeyBoardState = keyBoardState;
            myGame.prevButtonPressed = myGame.buttonPressed;
            base.Update(gameTime);
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE1", new Point(100, 45), new Point(1, 1), new Point(0, 0), 0, false);
            AnimationSet press = new AnimationSet("PRESS1", new Point(100, 45), new Point(1, 1), new Point(1, 0), 0, false);
            AnimationSet idle2 = new AnimationSet("IDLE2", new Point(100, 45), new Point(1, 1), new Point(0, 1), 0, false);
            AnimationSet press2 = new AnimationSet("PRESS2", new Point(100, 45), new Point(1, 1), new Point(1, 1), 0, false);
            AnimationSet idle3 = new AnimationSet("IDLE3", new Point(100, 45), new Point(1, 1), new Point(0, 2), 0, false);
            AnimationSet press3 = new AnimationSet("PRESS3", new Point(100, 45), new Point(1, 1), new Point(1, 2), 0, false);
            AnimationSet idle4 = new AnimationSet("IDLE4", new Point(100, 45), new Point(1, 1), new Point(0, 3), 0, false);
            AnimationSet press4 = new AnimationSet("PRESS4", new Point(100, 45), new Point(1, 1), new Point(1, 3), 0, false);
            AnimationSet idle5 = new AnimationSet("IDLE5", new Point(100, 45), new Point(1, 1), new Point(0, 4), 0, false);
            AnimationSet press5 = new AnimationSet("PRESS5", new Point(100, 45), new Point(1, 1), new Point(1, 4), 0, false);
            AnimationSet leftArrow = new AnimationSet("LEFT", new Point(25, 30), new Point(1, 1), new Point(0, 0), 0, false);
            AnimationSet rightArrow = new AnimationSet("RIGHT", new Point(25, 30), new Point(1, 1), new Point(1, 0), 0, false);
            AnimationSet idle9 = new AnimationSet("IDLE9", new Point(100, 45), new Point(1, 1), new Point(0, 5), 0, false);
            AnimationSet press9 = new AnimationSet("PRESS9", new Point(100, 45), new Point(1, 1), new Point(1, 5), 0, false);
            sets.Add(idle);
            sets.Add(press);
            sets.Add(idle2);
            sets.Add(press2);
            sets.Add(idle3); 
            sets.Add(press3);
            sets.Add(idle4);
            sets.Add(press4);
            sets.Add(idle5);
            sets.Add(press5);
            sets.Add(leftArrow);
            sets.Add(rightArrow);
            sets.Add(idle9);
            sets.Add(press9);
            setAnimation("IDLE1");
            base.addAnimations();
        }

    }
}