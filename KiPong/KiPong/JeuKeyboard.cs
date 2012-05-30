using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiPong
{
    /// <summary>
    /// Le jeu qui est jouable par le clavier
    /// </summary>
    class JeuKeyboard : Jeu
    {
        KeyboardInput input;

        public JeuKeyboard(KiPongGame g, Difficulty d, bool isOp, KeyboardInput i)
            : base(g, d, isOp)
        {
            input = i;
            setBats();
        }

        protected override void setBats()
        {
            playerOne = new BatKeyboard(game, Side.LEFT, difficulty, input);
            if (IsOnePlayer)
                bot = new AIBat(game, Side.RIGHT, difficulty, ball);
            else
                playerTwo = new BatKeyboard(game, Side.RIGHT, difficulty, input);
        }

        protected override void IncreaseSpeed()
        {
            base.IncreaseSpeed();
            ((BatKeyboard)playerOne).IncreaseSpeed();
            if(!IsOnePlayer)
                ((BatKeyboard)playerTwo).IncreaseSpeed();
        }
    }
}
