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
        public Player(Texture2D[] t, Vector2 v, GameManager g) : base(t,v,g)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = 0; i < myGame.enemyShots.Count; i++)
            {
                if (Collision.CheckEllipseEllipseCollision(myGame.enemyShots[i].getEllipse(), getEllipse()))
                {
                    colors[0] = Color.Red;
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
