using System;
using System.Collections.Generic;
using System.Linq;

public static class ItemsModule
{
    public static bool viewingItemDescription = false;
    private static int selectedItemIndex = 0;
    private static ItemFocus itemFocus = ItemFocus.Title;
    private static int currentPage = 0;
    private static ItemSortMode currentSort = ItemSortMode.Default;

    private const int ITEMS_PER_PAGE = 5;

    private static List<Item> items = new List<Item>
    {
        new Item("Beer «Baltika9»", "Zaspokaja pragnienie, przywraca +10 HP", 2),
        new Item("Chips «Doritos»", "Smaczna przekąska. Dodaje +10 do szczęścia.", 1),
        new Item("Stimpak", "Zatrzymuje krwawienie. Przywraca 15 HP", 5),
        new Item("Water", "Orzeźwiająca woda źródlana", 1),
        new Item("Candy Bar", "Daje dużo energii", 1),
        new Item("Painkillers", "Redukuje ból", 2),
    };

    public static void DrawItems()
    {
        Console.WriteLine("\n");
        Console.WriteLine(itemFocus == ItemFocus.Title ? "                       <[ ITEMS ]>\n" : "                          ITEMS\n");

        string sortLine = currentSort == ItemSortMode.Default ? "default" : "alphabetical";
        Console.WriteLine(itemFocus == ItemFocus.Sort ? $"                    <[Sort : {sortLine}]>" : $"                      Sort : {sortLine}");

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

    public static void DrawItemDescription()
    {
        var item = GetPageItems()[selectedItemIndex];
        Console.Clear();
        Console.WriteLine("\n");
        Console.WriteLine($"   {item.DisplayName}\n");
        Console.WriteLine($"   {item.Description}");
        Console.WriteLine("\n   [D] - wróć");
    }

    public static void HandleItemsInput(ConsoleKeyInfo key, List<Tab> tabs, ref int currentTabIndex)
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
                    ResetFocus();
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
                    ResetFocus();
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
                        ResetFocus();
                    }
                }
                break;
        }
    }

    public static void ResetFocus()
    {
        itemFocus = ItemFocus.Title;
        selectedItemIndex = 0;
        currentPage = 0;
    }

    private static List<Item> GetSortedItems() => currentSort == ItemSortMode.Alphabetical ? items.OrderBy(i => i.Name).ToList() : new List<Item>(items);
    private static List<Item> GetPageItems() => GetSortedItems().Skip(currentPage * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
}