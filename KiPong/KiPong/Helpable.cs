using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KiPong
{
    public abstract class Helpable : GameObject
    {
        private Texture2D image;
        private String text;
        private float scale;

        /// <summary>
        /// True pour demander l'aide
        /// </summary>
        public bool Help { get; set; }
        protected bool isPrintingHelp;

        public Helpable(KiPongGame g, String nameImage, String nameText) : base(g)
        {
            image = g.Content.Load<Texture2D>(nameImage);

            if (image.Height / game.ScreenHeight <= image.Width / game.ScreenWidth)
                scale = (float)game.ScreenHeight / (float)image.Height;
            else
                scale = (float)game.ScreenWidth / (float)image.Width;
            text = g.Content.Load<String>(nameText);
        }

        public override void Update()
        {
            if (Help && !isPrintingHelp)
            {
                isPrintingHelp = true;
                Utils.SpeechAsynchrone(text);
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
                game.SpriteBatch.Draw(image, new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
