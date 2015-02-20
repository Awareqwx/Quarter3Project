using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarter3Project
{
    public class BuildingEntity : Entity
    {
        public BuildingEntity(GameManager g, Texture2D[] t)
            : base(t, new Vector2(100, 100), g)
        {
        }

        public BuildingEntity(GameManager g, Texture2D[] t, Vector2 v)
            : base(t, v, g)
        {
        }

        public BuildingEntity(GameManager g, Texture2D t)
            : base(t, new Vector2(100, 100), g)
        {
        }

        public BuildingEntity(GameManager g, Texture2D t, Vector2 v)
            : base(t, v, g)
        {
        }

        public override void addAnimations()
        {
            colors[0] = Color.PaleVioletRed;
            colors[1] = Color.Black;
            AnimationSet idle = new AnimationSet("IDLE", new Point(207, 222), new Point(1, 1), new Point(0, 0), 16, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }

    }
}
