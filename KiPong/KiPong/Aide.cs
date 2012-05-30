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
        private KiPongGame game;
        private float scale;

        public Aide(KiPongGame g, String nameImage, String nameText)
        {
            game = g;
            image = g.Content.Load<Texture2D>(nameImage);

            if (image.Height / game.ScreenHeight <= image.Width / game.ScreenWidth)
            {
                scale = (float)game.ScreenHeight / (float)image.Height;
            }
            else
            {
                scale = (float)game.ScreenWidth / (float)image.Width;
            }
            Console.WriteLine("image " + image.Width * scale + "x" + image.Height*scale);
            text = g.Content.Load<String>(nameText);
        }

        public void Speech()
        {
            Utils.SpeechAsynchrone(text);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(image, new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
