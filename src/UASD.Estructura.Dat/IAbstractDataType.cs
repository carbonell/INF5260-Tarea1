
// reference for english method names here: https://www.geeksforgeeks.org/abstract-data-types/ and https://en.wikipedia.org/wiki/Abstract_data_type#Typical_operations
public interface IAbsractDataType<T>
{
    T Fetch(int index);
    void Store(T element);
    void Remove(T element);
    T RemoveAt(int index);
    void Sort();
    int Size { get; }
}