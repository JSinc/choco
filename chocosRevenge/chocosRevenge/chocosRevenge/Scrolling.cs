using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace chocosRevenge
{

    class Scrolling : Background
    {
        public Scrolling(Texture2D newTexture, Rectangle newRec)
        {
            texture = newTexture;
            rec = newRec;
        }

        public void Update(Player player)
        {
            if (player.position.X >= 700 && player.direction.X == player.moveRight)
                rec.X -= (int)player.frameSize.X;
        }
    }    
}
