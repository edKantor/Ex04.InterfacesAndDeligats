﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MenuItemNonActionable : MenuItem
    {
        private const int k_EscapeCharacter = 0;
        private string m_EscapeMenuLine = "Back";
        private List<MenuItem> m_SubMenuItems;
        private bool m_isMainMenu = false;

        public bool IsSetToMainMenu
        {
            get { return m_isMainMenu; }
            set
            {
                m_isMainMenu = value;
                m_EscapeMenuLine = "Exit";
            }
        }

        public MenuItemNonActionable(string i_Title, string i_Headline)
            : base(i_Title, i_Headline)
        {
        }

        protected int GetUserChoice()
        {
            string userChoiceString = Console.ReadLine();
            int result;
            int userChoice;
            if (int.TryParse(userChoiceString, out userChoice))
            {
                if (userChoice >= 1 && userChoice <= m_SubMenuItems.Count)
                {
                    //return userChoice;
                    result = userChoice;
                }
                else
                {
                    throw new ArgumentException(
                        string.Format("Expected a menu option value in range of of 1-{0}: Received input not in range.", m_SubMenuItems.Count));
                }
            }
            else
            {
                throw new FormatException("Expected a natural value to match a menu option: Received input not in format.");
            }

            return result;
        }

        public void AddSubMenuItem(MenuItem i_SubMenuItem)
        {
            m_SubMenuItems.Add(i_SubMenuItem);
        }

        private void showMenuItems()
        {
            StringBuilder menuOutput = new StringBuilder();
            menuOutput.AppendFormat("{0}:{1}", m_Headline, Environment.NewLine);
            int rowCounter = 0;
            menuOutput.AppendFormat("\t{0}. {1}{2}", rowCounter++, m_EscapeMenuLine, Environment.NewLine);
            foreach (MenuItem item in m_SubMenuItems)
            {
                menuOutput.AppendFormat("\t{0}. {1}{2}", rowCounter++, item.Title, Environment.NewLine);
            }

            Console.WriteLine(menuOutput);
        }

        public override void Activate()
        {
            bool isMenuActivated = true;

            while(isMenuActivated)
            {
                showMenuItems();
                int userChoice = GetUserChoice();

                if (k_EscapeCharacter == userChoice)
                {
                    isMenuActivated = false;
                }

                m_SubMenuItems[userChoice - 1].Activate();
            }
        }
    }
}