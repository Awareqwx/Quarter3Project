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
        public BuildingEntity(GameManager g, Texture2D[] t)
            : base(t, new Vector2(100, 100), g)
        {
            g.buildingRadius = 50f;
            g.Building.P.X = position.X + g.buildingRadius;
            g.Building.P.Y = position.Y + g.buildingRadius;
            g.Building.R = g.buildingRadius;
        }

        public BuildingEntity(GameManager g, Texture2D[] t, Vector2 v)
            : base(t, v, g)
        {
            g.buildingRadius = 50f;
            g.Building.P.X = position.X + g.buildingRadius;
            g.Building.P.Y = position.Y + g.buildingRadius;
            g.Building.R = g.buildingRadius;
        }

        public BuildingEntity(GameManager g, Texture2D t)
            : base(t, new Vector2(100, 100), g)
        {
            g.buildingRadius = 50f;
            g.Building.P.X = position.X + g.buildingRadius;
            g.Building.P.Y = position.Y + g.buildingRadius;
            g.Building.R = g.buildingRadius;
        }

        public BuildingEntity(GameManager g, Texture2D t, Vector2 v)
            : base(t, v, g)
        {
            g.buildingRadius = 50f;
            g.Building.P.X = position.X + g.buildingRadius;
            g.Building.P.Y = position.Y + g.buildingRadius;
            g.Building.R = g.buildingRadius;
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
