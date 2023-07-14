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
}