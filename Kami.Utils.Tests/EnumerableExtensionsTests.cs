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
}