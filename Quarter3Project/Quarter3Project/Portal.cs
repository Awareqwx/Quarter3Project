using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Quarter3Project
{
    public class Portal
    {
        public Rectangle collision;
        public Vector2 pos;
        public Location dest; //Where you end up on the other side of the portal
        public Vector2 destCoords; //Where in "dest" you end up

        public Portal(Rectangle c, Vector2 p, Location d, Vector2 dc)
        {
            collision = c;
            pos = p;
            dest = d;
            destCoords = dc;
        }
    }
}
