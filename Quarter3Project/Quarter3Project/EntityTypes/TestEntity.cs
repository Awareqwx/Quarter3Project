using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project.EntityTypes
{
    public class TestEntity : Quarter3Project.Entity
    {
        KeyboardState keyboardState, prevKBState;
        MouseState mouse;
        Random r;
        float colorTimer;
        int shotTimer;
        Texture2D atkTex;

        public TestEntity(GameManager g, Texture2D[] t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            mouse = Mouse.GetState();
            myGame = g;
            speed = 2;
            r = new Random();
            atkTex = myGame.Game.Content.Load<Texture2D>(@"Images/Spell");
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(108, 142), new Point(1, 1), new Point(0, 0), 16, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            colorTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            shotTimer -= gameTime.ElapsedGameTime.Milliseconds;

            mouse = Mouse.GetState();

            colors[0] = Color.White;
            if (colors.Length > 1)
            {
                if (colorTimer >= .1F)
                {
                    colors[1] = colors[2] = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                    colorTimer = 0;
                }
            }
                speed = 3;
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            else if(keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }            

            /*
            myGame.playerSegments[0] = new Collision.mapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X + currentSet.frameSize.X, (int)position.Y));
            myGame.playerSegments[1] = new Collision.mapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X, (int)position.Y + currentSet.frameSize.Y));
            myGame.playerSegments[2] = new Collision.mapSegment(new Point((int)position.X, (int)position.Y + currentSet.frameSize.Y), new Point((int)position.X + currentSet.frameSize.X, (int)position.Y + currentSet.frameSize.Y));
            myGame.playerSegments[3] = new Collision.mapSegment(new Point((int)position.X + currentSet.frameSize.X, (int)position.Y), new Point((int)position.X + currentSet.frameSize.X, (int)position.Y + currentSet.frameSize.Y));
            */

            /*
            foreach(Collision.mapSegment ms in myGame.playerSegments)
            {
                if(Collision.CheckCircleSegmentCollision(myGame.Building, ms)) {
                    position = prevPosition;
                    return;
                }
            }
             */

            foreach (BuildingEntity e in myGame.bE)
            {
                if (Collision.CheckCircleCircleCollision(e.collisionCircle, collisionCircle))
                {
                    position = prevPrevPosition;
                    speed = 1;
                }
                else
                {
                    speed = 3;
                }
            }

            for (int i = 0; i < myGame.mapSegments.Count; i++)
            {
                if(collisionRect().Intersects(myGame.mapSegments[i].collisionRect())) 
                {
                    position = prevPosition;
                }
            }

            if (myGame.classType == 3)
            {
                if (mouse.LeftButton == ButtonState.Pressed && shotTimer <= 0)
                {
                    Vector2 p = new Vector2(mouse.X - position.X, mouse.Y - position.Y);
                    myGame.friendlyShots.Add(new Attack(atkTex, position, myGame, p, 5, colors[1], new Point(40, 12)));
                    shotTimer = 100;
                }
            }

            base.Update(gameTime);
        }

    }
}
