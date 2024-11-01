using Shevchenko.Language;

namespace ShevchenkoTest.Language;

public class TestLetterCase
{
    [Fact]
    public void ShouldCopyLetterCaseFromGivenWord()
    {
        Assert.Equal("Шевченкові", LetterCase.CopyLetterCase("Шевченко", "шевченкові"));
        Assert.Equal("ШЕВЧЕНКОВІ", LetterCase.CopyLetterCase("ШЕВЧЕНКО", "шевченкові"));
        Assert.Equal("шевченкові", LetterCase.CopyLetterCase("шевченко", "ШЕВЧЕНКОВІ"));
    }
}