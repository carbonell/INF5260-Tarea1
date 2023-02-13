using UASD.Estructura.Dat;

namespace UASD.Estructura.ADT.Console;

public class GroceryList
{
    public string Name { get; set; }
    public MathSet<string> Groceries { get; set; } = new MathSet<string>();

    public GroceryList(string name) => Name = name;

    public override string ToString() => Name;
}