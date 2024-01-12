using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Sockets;
/**
* Project      : PongMimi
* Description  : A pong game made in Monogame for school
* File         : Pellet.cs
* FileDesc     : The pellet bouncing everywhere
* Author       : Weber Jamie
* Date         : 12 January 2024
**/
namespace PongMimi
{
    /// <summary>
    /// The pellet bouncing everywhere
    /// </summary>
    internal class Pellet : GameObject
    {
        /// <summary>
        /// The time since the pellet appeared
        /// </summary>
        private float time;

        /// <summary>
        /// The direction the pellet goes
        /// </summary>
        private Vector2 direction;

        /// <summary>
        /// The speed of the pellet
        /// </summary>
        private float speed;

        /// <summary>
        /// The constructor of the class
        /// </summary>
        /// <param name="position">The position of the pellet</param>
        /// <param name="sprite">The sprite of the pellet</param>
        public Pellet(Vector2 position, Texture2D sprite) : base(position, sprite)
        {
            this.time = 0;
            this.direction = new Vector2(new Random().Next(0,2) == 0 ? -1 : 1);
            this.speed = 100;
        }

        /// <summary>
        /// Move the pellet 2 seconds after it's instanciation
        /// </summary>
        /// <param name="elapsedTime">The time since last computing frame</param>
        /// <param name="limits">The limits of the playfield</param>
        public void Movement(float elapsedTime, Vector2 limits)
        {
            this.time += elapsedTime;
            if (this.time < 2)
            {
                return;
            }
            this.Move(this.direction.X * this.speed * elapsedTime, this.direction.Y * this.speed * elapsedTime, limits);
            if (this.position.Y == this.sprite.Height / 2 || this.position.Y == limits.Y - this.sprite.Height / 2)
            {
                this.direction.Y = this.direction.Y * -1;
                this.speed += 10 + 5 * (float)Math.Floor(time / 12);
            }
        }

        /// <summary>
        /// Check if there is a collision with a given racket
        /// </summary>
        /// <param name="racket">The racket to check collision with</param>
        public void CheckCollision(Racket racket)
        {
            float[] edges = new float[]
            {
                this.position.X - this.sprite.Width / 2,
                this.position.X + this.sprite.Width / 2,
                this.position.Y - this.sprite.Height / 2,
                this.position.Y + this.sprite.Height / 2,
            };

            float[] edges1 = new float[]
            {
                racket.Position.X - racket.Sprite.Width / 2,
                racket.Position.X + racket.Sprite.Width / 2,
                racket.Position.Y - racket.Sprite.Height / 2,
                racket.Position.Y + racket.Sprite.Height / 2,
            };

            if ((edges[0] <= edges1[1] && edges[1] >= edges1[0]) &&
                (edges[2] <= edges1[3] && edges[3] >= edges1[2]))
            {
                this.direction.X = this.direction.X * -1;
                this.speed += 10;
                float directionY = this.Position.Y - racket.Position.Y;
                this.direction.Y = directionY / Math.Abs(directionY);
            }
        }

        /// <summary>
        /// Check if someone has won
        /// </summary>
        /// <param name="limits">The limits of the playfield</param>
        /// <returns>A number depending on whether someone has won and who has won</returns>
        public int CheckWin(Vector2 limits)
        {
            return this.Position.X <= this.Sprite.Width / 2 ? 1 : this.Position.X >= limits.X - this.Sprite.Width / 2 ? 2 : 0;
        }

        /// <summary>
        /// Get the direction of the pellet
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }
        }
    }
}
