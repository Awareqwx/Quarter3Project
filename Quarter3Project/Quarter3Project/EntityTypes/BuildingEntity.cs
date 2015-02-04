using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarter3Project
{
    class BuildingEntity : Entity
    {
        public BuildingEntity(Game1 g, Texture2D t, Vector2 p):base(t, p, g)
        {
            colors = new Color[] { Color.White, Color.Orange };

            g.buildingSegments.Add(new Collision.mapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X + currentSet.frameSize.X, (int)position.Y)));
            g.buildingSegments.Add(new Collision.mapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X, (int)position.Y + currentSet.frameSize.Y)));
            g.buildingSegments.Add(new Collision.mapSegment(new Point((int)position.X, (int)position.Y + currentSet.frameSize.Y), new Point((int)position.X + currentSet.frameSize.X, (int)position.Y + currentSet.frameSize.Y)));
            g.buildingSegments.Add(new Collision.mapSegment(new Point((int)position.X + currentSet.frameSize.X, (int)position.Y), new Point((int)position.X + currentSet.frameSize.X, (int)position.Y + currentSet.frameSize.Y)));
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(100, 100), new Point(1, 1), new Point(0, 0), 16, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }

    }
}
