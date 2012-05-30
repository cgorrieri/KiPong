using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KiPong
{
    /// <summary>
    /// Le jeu qui est jouable par la kinect
    /// </summary>
    public class JeuKinect : Jeu
    {
        KinectInput input;

        private const String PlayerMissing = "Le joueur n'est pas detecte !";
        private const String PlayersMissing = "Le ou les joueurs ne sont pas detectes !";

        public JeuKinect(KiPongGame g, Difficulty d, bool isOp, KinectInput i)
            : base(g, d, isOp)
        {
            input = i;
            setBats();
        }

        protected override void setBats()
        {
            playerOne = new BatKinect(game, Side.LEFT, difficulty, input);
            if (IsOnePlayer)
                bot = new AIBat(game, Side.RIGHT, difficulty, ball);
            else
                playerTwo = new BatKinect(game, Side.RIGHT, difficulty, input);
        }

        public override void Update()
        {
            // Si le ou les joueur ne sont pas detecté on stop le jeu
            if (!input.ReadyForOne && IsOnePlayer || !input.ReadyForTwo && !IsOnePlayer)
                return;
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            // On dessine les messages d'erreur s'il y en a
            if (!input.ReadyForOne && IsOnePlayer)
                DrawErrorMessage(PlayerMissing);
            if (!input.ReadyForTwo && !IsOnePlayer)
                DrawErrorMessage(PlayersMissing);
        }

        /// <summary>
        /// Dessine un message d'erreur en blanc sur fond rouge au centre de l'écran
        /// </summary>
        /// <param name="text">Message d'érreur</param>
        private void DrawErrorMessage(String text)
        {
            int width = (int)game.Font.MeasureString(text).X, height = (int)game.Font.MeasureString(text).Y;
            Rectangle r = new Rectangle((game.ScreenWidth - width) / 2, (game.ScreenHeight - height) / 2, width, height);
            Utils.DrawRectangle(game.SpriteBatch, r, Color.Red);
            Utils.DrawStringAtCenter(game.SpriteBatch, game.Font, game.ScreenSize, text, Color.White);
        }
    }
}
