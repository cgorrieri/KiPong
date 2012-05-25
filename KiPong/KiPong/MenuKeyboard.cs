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
        public MenuKeyboard(Game1 g, String title, List<string> items, KeyboardInput input)
            : base(g, title, items)
        {
            this.input = input;
        }

        public override void Update()
        {
            if (input.DownRight || input.DownLeft)
            {
                Iterator++;
            }
            else if (input.UpRight || input.UpLeft)
            {
                Iterator--;
            }

            Valid = input.Valider();
            
            Back = input.Retour();
        }
    }
}
