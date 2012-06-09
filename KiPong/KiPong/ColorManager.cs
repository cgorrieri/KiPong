﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KiPong
{
    public static class ColorManager
    {
        private static int state;
        private const int MAX_STATE_INDEX = 1;
        // menu
        private static Color[] MenuBack = new Color[2] { new Color(238, 238, 238), new Color(255, 255, 255) };
        private static Color[] MenuBorder = new Color[2] { new Color(128, 128, 128), new Color(128, 128, 128) };
        private static Color[] MenuItemColor = new Color[2] { new Color(10, 0, 150), new Color(0, 0, 0) };
        private static Color[] MenuItemsBackground = new Color[2] { new Color(0, 0, 0), new Color(160, 160, 160) };


        public static void InitializeColors()
        {
            state = MAX_STATE_INDEX;
            ChangeColors();
        }

        public static void ChangeColors()
        {
            state = (state == MAX_STATE_INDEX) ? 0 : state + 1;
            // Menu
            Menu.Backgroung = MenuBack[state];
            Menu.Border = MenuBorder[state];
            Menu.ItemColor = MenuItemColor[state];
            Menu.ItemsBackground = MenuItemsBackground[state];
        }
    }
}
