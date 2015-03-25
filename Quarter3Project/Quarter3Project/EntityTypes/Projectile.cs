using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Quarter3Project.EntityTypes
{
    public class Projectile : Attack
    {

        Vector2 direction;

        public Projectile(Texture2D t, Vector2 v, GameManager g, Vector2 d, float s, Color c, Point fs, int dmg, int l, int i, bool b)
            : base(t, v, g, (float)(Math.Atan2(d.Y, d.X)), s, c, fs, dmg, l, i, b)
        {
            direction = Collision.unitVector(d);
        }

        public Projectile(Texture2D[] t, Vector2 v, GameManager g, Vector2 d, float s, Color[] c, Point fs, int dmg, int l, int i, bool b)
            : base(t, v, g, (float)(Math.Atan2(d.Y, d.X)), s, c, fs, dmg, l, i, b)
        {
            direction = Collision.unitVector(d);
        }

        public override void Update(GameTime gameTime)
        {
            position += (direction * (float)speed);
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
            spriteBatch.Draw(textures[0], position, getTexRectangle(), colors[0], rotation, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
        }

        protected Rectangle getTexRectangle()
        {
            return new Rectangle(currentSet.frameSize.X * currentFrame.X + currentSet.frameSize.X * currentSet.startPos.X, currentSet.frameSize.Y * currentFrame.Y + currentSet.frameSize.Y * currentSet.startPos.Y, currentSet.frameSize.X, currentSet.frameSize.Y);
        }

        public override Collision.Ellipse getEllipse()
        {
            Collision.Ellipse E = base.getEllipse();
            E.rotation = rotation * 180 / Math.PI;
            return E;
        }

    }
}
