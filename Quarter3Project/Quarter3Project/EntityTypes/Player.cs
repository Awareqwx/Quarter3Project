using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Quarter3Project.EntityTypes
{
    public class Player : Entity
    {

        public int health, mana, exp;

        public Player(Texture2D[] t, Vector2 v, GameManager g) : base(t,v,g)
        {

        }

        public override void Update(GameTime gameTime)
        {

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

            base.Update(gameTime);
            for (int i = 0; i < myGame.enemyShots.Count; i++)
            {
                if (Collision.CheckEllipseEllipseCollision(myGame.enemyShots[i].getEllipse(), getEllipse()))
                {
                    colors[0] = Color.Red;
                    health -= myGame.enemyShots[i].damage;
                    myGame.enemyShots.RemoveAt(i);
                    i--;
                }
                else
                {
                    colors[0] = Color.White;
                }
            }
        }
    }
}
