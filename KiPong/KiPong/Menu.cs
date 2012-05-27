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
        public List<string> MenuItems;
        private int iterator, lastIterator;
        public string Title { get; set; }
        protected Game1 game;
        public string Description { get; set;}
        public bool Start { get; set; }
        public bool Valid { get; set; }
        public bool Back { get; set; }
        public int Iterator
        {
            get { return iterator; }
            set
            {
                iterator = value;
                if (iterator > MenuItems.Count - 1) iterator = MenuItems.Count - 1;
                if (iterator < 0) iterator = 0;
                if (lastIterator != iterator)
                {
                    lastIterator = iterator;
                    Utils.SpeechAsynchrone(MenuItems[Iterator]);
                }
            }
        }

        public Menu(Game1 g)
        {
            game = g;
            iterator = lastIterator = 0;
        }

        public virtual void Update()
        {
            if (Start)
            {
                Utils.SpeechStop();
                Utils.SpeechSynchrone(Description);
                lastIterator = -1;
                Start = false;
                Iterator=0;
            }
        }

        public void Draw()
        {
            int count = MenuItems.Count;
            int margin = (int)(game.ScreenHeight * 0.01);
            int height = game.ScreenHeight / (1 + count) - margin * (2 + count) / (1 + count);
            int width = game.ScreenWidth - 2 * margin;

            Rectangle r = new Rectangle(margin, margin, width, height);
            Utils.DrawRectangle(game.SpriteBatch, r, Color.Yellow);
            Utils.DrawStringAtCenter(game.SpriteBatch, game.Font, r, Title, Color.Blue);
            
            for (int i = 0; i < count; i++)
            {
                r.Y += height + margin;
                if (i == Iterator)
                {
                    Utils.DrawRectangle(game.SpriteBatch, r, Color.Red);
                }
                else
                {
                    Utils.DrawRectangle(game.SpriteBatch, r, Color.Aqua);
                }
                Utils.DrawStringAtCenter(game.SpriteBatch, game.Font, r, MenuItems[i], Color.White);
            }
        }
    }
}
