using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project
{
    public static class Debug
    {
        public struct text
        {
            public text(SpriteFont sF, String txt, Vector2 pos, Color wash) : this()
            {
                this.font = sF;
                this.msg = txt;
                this.position = pos;
                this.textColor = wash;
            }
            public SpriteFont font { get; set; }
            public String msg { get; set; }
            public Vector2 position { get; set; }
            public Color textColor { get; set; }            
        }
    }
}
