﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Quarter3Project.EntityTypes
{
    public class Enemy : Entity
    {

        public Enemy(Texture2D t, Vector2 v, GameManager g)
            : base(t, v, g)
        {

        }

        public Enemy(Texture2D[] t, Vector2 v, GameManager g)
            : base(t, v, g)
        {

        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < myGame.friendlyShots.Count; i++)
            {
                if (Collision.CheckEllipseEllipseCollision(myGame.friendlyShots[i].getEllipse(), getEllipse()))
                {
                    colors[0] = Color.Red;
                    takeDamage(myGame.friendlyShots[i].damage);
                    myGame.friendlyShots[i].killHit();
                    i--;
                }
            }
            
            base.Update(gameTime);
        }

        public virtual void takeDamage(int damage) { }

    }
}
