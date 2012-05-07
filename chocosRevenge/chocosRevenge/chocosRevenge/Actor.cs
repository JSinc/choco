/*********************************************************************
 *  Due date: 2nd of may, 2012
 *  Student Number: C09700081
 *  Author: Karl Sherry
 *  Description: A Base class for all objects considered to be an 
 *  Actor. Assigns position, texture, direction, speed and health
 *  of an Actor.
 ********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chocosRevenge
{
    class Actor
    {
        public Texture2D characterTexture;
        public Vector2 position;
        public Vector2 direction;
        protected Vector2 speed;
        

        public int mass = 1;
        public int health = 100;
        public int lightAmmo = 110;
        public int heavyAmmo = 10;

        public void Update(GameTime gametime, Vector2 speed, Vector2 direction)
        {
            position += direction * speed;
        }
    }
}
