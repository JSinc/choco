using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace chocosRevenge
{
    class HealthBar
    {
        public Texture2D mHealthBar;
        int health;

        public void Update(Player player)
        {
            KeyboardState mKeys = Keyboard.GetState();

            UpdateHealth(player);
 
            health = (int)MathHelper.Clamp(health, 0, 100);
        }

        public void UpdateHealth(Player player)
        {
            health = player.health;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mHealthBar, new Rectangle(10, 10, 200, 30), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Gray); // Empty Healthbar
            spriteBatch.Draw(mHealthBar, new Rectangle(10, 10, (int)(200 * ((double)health / 100)), 30), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Red); // Full Healthbar
            spriteBatch.Draw(mHealthBar, new Rectangle(10, 10, 200, 30), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White); // Healthbar border

        }


    }
}
