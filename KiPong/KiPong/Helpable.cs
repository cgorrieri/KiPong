using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public abstract class Helpable : GameObject
    {
        private Help help;
        /// <summary>
        /// True pour demander l'aide
        /// </summary>
        public bool Help { get; set; }
        protected bool isPrintingHelp;

        public Helpable(KiPongGame g, Help a)
            : base(g)
        {
            help = a;
        }

        public override void Update()
        {
            if (Help && !isPrintingHelp)
            {
                isPrintingHelp = true;
                help.Speech();
            }
            else if (Help && isPrintingHelp)
            {
                isPrintingHelp = false;
                Utils.SpeechStop();
                LeaveHelp();
            }
        }

        protected virtual void LeaveHelp() { }

        public override void Draw()
        {
            // On dessine l'aide si elle est demandé
            if (isPrintingHelp)
            {
                game.SpriteBatch.GraphicsDevice.Clear(Color.Black);
                help.Draw(game.SpriteBatch);
                return;
            }
        }
    }
}
