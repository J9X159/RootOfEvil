using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Root_of_Evil
{
    internal class Program
    {
        enum Tab
        {
            Stats,
            Items,
            Skills,
            Quests,
            Settings
        }

        static void Main()
        {
            List<Tab> tabs = new List<Tab> { Tab.Stats, Tab.Skills, Tab.Items, Tab.Quests, Tab.Settings };
            int currentIndex = 0;

            Console.CursorVisible = false;
            bool running = true;

            while (running)
            {
                Console.Clear();


                switch (tabs[currentIndex])
                {
                    case Tab.Stats:
                        DrawStats();
                        break;
                    case Tab.Items:
                        DrawItems();
                        break;
                    case Tab.Skills:
                        DrawSkills();
                        break;
                    case Tab.Quests:
                        DrawQuests();
                        break;
                    case Tab.Settings:
                        DrawSettings();
                        break;
                }



                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.A:
                        if (currentIndex > 0)
                            currentIndex--;
                        break;
                    case ConsoleKey.D:
                        if (currentIndex < tabs.Count - 1)
                            currentIndex++;
                        break;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                }
            }


        }

        static void DrawStats()
        {
            Console.WriteLine("\n");
            Console.WriteLine("                          STATS ]>                        \n");
            Console.WriteLine("\n");
            Console.WriteLine("   Health : 100 / 100\n");
            Console.WriteLine("   Rad : 0");
        }

        static void DrawSkills()
        {
            Console.WriteLine("\n");
            Console.WriteLine("                      <[ SKILLS ]>\n");
            Console.WriteLine("                      * level : 4  ");
            Console.WriteLine("                     * xp : 10/500");
            Console.WriteLine("                  * remain points : 0\n");
            Console.WriteLine("   Hack : 2");
            Console.WriteLine("   Craft : 0");
            Console.WriteLine("   Survival : 2\n");

        }

        static void DrawItems()
        {
            Console.WriteLine("\n");
            Console.WriteLine("                       <[ ITEMS ]>                        \n");
            Console.WriteLine("                      Sort : default                    \n");
            Console.WriteLine("   page 1/1\n");
            Console.WriteLine("   [(3) Chips <Doritos> =]");
        }

        static void DrawQuests()
        {
            Console.WriteLine("\n");
            Console.WriteLine("                       <[ QUESTS ]                        \n");
        }

        static void DrawSettings()
        {
            Console.WriteLine("\n");
            Console.WriteLine("                     <[ SETTINGS \n");
        
            Console.WriteLine("   Color");
           
        }



            /*
    Console.WriteLine(
        "\n              WELCOME TO SHVA145!\r\n            " +
        "\r\n           (press [LMB] to continue)\r\n   " +
        "\n   Controls:\n   " +
        "[W][A][S][D] – scroll through the menu.\n" +
        "   [LMB] – action/confirm/apply\n   " +
        "[RMB] – back/close\n");
    Console.ReadKey(true);


     List<string> options = new List<string> { "Uczen", "Maturzysta", "Profesor" };
int selected = 0;
bool running = true;

Console.CursorVisible = false;

while (running)
{
    Console.Clear();
    Console.WriteLine("                   SELECT A CHARACTER\n");

    // Wyświetl opcje z zaznaczeniem
    for (int i = 0; i < options.Count; i++)
    {
        if (i == selected)
            Console.WriteLine($"< {options[i]} >");
        else
            Console.WriteLine($"  {options[i]}");
    }

    // Obsługa klawiatury
    ConsoleKeyInfo key = Console.ReadKey(true);

    switch (key.Key)
    {
        case ConsoleKey.W:
            if (selected > 0)
                selected--;
            break;
        case ConsoleKey.S:
            if (selected < options.Count - 1)
                selected++;
            break;
        case ConsoleKey.Enter:
            running = false;
            break;
    }
}

Console.Clear();
Console.WriteLine($"Wybrałeś: {options[selected]}");
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

    

