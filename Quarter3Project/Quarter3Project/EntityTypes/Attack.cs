<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
=======
﻿using System.Collections.Generic;
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
<<<<<<< HEAD
=======
using System;
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910

namespace Quarter3Project.EntityTypes
{
    public class Attack : Entity
    {
        protected float rotation;
<<<<<<< HEAD
        Boolean flip;
=======
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910
        public Point size;
        public int damage;

        public Attack(Texture2D t, Vector2 v, GameManager g, float r, float s, Color c, Point fs, int dmg)
            : base(t, v, g)
        {
            size = fs;
            speed = s;
            rotation = r;
<<<<<<< HEAD
            flip = Math.Sign(direction.X) == 1;
=======
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910
            colors = new Color[] { c };
            damage = dmg;
            addAnimations();
        }

        public Attack(Texture2D[] t, Vector2 v, GameManager g, float r, float s, Color[] c, Point fs, int dmg)
            : base(t, v, g)
        {
            size = fs;
            speed = s;
            rotation = r;
<<<<<<< HEAD
            flip = Math.Sign(direction.X) == 1;
=======
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910
            colors = c;
            damage = dmg;
            addAnimations();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void addAnimations()
        {
            if (size != Point.Zero)
            {
                Point s = size;
                sets = new List<AnimationSet>();
                AnimationSet idle = new AnimationSet("IDLE", s, new Point(1, 1), new Point(0, 0), 16, false);
                sets.Add(idle);
                setAnimation("IDLE");
                base.addAnimations();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
<<<<<<< HEAD
            spriteBatch.Draw(textures[0], position, getTexRectangle(), colors[0], rotation, new Vector2(0, 0), new Vector2(1, 1), flip ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
=======
            spriteBatch.Draw(textures[0], position, getTexRectangle(), colors[0], rotation, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910
        }

        Rectangle getTexRectangle()
        {
            return new Rectangle(currentSet.frameSize.X * currentFrame.X + currentSet.frameSize.X * currentSet.startPos.X, currentSet.frameSize.Y * currentFrame.Y + currentSet.frameSize.Y * currentSet.startPos.Y, currentSet.frameSize.X, currentSet.frameSize.Y);
        }

        public Point[] getPoints()
        {
<<<<<<< HEAD
            Point[] points = new Point[] { new Point((int)position.X, (int)position.Y), new Point((int)(position.X + (Math.Cos(rotation) * currentSet.frameSize.X)), (int)(position.Y + (Math.Sin(rotation) * currentSet.frameSize.X))), new Point((int)(position.X + (Math.Sin(rotation) * currentSet.frameSize.Y)), (int)(position.Y + (Math.Cos(rotation) * currentSet.frameSize.Y))),new Point((int)(position.X + (Math.Cos(rotation) * currentSet.frameSize.X) + (Math.Sin(rotation) * currentSet.frameSize.Y)),(int)(position.Y + (Math.Sin(rotation) * currentSet.frameSize.X) + (Math.Cos(rotation) * currentSet.frameSize.Y)))};
=======
            Point[] points = new Point[] { new Point((int)position.X, (int)position.Y), new Point((int)(position.X + (Math.Cos(rotation) * currentSet.frameSize.X)), (int)(position.Y + (Math.Sin(rotation) * currentSet.frameSize.X))), new Point((int)(position.X + (Math.Sin(rotation) * currentSet.frameSize.Y)), (int)(position.Y + (Math.Cos(rotation) * currentSet.frameSize.Y))), new Point((int)(position.X + (Math.Cos(rotation) * currentSet.frameSize.X) + (Math.Sin(rotation) * currentSet.frameSize.Y)), (int)(position.Y + (Math.Sin(rotation) * currentSet.frameSize.X) + (Math.Cos(rotation) * currentSet.frameSize.Y))) };
>>>>>>> c8cdf4fa39cc8a4e5d2d3639a439402ad136f910
            return points;
        }

        public Collision.mapSegment[] getSegments()
        {
            Point[] points = getPoints();
            Collision.mapSegment[] segs = new Collision.mapSegment[] { new Collision.mapSegment(points[0], points[2]), new Collision.mapSegment(points[2], points[3]), new Collision.mapSegment(points[3], points[1]), new Collision.mapSegment(points[1], points[0]) };
            return segs;
        }

        public override Collision.Ellipse getEllipse()
        {
            Collision.Ellipse E = base.getEllipse();
            E.rotation = rotation * 180 / Math.PI;
            return E;
        }

    }
}
