using Shevchenko.Language;

namespace ShevchenkoTest.AnthroponymDeclension.FamilyNameClassifier;

using Shevchenko.AnthroponymDeclension.FamilyNameClassifier;

public class TestFamilyNameClassifier
{
        [Fact]
        public void Classify_ShouldReturnCorrectClass_ForKnownWords()
        {
            // Arrange
            var classifier = new FamilyNameClassifier();

            // Act & Assert
            Assert.Equal(WordClass.Adjective, classifier.Classify("дручинська").WordClass);
            Assert.Equal(WordClass.Noun, classifier.Classify("шевченко").WordClass);
            Assert.Equal(WordClass.Noun, classifier.Classify("козак").WordClass);
            Assert.Equal(WordClass.Adjective, classifier.Classify("гарна").WordClass);
        }

        [Fact]
        public void Classify_ShouldBeCaseInsensitive_ForKnownWords()
        {
            // Arrange
            var classifier = new FamilyNameClassifier();

            // Act & Assert
            Assert.Equal(WordClass.Adjective, classifier.Classify("ДРУЧИНСЬКА").WordClass);
            Assert.Equal(WordClass.Noun, classifier.Classify("Шевченко").WordClass);
        }

        [Fact]
        public void Classify_ShouldReturnUnknown_ForUnknownWords()
        {
            // Arrange
            var classifier = new FamilyNameClassifier();

            // Act
            var result = classifier.Classify("незнайоме");

            // Assert
            Assert.Equal(WordClass.Unknown, result.WordClass);
        }

        [Fact]
        public void Classify_ShouldApplyHeuristics_ForUnknownWords()
        {
            // Arrange
            var classifier = new FamilyNameClassifier();

            // Act & Assert
            Assert.Equal(WordClass.Adjective, classifier.Classify("красивий").WordClass);
            Assert.Equal(WordClass.Noun, classifier.Classify("іваненко").WordClass);
            Assert.Equal(WordClass.Noun, classifier.Classify("кучерук").WordClass);
        }

        [Fact]
        public void Classify_ShouldThrowArgumentException_ForEmptyInput()
        {
            // Arrange
            var classifier = new FamilyNameClassifier();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => classifier.Classify(string.Empty));
        }

        [Fact]
        public void Classify_ShouldThrowArgumentException_ForNullInput()
        {
            // Arrange
            var classifier = new FamilyNameClassifier();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => classifier.Classify(null));
        }
}