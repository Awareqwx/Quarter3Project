using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project
{
    public class PopUp
    {
        public struct pop
        {
            public pop(Texture2D tex, Vector2 pos, Point s, Boolean v, String t, Int32 i) : this()
            {
                this.texture = tex;
                this.position = pos;
                this.size = s;
                this.visible = v;
                this.text = t;
                this.id = i;
            }
            public Texture2D texture { get; set; }
            public Vector2 position { get; set; }
            public Point size { get; set; }
            public Boolean visible { get; set; }
            public String text { get; set; }
            public Int32 id { get; set; }
        }

    }
}
