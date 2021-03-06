﻿namespace KiPong
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class AIBat : BatKeyboard
    {
        // La balle que le bot doit suivre
        private Ball ball;

        public AIBat(KiPongGame game, Side side, Difficulty d, Ball b) : base(game, side, d, null)
        {
            ball = b;
        }

        public override void Update()
        {
            if (ball.Direction > 1.5 * Math.PI || ball.Direction < 0.5 * Math.PI)
            {
                if (ball.Position.Y - 5 > Position.Y + Size.Height / 2)
                {
                    MoveDown();
                }
                else if (ball.Position.Y == Position.Y + Size.Height / 2)
                {
                }
                else if (ball.Position.Y + 5 < Position.Y + Size.Height / 2)
                {
                    MoveUp();
                }
            }
        }
    }
}
