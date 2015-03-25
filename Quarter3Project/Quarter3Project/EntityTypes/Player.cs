using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project.EntityTypes
{
    public class Player : Entity
    {

        #region Fields

        public int health,
                   mana, 
                   exp;

        protected bool isOff = false;

        protected bool using1, using2;

        KeyboardState keyboardState, prevKBState;

        Game1 G;

        #endregion

        #region Initialization

        public Player(Texture2D[] t, Vector2 v, GameManager g, Game1 g2) : base(t,v,g)
        {
            G = g2;
            using1 = using2 = false;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (prevKBState == null)
                prevKBState = keyboardState;

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.D))
                acceleration.X = .1f;
            else if (velocity.X > 0)
                acceleration.X = .1f * -velocity.X;

            if (keyboardState.IsKeyDown(Keys.Left))
                direction.X = -1;
            else if (keyboardState.IsKeyDown(Keys.Right))
                direction.X = 1;

            if (keyboardState.IsKeyDown(Keys.Up))
                direction.Y = -1;
            else if (keyboardState.IsKeyDown(Keys.Down))
                direction.Y = 1;

            velocity += acceleration;
            if (velocity.X > 3) velocity.X = 3;
            if (velocity.X < 0) velocity.X = 0;
            if (velocity.Y > 3) velocity.Y = 3;
            if (velocity.Y < 0) velocity.Y = 0;
            position += velocity * direction;

            foreach (BuildingEntity e in myGame.currentLoc.buildingList)
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

            for (int i = 0; i < myGame.currentLoc.mapList.Count; i++)
            {
                if (collisionRect().Intersects(myGame.currentLoc.mapList[i].collisionRect()))
                {
                    position = prevPosition;
                }
            }

            for (int i = 0; i < myGame.currentLoc.circleList.Count; i++)
            {
                if (Collision.CheckCircleEllipseCollision(myGame.currentLoc.circleList[i], getEllipse()))
                {
                    position = prevPosition;
                }
            }

            for (int i = 0; i < myGame.currentLoc.ellipseList.Count; i++)
            {
                if (Collision.CheckEllipseEllipseCollision(myGame.currentLoc.ellipseList[i], getEllipse()))
                {
                    position = prevPosition;
                }
            }

            for (int i = 0; i < myGame.currentLoc.portals.Count; i++)
            {
                if (collisionRect().Intersects(myGame.currentLoc.portals[i].collision))
                {
                    myGame.Teleport(myGame.currentLoc.portals[i]);
                }
            }

            for (int i = 0; i < G.popManager.popList.Count; i++)
            {
                if (G.popManager.popList.Count(p => p.getHoverState == true) >= 1)
                {
                    isOff = true;
                }
                else
                {
                    isOff = false;
                }
            }

            base.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.Q) && prevKBState.IsKeyUp(Keys.Q))
            {
                ability1();
            }
            if (keyboardState.IsKeyDown(Keys.E) && prevKBState.IsKeyUp(Keys.E))
            {
                ability2();
            }

            for (int i = 0; i < myGame.enemyShots.Count; i++)
            {
                if (Collision.CheckEllipseEllipseCollision(myGame.enemyShots[i].getEllipse(), getEllipse()))
                {
                    colors[0] = Color.Red;
                    takeDamage(myGame.enemyShots[i].damage);
                    myGame.enemyShots[i].killHit();
                    i--;
                }
                else
                {
                    colors[0] = Color.White;
                }
            }
            prevKBState = keyboardState;
        }

        #endregion

        #region Methods

        public virtual void ability1() { }

        public virtual void ability2() { }

        public virtual void takeDamage(int damage) { }

        public virtual void attack() { }

        #endregion
    }
}
