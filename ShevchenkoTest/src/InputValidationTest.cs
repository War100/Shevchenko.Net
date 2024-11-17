namespace ShevchenkoTest;

using Shevchenko;
using Shevchenko.Language;
using Shevchenko.WordDeclension;
using Shevchenko.AnthroponymDeclension;
using Shevchenko.GenderDetection;

public class TestInputValidation
{
    [Fact]
        public void ValidateDeclensionInput_ShouldThrowError_WhenInputIsNull()
        {
            Assert.Throws<InputValidationError>(() => InputValidation.ValidateDeclensionInput(null));
        }

        [Fact]
        public void ValidateDeclensionInput_ShouldThrowError_WhenUnsupportedGenderProvided()
        {
            var input = new DeclensionInput
            {
                Gender = (GrammaticalGender)999, // Непідтримуваний enum
                GivenName = "Тарас",
                PatronymicName = "Григорович",
                FamilyName = "Шевченко"
            };

            var exception = Assert.Throws<InputValidationError>(() =>
                InputValidation.ValidateDeclensionInput(input));

            Assert.Equal(
                "The \"gender\" parameter must be one of the following: \"Masculine\", \"Feminine\".",
                exception.Message);
        }

        [Fact]
        public void ValidateDeclensionInput_ShouldThrowError_WhenNoNameParametersProvided()
        {
            var input = new DeclensionInput
            {
                Gender = GrammaticalGender.Masculine
            };

            var exception = Assert.Throws<InputValidationError>(() =>
                InputValidation.ValidateDeclensionInput(input));

            Assert.Equal(
                "At least one of the following parameters must be present: \"givenName\", \"patronymicName\", \"familyName\".",
                exception.Message);
        }

        [Fact]
        public void ValidateDeclensionInput_ShouldPass_WhenValidGivenNameProvided()
        {
            var input = new DeclensionInput
            {
                Gender = GrammaticalGender.Masculine,
                GivenName = "Тарас"
            };

            var exception = Record.Exception(() =>
                InputValidation.ValidateDeclensionInput(input));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateDeclensionInput_ShouldPass_WhenValidPatronymicNameProvided()
        {
            var input = new DeclensionInput
            {
                Gender = GrammaticalGender.Masculine,
                PatronymicName = "Григорович"
            };

            var exception = Record.Exception(() =>
                InputValidation.ValidateDeclensionInput(input));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateDeclensionInput_ShouldPass_WhenValidFamilyNameProvided()
        {
            var input = new DeclensionInput
            {
                Gender = GrammaticalGender.Masculine,
                FamilyName = "Шевченко"
            };

            var exception = Record.Exception(() =>
                InputValidation.ValidateDeclensionInput(input));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateGenderDetectionInput_ShouldThrowError_WhenInputIsNull()
        {
            Assert.Throws<InputValidationError>(() => InputValidation.ValidateGenderDetectionInput(null));
        }

        [Fact]
        public void ValidateGenderDetectionInput_ShouldThrowError_WhenNoNameParametersProvided()
        {
            var input = new GenderDetectionInput();

            var exception = Assert.Throws<InputValidationError>(() =>
                InputValidation.ValidateGenderDetectionInput(input));

            Assert.Equal(
                "At least one of the following parameters must be present: \"givenName\", \"patronymicName\", \"familyName\".",
                exception.Message);
        }

        [Fact]
        public void ValidateGenderDetectionInput_ShouldPass_WhenValidGivenNameProvided()
        {
            var input = new GenderDetectionInput
            {
                GivenName = "Тарас"
            };

            var exception = Record.Exception(() =>
                InputValidation.ValidateGenderDetectionInput(input));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateGenderDetectionInput_ShouldPass_WhenValidFullNameProvided()
        {
            var input = new GenderDetectionInput
            {
                GivenName = "Тарас",
                PatronymicName = "Григорович",
                FamilyName = "Шевченко"
            };

            var exception = Record.Exception(() =>
                InputValidation.ValidateGenderDetectionInput(input));

            Assert.Null(exception);
        }
    }