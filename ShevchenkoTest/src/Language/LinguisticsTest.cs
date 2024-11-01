using Shevchenko.Language;

namespace ShevchenkoTest.Language;

public class TestLinguistics
{
    [Fact]
    public void ShouldReturnTrueIfTheGivenWordIsMonosyllable()
    {
        Assert.True(Linguistics.IsMonosyllable("Драй"));
    }
    
    [Fact]
    public void ShouldReturnFalseIfTheGivenWordIsNotMonosyllable()
    {
        Assert.False(Linguistics.IsMonosyllable("Хмара"));
    }
}