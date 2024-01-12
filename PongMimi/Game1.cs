using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/**
 * Project      : PongMimi
 * Description  : A pong game made in Monogame for school
 * File         : Game1.cs
 * FileDesc     : The main game class of the project
 * Author       : Weber Jamie
 * Date         : 12 January 2024
**/
namespace PongMimi
{
    /// <summary>
    /// The main game class of the project
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// The manager of all the graphics
        /// </summary>
        private GraphicsDeviceManager _graphics;

        /// <summary>
        /// The batch of all the drawn sprites
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// The list of all the textures
        /// </summary>
        private Texture2D[] textures;

        /// <summary>
        /// The font used for showing the score
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The limits of the playfield
        /// </summary>
        private Vector2 limits;

        /// <summary>
        /// The racket of the player 1
        /// </summary>
        private Racket player1;

        /// <summary>
        /// The racket of the player 2
        /// </summary>
        private Racket player2;

        /// <summary>
        /// The pellet that bounces everywhere
        /// </summary>
        private Pellet pellet;

        /// <summary>
        /// The scores of both players
        /// </summary>
        private int[] scores;

        /// <summary>
        /// The constructor of the class
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// The initializer of the options
        /// </summary>
        protected override void Initialize()
        {

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// Load the content of the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textures = new Texture2D[]
            {
                Content.Load<Texture2D>("racket"),
                Content.Load<Texture2D>("pellet")
            };
            player1 = new Racket(new Vector2(20, 240), textures[0], new Keys[] { Keys.W, Keys.S }, Color.Blue);
            player2 = new Racket(new Vector2(620, 240), textures[0], new Keys[] { Keys.Up, Keys.Down }, Color.Red);
            pellet = new Pellet(new Vector2(320, 240), textures[1]);
            limits = new Vector2(640, 480);
            scores = new int[] { 0, 0 };
            font = Content.Load<SpriteFont>("score");
        }

        /// <summary>
        /// The computing frames of the game
        /// </summary>
        /// <param name="gameTime">The time elapsed since last computing frame</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // get elapsed time in seconds
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Calculate movements
            player1.Movements(elapsedTime, limits);
            player2.Movements(elapsedTime, limits);
            pellet.Movement(elapsedTime, limits);
            // Calculate collisions
            float posXpellet = pellet.Position.X;
            float directionXpellet = pellet.Direction.X;
            if (posXpellet < 100 && directionXpellet == -1)
            {
                if (pellet.CheckCollision(player1)) pellet.Color = Color.Aqua;
            }
            if (posXpellet > 540 && directionXpellet == 1)
            {
                if (pellet.CheckCollision(player2)) pellet.Color = Color.Pink;
            }
            switch (pellet.CheckWin(limits))
            {
                case 1:
                    pellet = new Pellet(new Vector2(320, 240), textures[1]);
                    scores[1]++;
                    break;
                case 2:
                    pellet = new Pellet(new Vector2(320, 240), textures[1]);
                    scores[0]++;
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// The graphics frames of the game
        /// </summary>
        /// <param name="gameTime">he time elapsed since last graphics frame</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            // Draw rackets
            this.DrawObject(player1);
            this.DrawObject(player2);
            // Draw pellets
            this.DrawObject(pellet);
            // Draw score
            float stringLength = font.MeasureString($"{scores[0]}  -  {scores[1]}").X;
            _spriteBatch.DrawString(font, $"{scores[0]}  -  {scores[1]}", new Vector2(320 - stringLength / 2, 30), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draw a GameObject using the sprite batch
        /// </summary>
        /// <param name="obj">The object to draw</param>
        private void DrawObject(GameObject obj)
        {
            Texture2D sprite = obj.Sprite;
            Vector2 posBase = obj.Position;
            Vector2 pos = new Vector2(posBase.X - sprite.Width / 2, posBase.Y - sprite.Height / 2);
            Color color = obj.Color;
            _spriteBatch.Draw(
                sprite,
                pos,
                null,
                color
            );
        }
    }
}