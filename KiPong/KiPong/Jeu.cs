using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public abstract class Jeu
    {
        protected Game1 game;
        protected Bat playerOne, playerTwo;
        protected AIBat bot;
        protected Ball ball;
        protected Difficulty difficulty;
        private Side lastScored;

        public bool Finish {get; set;}
        public bool IsOnePlayer;

        /* -- TIMER --*/
        // Le temps qui s'écoule entre lorsqu'un but est marqué ou au départ
        private int resetTimer, afterPauseTimer;
        // Si le timer est activé
        private bool resetTimerInUse;
        private string decompte;

        private const String YouWin = "Vous avez gagner !";
        private const String BotWin = "Vous avez perdu ...";
        private const String PlayerOneWin = "Le joueur 1 gagne !";
        private const String PlayerTwoWin = "Le joueur 2 gagne !";

        public Jeu(Game1 g, Difficulty d, bool isOnePlayer) {
            game = g;
            IsOnePlayer = isOnePlayer;
            difficulty = d;
            ball = new Ball(game, d);
            resetTimer = 0;
            resetTimerInUse = true;
            decompte = "";
            lastScored = Side.LEFT;
        }

        protected abstract void setBats();

        public void SetAfterPause()
        {
            afterPauseTimer = 50;
        }

        public void Update()
        {
            if (afterPauseTimer > 0)
            {
                afterPauseTimer--;
                return;
            }

            if (playerOne.GetPoints() > 5 
                || IsOnePlayer && bot.GetPoints() > 5
                || !IsOnePlayer && playerTwo.GetPoints() > 5)
            {
                Finish = true;
            }

            if (resetTimerInUse)
            {
                resetTimer++;
                if (resetTimer < 40)
                    decompte = "3";
                else if (resetTimer < 80)
                    decompte = "2";
                else if (resetTimer < 110)
                    decompte = "1";
                else
                    decompte = "";
            }

            if (resetTimer == 120)
            {
                resetTimerInUse = false;
                ball.Reset(lastScored);
                resetTimer = 0;
            }

            playerOne.Update();
            (IsOnePlayer ? bot : playerTwo).Update();

            ball.Update();

            // si la balle se dirige vers la droite
            if (ball.GetDirection() > 1.5f * Math.PI || ball.GetDirection() < 0.5f * Math.PI)
            {
                // si la balle est sur la bat droite
                Bat bat = IsOnePlayer ? bot : playerTwo;
                if (bat.GetSize().Intersects(ball.GetSize()))
                {
                    ball.BatHit(CheckHitLocation(bat));
                    IncreaseSpeed();
                }
            } // sinon est ce que elle est sur la batte gauche
            else if (playerOne.GetSize().Intersects(ball.GetSize()))
            {
                ball.BatHit(CheckHitLocation(playerOne));
                IncreaseSpeed();
            }

            if (!resetTimerInUse)
            {
                // si la balle sort de l'ecran du cote droit
                if (ball.GetPosition().X > game.ScreenWidth)
                {
                    resetTimerInUse = true;
                    lastScored = Side.LEFT;
                    playerOne.IncrementPoints();
                    ball.Stop();
                }
                // ou si elle sort du cote gauche
                else if (ball.GetPosition().X + ball.GetSize().Width < 0)
                {
                    resetTimerInUse = true;
                    lastScored = Side.RIGHT;
                    (IsOnePlayer ? bot : playerTwo).IncrementPoints();
                    ball.Stop();
                }
            }
        }

        private int CheckHitLocation(Bat bat)
        {
            int block = 0;
            float y = ball.GetCenter().Y;
            if (y < bat.GetPosition().Y + bat.GetSize().Height / 20) block = 1;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 2) block = 2;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 3) block = 3;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 4) block = 4;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 5) block = 5;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 6) block = 6;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 7) block = 7;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 8) block = 8;
            else if (y < bat.GetPosition().Y + bat.GetSize().Height / 20 * 19) block = 9;
            else block = 10;
            return block;
        }

        protected virtual void IncreaseSpeed()
        {
            ball.IncreaseSpeed();
            if (IsOnePlayer)
                bot.IncreaseSpeed();
        }

        /// <summary>
        /// Retourne le message quand le jeu est fini
        /// </summary>
        /// <returns></returns>
        public String getMessage()
        {
            if (Finish)
            {
                if (playerOne.GetPoints() > 5)
                {
                    return IsOnePlayer ? YouWin : PlayerOneWin;
                }
                else if (IsOnePlayer && bot.GetPoints() > 5)
                {
                    return BotWin;
                }
                else if (!IsOnePlayer && playerTwo.GetPoints() > 5)
                {
                    return PlayerTwoWin;
                }
            }
            return "";
        }

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            playerOne.Draw(spriteBatch);
            Bat secondBat = IsOnePlayer ? bot : playerTwo;
            secondBat.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            // Dessine les points
            spriteBatch.DrawString(font, playerOne.GetPoints().ToString(), new Vector2(game.ScreenWidth / 4 - font.MeasureString(playerOne.GetPoints().ToString()).X, 20), Color.White);
            spriteBatch.DrawString(font, secondBat.GetPoints().ToString(), new Vector2(game.ScreenWidth / 4 * 3 - font.MeasureString(secondBat.GetPoints().ToString()).X, 20), Color.White);
            if (resetTimerInUse)
                game.DrawStringAtCenter(spriteBatch, decompte, Color.White);
        }
    }
}
