namespace KiPong
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class Bat
    {
        protected Vector2 position;
        private Rectangle size;
        private int points;
        private int yHeight;
        private Texture2D texture;
        protected Side side;

        private const int RatioWidth = 60;

        /// <summary>
        /// Initialise une batte
        /// </summary>
        /// <param name="game">Le jeu propriétaire</param>
        /// <param name="side">Si true alors à gauche sinon à droite de l'écran</param>
        public Bat(Game1 game, Side side, Difficulty d)
        {
            points = 0;
            this.side = side;
            int height = game.ScreenHeight / getRatio(d);
            int width = game.ScreenWidth / RatioWidth;
            texture = new Texture2D(game.GraphicsDevice, width, height);
            Color[] dataTitle = new Color[width * height];
            for (int index = 0; index < dataTitle.Length; ++index) dataTitle[index] = Color.Yellow;
            texture.SetData(dataTitle);
            size = new Rectangle(0, 0, width, height);
            if (side == Side.LEFT) position = new Vector2(0, game.ScreenHeight / 2 - height / 2);
            else position = new Vector2(game.ScreenWidth - width, game.ScreenHeight / 2 - height / 2);
            yHeight = game.ScreenHeight;
        }

        private int getRatio(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.EASY:
                    return 5;
                case Difficulty.MEDIUM:
                    return 6;
                default:
                    return 7;
            }
        }

        public Rectangle GetSize()
        {
            size.X = (int)position.X;
            size.Y = (int)position.Y;
            return size;
        }

        public void IncrementPoints()
        {
            points++;
        }

        public int GetPoints()
        {
            return points;
        }

        protected void SetPosition(Vector2 position)
        {
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.Y > yHeight - size.Height)
            {
                position.Y = yHeight - size.Height;
            }
            this.position = position;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public abstract void Update();

        public virtual void Reset()
        {
            SetPosition(new Vector2(GetPosition().X, yHeight / 2 - size.Height));
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }
    }
}
