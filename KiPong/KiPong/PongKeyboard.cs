using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiPong
{
    /// <summary>
    /// Le jeu qui est jouable par le clavier
    /// </summary>
    class PongKeyboard : Pong
    {
        KeyboardInput input;

        public PongKeyboard(KiPongGame g, Difficulty d, bool isOp, KeyboardInput i)
            : base(g, d, isOp, new Help(g, "aideJeuKeyboardImg", "aideJeuKeyboardTxt"))
        {
            input = i;
            setBats();
        }

        protected override void setBats()
        {
            bat1 = new BatKeyboard(game, Side.LEFT, difficulty, input);
            if (IsOnePlayer)
                bot = new AIBat(game, Side.RIGHT, difficulty, ball);
            else
                bat2 = new BatKeyboard(game, Side.RIGHT, difficulty, input);
        }

        protected override void IncreaseSpeed()
        {
            base.IncreaseSpeed();
            ((BatKeyboard)bat1).IncreaseSpeed();
            if(!IsOnePlayer)
                ((BatKeyboard)bat2).IncreaseSpeed();
        }
    }
}
