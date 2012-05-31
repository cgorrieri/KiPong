using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiPong
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class MenuKeyboard : Menu
    {
        KeyboardInput input;

        public MenuKeyboard(KiPongGame g, KeyboardInput input)
            : base(g, "aideMenuKeyboardImg", "aideMenuKeyboardTxt")
        {
            this.input = input;
        }

        public override void Update()
        {
            base.Update();
            if (!isPrintingHelp)
            {
                if (input.DownRight || input.DownLeft)
                {
                    Iterator++;
                }
                else if (input.UpRight || input.UpLeft)
                {
                    Iterator--;
                }

                Valid = input.Valid();
                Back = input.Back();
            }
        }
    }
}
