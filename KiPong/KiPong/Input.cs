using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;

namespace KiPong
{
    public abstract class Input
    {
        protected Game1 game;

        public Input(Game1 g)
        {
            game = g;
        }

        public virtual void Update() { }

        public virtual void UnloadContent() { }

        public abstract bool Retour();

        public abstract bool Valider();

        public abstract bool Pause();

        public abstract bool Aide();
    }
}
