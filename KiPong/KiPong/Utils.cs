using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KiPong
{
    public static class Utils
    {
        public static void DrawRectangle(SpriteBatch sb, GraphicsDevice gd, Rectangle r, Color c)
        {
            Texture2D rectTitle = new Texture2D(gd, r.Width, r.Height);
            Color[] dataTitle = new Color[r.Width * r.Height];
            for (int i = 0; i < dataTitle.Length; ++i) dataTitle[i] = c;
            rectTitle.SetData(dataTitle);

            // dessin du fond
            sb.Draw(rectTitle, new Vector2(r.X, r.Y), Color.White);
        }

        public static void DrawStringAtCenter(SpriteBatch sb, SpriteFont font, Rectangle r, String text, Color color)
        {
            sb.DrawString(font, text, new Vector2((r.Width - font.MeasureString(text).X) / 2 + r.X, (r.Height - font.MeasureString(text).Y) / 2 + r.Y), color);
        }
    }
}
