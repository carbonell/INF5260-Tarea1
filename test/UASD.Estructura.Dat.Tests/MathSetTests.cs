namespace UASD.Estructura.Dat.Tests;

public class MathSetTests
{
    [Fact]
    public void Insert_ByDefault_AddsElementToSet()
    {
        // Arrange
        var set = new MathSet<int>();

        // Act
        set.Store(1);

        // Assert
        var element = set.Fetch(0);
        Assert.Equal(1, element);
    }

    [Theory]
    [MemberData(nameof(RemoveAt_ByDefault_RemovesElementFromSet_Data))]
    public void RemoveAt_ByDefault_RemovesElementFromSet(int index)
    {
        // Arrange
        var set = new MathSet<int>();
        set.Store(0); // on index 0
        set.Store(1); // on index 1
        set.Store(2); // on index 2
        set.Store(3); // on index 3
        set.Store(4); // on index 4

        // Act
        set.RemoveAt(index);
        // Assert

        Assert.Equal(4, set.Size);
        var removedElementIndex = set.IndexOf(index);
        Assert.Equal(-1, removedElementIndex);
    }

    public static IEnumerable<object[]> RemoveAt_ByDefault_RemovesElementFromSet_Data =>
    new List<object[]>{
        new object[]{0},
        new object[]{1},
        new object[]{2},
        new object[]{3},
        new object[]{4},
    };

    [Fact]
    public void IndexOf_ByDefault_FindsIndexOnSet()
    {
        // Arrange
        var set = new MathSet<int>();
        set.Store(2);
        set.Store(4);
        set.Store(6);

        // Act
        var index = set.IndexOf(4);

        // Assert
        Assert.Equal(1, index);
    }

    [Fact]
    public void Remove_ByDefault_RemovesElementFromSet()
    {
        // Arrange
        var set = new MathSet<int>();
        set.Store(2);
        set.Store(4);
        set.Store(6);

        // Act
        set.Remove(4);
        Assert.Equal(2, set.Size);
        var removedElementIndex = set.IndexOf(4);
        // Assert
        Assert.Equal(-1, removedElementIndex);
    }

    [Fact]
    public void Union_ByDefault_CreatesNewSetContainingAllElementsOn2SetsWithRepetition()
    {
        // Arrange
        var set1 = new MathSet<int>();
        set1.Store(1);
        set1.Store(2);
        set1.Store(3);

        var set2 = new MathSet<int>();
        set2.Store(3);
        set2.Store(4);
        set2.Store(5);

        // Act
        var set1UnionSet2 = set1.Union(set2, allowRepeteatedElements: true);

        // Assert
        Assert.Equal(6, set1UnionSet2.Size);
        var removedElementIndex1 = set1UnionSet2.IndexOf(1);
        Assert.NotEqual(-1, removedElementIndex1);

        var removedElementIndex2 = set1UnionSet2.IndexOf(2);
        Assert.NotEqual(-1, removedElementIndex2);

        var removedElementIndex3 = set1UnionSet2.IndexOf(3);
        Assert.NotEqual(-1, removedElementIndex3);

        var removedElementIndex4 = set1UnionSet2.IndexOf(4);
        Assert.NotEqual(-1, removedElementIndex4);

        var removedElementIndex5 = set1UnionSet2.IndexOf(5);
        Assert.NotEqual(-1, removedElementIndex5);

        var removedElementIndex6 = set1UnionSet2.IndexOf(5);
        Assert.NotEqual(-1, removedElementIndex6);

    }

    [Fact]
    public void Union_WhenInvokedWithFalseAllowRepetedElementsFlag_CreatesNewSetContainingAllElementsOn2SetsWithoutRepetition()
    {
        // Arrange
        var set1 = new MathSet<int>();
        set1.Store(1);
        set1.Store(2);
        set1.Store(3);

        var set2 = new MathSet<int>();
        set2.Store(3);
        set2.Store(4);
        set2.Store(5);

        // Act
        var set1UnionSet2 = set1.Union(set2, allowRepeteatedElements: false);

        // Assert
        Assert.Equal(5, set1UnionSet2.Size);
        var removedElementIndex1 = set1UnionSet2.IndexOf(1);
        Assert.NotEqual(-1, removedElementIndex1);

        var removedElementIndex2 = set1UnionSet2.IndexOf(2);
        Assert.NotEqual(-1, removedElementIndex2);

        var removedElementIndex3 = set1UnionSet2.IndexOf(3);
        Assert.NotEqual(-1, removedElementIndex3);

        var removedElementIndex4 = set1UnionSet2.IndexOf(4);
        Assert.NotEqual(-1, removedElementIndex4);

        var removedElementIndex5 = set1UnionSet2.IndexOf(5);
        Assert.NotEqual(-1, removedElementIndex5);

        var removedElementIndex6 = set1UnionSet2.IndexOf(5);
        Assert.NotEqual(-1, removedElementIndex6);

    }

    [Fact]
    public void Intersection_ByDefault_CreatesNewSetContainingCommonElementsWithSet2()
    {
        // Arrange
        var set1 = new MathSet<int>();
        set1.Store(1);
        set1.Store(2);
        set1.Store(3);

        var set2 = new MathSet<int>();
        set2.Store(3);
        set2.Store(4);
        set2.Store(5);

        // Act
        var set1UnionSet2 = set1.Intersect(set2);

        // Assert

        var removedElementIndex3 = set1UnionSet2.IndexOf(3);
        Assert.NotEqual(-1, removedElementIndex3);
    }

    [Fact]
    public void Difference_ByDefault_CreatesNewSetContainingAllElementsOn2SetsWithoutRepetition()
    {
        // Arrange
        var set1 = new MathSet<int>();
        set1.Store(1);
        set1.Store(2);
        set1.Store(3);

        var set2 = new MathSet<int>();
        set2.Store(3);
        set2.Store(4);
        set2.Store(5);

        // Act
        var set1UnionSet2 = set1.Difference(set2);

        // Assert
        Assert.Equal(2, set1UnionSet2.Size);
        var removedElementIndex1 = set1UnionSet2.IndexOf(1);
        Assert.NotEqual(-1, removedElementIndex1);

        var removedElementIndex2 = set1UnionSet2.IndexOf(2);
        Assert.NotEqual(-1, removedElementIndex2);
    }

    [Fact]
    public void CartesianProduct_ByDefault_CreatesNewSetContainingCartesianProduct()
    {
        // Arrange
        var set1 = new MathSet<int>();
        set1.Store(1);
        set1.Store(2);

        var set2 = new MathSet<int>();
        set2.Store(3);
        set2.Store(4);

        // Act
        var set1UnionSet2 = set1.CartesianProduct(set2);

        // Assert
        Assert.Equal(4, set1UnionSet2.Size);
        var firstElement = set1UnionSet2.Fetch(0);
        Assert.Equal(firstElement, new int[] { 1, 3 });

        var secondElement = set1UnionSet2.Fetch(1);
        Assert.Equal(secondElement, new int[] { 1, 4 });

        var thirdElement = set1UnionSet2.Fetch(2);
        Assert.Equal(thirdElement, new int[] { 2, 3 });

        var fourthElement = set1UnionSet2.Fetch(3);
        Assert.Equal(fourthElement, new int[] { 2, 4 });

    }


    [Fact]
    public void NaturalProduct_ByDefault_CreatesNewSetContainingNaturalProduct()
    {
        // Arrange
        var set1 = new MathSet<int>();
        set1.Store(1);
        set1.Store(3);

        var set2 = new MathSet<int>();
        set2.Store(1);
        set2.Store(3);

        // Act
        var set1UnionSet2 = set1.NaturalProduct(set2);

        // Assert
        Assert.Equal(2, set1UnionSet2.Size);
        var firstElement = set1UnionSet2.Fetch(0);
        Assert.Equal(firstElement, new int[] { 1, 1 });

        var secondElement = set1UnionSet2.Fetch(1);
        Assert.Equal(secondElement, new int[] { 3, 3 });
    }
}