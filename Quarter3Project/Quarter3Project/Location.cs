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
        public Texture2D mapBG1;
        public List<Collision.mapSegment> mapList;
        public List<Collision.Circle> circleList;
        public List<Collision.Ellipse> ellipseList;
        public List<BuildingEntity> buildingList;
        public List<Enemy> enemyList;
        public List<Portal> portals;

        public Location(List<Collision.mapSegment> ml, List<Collision.Circle> cl, List<Collision.Ellipse> el, List<Portal> p, List<BuildingEntity> bl, List<Enemy> enemies, Texture2D mapbg)
        {
            mapList = ml;
            circleList = cl;
            ellipseList = el;
            portals = p;
            buildingList = bl;
            enemyList = enemies;
            mapBG1 = mapbg;
        }

        public Location(List<Collision.mapSegment> ml, List<Collision.Circle> cl, List<Collision.Ellipse> el, List<Portal> p, List<BuildingEntity> bl, Texture2D mapbg)
        {
            mapList = ml;
            circleList = cl;
            ellipseList = el;
            portals = p;
            buildingList = bl;
            enemyList = new List<Enemy>();
            mapBG1 = mapbg;
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
           
            spriteBatch.Draw(mapBG1, new Rectangle(-(mapBG1.Width / 2) + (960 / 2), -(mapBG1.Height / 2) + (640 / 2), mapBG1.Width, mapBG1.Height), Color.White);
       
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(gameTime, spriteBatch);
            }      
            
        }
    }
}
