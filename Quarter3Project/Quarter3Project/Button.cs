using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project
{
    public class Button
    {

        public struct btn
        {
            public btn(Texture2D tex, Vector2 pos, Point s, String txt, Color c, Boolean h, Boolean ph) : this()
            {
                this.btnTexture = tex;
                this.position = pos;
                this.size = s;
                this.text = txt;
                this.color = c;
                this.hover = h;
                this.phover = ph;
                
            }

            //This constructor is used if you're using a spritesheet for your buttons
            //pos is for the position of the frame, pos2 is for the position of the button on the screen
            //Note: It would probably be better to have pos be the positioning for the button on the screen
            //instead of pos2 however i'm currently too lazy to fix it at the moment.
            public btn(Texture2D tex, Vector2 pos, Vector2 pos2, Point s, String txt, Color c, Boolean h, Boolean ph)
                : this()
            {
                this.btnTexture = tex;
                this.position = pos;
                this.position2 = pos2;
                this.size = s;
                this.text = txt;
                this.color = c;
                this.hover = h;
                this.phover = ph;
            }
            public Texture2D btnTexture { get; set; }
            public Vector2 position { get; set; }
            public Vector2 position2 { get; set; }
            public Point size { get; set; }
            public String text { get; set; }
            public Color color { get; set; }
            public Boolean hover { get; set; }
            public Boolean phover { get; set; }
            
            public Rectangle collisionRect()
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }

            public Rectangle collisionRect2()
            {
                return new Rectangle((int)position2.X, (int)position2.Y, (int)size.X, (int)size.Y);
            }

        }

        
    }
}
