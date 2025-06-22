public class Item
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