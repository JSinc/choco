using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace chocosRevenge
{
    class Enemy : Actor
    {
        const int startX = 500;
        const int startY = 500;
        
        public int moveUp = -1;
        public int moveDown = 1;
        public int moveLeft = -1;
        public int moveRight = 1;
        public int health = 25;
        public Boolean isAlive = true;

        public Texture2D eTexture;
        public Rectangle destinationRectangle;
        public Rectangle sourceRectangle;

        int enemySpeed = 14;

        enum State
        {
            Walking
        }

        State enemyState = State.Walking;

        public Point frameSize;
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(2,1);

        public void LoadContent()
        {
            position = new Vector2(startX, startY);
            health = 25;
            mass = 2;
            enemySpeed = enemySpeed / mass;

            frameSize = new Point(characterTexture.Width / 2, characterTexture.Height / 1);

            sourceRectangle = new Rectangle(
                frameSize.X * currentFrame.X,
                frameSize.Y * currentFrame.Y,
                frameSize.X,
                frameSize.Y);

            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, characterTexture.Width / 2, characterTexture.Height / 1);
        }

        public void SeekPlayer(GameTime gametime, Player player)
        {
            UpdateFrame(player);

            if (position.X < player.position.X)
            {
                direction.X = moveRight;
                speed.X = enemySpeed;
            }
            if (position.X > player.position.X)
            {
                direction.X = moveLeft;
                speed.X = enemySpeed;
            }
            if (position.Y < player.position.Y)
            {
                direction.Y = moveDown;
                speed.Y = enemySpeed;
            }
            if (position.Y > player.position.Y)
            {
                direction.Y = moveUp;
                speed.Y = enemySpeed;
            }

            sourceRectangle = new Rectangle(frameSize.X * currentFrame.X, frameSize.Y * currentFrame.Y, frameSize.X, frameSize.Y);

            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, characterTexture.Width / 2, characterTexture.Height / 1);

            base.Update(gametime, speed, direction);
        }

        public void UpdateFrame(Player player)
        {
            if (player.position.X < position.X)
                currentFrame.X = 0;

            if (player.position.X > position.X) 
                currentFrame.X = 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(characterTexture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
