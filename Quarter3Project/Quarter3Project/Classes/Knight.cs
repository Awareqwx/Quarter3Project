using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quarter3Project.EntityTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project.Classes
{
    public class Knight : Player
    {

        KeyboardState keyboardState, prevKBState;
        MouseState mouse;
        Random r;
        float colorTimer;
        int shotTimer;
        int walkDir;
        Boolean isAttacking, isWalking;
        Game1 G;
        int using2Time;

        public Knight(Texture2D[] t, Vector2 v, GameManager g, Game1 g2)
            : base(t, v, g, g2)
        {
            health = 50;
            mana = 115;
            keyboardState = prevKBState = Keyboard.GetState();
            mouse = Mouse.GetState();
            myGame = g;
            speed = 2;
            r = new Random();
            shotTimer = 0;
            colors = new Color[3];
            addAnimations();
            G = g2;
        }

        public override void addAnimations()
        {
            AnimationSet idlef = new AnimationSet("IDLEF", new Point(60, 97), new Point(1, 1), new Point(0, 0), 0, true);
            AnimationSet idleb = new AnimationSet("IDLEB", new Point(60, 97), new Point(1, 1), new Point(0, 2), 0, true);
            AnimationSet idlel = new AnimationSet("IDLEL", new Point(60, 97), new Point(1, 1), new Point(0, 3), 0, true);
            AnimationSet idler = new AnimationSet("IDLER", new Point(60, 97), new Point(1, 1), new Point(0, 1), 0, true);
            AnimationSet walkf = new AnimationSet("WALKF", new Point(60, 97), new Point(4, 1), new Point(0, 0), 150, true);
            AnimationSet walkb = new AnimationSet("WALKB", new Point(60, 97), new Point(4, 1), new Point(0, 2), 150, true);
            AnimationSet walkl = new AnimationSet("WALKL", new Point(60, 97), new Point(4, 1), new Point(0, 3), 150, true);
            AnimationSet walkr = new AnimationSet("WALKR", new Point(60, 97), new Point(4, 1), new Point(0, 1), 150, true);
            AnimationSet atkf = new AnimationSet("ATKF", new Point(59, 97), new Point(3, 0), new Point(0, 5), 100, false);
            AnimationSet atkb = new AnimationSet("ATKB", new Point(66, 97), new Point(3, 0), new Point(0, 7), 100, false);
            AnimationSet atkl = new AnimationSet("ATKL", new Point(90, 97), new Point(3, 0), new Point(0, 6), 100, false);
            AnimationSet atkr = new AnimationSet("ATKR", new Point(90, 97), new Point(3, 0), new Point(0, 4), 100, false);
            sets.Add(idlef);
            sets.Add(idleb);
            sets.Add(idlel);
            sets.Add(idler);
            sets.Add(walkf);
            sets.Add(walkb);
            sets.Add(walkl);
            sets.Add(walkr);
            sets.Add(atkf);
            sets.Add(atkb);
            sets.Add(atkl);
            sets.Add(atkr);
            setAnimation("IDLEF");
            base.addAnimations();
        }        

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            colorTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.D))
            {
                acceleration.X = .1f;
            }
            else if (velocity.X > 0)
            {
                acceleration.X = .08f * -velocity.X;
            }

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.S))
            {
                acceleration.Y = .1f;
            }
            else if (velocity.Y > 0)
            {
                acceleration.Y = .08f * -velocity.Y;
            }

            if (keyboardState.IsKeyDown(Keys.A))
                direction.X = -1;
            else if (keyboardState.IsKeyDown(Keys.D))
                direction.X = 1;

            if (keyboardState.IsKeyDown(Keys.W))
                direction.Y = -1;
            else if (keyboardState.IsKeyDown(Keys.S))
                direction.Y = 1;

            velocity += acceleration;
            if (velocity.X > 3) velocity.X = 3;
            if (velocity.X < 0) velocity.X = 0;
            if (velocity.Y > 3) velocity.Y = 3;
            if (velocity.Y < 0) velocity.Y = 0;
            position += velocity * direction;


            mouse = Mouse.GetState();

            colors[0] = Color.White;
            if (colors.Length > 1)
            {
                if (colorTimer >= .1F)
                {
                    if (using2)
                    {
                        colors[1] = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                    }
                    else
                    {
                        colors[1] = Color.Gray;
                    }
                    if (using1)
                    {
                        colors[2] = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                    }
                    else
                    {
                        colors[2] = Color.LightGray;
                    }
                    colorTimer = 0;
                }
            }
            isWalking = false;
            #region Attack
            if (!isAttacking)
                {
                    if (keyboardState.IsKeyDown(Keys.S))
                    {
                        isWalking = true;
                        setAnimation("WALKF");
                        walkDir = 0;
                    }
                    else if (keyboardState.IsKeyDown(Keys.W))
                    {
                        isWalking = true;
                        setAnimation("WALKB");
                        walkDir = 1;
                    }
                    if (keyboardState.IsKeyDown(Keys.D))
                    {
                        if (!isWalking)
                        {
                            setAnimation("WALKR");
                            walkDir = 2;
                        }
                        isWalking = true;
                    }
                    else if (keyboardState.IsKeyDown(Keys.A))
                    {
                        if (!isWalking)
                        {
                            setAnimation("WALKL");
                            walkDir = 3;
                        }
                        isWalking = true;
                    }

                    if (!isWalking)
                    {
                        switch (walkDir)
                        {
                            case 1:
                                setAnimation("IDLEB");
                                break;
                            case 2:
                                setAnimation("IDLER");
                                break;
                            case 3:
                                setAnimation("IDLEL");
                                break;
                            case 0:
                            default:
                                setAnimation("IDLEF");
                                break;
                        }
                    }
                }
                else
                {
                    if (animIsOver)
                    {
                        isAttacking = false;
                        if (walkDir == 3)
                        {
                            position.X += 30;
                        }
                    }
                }

            if(isOff == false) {
                if (mouse.LeftButton == ButtonState.Pressed && shotTimer <= 0)
                {
                    if (!isAttacking && !isWalking)
                    {
                        switch (walkDir)
                        {
                            case 0:
                            default:
                                setAnimation("IDLEF");
                                break;
                            case 1:
                                setAnimation("IDLEB");
                                break;
                            case 2:
                                setAnimation("IDLER");
                                break;
                            case 3:
                                setAnimation("IDLEL");
                                break;
                        }

                    }

                    switch (walkDir)
                    {
                        case 1:
                            setAnimation("ATKB");
                            break;
                        case 2:
                            setAnimation("ATKR");
                            break;
                        case 3:
                            if (currentSet.name != "ATKL")
                            {
                                position.X -= 30;
                            }
                            setAnimation("ATKL");
                            break;
                        case 0:
                        default:
                            setAnimation("ATKF");
                            break;
                    }
                    if (!isAttacking)
                    {
                        attack();
                    }
                    isAttacking = true;

                    //myGame.friendlyShots.Add(new 

                    if (!isWalking && !isAttacking)
                    {
                        switch (walkDir)
                        {
                            case 0:
                            default:
                                setAnimation("IDLEF");
                                break;
                            case 1:
                                setAnimation("IDLEB");
                                break;
                            case 2:
                                setAnimation("IDLER");
                                break;
                            case 3:
                                setAnimation("IDLEL");
                                break;
                        }
                    }
                }
            #endregion

            }

            using2Time -= gameTime.ElapsedGameTime.Milliseconds;
            if (using2Time <= 0)
            {
                using2 = false;
            }

            if (mana == 0)
            {
                using1  = false;
            }

            base.Update(gameTime);
        }

        public override void ability1()
        {
            using1 = !using1;
        }

        public override void ability2()
        {
            if(mana >= 15){
            if (!using2) 
                mana -= 15;
            using2 = true;
            using2Time = 2500;
            }
        }

        public override void takeDamage(int damage)
        {
            if(!using2)
            {
                health -= damage - 1;
            }
            else
            {
                health -= damage / 2 - 1;
            }

        }

        public override void attack()
        {
            int damage = using1 ? 5 : 10;
            if (using1) 
                mana -= 5;
            switch (walkDir)
            {
                case 0:
                    myGame.friendlyShots.Add(new Attack(new Texture2D(myGame.Game.GraphicsDevice, 60, 10), new Vector2(position.X, position.Y + currentSet.frameSize.Y), myGame, 0, 0, Color.White, new Point(60, 10), damage, 300, myGame.friendlyShots.Count, true)); 
                    break;
                case 1:
                    myGame.friendlyShots.Add(new Attack(new Texture2D(myGame.Game.GraphicsDevice, 60, 10), new Vector2(position.X, position.Y - 10), myGame, 0, 0, Color.White, new Point(60, 10), damage, 300, myGame.friendlyShots.Count, true));
                    break;
                case 2:
                    myGame.friendlyShots.Add(new Attack(new Texture2D(myGame.Game.GraphicsDevice, 10, 97), new Vector2(position.X + currentSet.frameSize.X, position.Y), myGame, 0, 0, Color.White, new Point(10, 97), damage, 300, myGame.friendlyShots.Count, true));
                    break;
                case 3:
                    myGame.friendlyShots.Add(new Attack(new Texture2D(myGame.Game.GraphicsDevice, 10, 97), new Vector2(position.X - 10, position.Y), myGame, 0, 0, Color.White, new Point(60, 10), damage, 300, myGame.friendlyShots.Count, true));
                    break;
            }
        }

    }
}
