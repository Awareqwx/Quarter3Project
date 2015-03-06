﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project
{
    public class ItemData
    {
        public struct btn
        {
            public btn(Texture2D tex, Vector2 pos, Point s, String txt, Color c, Boolean h, Boolean ph, Boolean v, int i, int ui)
                : this()
            {
                this.btnTexture = tex;
                this.position = pos;
                this.size = s;
                this.text = txt;
                this.color = c;
                this.hover = h;
                this.phover = ph;
                this.visible = v;
                this.id = i;
                this.uniqueid = ui;
            }

            //This constructor is used if you're using a spritesheet for your buttons
            //pos is for the position of the frame, pos2 is for the position of the button on the screen
            //Note: It would probably be better to have pos be the positioning for the button on the screen
            //instead of pos2 however i'm currently too lazy to fix it at the moment.
            public btn(Texture2D tex, Vector2 pos, Vector2 pos2, Point s, String txt, Color c, Boolean h, Boolean ph, Boolean v, int i, int ui)
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
                this.visible = v;
                this.id = i;
                this.uniqueid = ui;
            }
            public Texture2D btnTexture { get; set; }
            public Vector2 position { get; set; }
            public Vector2 position2 { get; set; }
            public Point size { get; set; }
            public String text { get; set; }
            public Color color { get; set; }
            public Boolean hover { get; set; }
            public Boolean phover { get; set; }
            public Boolean visible { get; set; }
            public int id { get; set; }
            public int uniqueid { get; set; }

            public Rectangle collisionRect()
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }

            public Rectangle collisionRect2()
            {
                return new Rectangle((int)position2.X, (int)position2.Y, (int)size.X, (int)size.Y);
            }

        }

        public struct chr
        {
            public chr(Texture2D t, Vector2 p, String n, String d, Int32 i, Boolean b)
                : this()
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

        public struct text
        {
            public text(SpriteFont sF, String txt, Vector2 pos, Color wash)
                : this()
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

        public struct pop
        {
            public pop(Texture2D backgroundTexture, Texture2D borderTexture, Vector2 position, Point size, int boxType, string[] boxText, bool visibility, bool hoverState, bool prevHoverState, int boxID)
                : this()
            {
                this.bgTex = backgroundTexture;
                this.bdTex = borderTexture;
                this.pos = position;
                this.s = size;
                this.bType = boxType;
                this.txt = boxText;
                this.vis = visibility;                
                this.hs = hoverState;
                this.phs = prevHoverState;
                this.ID = boxID;
            }

            private Texture2D bgTex { get; set; }
            public Texture2D getBGTex { get { return bgTex; } }

            private Texture2D bdTex { get; set; }
            public Texture2D getBDTex { get { return bdTex; } }

            private Vector2 pos { get; set; }
            public Vector2 getPos { get { return pos; } }

            private Point s { get; set; }
            public Point getSize { get { return s; } }

            private int bType { get; set; }
            public int getBoxType { get { return bType; } }

            private string[] txt { get; set; }
            public string[] getText { get { return txt; } }

            private bool vis { get; set; }
            public bool getVis { get { return vis; } }

            private bool hs { get; set; }
            public bool getHoverState { get { return hs; } }

            private bool phs { get; set; }
            public bool getPrevHoverState { get { return phs; } }

            private int ID { get; set; }
            public int getID { get { return ID; } }

            public Rectangle collisionRect()
            {
                return new Rectangle((int)pos.X, (int)pos.Y, (int)s.X, (int)s.Y);
            }

            public Rectangle collisionRect(int y)
            {
                return new Rectangle((int)pos.X, (int)pos.Y, s.X, s.Y - y);
            }

        }


        public struct skill
        {
            public skill(Texture2D tex, Vector2 pos, Point s, int cid, int sid) : this()
            {
                this.texture = tex;
                this.position = pos;
                this.size = s;
                this.classid = cid;
                this.skillid = sid;
            }
            public Texture2D texture { get; set; }
            public Vector2 position { get; set; }
            public Point size { get; set; }
            public int classid { get; set; }
            public int skillid { get; set; }

            public Rectangle collisionRect()
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }
        }

        public struct skillDescr
        {
            public skillDescr(Texture2D bg, Vector2 pos, Point s, int pid, int sid, String sName, String sDescr, String sType) : this()
            {
                this.background = bg;
                this.position = pos;
                this.size = s;
                this.popid = pid;
                this.skillid = sid;
                this.skillName = sName;
                this.skillDesc = sDescr;
                this.skillType = sType;
            }

            public Texture2D background { get; set; }
            public Vector2 position { get; set; }
            public Point size { get; set; }
            public int popid { get; set; }
            public int skillid { get; set; }
            public String skillName { get; set; }
            public String skillDesc { get; set; }
            public String skillType { get; set; }
        }
    }
}
