using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project.EntityTypes
{
    class TestEnemy : Enemy
    {
        KeyboardState keyboardState, prevKBState;
        Random r;
        int timer;
        int shotTime = 1;

        Texture2D atk;

        public TestEnemy(GameManager g, Texture2D[] t)
            : base(t, new Vector2(100, 100), g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            speed = 5;
            r = new Random();
            atk = myGame.Game.Content.Load<Texture2D>(@"Images/Spell");
        }

        public TestEnemy(GameManager g, Texture2D[] t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
            atk = myGame.Game.Content.Load<Texture2D>(@"Images/Spell");
        }

        public TestEnemy(GameManager g, Texture2D t)
            : base(t, new Vector2(100, 100), g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
            atk = myGame.Game.Content.Load<Texture2D>(@"Images/Spell");
        }

        public TestEnemy(GameManager g, Texture2D t, Vector2 v)
            : base(t, v, g)
        {
            keyboardState = prevKBState = Keyboard.GetState();
            myGame = g;
            speed = 5;
            r = new Random();
            atk = myGame.Game.Content.Load<Texture2D>(@"Images/Spell");
        }

        public override void addAnimations()
        {
            AnimationSet idle = new AnimationSet("IDLE", new Point(100, 100), new Point(1, 1), new Point(0, 0), 16, false);
            sets.Add(idle);
            setAnimation("IDLE");
            base.addAnimations();
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            colors[0] = Color.White;

            if (timer >= shotTime)
            {
                fireShot();
                timer -= shotTime;
            }

            base.Update(gameTime);
        }

        void fireShot()
        {
            Vector2 p = myGame.tests[0].getPos() - position;
            myGame.enemyShots.Add(new Attack(atk, position, myGame, p, 5, Color.Red, new Point(40, 12)));
        }

    }
}
