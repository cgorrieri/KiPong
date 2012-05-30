using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public class BatKeyboard : Bat
    {
        private float BaseSpeed, MaxSpeed, speed, increaseSpeed;
        private KeyboardInput input;

        /// <summary>
        /// Initialise une batte
        /// </summary>
        /// <param name="game">Le jeu propriétaire</param>
        /// <param name="side">Si true alors à gauche sinon à droite de l'écran</param>
        public BatKeyboard(KiPongGame game, Side side, Difficulty d, KeyboardInput i) :  base(game, side, d)
        {
            input = i;
            switch (d)
            {
                case Difficulty.EASY:
                    BaseSpeed = 8f;
                    increaseSpeed = 0.9f;
                    break;
                case Difficulty.MEDIUM:
                    BaseSpeed = 10f;
                    increaseSpeed = 1.8f;
                    break;
                default:
                    BaseSpeed = 12f;
                    increaseSpeed = 3.6f;
                    break;
            }
            MaxSpeed = BaseSpeed + 3 * increaseSpeed;
            speed = BaseSpeed;
        }

        public override void Update()
        {
            if (side == Side.LEFT)
            {
                if (input.DownLeft)
                    MoveDown();
                if (input.UpLeft)
                    MoveUp();
            }
            else
            {                
                if (input.DownRight)
                    MoveDown();
                if (input.UpRight)
                    MoveUp();
            }
        }

        protected void MoveDown()
        {
            setPosition(position + new Vector2(0, speed));
        }

        protected void MoveUp()
        {
            setPosition(position + new Vector2(0, -speed));
        }

        public override void Reset()
        {
            base.Reset();
            speed = BaseSpeed;
        }

        public void IncreaseSpeed()
        {
            if (speed < MaxSpeed)
                speed += increaseSpeed;
        }
    }
}
