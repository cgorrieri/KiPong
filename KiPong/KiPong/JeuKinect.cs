using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public class JeuKinect : Jeu
    {
        KinectInput input;

        private const String PlayerMissing = "Le joueur n'est pas detecte !";
        private const String PlayersMissing = "Le ou les joueurs ne sont pas detectes !";

        public JeuKinect(Game1 g, Difficulty d, bool isOp, KinectInput i)
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

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            base.Draw(spriteBatch, font);
            
                if (!input.ReadyForOne && IsOnePlayer)
                    DrawErrorMessage(spriteBatch, font, PlayerMissing);
                if (!input.ReadyForTwo && !IsOnePlayer)
                    DrawErrorMessage(spriteBatch, font, PlayersMissing);
        }

        private void DrawErrorMessage(SpriteBatch sb, SpriteFont font, String text)
        {
            int width = (int)font.MeasureString(text).X, height = (int)font.MeasureString(text).Y;
            Rectangle r = new Rectangle((game.ScreenWidth - width) / 2, (game.ScreenHeight - height) / 2, width, height);
            Utils.DrawRectangle(sb, game.GraphicsDevice, r, Color.Red);
            game.DrawStringAtCenter(sb, text, Color.White);
        }
    }
}
