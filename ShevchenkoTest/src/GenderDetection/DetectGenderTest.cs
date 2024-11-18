namespace ShevchenkoTest.GenderDetection;

using Shevchenko.Language;
using Shevchenko.GenderDetection;
using Shevchenko.AnthroponymDeclension;

public class TestDetectGender
{
    [Fact]
    public async void ShouldReturnTheGrammaticalGender()
    {
        Assert.Equal(GrammaticalGender.Masculine, await GenderDetector.DetectGender(new Anthroponym { GivenName = "Тарас" }));
        Assert.Equal(GrammaticalGender.Masculine, await GenderDetector.DetectGender(new Anthroponym { PatronymicName = "Григорович" }));
        Assert.Equal(GrammaticalGender.Masculine, await GenderDetector.DetectGender(new Anthroponym { GivenName = "Тарас", PatronymicName = "Григорович" }));
        Assert.Equal(GrammaticalGender.Masculine, await GenderDetector.DetectGender(new Anthroponym { GivenName = "Лариса", PatronymicName = "Григорович" }));
        Assert.Equal(GrammaticalGender.Feminine, await GenderDetector.DetectGender(new Anthroponym { GivenName = "Лариса" }));
        Assert.Equal(GrammaticalGender.Feminine, await GenderDetector.DetectGender(new Anthroponym { PatronymicName = "Петрівна" }));
        Assert.Equal(GrammaticalGender.Feminine, await GenderDetector.DetectGender(new Anthroponym { GivenName = "Лариса", PatronymicName = "Петрівна" }));
        Assert.Equal(GrammaticalGender.Feminine, await GenderDetector.DetectGender(new Anthroponym { GivenName = "Тарас", PatronymicName = "Петрівна" }));
    }

    [Fact]
    public async void ShouldReturnNullIfTheGrammaticalGenderCannotBeDetected()
    {
        Assert.Null(await GenderDetector.DetectGender(new Anthroponym { FamilyName = "Шевченко" }));
        Assert.Null(await GenderDetector.DetectGender(new Anthroponym { FamilyName = "Косач" }));
        Assert.Null(await GenderDetector.DetectGender(new Anthroponym { GivenName = "Тара" }));
        Assert.Null(await GenderDetector.DetectGender(new Anthroponym { GivenName = "Лара" }));
    }
}