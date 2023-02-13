namespace UASD.Estructura.Dat;

// Lenguaje Fetch/Store sacado del artículo: https://en.wikipedia.org/wiki/Abstract_data_type#Typical_operations
public class MathSet<T> : IAbsractDataType<T>
{

    private int _dim = 0;
    private T[] _elements = new T[] { };

    public int Size => _dim;

    public T Fetch(int index)
        => _elements[index];

    public MathSet()
    {

    }

    public MathSet(T[] elements)
    {
        _elements = elements;
    }


    // "Alta"
    public void Store(T element)
    {
        var elementsPlusOne = new T[_dim + 1];
        Array.Copy(_elements, elementsPlusOne, _dim);
        elementsPlusOne[_dim] = element;
        _elements = elementsPlusOne;
        _dim++;
    }

    // Bonus ;) 
    public T Pop()
    {
        var elem = Fetch(_dim - 1);
        _dim--;
        var elementsMinusOne = new T[_dim];
        Array.Copy(_elements, elementsMinusOne, _dim);
        _elements = elementsMinusOne;
        return elem;
    }

    // "Baja"
    public T RemoveAt(int index)
    {
        var element = _elements[index];
        _dim--;
        var elementsMinusOne = new T[_dim];
        if (index > 0)
        {
            Array.Copy(_elements, elementsMinusOne, index);
        }
        Array.Copy(_elements, index + 1, elementsMinusOne, index, _dim - index);
        _elements = elementsMinusOne;
        return element;
    }

    public void Sort()
    {
        throw new NotImplementedException();
    }

    // Búsqueda
    public int IndexOf(T element)
    {
        var notFoundIndex = -1;
        for (var i = 0; i < _elements.Count(); i++)
        {
            var elem = _elements[i];
            if (elem is not null && elem.Equals(element))
                return i;
        }
        return notFoundIndex;
    }

    public void Remove(T element)
    {
        var index = IndexOf(element);
        RemoveAt(index);
    }

    public MathSet<T> Union(MathSet<T> set2, bool allowRepeteatedElements = true)
    {
        return Union(this, set2, allowRepeteatedElements);
    }

    private static MathSet<T> Union(MathSet<T> set1, MathSet<T> set2, bool allowRepeteatedElements = true)
    {
        var union = new MathSet<T>();
        for (int i = 0; i < set1.Size; i++)
        {
            union.Store(set1.Fetch(i));
        }

        for (int i = 0; i < set2.Size; i++)
        {
            var elemInSet2 = set2.Fetch(i);
            if (allowRepeteatedElements || set1.IndexOf(elemInSet2) == -1)
                union.Store(set2.Fetch(i));
        }

        return union;
    }

    // This one is not really needed. It's just to include an "overloading" example.
    public MathSet<T> Union(MathSet<T> set2)
    {
        return Union(set2, allowRepeteatedElements: true);
    }


    public MathSet<T> Difference(MathSet<T> set2)
    {
        return Difference(this, set2);
    }

    private static MathSet<T> Difference(MathSet<T> set1, MathSet<T> set2)
    {
        var difference = new MathSet<T>();
        for (int i = 0; i < set1.Size; i++)
        {
            var elem = set1.Fetch(i);
            if (set2.IndexOf(elem) == -1)
                difference.Store(elem);
        }
        return difference;
    }

    public MathSet<T> Intersect(MathSet<T> set2)
    {
        var intersection = new MathSet<T>();
        for (int i = 0; i < Size; i++)
        {
            var elem = Fetch(i);
            if (set2.IndexOf(elem) != -1)
                intersection.Store(elem);
        }
        return intersection;
    }

    // This one is how I would really do it
    public MathSet<(T, T)> TupleCartesianProduct(MathSet<T> set2)
    {
        var product = new MathSet<(T, T)>();
        for (int i = 0; i < Size; i++)
        {
            var elem = Fetch(i);
            for (int j = 0; j < set2.Size; j++)
            {
                product.Store((elem, set2.Fetch(j)));
            }
        }
        return product;
    }

    public MathSet<T[]> CartesianProduct(MathSet<T> set2)
    {
        return CartesianProduct(this, set2);
    }

    private static MathSet<T[]> CartesianProduct(MathSet<T> set1, MathSet<T> set2)
    {
        var product = new MathSet<T[]>();
        for (int i = 0; i < set1.Size; i++)
        {
            var elem = set1.Fetch(i);
            for (int j = 0; j < set2.Size; j++)
            {
                var orderedPair = new T[2] { elem, set2.Fetch(j) };
                product.Store(orderedPair);
            }
        }
        return product;
    }

    public static MathSet<T[]> NaturalProduct(MathSet<T> set1, MathSet<T> set2)
    {
        var product = new MathSet<T[]>();
        for (int i = 0; i < set1.Size; i++)
        {
            var elem = set1.Fetch(i);
            for (int j = 0; j < set2.Size; j++)
            {
                if (elem != null && elem.Equals(set2.Fetch(j)))
                {
                    var orderedPair = new T[2] { elem, set2.Fetch(j) };
                    product.Store(orderedPair);
                }
            }
        }
        return product;
    }

    public MathSet<T[]> NaturalProduct(MathSet<T> set2)
    {
        return NaturalProduct(this, set2);
    }

    // Operators

    public static MathSet<T> operator +(MathSet<T> set1, MathSet<T> set2)
    {
        return Union(set1, set2);
    }

    public static MathSet<T> operator -(MathSet<T> set1, MathSet<T> set2)
    {
        return set1.Difference(set2);
    }

    public static MathSet<T[]> operator *(MathSet<T> set1, MathSet<T> set2)
    {
        return set1.CartesianProduct(set2);
    }
}
