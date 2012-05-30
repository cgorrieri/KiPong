using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech;
using System.Speech.Synthesis;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// tralalalala
namespace KiPong
{
    public static class Utils
    {
        public static SpeechSynthesizer OldSpeech;
        public static SoundPlayer sound;

        /// <summary>
        /// Dessine un rectangle
        /// </summary>
        /// <param name="sb">Le SpriteBach avec lequel on dessine</param>
        /// <param name="r">Le rectangle à dessiner</param>
        /// <param name="c">La couleur du rectangle</param>
        public static void DrawRectangle(SpriteBatch sb, Rectangle r, Color c)
        {
            Texture2D rectTitle = new Texture2D(sb.GraphicsDevice, r.Width, r.Height);
            Color[] dataTitle = new Color[r.Width * r.Height];
            for (int i = 0; i < dataTitle.Length; ++i) dataTitle[i] = c;
            rectTitle.SetData(dataTitle);

            // dessin du fond
            sb.Draw(rectTitle, new Vector2(r.X, r.Y), Color.White);
        }

        /// <summary>
        /// Dessine le texte au milieu d'un rectangle
        /// </summary>
        /// <param name="sb">Le SpriteBach avec lequel on dessine</param>
        /// <param name="font">La font à utiliser</param>
        /// <param name="r">Le rectangle</param>
        /// <param name="text">Le texte a écrire</param>
        /// <param name="color">La couleur du texte</param>
        public static void DrawStringAtCenter(SpriteBatch sb, SpriteFont font, Rectangle r, String text, Color color)
        {
            sb.DrawString(font, text, new Vector2((r.Width - font.MeasureString(text).X) / 2 + r.X, (r.Height - font.MeasureString(text).Y) / 2 + r.Y), color);
        }

        /// <summary>
        /// Méthode permettant de lancer la synthese vocale et mode asynchrone.
        /// Elle coupe celle qui est en cours si il y en a une
        /// </summary>
        /// <param name="texte">Texte à dire</param>
        public static void SpeechAsynchrone(String texte)
        {
            if (OldSpeech != null) OldSpeech.Pause();

            SpeechSynthesizer s = new SpeechSynthesizer();
            PromptBuilder builder = new PromptBuilder(new System.Globalization.CultureInfo("fr-fr"));
            builder.AppendText(texte);
            s.SpeakAsync(builder);
            OldSpeech = s;
        }

        /// <summary>
        /// Méthode permettant de lancer la synthèse vocale en mode synchrone
        /// </summary>
        /// <param name="texte">Texte à dire</param>
        public static void SpeechSynchrone(String text)
        {
            SpeechSynthesizer s = new SpeechSynthesizer();
            String voix = "ScanSoft Virginie_Dri40_16kHz";
            s.SelectVoice(voix);
            s.Speak(text);
        }

        /// <summary>
        /// Arrette la voix en cour
        /// </summary>
        /// <param name="texte">Texte à dire</param>
        public static void SpeechStop()
        {
            if(OldSpeech != null)
                OldSpeech.Pause();
        }
    }
}
