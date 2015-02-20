using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project
{
    public class ChrS
    {
        public struct chr
        {
            public chr(Texture2D t, Vector2 p, String n, String d, Int32 i, Boolean b) : this()
            {
                this.tex = t;
                this.pos = p;
                this.name = n;
                this.descr = d;
                this.id = i;
                this.visible = b;
            }

            public Texture2D tex { get; set; }
            public Vector2 pos { get; set; }
            public String name { get; set; }
            public String descr { get; set; }
            public Int32 id { get; set; }
            public Boolean visible { get; set; }
        }
    }
}
