using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.Managers
{
    public class YesNo : AnimatedSprite
    {
        Game1 myGame;
        PopUpManager m;
        public String ss;

        public YesNo(Game1 g, Texture2D t, Vector2 v, PopUpManager M, String s) : base(t)
        {
            myGame = g;
            position = v;
            m = M;
            ss = s;
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < m.home.Length; i++)
                m.home[i].Update(gameTime);
            
            base.Update(gameTime);
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(600, 200), new Point(1, 1), new Point(0, 0), 0, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }

    }
}
