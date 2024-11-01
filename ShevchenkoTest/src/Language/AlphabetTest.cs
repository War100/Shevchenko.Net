using Shevchenko.Language;

namespace ShevchenkoTest.Language;

public class TestAlphabet
{
    [Fact]
    public void ShouldEncodeAlphabetLettersFrom1To33()
    {
        Assert.Equal(1, (int)AlphabetEncoding.А);
        Assert.Equal(33, (int)AlphabetEncoding.Я);
    }

    [Fact]
    public void AlphabetSizeShouldBeEqualTo33()
    {
        Assert.Equal(33, AlphabetConstants.ALPHABET_SIZE);
    }
}