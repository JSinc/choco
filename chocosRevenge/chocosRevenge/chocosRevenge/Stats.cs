using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace chocosRevenge
{
    class Stats
    {
        SpriteFont font;
        Texture2D healthBar;
        Texture2D ammoBar;

        String hString; // health string
        String sString; // score string
        String mString; // mass string

        public int health = 0;
        public int ammo = 0;
        public int mass = 0;
        int score = 0;

        const int screenWidth = 1000;
        const int screenHeight = 700;

        public Stats(Player player)
        {
            health = player.health;
            mass = player.mass;
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("SpriteFont1");

            healthBar = Content.Load<Texture2D>("HealthBar");
            ammoBar = Content.Load<Texture2D>("HealthBar");
        }

        public void Update(Player player)
        {
            health = player.health;
            mass = player.mass;

            hString = "Health: " + health.ToString() + "%";
            sString = "Score: " + score.ToString();
            mString = "Mass: " + mass.ToString();
            score++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, sString, new Vector2(screenWidth - 120, 10), Color.Black);
            spriteBatch.DrawString(font, hString, new Vector2(10, 35), Color.Black);
            spriteBatch.DrawString(font, mString, new Vector2(screenWidth - 120, 35), Color.Black);

            //HealthBar change position to ur liking
            spriteBatch.Draw(healthBar, new Rectangle(10, 10, 200, 30), new Rectangle(0, 45, healthBar.Width, 44), Color.Gray); // Empty Healthbar
            spriteBatch.Draw(healthBar, new Rectangle(10, 10, (int)(200 * ((double)health / 100)), 30), new Rectangle(0, 45, healthBar.Width, 44), Color.Red); // Full Healthbar
            spriteBatch.Draw(healthBar, new Rectangle(10, 10, 200, 30), new Rectangle(0, 0, healthBar.Width, 44), Color.White); // Healthbar border
            
            //AmmoBar change position to ur liking
            spriteBatch.Draw(ammoBar, new Rectangle(10, 65, 100, 15), new Rectangle(0, 45, ammoBar.Width, 44), Color.Gray); // Empty Ammobar
            spriteBatch.Draw(ammoBar, new Rectangle(10, 65, (int)(100 * ((double)ammo / 500)), 15), new Rectangle(0, 45, ammoBar.Width, 44), Color.OrangeRed); // Full Ammobar
            spriteBatch.Draw(ammoBar, new Rectangle(10, 65, 100, 15), new Rectangle(0, 0, ammoBar.Width, 44), Color.White); // Ammobar border
        }
    }
}
