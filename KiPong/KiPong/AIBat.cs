namespace KiPong
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class AIBat : BatKeyboard
    {
        private Ball ball;
        public AIBat(Game1 game, Side side, Difficulty d, Ball b) : base(game, side, d, null)
        {
            ball = b;
        }

        public override void Update()
        {
            if (ball.GetDirection() > 1.5 * Math.PI || ball.GetDirection() < 0.5 * Math.PI)
            {
                if (ball.GetPosition().Y - 5 > GetPosition().Y + GetSize().Height / 2)
                {
                    MoveDown();
                }
                else if (ball.GetPosition().Y == GetPosition().Y + GetSize().Height / 2)
                {
                }
                else if (ball.GetPosition().Y + 5 < GetPosition().Y + GetSize().Height / 2)
                {
                    MoveUp();
                }
            }
        }
    }
}
