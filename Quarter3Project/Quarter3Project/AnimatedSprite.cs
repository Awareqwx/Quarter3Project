using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quarter3Project
{
    public class AnimatedSprite
    {

        public struct AnimationSet
        {
            public string name; //Should be all caps OKAY
            public Point frameSize; //The size of each individual frame (px, px)
            public Point sheetSize; //The number of frames in either direction 
            public Point startPos; //The starting point of the animation, the top left frame is (0, 0)
            public int millisPerFrame; //The time each frame should be up, in milliseconds (1000ms = 1s)
            public Boolean doesLoop; //Does the animation loop?
            public AnimationSet(string n, Point fs, Point ss, Point sp, int mpf, Boolean l)
            {
                name = n;
                frameSize = fs;
                sheetSize = ss;
                startPos = sp;
                millisPerFrame = mpf;
                doesLoop = l;
            }
        };

        protected List<AnimationSet> sets;
        protected AnimationSet currentSet;

        public Texture2D[] textures;

        protected Vector2 position, prevPosition;
        protected Color[] colors;

        protected Point currentFrame;

        protected Boolean animIsOver;

        public float speed;
        protected int timeSinceLast;

        public AnimatedSprite(Texture2D[] t)
        {
            textures = t;
            currentFrame = Point.Zero;
            sets = new List<AnimationSet>();
            animIsOver = false;
            addAnimations();
        }
        public AnimatedSprite(Texture2D t)
        {
            textures = new Texture2D[]{t};
            currentFrame = Point.Zero;
            sets = new List<AnimationSet>();
            animIsOver = false;
            addAnimations();
        }

        public virtual void Update(GameTime gameTime)
        {
            timeSinceLast += gameTime.ElapsedGameTime.Milliseconds;
            if (!animIsOver)
            {
                if (timeSinceLast >= currentSet.millisPerFrame)
                {
                    timeSinceLast = 0;
                    currentFrame.X++;
                    if (currentFrame.X >= currentSet.sheetSize.X)
                    {
                        currentFrame.X = 0;
                        currentFrame.Y++;
                        if (currentFrame.Y >= currentSet.sheetSize.Y)
                        {
                            if (currentSet.doesLoop)
                            {
                                currentFrame.Y = 0;
                            }
                            else
                            {
                                animIsOver = true;
                            }
                        }
                    }
                }
            }
            else
            {
                currentFrame = Point.Zero;
            }
            prevPosition = position;
        }

        public virtual void addAnimations() { }

        public void setAnimation(string setName)
        {
            if (currentSet.name != setName)
            {
                Boolean loopBreak = false;
                for (int i = 0; i < sets.Count && !loopBreak; i++)
                {
                    if (sets[i].name == setName)
                    {
                        currentSet = sets[i];
                        currentFrame = Point.Zero;
                        loopBreak = true;
                        animIsOver = false;
                    }
                }
            }
        }

        public Vector2 getPos()
        {
            return position;
        }

        public Point getFrameSize()
        {
            return currentSet.frameSize;
        }

        public Rectangle collisionRect()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)currentSet.frameSize.X, (int)currentSet.frameSize.Y);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < textures.Length; i++)
            {
                spriteBatch.Draw(textures[i], position, new Rectangle(currentSet.frameSize.X * currentFrame.X + currentSet.frameSize.X * currentSet.startPos.X, currentSet.frameSize.Y * currentFrame.Y + currentSet.frameSize.Y * currentSet.startPos.Y, currentSet.frameSize.X, currentSet.frameSize.Y), colors[i]);
            }
        }
    }
}