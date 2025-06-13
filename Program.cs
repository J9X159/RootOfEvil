using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root_of_Evil
{
    internal class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine("\n              WELCOME TO SHVA145!\r\n            " +
                              "\r\n           (press [LMB] to continue)\r\n   " +
                              "\n   Controls:\n   " +
                "[W][A][S][D] – scroll through the menu.\n" +
                "   [LMB] – action/confirm/apply\n   " +
                "[RMB] – back/close\n");
            Console.ReadKey();
/*
        string[] options = { "Start Game", "Endless Mode", "Exit" };
            int selected = 0;
            ConsoleKey key;
            void StartGame()
            {
                Console.WriteLine("Start game works");
            }

            void EndlessMode()
            {
                Console.WriteLine("Endless works");
            }

            do
            {
                Intro.ShowIntro();

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("> " + options[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("  " + options[i]);
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected == 0) ? options.Length - 1 : selected - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % options.Length;
                }
            }
            while (key != ConsoleKey.Enter);

            switch (selected)
            {
                case 0:
                    StartGame(); break;
                case 1:
                    EndlessMode(); break;
                case 2:
                    Environment.Exit(0); break;
            }
         
            */

        }
    }
}
