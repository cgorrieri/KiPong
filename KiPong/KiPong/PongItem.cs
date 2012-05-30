using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KiPong
{
    /// <summary>
    /// Un objet du jeu
    /// </summary>
    public abstract class PongItem : GameObject
    {
        // La position de l'objet
        protected Vector2 position;
        /// <summary>
        /// Obtient la position de l'objet
        /// </summary>
        public Vector2 Position { get { return position; } }

        // Taille a prendre en compte pour l'objet
        protected Rectangle size;
        /// <summary>
        /// Obtient la taille de l'objet
        /// </summary>
        public Rectangle Size
        {
            get
            {
                size.X = (int)position.X;
                size.Y = (int)position.Y;
                return size;
            }
        }

        // Texture a dessiner
        protected Texture2D texture;

        public PongItem(KiPongGame g)
            : base(g)
        {}
    }
}
