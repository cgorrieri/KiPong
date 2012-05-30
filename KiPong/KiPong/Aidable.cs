using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public abstract class Aidable : GameObject
    {
        private Aide aide;
        /// <summary>
        /// True pour demander l'aide
        /// </summary>
        public bool Help { get; set; }
        protected bool isPrintingHelp;

        public Aidable(KiPongGame g, Aide a)
            : base(g)
        {
            aide = a;
        }

        public override void Update()
        {
            if (Help && !isPrintingHelp)
            {
                isPrintingHelp = true;
                aide.Speech();
            }
            else if (Help && isPrintingHelp)
            {
                isPrintingHelp = false;
                Utils.SpeechStop();
                QuitteAide();
            }
        }

        protected virtual void QuitteAide() { }

        public override void Draw()
        {
            // On dessine l'aide si elle est demandé
            if (isPrintingHelp)
            {
                game.SpriteBatch.GraphicsDevice.Clear(Color.Black);
                aide.Draw(game.SpriteBatch);
                return;
            }
        }
    }
}
