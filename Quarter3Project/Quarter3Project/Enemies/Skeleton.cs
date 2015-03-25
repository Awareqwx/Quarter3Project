using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quarter3Project.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.Enemies
{
    class Skeleton : Enemy
    {

        public Skeleton(Texture2D[] t, Vector2 v, GameManager g)
            : base(t, v, g)
        {

        }

    }
}
