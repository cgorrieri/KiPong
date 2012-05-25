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

    public abstract class Menu
    {
        protected List<string> MenuItems;
        private int iterator;
        public string Title { get; set; }
        protected Game1 game;
        public bool Valid { get; set; }
        public bool Back { get; set; }
        public int Iterator
        {
            get
            {
                return iterator;
            }
            set
            {
                iterator = value;
                if (iterator > MenuItems.Count - 1) iterator = MenuItems.Count - 1;
                if (iterator < 0) iterator = 0;
            }
        }

        public Menu(Game1 g, String title, List<string> items)
        {
            game = g;
            Title = title;
            MenuItems = items;
            Iterator = 0;
        }

        public abstract void Update();

        public int GetNumberOfOptions()
        {
            return MenuItems.Count;
        }

        public string GetItem(int index)
        {
            return MenuItems[index];
        }

        public void Draw(SpriteBatch sb, SpriteFont font)
        {
            int margin = (int)(game.ScreenHeight * 0.01);
            int height = game.ScreenHeight / (1 + GetNumberOfOptions()) - margin * (2 + GetNumberOfOptions()) / (1 + GetNumberOfOptions());
            int width = game.ScreenWidth - 2 * margin;

            Rectangle r = new Rectangle(margin, margin, width, height);
            Utils.DrawRectangle(sb, game.GraphicsDevice, r, Color.Yellow);
            Utils.DrawStringAtCenter(sb, font, r, Title, Color.Blue);
            
            for (int i = 0; i < GetNumberOfOptions(); i++)
            {
                r.Y += height + margin;
                if (i == Iterator)
                {
                    Utils.DrawRectangle(sb, game.GraphicsDevice, r, Color.Red);
                }
                else
                {
                    Utils.DrawRectangle(sb, game.GraphicsDevice, r, Color.Aqua);
                }
                Utils.DrawStringAtCenter(sb, font, r, GetItem(i), Color.White);
            }
        }
    }
}
