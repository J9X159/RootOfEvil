using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading; 


class Program
{
    static void Main()
    {
        Intro.ShowIntro();

        List<Tab> tabs = new List<Tab> { Tab.Stats, Tab.Items, Tab.Skills, Tab.Quests, Tab.Shops, Tab.Settings };
        int currentTabIndex = 0;

        Console.CursorVisible = false;
        bool running = true;

        while (running)
        {
            Console.Clear();

            if (ItemsModule.viewingItemDescription)
            {
                ItemsModule.DrawItemDescription();
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D)
                    ItemsModule.viewingItemDescription = false;
                continue;
            }

            if (SkillsModule.viewingSkillDescription)
            {
                SkillsModule.DrawSkillDescription();
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D)
                    SkillsModule.viewingSkillDescription = false;
                continue;
            }

            switch (tabs[currentTabIndex])
            {
                case Tab.Stats: StatsModule.DrawStats(); break;
                case Tab.Items: ItemsModule.DrawItems(); break;
                case Tab.Skills: SkillsModule.DrawSkills(); break;
                case Tab.Quests: QuestsModule.DrawQuests(); break;
                case Tab.Shops: ShopsModule.DrawShops(); break;
                case Tab.Settings: SettingsModule.DrawSettings(); break;
            }

            var keyInput = Console.ReadKey(true);

            if (tabs[currentTabIndex] == Tab.Items)
                ItemsModule.HandleItemsInput(keyInput, tabs, ref currentTabIndex);
            else if (tabs[currentTabIndex] == Tab.Skills)
                SkillsModule.HandleSkillsInput(keyInput, tabs, ref currentTabIndex);
            else
            {
                switch (keyInput.Key)
                {
                    case ConsoleKey.A:
                        if (currentTabIndex > 0) currentTabIndex--;
                        break;
                    case ConsoleKey.D:
                        if (currentTabIndex < tabs.Count - 1) currentTabIndex++;
                        break;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                }

                ItemsModule.ResetFocus();
            }
        }

        Console.Clear();
        Console.WriteLine("Zamknięto.");
    }
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


                   List<string> postacie = new List<string> { "Uczeń", "Maturzysta", "Profesor" };
             int selectedIndex = 0;
             bool showMenu = true;

             Console.CursorVisible = false;

             while (true)
             {
                 if (showMenu)
                 {
                     Console.Clear();
                     Console.WriteLine("                   SELECT A CHARACTER\n");
                         Console.WriteLine("                      [A]-Description");
                     for (int i = 0; i < postacie.Count; i++)
                     {
                         if (i == selectedIndex)
                             Console.WriteLine($"       < {postacie[i]} >");
                         else
                             Console.WriteLine($"         {postacie[i]}");
                     }



                     var key = Console.ReadKey(true);
                     switch (key.Key)
                     {
                         case ConsoleKey.W:
                             if (selectedIndex > 0) selectedIndex--;
                             break;
                         case ConsoleKey.S:
                             if (selectedIndex < postacie.Count - 1) selectedIndex++;
                             break;
                         case ConsoleKey.A:
                             showMenu = false; // przejście do opisu
                             break;
                         case ConsoleKey.Enter:
                             Console.Clear();
                             Console.WriteLine($"\nWybrałeś postać: {postacie[selectedIndex]}");
                             Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
                             Console.ReadKey();
                             return;
                         case ConsoleKey.Escape:
                             return;
                     }
                 }
                 else
                 {
                     // Wyświetl opis postaci
                     Console.Clear();
                     Console.WriteLine($"        OPIS POSTACI: {postacie[selectedIndex]}\n");

                     switch (postacie[selectedIndex])
                     {
                         case "Uczeń":
                             Console.WriteLine("  - Młody i chłonny wiedzy");
                             Console.WriteLine("  - Niski poziom umiejętności");
                             Console.WriteLine("  - Duży potencjał rozwoju");
                             break;
                         case "Maturzysta":
                             Console.WriteLine("  - Wysoki stres, ale też determinacja");
                             Console.WriteLine("  - Średni poziom umiejętności");
                             Console.WriteLine("  - Gotów do podjęcia wyzwań");
                             break;
                         case "Profesor":
                             Console.WriteLine("  - Doświadczony ekspert");
                             Console.WriteLine("  - Wysoki poziom wiedzy");
                             Console.WriteLine("  - Mało energii, ale ogromne możliwości");
                             break;
                     }

                     Console.WriteLine("\n\n  [D] wróć");

                     var key = Console.ReadKey(true);
                     if (key.Key == ConsoleKey.D)
                     {
                         showMenu = true;
                     }
                 }
             }
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


    


    