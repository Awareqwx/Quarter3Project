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
    public class Mage : Player
    {

        KeyboardState keyboardState, prevKBState;
        MouseState mouse;
        Random r;
        float colorTimer;
        int shotTimer;
        int walkDir;
        Boolean isAttacking, isWalking;
        Texture2D spell, fireball;

        public Mage(Texture2D[] t, Vector2 v, GameManager g, Game1 g2)
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
            spell = myGame.Game.Content.Load<Texture2D>(@"Images\Spell");
            fireball = myGame.Game.Content.Load<Texture2D>(@"Images\Wizard\Fireball");
        }

        public override void addAnimations()
        {
            AnimationSet idlef = new AnimationSet("IDLEF", new Point(60, 97), new Point(1, 1), 0, new Point(0, 104), true);
            AnimationSet idleb = new AnimationSet("IDLEB", new Point(60, 97), new Point(1, 1), 0, new Point(0, 204), true);
            AnimationSet idlel = new AnimationSet("IDLEL", new Point(60, 97), new Point(1, 1), 0, new Point(0, 306), true);
            AnimationSet idler = new AnimationSet("IDLER", new Point(60, 97), new Point(1, 1), 0, new Point(0, 0), true);
            AnimationSet walkf = new AnimationSet("WALKF", new Point(60, 97), new Point(2, 1), 150, new Point(0, 104), true);
            AnimationSet walkb = new AnimationSet("WALKB", new Point(60, 97), new Point(2, 1), 150, new Point(0, 204), true);
            AnimationSet walkl = new AnimationSet("WALKL", new Point(60, 97), new Point(2, 1), 150, new Point(0, 306), true);
            AnimationSet walkr = new AnimationSet("WALKR", new Point(60, 97), new Point(2, 1), 150, new Point(0, 0), true);
            AnimationSet atkf = new AnimationSet("ATKF", new Point(59, 97), new Point(2, 0), 150, new Point(61, 104), false);
            AnimationSet atkb = new AnimationSet("ATKB", new Point(66, 97), new Point(2, 0), 150, new Point(64, 204), false);
            AnimationSet atkl = new AnimationSet("ATKL", new Point(110, 95), new Point(2, 0), 150, new Point(63, 306), false);
            AnimationSet atkr = new AnimationSet("ATKR", new Point(110, 95), new Point(2, 0), 150, new Point(61, 0), false);
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
            colorTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            mouse = Mouse.GetState();

            colors[0] = Color.White;
            colors[2] = Color.DarkGreen;
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

                        colors[1] = Color.Blue;
                    }
                    colorTimer = 0;
                }
            }
            speed = 3;
            keyboardState = Keyboard.GetState();
            isWalking = false;

            if (!isAttacking)
            {
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    position.Y += speed;
                    isWalking = true;
                    setAnimation("WALKF");
                    walkDir = 0;
                }
                else if (keyboardState.IsKeyDown(Keys.W))
                {
                    position.Y -= speed;
                    isWalking = true;
                    setAnimation("WALKB");
                    walkDir = 1;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    position.X += speed;
                    if (!isWalking)
                    {
                        setAnimation("WALKR");
                        walkDir = 2;
                    }
                    isWalking = true;
                }
                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    position.X -= speed;
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

            if (isOff == false)
            {
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
                }

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

            if (mana == 0)
            {
                using2 = false;
            }

            base.Update(gameTime);
        }

        public override void ability1()
        {
            if (mana >= 5)
            {
                mana -= 10;
                myGame.friendlyShots.Add(new Projectile(fireball, getCenter(), myGame, new Vector2(mouse.X -getCenter().X, mouse.Y - getCenter().Y), 10, Color.White, new Point(30, 20), 20, 10000, myGame.friendlyShots.Count, true));
            }
        }

        public override void ability2()
        {
            using2 = !using2;
        }

        public override void attack()
        {
            myGame.friendlyShots.Add(new Projectile(spell, getCenter(), myGame, new Vector2(mouse.X - getCenter().X, mouse.Y - getCenter().Y), 5, Color.DarkGreen, new Point(40, 12), 10, 10000, myGame.friendlyShots.Count, true));
        }

        public override void takeDamage(int damage)
        {
            if (using2)
            {
                health -= damage / 3;
                mana -= (int)(damage / 1.5);
            }
            else
            {
                health -= damage;
            }
        }

    }
}
