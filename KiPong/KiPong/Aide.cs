using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public class Aide
    {
        private Texture2D image;
        private String text;
        private Game1 game;

        public Aide(Game1 g, String nameImage, String nameText)
        {
            game = g;
            image = g.Content.Load<Texture2D>(nameImage);
            text = g.Content.Load<String>(nameText);
            Console.WriteLine(text);
        }

        public void Speech()
        {
            Utils.SpeechAsynchrone(text);
        }

        public void Draw(SpriteBatch batch)
        {
            // TODO faire scale de la photo pour qu'elle prène tout l'écran
            float scale = 1;
            batch.Draw(image, new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
