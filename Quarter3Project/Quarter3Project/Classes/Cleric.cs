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
    class Cleric : Player
    {

        KeyboardState keyboardState, prevKBState;
        MouseState mouse;
        Random r;
        float colorTimer;
        int shotTimer;
        int walkDir;
        Boolean isAttacking, isWalking;
        Texture2D spell, arrow;

        public Cleric(Texture2D[] t, Vector2 v, GameManager g, Game1 g2)
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
            colors = new Color[1];
            spell = myGame.Game.Content.Load<Texture2D>(@"Images\Spell");
            arrow = myGame.Game.Content.Load<Texture2D>(@"Images\Cleric\lightArrow");
            addAnimations();
        }

        public override void addAnimations()
        {
            AnimationSet idlef = new AnimationSet("IDLEF", new Point(59, 97), new Point(1, 1), new Point(0, 0), 16, false);
            AnimationSet idleb = new AnimationSet("IDLEB", new Point(59, 97), new Point(1, 1), new Point(0, 1), 16, false);
            AnimationSet idlel = new AnimationSet("IDLEL", new Point(59, 97), new Point(1, 1), new Point(0, 3), 16, false);
            AnimationSet idler = new AnimationSet("IDLER", new Point(59, 97), new Point(1, 1), new Point(0, 2), 16, false);
            AnimationSet walkf = new AnimationSet("WALKF", new Point(59, 97), new Point(2, 1), new Point(0, 0), 150, true);
            AnimationSet walkb = new AnimationSet("WALKB", new Point(59, 97), new Point(2, 1), new Point(0, 1), 150, true);
            AnimationSet walkl = new AnimationSet("WALKL", new Point(59, 97), new Point(2, 1), new Point(0, 3), 150, true);
            AnimationSet walkr = new AnimationSet("WALKR", new Point(59, 97), new Point(2, 1), new Point(0, 2), 150, true);
            AnimationSet atkf = new AnimationSet("ATKF", new Point(59, 97), new Point(2, 1), new Point(1, 0), 150, false);
            AnimationSet atkb = new AnimationSet("ATKB", new Point(59, 97), new Point(2, 1), new Point(1, 1), 150, false);
            AnimationSet atkl = new AnimationSet("ATKL", new Point(59, 97), new Point(2, 1), new Point(1, 3), 150, false);
            AnimationSet atkr = new AnimationSet("ATKR", new Point(59, 97), new Point(2, 1), new Point(1, 2), 150, false);
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
                }
            }

            if (isOff == false)
            {
                if (mouse.LeftButton == ButtonState.Pressed && shotTimer <= 0)
                {
                    if (!isAttacking)
                    {
                        setAnimation("IDLEF");
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
            }

            base.Update(gameTime);
        }

        public override void attack()
        {
            myGame.friendlyShots.Add(new Projectile(spell, getCenter(), myGame, new Vector2(mouse.X - getCenter().X, mouse.Y - getCenter().Y), 5, Color.Gold, new Point(40, 12), 5, 10000, myGame.friendlyShots.Count, true));
        }

        public override void takeDamage(int damage)
        {
            health -= damage;
        }

        public override void ability1()
        {
            if (mana >= 10)
            {
                mana -= 10;
                myGame.friendlyShots.Add(new Projectile(arrow, getCenter(), myGame, new Vector2(mouse.X - getCenter().X, mouse.Y - getCenter().Y), 20, Color.White, new Point(28, 8), 15, 10000, myGame.friendlyShots.Count, true));
            }
        }

        public override void ability2()
        {
            if(mana >= 15)
            {
                mana -= 15;
                health += 25;
            }
        }

    }
}
