using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

enum Tab { Stats, Items, Skills, Quests, Shops, Settings }
enum ItemFocus { Title, Sort, Page, Item }
enum ItemSortMode { Default, Alphabetical }
enum SkillsFocus { Title, Skill }

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

class Program
{
    static List<Item> items = new List<Item>
    {
        new Item("Beer «Baltika9»", "Zaspokaja pragnienie, przywraca +10 HP", 2),
        new Item("Chips «Doritos»", "Smaczna przekąska. Dodaje +10 do szczęścia.", 1),
        new Item("Stimpak", "Zatrzymuje krwawienie. Przywraca 15 HP", 5),
        new Item("Water", "Orzeźwiająca woda źródlana", 1),
        new Item("Candy Bar", "Daje dużo energii", 1),
        new Item("Painkillers", "Redukuje ból", 2),
    };

    static int selectedItemIndex = 0;
    static ItemFocus itemFocus = ItemFocus.Title;
    static bool viewingItemDescription = false;
    static int currentPage = 0;
    static ItemSortMode currentSort = ItemSortMode.Default;

    const int ITEMS_PER_PAGE = 5;

    static SkillsFocus skillsFocus = SkillsFocus.Title;
    static bool viewingSkillDescription = false;

    static int hackLevel = 2;
    static int craftLevel = 0;
    static int survivalLevel = 2;

    static int level = 4;
    static int xp = 10;
    static int remainingPoints = 3;
    private static int selectedSkillIndex;

    static void Main()
    {
        ShowIntro();

        static void ShowIntro()
        {
            Console.Clear();
            string[] intro = new string[]
            {
                "\n",
                "                     THE STORY BEGINS",
                "\n",
                "   ====================================================\n",
                "   First of the three heroes was Amadeus, a wizard. Not\n",
                "   Perhaps the bravest or the most powerful, but he was\n",
                "   clever and sensible\n",
                "\n",
                "   Then there was Pontius the knight, fearless protector\n",
                "   of the realm, who loved good food, drink and battle.\n",
                "\n",
                "   Last but not least was Zoya, a thief. She was\n",
                "   mysterious and only seen as a passing shadow on a\n",
                "   cloudy night.\n"
            };

            foreach (string line in intro)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(2);
                }
                Console.WriteLine();
            }

            Thread.Sleep(2500);

            Console.WriteLine("\n   Press ENTER to continue...");
            Console.ReadLine();
            Console.Clear();

            List<Tab> tabs = new List<Tab> { Tab.Stats, Tab.Items, Tab.Skills, Tab.Quests, Tab.Shops, Tab.Settings };
            int currentTabIndex = 0;

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

                if (viewingSkillDescription)
                {
                    DrawSkillDescription();
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.D)
                        viewingSkillDescription = false;
                    continue;
                }

                switch (tabs[currentTabIndex])
                {
                    case Tab.Stats: DrawStats(); break;
                    case Tab.Items: DrawItems(); break;
                    case Tab.Skills: DrawSkills(); break;
                    case Tab.Quests: DrawQuests(); break;
                    case Tab.Shops: DrawShops(); break;
                    case Tab.Settings: DrawSettings(); break;
                }

                var keyInput = Console.ReadKey(true);

                if (tabs[currentTabIndex] == Tab.Items)
                    HandleItemsInput(keyInput, tabs, ref currentTabIndex);
                else if (tabs[currentTabIndex] == Tab.Skills)
                    HandleSkillsInput(keyInput, tabs, ref currentTabIndex);
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

                    itemFocus = ItemFocus.Title;
                    selectedItemIndex = 0;
                    currentPage = 0;
                }
            }

            Console.Clear();
            Console.WriteLine("Zamknięto.");
        }
    }

    static void HandleSkillsInput(ConsoleKeyInfo key, List<Tab> tabs, ref int currentTabIndex)
{
    if (viewingSkillDescription)
    {
        if (key.Key == ConsoleKey.D)
        {
            viewingSkillDescription = false;
        }
        return;
    }

    switch (key.Key)
    {
        case ConsoleKey.S:
            if (skillsFocus == SkillsFocus.Title)
            {
                skillsFocus = SkillsFocus.Skill;
                selectedSkillIndex = 0;
            }
            else if (skillsFocus == SkillsFocus.Skill)
            {
                if (selectedSkillIndex < 2) selectedSkillIndex++; // nie zawija
            }
            break;

        case ConsoleKey.W:
            if (skillsFocus == SkillsFocus.Skill)
            {
                if (selectedSkillIndex > 0)
                {
                    selectedSkillIndex--; // nie zawija
                }
                else
                {
                    skillsFocus = SkillsFocus.Title; // wraca do tytułu
                }
            }
            break;

        case ConsoleKey.A:
            if (skillsFocus == SkillsFocus.Skill)
            {
                viewingSkillDescription = true; // pokazuje opis
            }
            else if (skillsFocus == SkillsFocus.Title && currentTabIndex > 0)
            {
                currentTabIndex--;
            }
            break;

        case ConsoleKey.D:
            if (skillsFocus == SkillsFocus.Title && currentTabIndex < tabs.Count - 1)
            {
                currentTabIndex++;
            }
            break;

        case ConsoleKey.Enter:
            if (skillsFocus == SkillsFocus.Skill && remainingPoints > 0)
            {
                switch (selectedSkillIndex)
                {
                    case 0: hackLevel++; break;
                    case 1: craftLevel++; break;
                    case 2: survivalLevel++; break;
                }
                remainingPoints--;
                // nie resetujemy focusu — zostajesz na skillu
            }
            break;
    }
}

    static void DrawSkillDescription()
{
    Console.Clear();
    string skillName = selectedSkillIndex switch
    {
        0 => "Hack",
        1 => "Craft",
        2 => "Survival",
        _ => ""
    };

    string description = selectedSkillIndex switch
    {
        0 => "Hack: Pozwala włamywać się do systemów i terminali.",
        1 => "Craft: Umożliwia tworzenie przedmiotów i ulepszeń.",
        2 => "Survival: Zwiększa odporność, szansę na przetrwanie.",
        _ => ""
    };

    Console.WriteLine($"\n  {skillName.ToUpper()}");
    Console.WriteLine($"\n  {description}");
    Console.WriteLine("\n  [D] - wróć");
}
    static void DrawStats()
    {
        Console.WriteLine("\n");
        Console.WriteLine("                          STATS ]>\n");
        Console.WriteLine("   Health : 100 / 100");
        Console.WriteLine("   Magicka : 100 / 100");
        Console.WriteLine("   Gold: 0");
    }

    static void DrawSkills()
    {
        Console.WriteLine("\n");
        Console.WriteLine(skillsFocus == SkillsFocus.Title ? "                      <[ SKILLS ]>\n" : "                         SKILLS\n");
        Console.WriteLine($"                      * level : {level}");
        Console.WriteLine($"                     * xp : {xp}/500");
        Console.WriteLine($"                  * remain points : {remainingPoints}\n");

        void DrawSkill(string name, int level, int index)
        {
            string prefix = (skillsFocus == SkillsFocus.Skill && selectedSkillIndex == index)
            ? "[ " : "";
            string suffix = (skillsFocus == SkillsFocus.Skill && selectedSkillIndex == index)
            ? " ] [+] ([A]-description)" : "";
            Console.WriteLine($"   {prefix}{name} : {level}{suffix}");
        }

        DrawSkill("Hack", hackLevel, 0);
        DrawSkill("Craft", craftLevel, 1);
        DrawSkill("Survival", survivalLevel, 2);

        Console.WriteLine();

        if (skillsFocus == SkillsFocus.Skill)
           {}
    }

    static void DrawQuests()
    {
        Console.WriteLine("\n");
        Console.WriteLine("                       <[ QUESTS ]>\n");
        Console.WriteLine("   Main");
    }

    static void DrawShops()
    {
        Console.WriteLine("\n");
        Console.WriteLine("                       <[ SHOPS ]>\n");
    }

    static void DrawSettings()
    {
        Console.WriteLine("\n");
        Console.WriteLine("                       <[ SETTINGS \n");
        Console.WriteLine("   Color");
    }

    static void HandleItemsInput(ConsoleKeyInfo key, List<Tab> tabs, ref int currentTabIndex)
    {
        int maxPage = (int)Math.Ceiling(GetSortedItems().Count / (double)ITEMS_PER_PAGE);

        switch (key.Key)
        {
            case ConsoleKey.S:
                if (itemFocus == ItemFocus.Title) itemFocus = ItemFocus.Sort;
                else if (itemFocus == ItemFocus.Sort) itemFocus = (maxPage > 1) ? ItemFocus.Page : ItemFocus.Item;
                else if (itemFocus == ItemFocus.Page && maxPage > 1) itemFocus = ItemFocus.Item;
                else if (itemFocus == ItemFocus.Item && selectedItemIndex < ITEMS_PER_PAGE - 1)
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
                if (itemFocus == ItemFocus.Sort)
                    currentSort = ItemSortMode.Default;
                else if (itemFocus == ItemFocus.Page && currentPage > 0)
                    currentPage--;
                else if (itemFocus == ItemFocus.Item && GetPageItems().Count > 0)
                    viewingItemDescription = true;
                else if (itemFocus != ItemFocus.Item && currentTabIndex > 0)
                {
                    currentTabIndex--;
                    itemFocus = ItemFocus.Title;
                    selectedItemIndex = 0;
                    currentPage = 0;
                }
                break;

            case ConsoleKey.D:
                if (itemFocus == ItemFocus.Sort)
                    currentSort = ItemSortMode.Alphabetical;
                else if (itemFocus == ItemFocus.Page && currentPage < maxPage - 1)
                    currentPage++;
                else if (itemFocus != ItemFocus.Item && currentTabIndex < tabs.Count - 1)
                {
                    currentTabIndex++;
                    itemFocus = ItemFocus.Title;
                    selectedItemIndex = 0;
                    currentPage = 0;
                }
                break;

            case ConsoleKey.Enter:
                if (itemFocus == ItemFocus.Item && GetPageItems().Count > 0)
                {
                    var currentItem = GetPageItems()[selectedItemIndex];
                    currentItem.Quantity--;
                    if (currentItem.Quantity <= 0)
                    {
                        items.Remove(currentItem);
                        if (selectedItemIndex >= GetPageItems().Count)
                            selectedItemIndex = Math.Max(0, GetPageItems().Count - 1);
                    }

                    if (items.Count == 0)
                    {
                        itemFocus = ItemFocus.Title;
                        selectedItemIndex = 0;
                        currentPage = 0;
                    }
                }
                break;
        }
    }

    static List<Item> GetSortedItems() => currentSort == ItemSortMode.Alphabetical ? items.OrderBy(i => i.Name).ToList() : new List<Item>(items);
    static List<Item> GetPageItems() => GetSortedItems().Skip(currentPage * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();

    static void DrawItems()
    {
        Console.WriteLine("\n");
        Console.WriteLine(itemFocus == ItemFocus.Title ? "                       <[ ITEMS ]>\n" : "                          ITEMS\n");

        string sortLine = currentSort == ItemSortMode.Default ? "default" : "alphabetical";
        Console.WriteLine(itemFocus == ItemFocus.Sort ? $"                    <[Sort : {sortLine}]>" : $"                    Sort : {sortLine}");

        int totalPages = (int)Math.Ceiling(GetSortedItems().Count / (double)ITEMS_PER_PAGE);
        Console.WriteLine(totalPages > 1
            ? itemFocus == ItemFocus.Page ? $"   <[page {currentPage + 1}/{totalPages}]>" : $"   page {currentPage + 1}/{totalPages}"
            : "   page 1/1");

        if (itemFocus == ItemFocus.Item && GetPageItems().Count > 0)
            Console.WriteLine("                                [A] - description");
        else
            Console.WriteLine("");

        var pageItems = GetPageItems();
        for (int i = 0; i < pageItems.Count; i++)
        {
            if (itemFocus == ItemFocus.Item && i == selectedItemIndex)
                Console.WriteLine($"   [{pageItems[i].DisplayName}] [use]");
            else
                Console.WriteLine($"   {pageItems[i].DisplayName}");
        }

        if (pageItems.Count == 0)
            Console.WriteLine("");
    }

    static void DrawItemDescription()
    {
        var item = GetPageItems()[selectedItemIndex];
        Console.Clear();
        Console.WriteLine("\n");
        Console.WriteLine($"   {item.DisplayName}\n");
        Console.WriteLine($"   {item.Description}");
        Console.WriteLine("\n   [D] - wróć");
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


    


    