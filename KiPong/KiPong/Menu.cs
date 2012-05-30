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

    public abstract class Menu : GameObject
    {
        private static Color Gray = new Color(238, 238, 238);
        private static Color DarkGray = new Color(128, 128, 128);
        private static Color Blue = new Color(10, 0, 150);
        private static float ratioTitle = 5f / 16f;

        // Menu
        public string Title { get; set; }
        public string Description { get; set; }
        private List<string> menuItems;
        public List<string> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; SetItems(); }
        }
        private int iterator, lastIterator;
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
        private Aide aide;
        public bool Help { get; set; }
        protected bool isPrintingHelp;
        // Draw
        private bool isDraw;
        private bool start;

        private Rectangle rectTitle, bordTitle, rectItem, rectItems;
        private int margin;
        // Actions
        public bool Valid { get; set; }
        public bool Back { get; set; }

        public Menu(KiPongGame g, Aide a) : base(g)
        {
            aide = a;
            iterator = lastIterator = 0;
            margin = game.ScreenHeight /60;

            int width = game.ScreenWidth - 2 * margin;
            bordTitle = new Rectangle(margin, margin, width, (int)(game.ScreenHeight * ratioTitle));
            int margin2 = margin * 2;
            rectTitle = new Rectangle(margin2, margin2, width - margin2, bordTitle.Height - margin2);
            rectItems = new Rectangle(margin, bordTitle.Height + 2 * margin, width, game.ScreenHeight - bordTitle.Height - 3 * margin);
        }

        private void SetItems()
        {
            int width = rectItems.Width - 2 * margin;
            int height = (rectItems.Height - (1 + menuItems.Count) * margin) / menuItems.Count;
            rectItem = new Rectangle(margin*2, 0, width, height);
        }

        public void StartDescription() { start = true; }

        public override void Update()
        {
            if (start && isDraw)
            {
                Utils.SpeechStop();
                Utils.SpeechSynchrone(Description);
                lastIterator = -1;
                start = false;
                Iterator=0;
            }

            if (Help && !isPrintingHelp)
            {
                isPrintingHelp = true;
                aide.Speech();
            }
            else if (Help && isPrintingHelp)
            {
                isPrintingHelp = false;
                Utils.SpeechStop();
                start = true;
            }
        }

        public override void Draw()
        {
            // Aide
            if (isPrintingHelp)
            {
                game.SpriteBatch.GraphicsDevice.Clear(Color.Black);
                aide.Draw(game.SpriteBatch);
                return;
            }

            game.SpriteBatch.GraphicsDevice.Clear(Gray);

            isDraw = start;

            Utils.DrawRectangle(game.SpriteBatch, bordTitle, DarkGray);
            Utils.DrawRectangle(game.SpriteBatch, rectTitle, Gray);
            Utils.DrawStringAtCenter(game.SpriteBatch, game.FontTitle, rectTitle, Title, Blue);

            Utils.DrawRectangle(game.SpriteBatch, rectItems, Color.Black);

            rectItem.Y = rectItems.Y + margin;

            if (menuItems != null)
            {
                Color back, font;
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    if (i == Iterator)
                    {
                        back = Color.White;
                        font = Blue;
                    }
                    else
                    {
                        back = Blue;
                        font = Color.White;
                    }
                    Utils.DrawRectangle(game.SpriteBatch, rectItem, back);
                    Utils.DrawStringAtCenter(game.SpriteBatch, game.Font, rectItem, MenuItems[i], font);
                    rectItem.Y += rectItem.Height + margin;
                }
            }
        }
    }
}
