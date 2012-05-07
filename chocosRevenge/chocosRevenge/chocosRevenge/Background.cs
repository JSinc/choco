using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace chocosRevenge
{
    class Background
    {
        public Texture2D texture;
        public Rectangle rec;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, Color.White);
        }
    }
}
