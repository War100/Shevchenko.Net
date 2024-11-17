namespace ShevchenkoTest.AnthroponymDeclension.FamilyNameClassifier;

using Shevchenko.AnthroponymDeclension.FamilyNameClassifier;

public class TestWordTransformer
{
    [Fact]
    public void Encode_ShouldEncodeIntoSameSizeVector()
    {
        // Arrange
        var word = "шевченко";
        var wordTransformer = new WordTransformer(word.Length);

        // Act
        var result = wordTransformer.Encode(word);

        // Assert
        Assert.Equal(new byte[] { 29, 7, 3, 28, 7, 18, 15, 19 }, result);
    }

    [Fact]
    public void Encode_ShouldEncodeIntoLargerSizeVector()
    {
        // Arrange
        var word = "шевченко";
        var wordTransformer = new WordTransformer(word.Length + 5);

        // Act
        var result = wordTransformer.Encode(word);

        // Assert
        Assert.Equal(new byte[] { 0, 0, 0, 0, 0, 29, 7, 3, 28, 7, 18, 15, 19 }, result);
    }

    [Fact]
    public void Encode_ShouldEncodeIntoSmallerSizeVector()
    {
        // Arrange
        var word = "шевченко";
        var wordTransformer = new WordTransformer(word.Length - 5);

        // Act
        var result = wordTransformer.Encode(word);

        // Assert
        Assert.Equal(new byte[] { 18, 15, 19 }, result);
    }

    [Fact]
    public void Encode_ShouldIgnoreLetterCase()
    {
        // Arrange
        var word = "шевченко";
        var wordTransformer = new WordTransformer(word.Length);

        // Act
        var resultLowerCase = wordTransformer.Encode(word);
        var resultUpperCase = wordTransformer.Encode(word.ToUpperInvariant());

        // Assert
        Assert.Equal(resultLowerCase, resultUpperCase);
    }
}