/*
 *  Due date: 2nd of may, 2012
 *  Student Number: C09700081
 *  Author: Karl Sherry
 *  Description: Inherits the Actor class. SpriteSheet, Movement
 *  and Collision functionality are included amongst the main
 *  methods.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace chocosRevenge
{
    class Player : Actor 
    {
        Vector2 jumpStartPosition = Vector2.Zero;

        const int screenWidth = 1000;
        const int screenHeight = 700;
        const int walkingPointLimit = 700;

        int playerSpeed = 10;

        int moveUp = -1;
        int moveDown = 1;
        int moveLeft = -1;
        public int moveRight = 1;

        const int sheetWidth = 6;
        const int sheetHeight = 10;

        //LightWeapon initialWeapon = new LightWeapon;

        public Point frameSize;
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(sheetWidth, sheetHeight);

        KeyboardState currentState;
        KeyboardState theKeyboardState;
        KeyboardState oldKeyboardState;

        enum State
        {
            Walking, 
            Jumping, 
            Running
        }

        State playerState = State.Walking;

        public Rectangle sourceRectangle;
        public Rectangle playerRectangle;

        /********** LOAD CONTENT METHOD ********/

        public void LoadContent()
        {
            position = new Vector2(0, screenHeight / 2);

            health = 100;
            lightAmmo = 110;
            heavyAmmo = 20;
            mass = 1;
            playerSpeed = playerSpeed / mass;

            frameSize = new Point(characterTexture.Width / sheetWidth, characterTexture.Height / sheetHeight);

            sourceRectangle = new Rectangle(
                frameSize.X * currentFrame.X,
                frameSize.Y * currentFrame.Y,
                frameSize.X,
                frameSize.Y);

            playerRectangle = new Rectangle((int)position.X, (int)position.Y, characterTexture.Width / sheetWidth, characterTexture.Height / sheetHeight);
        }

        /************ UPDATE METHOD ************/

        public void Update(GameTime gametime, Enemy enemy)
        {
            currentState = Keyboard.GetState();
            theKeyboardState = Keyboard.GetState();

            // TODO: Add your update logic here
            UpdateWalking();
            UpdateJumping();
            UpdateRunning();
            UpdateBorderCollision();
            UpdateEnemyCollision(enemy);

            oldKeyboardState = theKeyboardState;

            sourceRectangle = new Rectangle(frameSize.X * currentFrame.X, frameSize.Y * currentFrame.Y, frameSize.X, frameSize.Y);

            playerRectangle = new Rectangle((int)position.X, (int)position.Y, characterTexture.Width / sheetWidth , characterTexture.Height / sheetHeight);

            base.Update(gametime, speed, direction);
        }

        /*********** UPDATE WALKING METHOD ************/

        public void UpdateWalking()
        {
            if (playerState == State.Walking)
            {
                speed = Vector2.Zero;
                direction = Vector2.Zero;

                if (currentState.IsKeyDown(Keys.Right))
                {
                    currentFrame.Y = 0;
                    UpdateFrame();

                    speed.X = playerSpeed;
                    direction.X = moveRight;
                }

                if (currentState.IsKeyDown(Keys.Left))
                {
                    currentFrame.Y = 1;
                    UpdateFrame();

                    speed.X = playerSpeed;
                    direction.X = moveLeft;
                }

                if (currentState.IsKeyDown(Keys.Down))
                {
                    speed.Y = playerSpeed;
                    direction.Y = moveDown;
                }

                if (currentState.IsKeyDown(Keys.Up))
                {
                    speed.Y = playerSpeed;
                    direction.Y = moveUp;
                }
            }
        }
        
        /*********** UPDATE JUMPING METHOD ************/

        public void UpdateJumping()
        {
            if (playerState == State.Walking)
            {
                if (currentState.IsKeyDown(Keys.Space) == true)// && previousState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }

            } // End of if State Walking

            if (playerState == State.Jumping)
            {
                if (jumpStartPosition.Y - position.Y > 125)
                {
                    direction.Y = moveDown * 2.0f;
                }

                if (position.Y > jumpStartPosition.Y)
                {
                    position.Y = jumpStartPosition.Y;
                    playerState = State.Walking;
                    direction = Vector2.Zero;
                }
            } // End of if State Jumping
        }

        /*********** UPDATE RUNNING METHOD ************/

        public void UpdateRunning()
        {
            if(currentState.IsKeyDown(Keys.LeftShift))
                playerState = State.Running;

            if (playerState == State.Running)
            {
                Run();
                playerState = State.Walking;
            }
        }

        /* JUMP METHOD */

        public void Jump()
        {
            playerState = State.Jumping;
            jumpStartPosition = position;
            moveUp = -1;
            direction.Y = moveUp * 2;

            if (currentState.IsKeyDown(Keys.LeftShift))
                speed = new Vector2(playerSpeed * 3, playerSpeed * 2);
            else
                speed = new Vector2(playerSpeed, playerSpeed);
        }

        /*********** RUN METHOD ************/

        public void Run()
        {
            speed = new Vector2(playerSpeed * 3, playerSpeed * 3);
        }

        /**************** UPDATE FRAME ****************/

        public void UpdateFrame()
        {
            if(playerState == State.Walking)
                currentFrame.X++;
            if (playerState == State.Running)
                currentFrame.X += 2;

            if (currentFrame.X >= 6) currentFrame.X = 0;
        }

        /******* UPDATE BORDER COLLISION METHOD *******/

        public void UpdateBorderCollision()
        {
            if (position.Y <= (screenHeight / 2) - 100) position.Y = screenHeight / 2 - 100;
            if (position.X <= 0) position.X = 0;
            if (position.Y >= screenHeight - frameSize.Y) position.Y = screenHeight - frameSize.Y;
            if (position.X >= walkingPointLimit) position.X = walkingPointLimit;
        }

        /******* UPDATE ENEMY COLLISION METHOD *******/

        public void UpdateEnemyCollision(Enemy enemy)
        {
            if (health > 0 && playerRectangle.Intersects(enemy.destinationRectangle))
                health--;
        }

        /*************** DRAW METHOD ******************/

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, characterPosition, sourceRectangle, Color.White);
            spriteBatch.Draw(characterTexture, playerRectangle, sourceRectangle, Color.White);
        }
    }
}
