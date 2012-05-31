using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace KiPong
{
    public abstract class Pong : Helpable
    {
        protected Bat bat1, bat2;
        protected AIBat bot;
        protected Ball ball;
        protected Difficulty difficulty;
        private SoundEffect goalSound;
        private Side lastScored;

        private bool finish;
        /// <summary>
        /// Retourne si le jeu est fini 
        /// </summary>
        public bool IsFinish { get { return finish; } }
        public bool IsOnePlayer;

        /* DRAW */
        private Vector2 posPointsBat1, posPointsBat2;
        private Rectangle line;

        /* -- TIMER --*/
        // Le temps qui s'écoule entre lorsqu'un but est marqué ou au départ
        private int resetTimer, afterBreakTimer;
        // Si le timer est activé
        private bool resetTimerInUse;
        private string decompte;

        /* STRINGS */
        private const String YouWin = "Vous avez gagner !";
        private const String BotWin = "Vous avez perdu ...";
        private const String Bat1Win = "Le joueur 1 gagne !";
        private const String Bat2Win = "Le joueur 2 gagne !";

        public Pong(KiPongGame g, String helpImg, String helpText, Difficulty d, bool isOnePlayer)
            : base(g, helpImg, helpText)
        {
            IsOnePlayer = isOnePlayer;
            difficulty = d;
            ball = new Ball(game, d);
            resetTimer = 0;
            resetTimerInUse = true;
            decompte = "";
            lastScored = Side.LEFT;

            goalSound = g.Content.Load<SoundEffect>("goal");

            // DRAW
            int Wdiv2 = game.ScreenWidth / 2;
            posPointsBat1 = new Vector2(Wdiv2 - game.FontTitle.MeasureString("0").X - 15, 10);
            posPointsBat2 = new Vector2(Wdiv2 + 15, 10);
            line = new Rectangle(Wdiv2 - 5, 0, 10, game.ScreenHeight);
        }

        /// <summary>
        /// Créer les bats
        /// </summary>
        protected abstract void setBats();

        /// <summary>
        /// Met en place le timer pour que la partie ne reprène pas directement après la pause
        /// </summary>
        public void SetAfterBreak()
        {
            afterBreakTimer = 50;
        }

        public override void Update()
        {
            base.Update();
            if (isPrintingHelp) return;

            #region Timer
            if (afterBreakTimer > 0)
            {
                afterBreakTimer--;
                return;
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

                if (resetTimer == 120)
                {
                    resetTimerInUse = false;
                    ball.Reset(lastScored);
                    resetTimer = 0;
                }
            }
            #endregion Timer

            if (bat1.Points > 5 
                || IsOnePlayer && bot.Points > 5
                || !IsOnePlayer && bat2.Points > 5)
            {
                finish = true;
            }

            bat1.Update();
            (IsOnePlayer ? bot : bat2).Update();

            ball.Update();

            if (!resetTimerInUse)
            {
                // si la balle se dirige vers la droite
                if (ball.Direction > 1.5f * Math.PI || ball.Direction < 0.5f * Math.PI)
                {
                    // si la balle est sur la bat droite
                    Bat bat = IsOnePlayer ? bot : bat2;
                    if (bat.Size.Intersects(ball.Size))
                    {
                        ball.BatHit(CheckHitLocation(bat));
                        IncreaseSpeed();
                    }
                } // sinon est ce que elle est sur la batte gauche
                else if (bat1.Size.Intersects(ball.Size))
                {
                    ball.BatHit(CheckHitLocation(bat1));
                    IncreaseSpeed();
                }

                // si la balle sort de l'ecran du cote droit
                if (ball.Position.X > game.ScreenWidth)
                {
                    resetTimerInUse = true;
                    lastScored = Side.LEFT;
                    bat1.IncrementPoints();
                    ball.Stop();
                    goalSound.Play();
                }
                // ou si elle sort du cote gauche
                else if (ball.Position.X + ball.Size.Width < 0)
                {
                    resetTimerInUse = true;
                    lastScored = Side.RIGHT;
                    (IsOnePlayer ? bot : bat2).IncrementPoints();
                    ball.Stop();
                    goalSound.Play();
                }
            }
        }

        /// <summary>
        /// Récupère le block de la bat sur lequelle la ball tape
        /// </summary>
        /// <param name="bat">La Bat où il y a collision</param>
        /// <returns>Le block sur lequel la balle a tapée</returns>
        private int CheckHitLocation(Bat bat)
        {
            int block = 0;
            float y = ball.Center.Y;
            if (y < bat.Position.Y + bat.Size.Height / 20) block = 1;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 2) block = 2;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 3) block = 3;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 4) block = 4;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 5) block = 5;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 6) block = 6;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 7) block = 7;
            else if (y < bat.Position.Y + bat.Size.Height / 10 * 8) block = 8;
            else if (y < bat.Position.Y + bat.Size.Height / 20 * 19) block = 9;
            else block = 10;
            return block;
        }

        /// <summary>
        /// Augmente la vitesse de la balle et des battes
        /// </summary>
        protected virtual void IncreaseSpeed()
        {
            ball.IncreaseSpeed();
            if (IsOnePlayer)
                bot.IncreaseSpeed();
        }

        /// <summary>
        /// Retourne le message correspondant à l'état de la fin du jeu
        /// </summary>
        /// <returns>L'état si le jeu est fini, sinon vide</returns>
        public String getMessage()
        {
            if (finish)
            {
                if (bat1.Points > 5)
                {
                    return IsOnePlayer ? YouWin : Bat1Win;
                }
                else if (IsOnePlayer && bot.Points > 5)
                {
                    return BotWin;
                }
                else if (!IsOnePlayer && bat2.Points > 5)
                {
                    return Bat2Win;
                }
            }
            return "";
        }

        public override void Draw()
        {
            base.Draw();
            if (isPrintingHelp) return;

            game.SpriteBatch.GraphicsDevice.Clear(Color.Black);
            Bat secondBat = IsOnePlayer ? bot : bat2;
            // Points et ligne
            game.SpriteBatch.DrawString(game.FontTitle, bat1.Points.ToString(), posPointsBat1, Color.White);
            game.SpriteBatch.DrawString(game.FontTitle, secondBat.Points.ToString(), posPointsBat2, Color.White);
            Utils.DrawRectangle(game.SpriteBatch, line, Color.Gray);
            // Bats et ball
            bat1.Draw();
            secondBat.Draw();
            ball.Draw();
            // Timer si activé
            if (resetTimerInUse)
                Utils.DrawStringAtCenter(game.SpriteBatch, game.Font, game.ScreenSize, decompte, Color.White);
        }

        protected override void LeaveHelp()
        {
            SetAfterBreak();
        }
    }
}
