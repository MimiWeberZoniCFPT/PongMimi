using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Project      : PongMimi
 * Description  : A pong game made in Monogame for school
 * File         : GameObject.cs
 * FileDesc     : The base of all objects in the game excluding UI
 * Author       : Weber Jamie
 * Date         : 12 January 2024
**/
namespace PongMimi
{
    /// <summary>
    /// The base class of all objects in the game excluding UI
    /// </summary>
    internal class GameObject
    {
        /// <summary>
        /// The position of the object
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// The sprite the object uses
        /// </summary>
        protected Texture2D sprite;

        /// <summary>
        /// The color of the object as a filter
        /// </summary>
        protected Color color;

        /// <summary>
        /// The constructor of the class
        /// </summary>
        /// <param name="position">The base position of the object</param>
        /// <param name="sprite">The sprite of the object</param>
        /// <param name="color">The color of the objet as a filter, base white</param>
        public GameObject(Vector2 position, Texture2D sprite, Color? color = null)
        {
            this.position = position;
            this.sprite = sprite;
            this.color = color ?? Color.White;
        }

        /// <summary>
        /// Change the position relative to the current position
        /// </summary>
        /// <param name="x">How much you want to move in the X axis</param>
        /// <param name="y">How much you want to move in the Y axis</param>
        /// <param name="limits">The limits of the playfield</param>
        protected void Move(float x, float y, Vector2 limits)
        {
            this.position.X += x;
            this.position.Y += y;
            if (this.position.X > (limits.X - this.sprite.Width / 2)) this.position.X = limits.X - this.sprite.Width / 2;
            else if (this.position.X < (this.sprite.Width / 2)) this.position.X = this.sprite.Width / 2;
            if (this.position.Y > (limits.Y - this.sprite.Height / 2)) this.position.Y = limits.Y - this.sprite.Height / 2;
            else if (this.position.Y < (this.sprite.Height / 2)) this.position.Y = this.sprite.Height / 2;
        }

        /// <summary>
        /// Get the position of the object
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        /// <summary>
        /// Get the sprite of the object
        /// </summary>
        public Texture2D Sprite
        {
            get
            {
                return this.sprite;
            }
        }

        /// <summary>
        /// Get or Set the filter color of the object
        /// </summary>
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }
    }
}
