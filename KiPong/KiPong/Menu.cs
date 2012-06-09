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

    public abstract class Menu : Helpable
    {
        public static Color Backgroung, Border, ItemsBackground, ItemColor;
        private static float ratioTitle = 5f / 16f;

        // Menu
        /// <summary>
        /// Obtient ou modifi le titre du menu
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Obtient ou modifi la description du menu qui sera dite par la synthèse vocale
        /// </summary>
        public string Description { get; set; }
        private List<string> menuItems;
        /// <summary>
        /// Obtient ou modifi la liste des items
        /// </summary>
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
        // Draw
        private bool isDraw;
        private bool start;

        private Rectangle rectTitle, bordTitle, rectItem, rectItems;
        private int margin;
        // Actions
        public bool Valid { get; set; }
        public bool Back { get; set; }

        public Menu(KiPongGame g, String helpImg, String helpText)
            : base(g, helpImg, helpText)
        {
            iterator = lastIterator = 0;
            margin = game.ScreenHeight /60;

            // Dessine les rectangles du font du menu
            int width = game.ScreenWidth - 2 * margin;
            bordTitle = new Rectangle(margin, margin, width, (int)(game.ScreenHeight * ratioTitle));
            int margin2 = margin * 2;
            rectTitle = new Rectangle(margin2, margin2, width - margin2, bordTitle.Height - margin2);
            rectItems = new Rectangle(margin, bordTitle.Height + 2 * margin, width, game.ScreenHeight - bordTitle.Height - 3 * margin);
        }

        /// <summary>
        /// Créer le rectangle d'un item
        /// </summary>
        private void SetItems()
        {
            int width = rectItems.Width - 2 * margin;
            int height = (rectItems.Height - (1 + menuItems.Count) * margin) / menuItems.Count;
            rectItem = new Rectangle(margin*2, 0, width, height);
        }

        /// <summary>
        /// Lance la sythèse vocale qui va dire la description
        /// </summary>
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

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (isPrintingHelp) return;

            game.SpriteBatch.GraphicsDevice.Clear(Backgroung);

            isDraw = start;
            // On dessine le titre
            Utils.DrawRectangle(game.SpriteBatch, bordTitle, Border);
            Utils.DrawRectangle(game.SpriteBatch, rectTitle, Backgroung);
            Utils.DrawStringAtCenter(game.SpriteBatch, game.FontTitle, rectTitle, Title, ItemColor);
            // On dessine les items
            Utils.DrawRectangle(game.SpriteBatch, rectItems, ItemsBackground);
            rectItem.Y = rectItems.Y + margin;
            if (menuItems != null)
            {
                Color back, font;
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    if (i == Iterator)
                    {
                        back = Color.White;
                        font = ItemColor;
                    }
                    else
                    {
                        back = ItemColor;
                        font = Color.White;
                    }
                    Utils.DrawRectangle(game.SpriteBatch, rectItem, back);
                    Utils.DrawStringAtCenter(game.SpriteBatch, game.Font, rectItem, MenuItems[i], font);
                    rectItem.Y += rectItem.Height + margin;
                }
            }
        }

        protected override void LeaveHelp()
        {
            start = true;
        }
    }
}
