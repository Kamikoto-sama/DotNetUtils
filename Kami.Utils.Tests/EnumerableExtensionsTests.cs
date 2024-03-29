namespace Kami.Utils.Tests;

[TestFixture]
public class EnumerableExtensionsTests
{
    [Test]
    public void ToEnumerable_FromIntOne_ConsistsOfIntOne()
    {
        const int singleValueItem = 1;
        Assert.Multiple(() =>
        {
            Assert.That(singleValueItem.ToEnumerable().Count(), Is.EqualTo(1));
            Assert.That(singleValueItem.ToEnumerable().Single(), Is.EqualTo(singleValueItem));
        });
    }

    [TestCase(new[] { 1 }, true)]
    [TestCase(new[] { 1, 2, 3 }, true)]
    [TestCase(new[] { 0, 2 }, false)]
    [TestCase(new[] { 2, 3 }, false)]
    [TestCase(new[] { 1, 2, 3, 4 }, false)]
    public void StartsWith_ReturnsTrue_WhenStartsWith(int[] comparisonSeq, bool expectedResult)
    {
        var source = new[] { 1, 2, 3 };

        var result = source.StartsWith(comparisonSeq);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SelectMany_ActsAsLinqSelectMany()
    {
        var sequenceOfSequences = new[]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 }
        };

        var result = sequenceOfSequences.SelectMany();
        var expected = sequenceOfSequences.SelectMany(x => x);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Shuffle_ReturnsShuffledCollection()
    {
        var sourceArray = new[] { 1, 2, 3, 4, 5 };
        var shuffledKeys = new[] { 3, 5, 1, 4, 2 };
        var index = 0;

        var shuffledArray = sourceArray.Shuffle(() => shuffledKeys[index++]);

        Assert.That(shuffledArray, Is.EquivalentTo(shuffledKeys));
    }
}