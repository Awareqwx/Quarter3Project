using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project.EntityTypes
{
    class TestEntity : Quarter3Project.Entity
    {
        KeyboardState keyboardState, prevKBState;
        Random r;
        float timer;

        public TestEntity(GameManager g, Texture2D[] t)
            : base(t, new Vector2(100, 100), g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
        }

        public TestEntity(GameManager g, Texture2D[] t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
        }

        public TestEntity(GameManager g, Texture2D t)
            : base(t, new Vector2(100, 100), g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
        }

        public TestEntity(GameManager g, Texture2D t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(108, 142), new Point(1, 1), new Point(0, 0), 16, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            colors[0] = Color.White;
            if (timer >= .1F)
            {
                colors[1] = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
                timer = 0;
            }
                speed = 20;
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
                    speed = 5;
                }
            }

            for (int i = 0; i < myGame.mapSegments.Count; i++)
            {
                if(collisionRect().Intersects(myGame.mapSegments[i].collisionRect())) 
                {
                    position = prevPosition;
                }
            }
            
            
            base.Update(gameTime);
        }

    }
}
