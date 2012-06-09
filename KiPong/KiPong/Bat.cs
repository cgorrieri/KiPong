namespace KiPong
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class Bat : PongItem
    {
        public static Color Background;

        private Color CurBackground;

        private int points;
        /// <summary>
        /// Obtient les points que le joueur a marqué
        /// </summary>
        public int Points { get { return points; } }

        private int yHeight;
        protected Side side;

        /// <summary>
        /// L'aipaisseur de la bat
        /// </summary>
        private const int RatioWidth = 50;

        /// <summary>
        /// Initialise une batte
        /// </summary>
        /// <param name="game">Le jeu propriétaire</param>
        /// <param name="side">Si true alors à gauche sinon à droite de l'écran</param>
        public Bat(KiPongGame g, Side side, Difficulty d) : base(g)
        {
            points = 0;
            this.side = side;
            // La raquette est un cube ou l'on ne voit qu'une partie
            int cote = game.ScreenHeight / getRatio(d);
            int apparentWidth = game.ScreenWidth / RatioWidth;
            texture = new Texture2D(game.GraphicsDevice, cote, cote);
            changeColor();
            size = new Rectangle(0, 0, cote, cote);
            if (side == Side.LEFT) position = new Vector2(apparentWidth - cote, game.ScreenHeight / 2 - cote / 2);
            else position = new Vector2(game.ScreenWidth - apparentWidth, game.ScreenHeight / 2 - cote / 2);
            yHeight = game.ScreenHeight;
        }

        /// <summary>
        /// Obtient le ration à utiliser selon la difficultées
        /// </summary>
        private int getRatio(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.EASY:
                    return 4;
                case Difficulty.MEDIUM:
                    return 5;
                default:
                    return 6;
            }
        }

        /// <summary>
        /// Ajoute un point
        /// </summary>
        public void IncrementPoints()
        {
            points++;
        }

        /// <summary>
        /// Change la position de la bat
        /// </summary>
        /// <param name="position">Nouvelle position de la bat</param>
        protected void setPosition(Vector2 position)
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

        /// <summary>
        /// Réinitialize la bat
        /// </summary>
        public virtual void Reset()
        {
            setPosition(new Vector2(position.X, yHeight / 2 - size.Height));
        }

        public override void Draw()
        {
            if (!Background.Equals(CurBackground))
            {
                changeColor();
                CurBackground = Background;
            }
            game.SpriteBatch.Draw(texture, position, Color.White);
        }

        private void changeColor()
        {
            Color[] dataTitle = new Color[texture.Width * texture.Width];
            for (int index = 0; index < dataTitle.Length; ++index) dataTitle[index] = Background;
            texture.SetData(dataTitle);
        }
    }
}
