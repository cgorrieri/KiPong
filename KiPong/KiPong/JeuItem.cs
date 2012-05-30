using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KiPong
{
    public abstract class JeuItem : GameObject
    {
        protected Vector2 position;
        public Vector2 Position { get { return position; } }

        protected Rectangle size;
        public Rectangle Size
        {
            get
            {
                size.X = (int)position.X;
                size.Y = (int)position.Y;
                return size;
            }
        }

        protected Texture2D texture;

        public JeuItem(KiPongGame g)
            : base(g)
        {

        }
    }
}
