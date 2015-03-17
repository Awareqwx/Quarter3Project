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
    class Wizard : Player
    {

        KeyboardState keyboardState, prevKBState;
        MouseState mouse;
        Random r;
        float colorTimer;
        int shotTimer;
        int walkDir;
        Boolean isAttacking, isWalking;

        Texture2D atkTex;

        public Wizard(Texture2D[] t, Vector2 v, GameManager g)
            : base(t, v, g)
        {
            health = 50;
            keyboardState = prevKBState = Keyboard.GetState();
            mouse = Mouse.GetState();
            myGame = g;
            speed = 2;
            r = new Random();
            shotTimer = 0;
            colors = new Color[3];
            addAnimations();

            atkTex = myGame.Game.Content.Load<Texture2D>(@"Images\Spell");
        }

        public override void addAnimations()
        {
            AnimationSet idlef = new AnimationSet("IDLEF", new Point(60, 97), new Point(1, 1), new Point(0, 0), 16, false);
            AnimationSet idleb = new AnimationSet("IDLEB", new Point(60, 97), new Point(1, 1), new Point(0, 2), 16, false);
            AnimationSet idlel = new AnimationSet("IDLEL", new Point(60, 97), new Point(1, 1), new Point(0, 3), 16, false);
            AnimationSet idler = new AnimationSet("IDLER", new Point(60, 97), new Point(1, 1), new Point(0, 1), 16, false);
            AnimationSet walkf = new AnimationSet("WALKF", new Point(60, 97), new Point(4, 1), new Point(0, 0), 150, true);
            AnimationSet walkb = new AnimationSet("WALKB", new Point(60, 97), new Point(4, 1), new Point(0, 2), 150, true);
            AnimationSet walkl = new AnimationSet("WALKL", new Point(60, 97), new Point(4, 1), new Point(0, 3), 150, true);
            AnimationSet walkr = new AnimationSet("WALKR", new Point(60, 97), new Point(4, 1), new Point(0, 1), 150, true);
            AnimationSet atkf = new AnimationSet("ATKF", new Point(60, 97), new Point(3, 1), new Point(0, 5), 100, false);
            AnimationSet atkb = new AnimationSet("ATKB", new Point(66, 97), new Point(3, 1), new Point(0, 7), 100, false);
            AnimationSet atkl = new AnimationSet("ATKL", new Point(90, 97), new Point(3, 1), new Point(0, 6), 100, false);
            AnimationSet atkr = new AnimationSet("ATKR", new Point(90, 97), new Point(3, 1), new Point(0, 4), 100, false);
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
            if (colors.Length > 1)
            {
                if (colorTimer >= .1F)
                {
                    colors[1] = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                    colors[2] = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                    colorTimer = 0;
                }
            }
            colors[1] = Color.Gold;
            colors[2] = Color.Cyan;
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
                isAttacking = true;
                Vector2 dir = Collision.unitVector((new Vector2(mouse.X, mouse.Y)) - getCenter());
                
                myGame.friendlyShots.Add(new Projectile(atkTex, getCenter(), myGame, dir, 4, colors[2], new Point(40, 12), 5));
            }

            base.Update(gameTime);
        }
    }
}
