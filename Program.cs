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
            Stats,Items,Skills,Quests,Settings
        }
        enum ItemFocus
        {
            Title, Sort, Page, Item
        }

       class Item
    {
        public string Name;
        public string Description;
        public int Quantity;

        public Item(string name, string description, int quantity)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
        }

        public string DisplayName => $"({Quantity}) {Name}";
    }

    static List<Item> items = new List<Item>
    {
        new Item("Beer «Baltika9»", "Zaspokaja pragnienie, przywraca +10 HP", 2),
        new Item("Chips «Doritos»", "Smaczna przekąska. Dodaje +10 do szczęścia.", 1),
        new Item("Stimpak", "Zatrzymuje krwawienie. Przywraca 15 HP", 5),
    };

    static int selectedItemIndex = 0;
    static ItemFocus itemFocus = ItemFocus.Title;
    static bool viewingItemDescription = false;

    static void Main()
    {
        List<Tab> tabs = new List<Tab> { Tab.Stats, Tab.Items, Tab.Skills, Tab.Quests, Tab.Settings };
        int currentIndex = 0;

        Console.CursorVisible = false;
        bool running = true;

        while (running)
        {
            Console.Clear();

            if (viewingItemDescription)
            {
                DrawItemDescription();
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D)
                    viewingItemDescription = false;
                continue;
            }

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

            var keyInput = Console.ReadKey(true);

           bool isInItemsTab = tabs[currentIndex] == Tab.Items;

if (isInItemsTab)
{
    // Obsługa klawiszy A/D dla zmiany zakładek w Items,
    // ale tylko jeśli nie jesteś na przedmiocie
    if ((itemFocus != ItemFocus.Item || items.Count == 0) && (keyInput.Key == ConsoleKey.A || keyInput.Key == ConsoleKey.D))
    {
        if (keyInput.Key == ConsoleKey.A && currentIndex > 0) currentIndex--;
        else if (keyInput.Key == ConsoleKey.D && currentIndex < tabs.Count - 1) currentIndex++;

        // Reset focus przy zmianie zakładki
        itemFocus = ItemFocus.Title;
        selectedItemIndex = 0;
    }
    else
    {
        HandleItemsInput(keyInput);
    }
}
else
{
    switch (keyInput.Key)
    {
        case ConsoleKey.A:
            if (currentIndex > 0) currentIndex--;
            break;
        case ConsoleKey.D:
            if (currentIndex < tabs.Count - 1) currentIndex++;
            break;
        case ConsoleKey.Escape:
            running = false;
            break;
    }

    // Reset focus przy wejściu do innej zakładki
    itemFocus = ItemFocus.Title;
    selectedItemIndex = 0;
}
        }

        Console.Clear();
        Console.WriteLine("Zamknięto.");
    }

    static void HandleItemsInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.S:
                if (itemFocus == ItemFocus.Title) itemFocus = ItemFocus.Sort;
                else if (itemFocus == ItemFocus.Sort) itemFocus = ItemFocus.Page;
                else if (itemFocus == ItemFocus.Page && items.Count > 0) itemFocus = ItemFocus.Item;
                else if (itemFocus == ItemFocus.Item && selectedItemIndex < items.Count - 1)
                    selectedItemIndex++;
                break;

            case ConsoleKey.W:
                if (itemFocus == ItemFocus.Item && selectedItemIndex > 0)
                    selectedItemIndex--;
                else if (itemFocus == ItemFocus.Item) itemFocus = ItemFocus.Page;
                else if (itemFocus == ItemFocus.Page) itemFocus = ItemFocus.Sort;
                else if (itemFocus == ItemFocus.Sort) itemFocus = ItemFocus.Title;
                break;

            case ConsoleKey.A:
                if (itemFocus == ItemFocus.Item && items.Count > 0)
                    viewingItemDescription = true;
                break;

            case ConsoleKey.Enter:
                if (itemFocus == ItemFocus.Item && items.Count > 0)
                {
                    items[selectedItemIndex].Quantity--;
                    if (items[selectedItemIndex].Quantity <= 0)
                        items.RemoveAt(selectedItemIndex);

                    if (selectedItemIndex >= items.Count)
                        selectedItemIndex = Math.Max(0, items.Count - 1);

                   if (items.Count == 0)
{
    itemFocus = ItemFocus.Page;
    selectedItemIndex = 0;
}
                }
                break;
        }
    }

        static void DrawStats()
        {
            Console.WriteLine("\n");
            Console.WriteLine("                          STATS ]>\n");
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

    static void DrawQuests()
    {
        Console.WriteLine("\n               <[ QUESTS ]>\n");
    }

    static void DrawSettings() {
            Console.WriteLine("\n");
            Console.WriteLine("                     <[ SETTINGS \n");
            Console.WriteLine("   Color"); }

    static void DrawItems()
    {
        Console.WriteLine("\n");
        Console.WriteLine(itemFocus == ItemFocus.Title ? "                       <[ ITEMS ]>\n" :                                                "                          ITEMS\n");
        Console.WriteLine(itemFocus == ItemFocus.Sort ?  "                    < Sort : default >" : "                        Sort : default");
        Console.WriteLine(itemFocus == ItemFocus.Page ? "   < page 1/1 >" : "   page 1/1");

        if (items.Count == 0)
        {
            
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (itemFocus == ItemFocus.Item && i == selectedItemIndex)
                    Console.WriteLine($"   < {items[i].DisplayName} >");
                else
                    Console.WriteLine($"   {items[i].DisplayName}");
            }

            if (itemFocus == ItemFocus.Item)
                Console.WriteLine("\n                                [A] - description  ");
        }

       
    }

    static void DrawItemDescription()
    {
        var item = items[selectedItemIndex];
        Console.Clear();
        
        Console.WriteLine($"  {item.DisplayName}\n");
        Console.WriteLine($"  {item.Description}");
        Console.WriteLine("\n  [D] - wróć");
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


        }
    