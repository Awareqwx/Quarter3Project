using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarter3Project
{
    public class Entity : AnimatedSprite
    {
        protected GameManager myGame;

        public Entity(Texture2D[] t, Vector2 p, GameManager g) : base(t)
        {
            myGame = g;
            position = p;
        }

        public Entity(Texture2D t, Vector2 p, GameManager g)
            : base(t)
        {
            myGame = g;
            position = p;
        }

        public Entity(Texture2D[] t, GameManager g)
            : base(t)
        {
            myGame = g;
        }

        public Entity(Texture2D t, GameManager g)
            : base(t)
        {
            myGame = g;
        }

        public virtual Collision.Ellipse getEllipse()
        {
            Collision.Ellipse E = new Collision.Ellipse(new Vector2(position.X + (currentSet.frameSize.X / 2), position.Y + (currentSet.frameSize.Y / 2)), currentSet.frameSize.X / 2, currentSet.frameSize.Y / 2, 0);
            return E;
        }
    }
}