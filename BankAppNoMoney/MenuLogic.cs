using BankAppNoMoney.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAppNoMoney.Base
{
    public class MenuLogic
    {
        // HANTERAR MENYN, OCH HANTERAR INVALIDA VAL
        internal int PrintMenu(List<string> menu, int index)
        {
            if (menu == null || menu.Count == 0)
                return -1;

            int current = Math.Clamp(index, 0, menu.Count - 1);

            ConsoleKey key;
            do
            {
                Console.Clear();
                DrawMenu(menu, current);

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        current = (current - 1 + menu.Count) % menu.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        current = (current + 1) % menu.Count;
                        break;
                    case ConsoleKey.Enter:
                        break;
                    default:
                        Console.WriteLine("Use Up/Down arrows to navigate and Enter to select.");
                        Console.ReadKey(true); // Wait for user to read the message
                        break;
                }

            } while (key != ConsoleKey.Enter);

            Console.Clear();
            return current;
        }

        // RITAR MENYN, OCH VISAR VILKET ALTERNATIV SOM ÄR VALT
        static void DrawMenu(List<string> menu, int selectedIndex)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine($"< {menu[i]} >");
                }
                else
                {
                    Console.WriteLine($"  {menu[i]}");
                }
            }
        }
    }
}
