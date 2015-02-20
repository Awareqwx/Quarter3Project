using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Quarter3Project.Managers
{
    public class CCUI : AnimatedSprite
    {
        Texture2D tex;
        Game1 myGame;

        public CCUI(Game1 g, Vector2 v, Texture2D t)
            : base(t)
        {
            tex = t;
            myGame = g;
            position = v;
        }

        public override void Update(GameTime gameTime)
        {
            if (myGame.currentCharcc == 1)
            {
                setAnimation("CCWIZARD");
            }
            else if(myGame.currentCharcc == 2)
            {
                setAnimation("CCCLERIC");
            }
            else if (myGame.currentCharcc == 3)
            {
                setAnimation("CCKNIGHT");
            }
            base.Update(gameTime);
        }

        public override void addAnimations()
        {
            AnimationSet ccWizard = new AnimationSet("CCWIZARD", new Point(45, 60), new Point(1, 1), new Point(0, 0), 0, false);
            AnimationSet ccCleric = new AnimationSet("CCCLERIC", new Point(45, 60), new Point(1, 1), new Point(1, 0), 0, false);
            AnimationSet ccKnight = new AnimationSet("CCKNIGHT", new Point(45, 60), new Point(1, 1), new Point(2, 0), 0, false);
            sets.Add(ccWizard);
            sets.Add(ccCleric);
            sets.Add(ccKnight);
            setAnimation("CCWIZARD");
            base.addAnimations();
        }

    }
}
