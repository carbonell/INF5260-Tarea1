namespace UASD.Estructura.ADT.Console;
using System;
using UASD.Estructura.Dat;

public class ConsoleMenu
{
    private readonly List<GroceryList> _grocerylists = new List<GroceryList>();
    public void MainMenu()
    {
        NewLine();
        Write("Bienvenido a la tarea de estructura de datos de Carlos Carbonell. Simularemos una lista de compra para esto. Inserte el número de la operación que desea realizar: ");
        Write("1. Crear una listas de Compra");
        Write("2. Manejar listas de Compra");
        Write("3. Pruebas de Consola");
        int? optionValue = ReadIntOption(numberOfOptions: 3);
        switch (optionValue)
        {
            case 1:
                CreateNewGroceryList();
                break;
            case 2:
                ManageGroceryLists();
                break;
            case 3:
                ConsoleTests();
                break;
            default:
                MainMenu();
                break;
        }


    }

    private int? ReadIntOption(int numberOfOptions)
    {
        var option = ReadKey();
        var optionValue = TryParseOption(option.KeyChar);
        if (optionValue.HasValue && optionValue > 0 && optionValue <= numberOfOptions)
            return optionValue;
        return null;
    }

    private int? TryParseOption(char opt)
    {
        if (int.TryParse(opt.ToString(), out int optResult))
        {
            return optResult;
        }
        return null;
    }

    private void Write(string text)
    {
        Console.WriteLine(text);
    }

    private void WriteTitle(string text)
    {
        NewLine();
        Write(text);
    }
    private void NewLine()
    {
        Write("");
    }

    private ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    private string? Read()
    {
        return Console.ReadLine();
    }

    private void CreateNewGroceryList()
    {
        string? name = null;
        WriteTitle("Digite el nombre de la lista que desea utilizar: \n");
        name = Read();
        if (!string.IsNullOrEmpty(name))
        {
            var list = new GroceryList(name);
            _grocerylists.Add(list);
            AddGroceriesToList(list);
        }
        else
        {
            Write("Debes digitar un nombre válido en la lista primero:");
            MainMenu();
        }
    }

    private void AddGroceriesToList(GroceryList list)
    {
        string? grocery = null;
        do
        {
            WriteTitle($"Digite el producto que desea agregar a la lista {list}. Presione enter para terminar: ");
            grocery = Read();

            if (!string.IsNullOrEmpty(grocery))
            {
                list.Groceries.Store(grocery);
                NewLine();
                Write($"{grocery} agregado a la lista {list} exitosamente.");
            }

        } while (!string.IsNullOrEmpty(grocery));
        PrintList(list);
        MainMenu();
    }

    private void PrintList(GroceryList list)
    {
        WriteTitle($"Tu lista {list} contiene los siguientes productos: ");
        PrintGroceries(list.Groceries);
    }

    private void PrintGroceries(MathSet<string> groceries)
    {
        for (int i = 0; i < groceries.Size; i++)
        {
            Write($"{i + 1}. {groceries.Fetch(i)}");
        }
    }

    private void PrintCartesianProductGroceries(MathSet<string[]> groceries)
    {
        for (int i = 0; i < groceries.Size; i++)
        {
            Write($"{i + 1}. {groceries.Fetch(i)[0]},{groceries.Fetch(i)[1]}");
        }
    }

    public void ManageGroceryLists()
    {
        ValidateListMinLength();
        WriteTitle("Seleccione el número de la lista con la que desea trabajar: ");
        PrintGroceryLists();
        var option = ReadIntOption(_grocerylists.Count);
        if (option.HasValue)
        {
            var selectedList = _grocerylists[option.Value - 1];
            ManageList(selectedList);
        }
        else
        {
            Write("Valor Inválido");
            MainMenu();
        }

    }

    private void PrintGroceryLists(List<GroceryList> lists)
    {
        for (int i = 0; i < lists.Count; i++)
        {
            var list = lists[i];
            Write($"{i + 1}.{list}");
        }
    }

    private void PrintGroceryLists()
    {
        PrintGroceryLists(_grocerylists);
    }

    private void ValidateListMinLength(List<GroceryList> list, int listLimit = 1)
    {
        if (list.Count < listLimit)
        {
            WriteTitle($"Debe tenera al menos {listLimit} lista(s) para realizar esta operacion.");
            MainMenu();
        }
    }

    private void ValidateListMinLength(int listLimit = 1)
    {
        ValidateListMinLength(_grocerylists, listLimit);
    }

    private void ManageList(GroceryList list)
    {
        WriteTitle($"¿Qué desea realizar con la lista {list}?");
        Write("1. Unir a otra lista");
        Write("2. Comparar con otra lista");
        Write("3. Producto Cartesiano (¿Por qué no?)");
        Write("4. Producto Natural (Khe??)");

        var option = ReadIntOption(numberOfOptions: 4);
        switch (option)
        {
            case 1:
                JoinList(list);
                break;
            case 2:
                CompareLists(list);
                break;
            case 3:
                DoCartesianProduct(list);
                break;
            case 4:
                DoNaturalProduct(list);
                break;
            default:
                Write("Valor Inválido");
                MainMenu();
                break;
        }
    }

    private void DoCartesianProduct(GroceryList list)
    {
        ValidateListMinLength(listLimit: 2);
        WriteTitle($"¿Con cual lista desea unir {list}?");

        List<GroceryList> candidates = ExtractCandidates(list);

        WriteTitle("Seleccione el número de la lista con la que desea trabajar: ");
        PrintGroceryLists(candidates);
        var option = ReadIntOption(numberOfOptions: candidates.Count);
        if (option.HasValue)
        {
            var list2 = candidates[option.Value - 1];
            var cartesianProduct = list.Groceries * list2.Groceries;
            PrintCartesianProductGroceries(cartesianProduct);
        }
        else
        {
            Write("Valor Inválido");
        }
        MainMenu();
    }

    private void DoNaturalProduct(GroceryList list)
    {
        ValidateListMinLength(listLimit: 2);
        WriteTitle($"¿Con cual lista desea unir {list}?");

        List<GroceryList> candidates = ExtractCandidates(list);

        WriteTitle("Seleccione el número de la lista con la que desea trabajar: ");
        PrintGroceryLists(candidates);
        var option = ReadIntOption(numberOfOptions: candidates.Count);
        if (option.HasValue)
        {
            var list2 = candidates[option.Value - 1];
            var naturalProduct = list.Groceries.NaturalProduct(list2.Groceries);
            PrintCartesianProductGroceries(naturalProduct);
        }
        else
        {
            Write("Valor Inválido");
        }
        MainMenu();
    }

    private void JoinList(GroceryList list)
    {
        ValidateListMinLength(listLimit: 2);
        WriteTitle($"¿Con cual lista desea unir {list}?");

        List<GroceryList> candidates = ExtractCandidates(list);

        WriteTitle("Seleccione el número de la lista con la que desea trabajar: ");
        PrintGroceryLists(candidates);
        var option = ReadIntOption(numberOfOptions: candidates.Count);
        if (option.HasValue)
        {
            var list2 = candidates[option.Value - 1];
            JoinLists(list, list2);
            WriteTitle($"Los productos de la lista {list2} han sido agregados exitosamente a {list}");
            PrintList(list);
        }
        else
        {
            Write("Valor Inválido");
        }
        MainMenu();
    }

    private List<GroceryList> ExtractCandidates(GroceryList list)
    {
        var candidates = new List<GroceryList>();
        for (int i = 0; i < _grocerylists.Count; i++)
        {
            var candidate = _grocerylists[i];
            if (candidate.Name != list.Name)
                candidates.Add(candidate);
        }

        return candidates;
    }

    public void CompareLists(GroceryList list)
    {
        ValidateListMinLength(listLimit: 2);
        List<GroceryList> candidates = ExtractCandidates(list);
        WriteTitle("Seleccione el número de la lista con la que desea trabajar: ");
        PrintGroceryLists(candidates);
        var option = ReadIntOption(numberOfOptions: candidates.Count);
        if (option.HasValue)
        {
            // Intersection
            var list2 = candidates[option.Value - 1];
            PerformListComparison(list, list2);
        }
        else
        {
            Write("Valor Inválido");
        }
        MainMenu();
    }

    private void PerformListComparison(GroceryList list, GroceryList list2)
    {
        var commonProducts = list.Groceries.Intersect(list2.Groceries);
        WriteTitle("Los siguientes productos están en ambas listas: ");
        PrintGroceries(commonProducts);

        var onlyInSourcelistProducts = list.Groceries - list2.Groceries;
        WriteTitle($"Los siguientes son exclusivos de la lista {list}: ");
        PrintGroceries(onlyInSourcelistProducts);

        var onlyInDestinationListProducts = list2.Groceries - list.Groceries;
        WriteTitle($"Los siguientes son exclusivos de la lista {list2}: ");
        PrintGroceries(onlyInDestinationListProducts);
    }

    private static void JoinLists(GroceryList list, GroceryList list2)
    {
        var joinedList = list.Groceries + list2.Groceries;
        list.Groceries = joinedList;
    }

    public void ConsoleTests()
    {
        WriteTitle("Elija la prueba que desea realizar: ");
        Write("1. Prueba de unión de listas");
        Write("2. Prueba de comparación de listas");
        Write("3. Prueba de Producto de listas");
        var option = ReadIntOption(numberOfOptions: 3);
        switch (option)
        {
            case 1:
                TestUnion();
                break;
            case 2:
                TestComparison();
                break;
            case 3:
                TestProducts();
                break;
        }

    }

    private void TestProducts()
    {
        WriteTitle("Prueba de Producto de listas: ");
        var (bravo, pola) = GetSampleData();
        PrintList(bravo);
        PrintList(pola);
        NewLine();
        WriteTitle("Producto Cartesiano");
        var cartesianProduct = bravo.Groceries * pola.Groceries;
        PrintCartesianProductGroceries(cartesianProduct);

        WriteTitle("Producto Natural");
        var naturalProduct = bravo.Groceries.NaturalProduct(pola.Groceries);
        PrintCartesianProductGroceries(naturalProduct);
        MainMenu();
    }

    private (GroceryList, GroceryList) GetSampleData()
    {
        var bravo = new GroceryList("bravo");
        bravo.Groceries.Store("Pan");
        bravo.Groceries.Store("Chocolate");
        bravo.Groceries.Store("Leche");

        var pola = new GroceryList("pola");
        pola.Groceries.Store("Huevos");
        pola.Groceries.Store("Jugo");
        pola.Groceries.Store("Pan");
        return (bravo, pola);
    }

    private void TestComparison()
    {
        WriteTitle("Prueba de Comparación: ");
        var (bravo, pola) = GetSampleData();
        PrintList(bravo);
        PrintList(pola);
        NewLine();
        WriteTitle("Comparación");
        PerformListComparison(bravo, pola);
        MainMenu();
    }

    private void TestUnion()
    {
        WriteTitle("Prueba de Union de listas: ");
        var (bravo, pola) = GetSampleData();
        PrintList(bravo);
        PrintList(pola);
        NewLine();
        WriteTitle("Union");
        var union = bravo.Groceries + pola.Groceries;
        PrintGroceries(union);
        MainMenu();
    }
}
