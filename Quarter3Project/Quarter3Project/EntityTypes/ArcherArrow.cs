using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter3Project.EntityTypes
{
    class ArcherArrow : Projectile
    {

        bool using2;

        public ArcherArrow(Texture2D[] t, Vector2 v, GameManager g, Vector2 d, float s, Color[] c, Point fs, int dmg, int l, int i, bool b, bool u)
            : base(t, v, g, d, s, c, fs, dmg, l, i, b)
        {
            using2 = u;
        }

        public override void killHit()
        {
            if (using2)
            {
                myGame.player.health += 2;
            }
            base.kill();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < textures.Length; i++)
            {
                spriteBatch.Draw(textures[i], position, getTexRectangle(), colors[i], rotation, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
            }
        }

    }
}
