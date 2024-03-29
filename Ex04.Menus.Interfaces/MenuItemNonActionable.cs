﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class MenuItemNonActionable : MenuItem
    {
        private List<MenuItem> m_SubMenuItems;

        public MenuItemNonActionable(string i_TitleNameString)
            : base(i_TitleNameString)
        {
        }

        public override bool StartMenuItem()
        {
            showSubMenuItemTitles();
            int userChoice = getUserChoice();

            while (userChoice != 0)
            {
                m_SubMenuItems[userChoice - 1].StartMenuItem();
                showSubMenuItemTitles();
                userChoice = getUserChoice();
            }

            return true;
        }

        public void AddSubMenuItem(MenuItem i_MenuItem)
        {
            if (m_SubMenuItems == null)
            {
                m_SubMenuItems = new List<MenuItem>();
            }

            m_SubMenuItems.Add(i_MenuItem);
            i_MenuItem.SetParent(this);
        }

        private void showSubMenuItemTitles()
        {
            Console.Clear();
            int choiceCounter = 0;
            StringBuilder menuStringBuilder = new StringBuilder();
            menuStringBuilder.AppendLine(string.Format(":: {0} ::", Title));
            if (m_BaseMenuItem == null)
            {
                menuStringBuilder.AppendLine("0. Exit");
            }
            else
            {
                menuStringBuilder.AppendLine("0. Back");
            }

            foreach (MenuItem subMenuItem in m_SubMenuItems)
            {
                menuStringBuilder.AppendLine(string.Format("{0}. {1}", ++choiceCounter, subMenuItem.Title));
            }

            System.Console.WriteLine(menuStringBuilder.ToString());
        }

        private int getUserChoice()
        {
            System.Console.WriteLine("Choose from the choices above:");
            int userInput;

            while ((!int.TryParse(System.Console.ReadLine(), out userInput)) || (userInput < 0 || userInput > m_SubMenuItems.Count))
            {
                System.Console.WriteLine("input not valid try again:");
            }

            return userInput;
        }
    }
}
