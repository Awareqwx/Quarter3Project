using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quarter3Project.EntityTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarter3Project
{
    public class Location
    {
        public List<Collision.mapSegment> mapList;
        public List<Collision.Circle> circleList;
        public List<Collision.Ellipse> ellipseList;
        public List<BuildingEntity> buildingList;
        public List<Enemy> enemyList;
        public List<Portal> portals;

        public Location(List<Collision.mapSegment> ml, List<Collision.Circle> cl, List<Collision.Ellipse> el, List<Portal> p, List<BuildingEntity> bl, List<Enemy> enemies)
        {
            mapList = ml;
            circleList = cl;
            ellipseList = el;
            portals = p;
            buildingList = bl;
            enemyList = enemies;
        }

        public Location(List<Collision.mapSegment> ml, List<Collision.Circle> cl, List<Collision.Ellipse> el, List<Portal> p, List<BuildingEntity> bl)
        {
            mapList = ml;
            circleList = cl;
            ellipseList = el;
            portals = p;
            buildingList = bl;
            enemyList = new List<Enemy>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(gameTime, spriteBatch);
            }
        }
    }
}
