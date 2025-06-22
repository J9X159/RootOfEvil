using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public static class Intro
{
   public  static void ShowIntro()
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
        }
}

/*
static void DrawSkills()
            //tutaj beda wszystkie skille ktore mozna ulepszac poprzez progresje poziomowa
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
        new Item("item4", "Opis przedmiotu", 1),
        
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
            Console.WriteLine(itemFocus == ItemFocus.Item ?                 "                                [A] - description" : "");
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

















public static class Intro
{
    
  
    
 /*





                public static void ShowIntro()
                {
                    Console.Clear();
                    string[] logoLines = new string[]
                    {


                        "  ______________________________________________________________________   ",
                        "      ____                                 _         _____",
                        "      /    )                             /  `        /    '        ,   /   ",
                        "  ---/___ /----__----__--_/_-------__--_/__---------/__---------------/-   ",
                        "    /    |   /   ) /   ) /       /   ) /           /        | /  /   /     ",
                        "  _/_____|__(___/_(___/_(_ _____(___/_/___________/____ ____|/__/___/___   ",
                        "  _______________________________________________________________________  ",
                        "",
                        "        [:: Root of Evil ::]\n",
                        "   Solve to Survive... or Be Consumed."

                    };

                    foreach (string line in logoLines)
                    {
                        foreach (char c in line)
                        {
                            Console.Write(c);
                            Thread.Sleep(3); 
                        }
                        Console.WriteLine();
                    }

                    Thread.Sleep(2500);

                    Console.WriteLine("\n   Press ENTER to continue...");

                    Console.ReadLine();

                    Console.Clear();
                   // ShowDifficultySelection();
                }
              /*  public static void ShowDifficultySelection()
                {
                    Console.WriteLine("        [:: Choose the Difficulty ::]\n");
                    Console.WriteLine("   1. Easy      (Master the basics) ");
                    Console.WriteLine("   2. Medium    (Improve the average stuff) ");
                    Console.WriteLine("   2. Hard      (Become a part with Root of Evil) ");
                    Console.Write("\nEnter choice (1-3): ");
                    string input = Console.ReadLine();

                    Console.WriteLine($"\nYou selected option {input}. Good luck!");

                    Thread.Sleep(2500);

                    Console.WriteLine("\nPres ENTER to continue...");
                    Console.ReadLine();

                    Console.Clear();

                }
                public static void ShowCharacterSelection()
                {
                    Console.WriteLine("   Select your class:");
                    Console.WriteLine("1. Analyst      (+10% solve speed)");
                    Console.WriteLine("2. Runner       (+1 tile movement)");
                    Console.WriteLine("3. Solver       (+1 extra life)");
                    Console.Write("\nEnter choice (1-3): ");
                    string input = Console.ReadLine();

                    Console.WriteLine($"\nYou selected option {input}. Good luck!");


                }*/




