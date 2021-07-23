using System;

namespace MenuControl
{
    public class Menu : MenuParent
    {
        public Menu(string text, MenuParent[] menus, Action<MenuParent> toDo)
        {
            Menus.AddRange(menus);
            Text = text;
            Index = 0;
            this.toDo = toDo;
        }

        public Menu(string text, MenuParent[] menus)
        {
            Menus.AddRange(menus);
            Text = text;
            Index = 0;

        }

        /// <summary>
        /// Creates menu.
        /// </summary>
        public override void Create()
        {
            try
            {
                while (true)
                {
                    Draw();
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            SelectPrevious();
                            break;
                        case ConsoleKey.DownArrow:
                            SelectNext();
                            break;
                        case ConsoleKey.Enter:
                            if (Index == Menus.Count)
                            {
                                Back.Create();
                                return;
                            }

                            Menus[Index].Create();
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}