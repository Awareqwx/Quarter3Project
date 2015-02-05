﻿using System;
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
        public TestEntity(GameManager g, Texture2D[] t)
            : base(t, new Vector2(100, 100), g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            colors = new Color[] { Color.White };
            speed = 5;
        }

        public TestEntity(GameManager g, Texture2D[] t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            colors = new Color[] { Color.White };
            speed = 5;
        }

        public TestEntity(GameManager g, Texture2D t)
            : base(t, new Vector2(100, 100), g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            colors = new Color[] { Color.White };
            speed = 5;
        }

        public TestEntity(GameManager g, Texture2D t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            colors = new Color[] { Color.White };
            speed = 5;
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(100, 100), new Point(1, 1), new Point(0, 0), 16, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }

        public override void Update(GameTime gameTime)
        {
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

            myGame.playerRadius = 50f;
            myGame.Player.P.X = position.X + myGame.playerRadius;
            myGame.Player.P.Y = position.Y + myGame.playerRadius;
            myGame.Player.R = myGame.playerRadius;

            if(Collision.CheckCircleCircleCollision(myGame.Building, myGame.Player))
            {
                position = prevPosition;
                return;
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
