using System;
using System.Collections.Generic;

namespace MenuControl
{
    /// <summary>
    /// General type for menus.
    /// </summary>
    public abstract class MenuParent
    {
        /// <summary>
        /// Method to update menus.
        /// </summary>
        protected Action<MenuParent> toDo;

        /// <summary>
        /// Prints text in color.
        /// </summary>
        /// <param name="toPrint"> String to print. </param>
        /// <param name="color"> Color to use. </param>
        public static void PrintInColor(string toPrint, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(toPrint);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Previous menu.
        /// </summary>
        public MenuParent Back { get; set; }

        /// <summary>
        /// All menus of current menu. 
        /// </summary>
        public List<MenuParent> Menus { get; set; }

        /// <summary>
        /// Current index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Menu's text.
        /// </summary>
        public string Text { get; set; }

        protected MenuParent()
        {
            Menus = new List<MenuParent>();
        }

        /// <summary>
        /// Object to store.
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Rendering.
        /// </summary>
        protected void Draw()
        {
            toDo?.Invoke(this);
            Console.Clear();

            for (int i = 0; i < Menus.Count; i++)
            {
                if (Index == i)
                {
                    PrintInColor($"> {Menus[i].Text}", ConsoleColor.Cyan);
                }
                else
                {
                    PrintInColor($" {Menus[i].Text}", ConsoleColor.Yellow);
                }
            }

            if (Index == Menus.Count)
            {
                if (Text.Equals("Start menu"))
                {
                    PrintInColor("> Close app with save", ConsoleColor.Cyan);
                }
                else
                {
                    PrintInColor("> Back", ConsoleColor.Cyan);
                }
            }
            else
            {
                if (Text.Equals("Start menu"))
                {
                    PrintInColor(" Close app with save", ConsoleColor.Yellow);
                }
                else
                {
                    PrintInColor(" Back", ConsoleColor.Yellow);
                }
            }
        }

        /// <summary>
        /// Next item select.
        /// </summary>
        protected void SelectNext()
        {
            if (Index == Menus.Count)
            {
                return;
            }

            ++Index;
        }

        /// <summary>
        /// Previous item select.
        /// </summary>
        protected void SelectPrevious()
        {
            if (Index == 0)
            {
                return;
            }

            --Index;
        }

        /// <summary>
        /// Adds new menu.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        public void AddItem(MenuParent menu)
        {
            Menus.Add(Menus[^1]);

            Menus[^2] = menu;
        }

        public abstract void Create();
    }
}
