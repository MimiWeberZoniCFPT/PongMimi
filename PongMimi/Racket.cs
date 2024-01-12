using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Project      : PongMimi
 * Description  : A pong game made in Monogame for school
 * File         : Racket.cs
 * FileDesc     : The racket controlled by the player
 * Author       : Weber Jamie
 * Date         : 12 January 2024
**/
namespace PongMimi
{
    /// <summary>
    /// The racket controlled by the player
    /// </summary>
    internal class Racket : GameObject
    {
        /// <summary>
        /// The inputs to go up and down
        /// </summary>
        private Keys[] inputs;

        /// <summary>
        /// The constructor of the class
        /// </summary>
        /// <param name="position">The base position of the racket</param>
        /// <param name="sprite">The sprite of the racket</param>
        /// <param name="color">The color of the racket as a filter</param>
        /// <param name="inputs">The inputs to go up and down</param>
        public Racket(Vector2 position, Texture2D sprite, Keys[] inputs, Color color) : base(position, sprite, color)
        {
            this.inputs = inputs;
        }

        /// <summary>
        /// Move the racket up and down if the inputs are correct
        /// </summary>
        /// <param name="elapsedTime">The amount of time since last computing frame</param>
        /// <param name="limits">The limits of the game</param>
        public void Movements(float elapsedTime, Vector2 limits)
        {
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(this.inputs[0]))
            {
                this.Move(0, -elapsedTime * 250, limits);
            }
            else if (kstate.IsKeyDown(this.inputs[1]))
            {
                this.Move(0, elapsedTime * 250, limits);
            }
        }
    }
}
