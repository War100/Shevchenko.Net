namespace ShevchenkoTest.AnthroponymDeclension.FamilyNameClassifier;

using Shevchenko.AnthroponymDeclension.FamilyNameClassifier;
using Shevchenko.Language;

public class TestFamilyNameClassTransformer
{
    private const float MinValueThreshold = 1e-7f;

        [Fact]
        public void Encode_ShouldEncodeNounWordClass()
        {
            var transformer = new FamilyNameClassTransformer();
            var familyNameClass = new FamilyNameClass { WordClass = WordClass.Noun };

            var result = transformer.Encode(familyNameClass);

            Assert.Equal(new byte[] { 1 }, result);
        }

        [Fact]
        public void Encode_ShouldEncodeAdjectiveWordClass()
        {
            var transformer = new FamilyNameClassTransformer();
            var familyNameClass = new FamilyNameClass { WordClass = WordClass.Adjective };

            var result = transformer.Encode(familyNameClass);

            Assert.Equal(new byte[] { 0 }, result);
        }

        [Fact]
        public void Encode_ShouldThrowArgumentException_WhenWordClassIsInvalid()
        {
            // Arrange
            var transformer = new FamilyNameClassTransformer();
            var familyNameClass = new FamilyNameClass { WordClass = (WordClass)99 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => transformer.Encode(familyNameClass));
        }

        [Theory]
        [InlineData(0.5f)]
        [InlineData(1f)]
        public void Decode_ShouldDecodeToNoun(float value)
        {
            var transformer = new FamilyNameClassTransformer();
            var vector = new float[] { value };

            var result = transformer.Decode(vector);

            Assert.Equal(WordClass.Noun, result.WordClass);
        }

        [Theory]
        [InlineData(0f)]
        [InlineData(0.5f - MinValueThreshold)]
        public void Decode_ShouldDecodeToAdjective(float value)
        {
            var transformer = new FamilyNameClassTransformer();
            var vector = new float[] { value };

            var result = transformer.Decode(vector);

            Assert.Equal(WordClass.Adjective, result.WordClass);
        }

        [Fact]
        public void Decode_ShouldThrowRangeErrorForInvalidLength()
        {
            var transformer = new FamilyNameClassTransformer();
            var vector = new float[] { 0f, 1f };

            Assert.Throws<ArgumentOutOfRangeException>(() => transformer.Decode(vector));
        }

        [Theory]
        [InlineData(-MinValueThreshold)]
        [InlineData(1f + MinValueThreshold)]
        public void Decode_ShouldThrowRangeErrorForOutOfRangeValues(float value)
        {
            var transformer = new FamilyNameClassTransformer();
            var vector = new float[] { value };

            Assert.Throws<ArgumentOutOfRangeException>(() => transformer.Decode(vector));
        }
}