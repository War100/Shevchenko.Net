namespace Shevchenko
{
    using System;
    using Shevchenko.Language;
    
    public class InputValidationError : Exception
    {
        public InputValidationError(string message) : base(message) { }
    }
    
    public static class InputValidation
    {
        /// <summary>
        /// Validates if a given value is a valid input for declension.
        /// </summary>
        /// <param name="value">The input value to validate.</param>
        /// <exception cref="InputValidationError">Thrown if the input is invalid.</exception>
        public static void ValidateDeclensionInput(AnthroponymDeclension.Anthroponym value)
        {
            if (value == null)
                throw new InputValidationError("The input type must be an object.");

            if (value.Gender != null && !Enum.IsDefined(typeof(GrammaticalGender), value.Gender))
            {
                throw new InputValidationError(
                    $"The \"gender\" parameter must be one of the following: " +
                    $"\"{GrammaticalGender.Masculine}\", \"{GrammaticalGender.Feminine}\".");
            }

            if (string.IsNullOrWhiteSpace(value.GivenName) &&
                string.IsNullOrWhiteSpace(value.PatronymicName) &&
                string.IsNullOrWhiteSpace(value.FamilyName))
            {
                throw new InputValidationError(
                    "At least one of the following parameters must be present: \"givenName\", \"patronymicName\", \"familyName\".");
            }

            if (!string.IsNullOrWhiteSpace(value.GivenName) && !IsString(value.GivenName))
            {
                throw new InputValidationError("The \"givenName\" parameter must be a string.");
            }

            if (!string.IsNullOrWhiteSpace(value.PatronymicName) && !IsString(value.PatronymicName))
            {
                throw new InputValidationError("The \"patronymicName\" parameter must be a string.");
            }

            if (!string.IsNullOrWhiteSpace(value.FamilyName) && !IsString(value.FamilyName))
            {
                throw new InputValidationError("The \"familyName\" parameter must be a string.");
            }
        }
        
        /// <summary>
        /// Validates if a given value is a valid input for gender detection.
        /// </summary>
        /// <param name="value">The input value to validate.</param>
        /// <exception cref="InputValidationError">Thrown if the input is invalid.</exception>
        public static void ValidateGenderDetectionInput(AnthroponymDeclension.Anthroponym value)
        {
            if (value == null)
                throw new InputValidationError("The input type must be an object.");

            if (string.IsNullOrWhiteSpace(value.GivenName) &&
                string.IsNullOrWhiteSpace(value.PatronymicName) &&
                string.IsNullOrWhiteSpace(value.FamilyName))
            {
                throw new InputValidationError(
                    "At least one of the following parameters must be present: \"givenName\", \"patronymicName\", \"familyName\".");
            }

            if (!string.IsNullOrWhiteSpace(value.GivenName) && !IsString(value.GivenName))
            {
                throw new InputValidationError("The \"givenName\" parameter must be a string.");
            }

            if (!string.IsNullOrWhiteSpace(value.PatronymicName) && !IsString(value.PatronymicName))
            {
                throw new InputValidationError("The \"patronymicName\" parameter must be a string.");
            }

            if (!string.IsNullOrWhiteSpace(value.FamilyName) && !IsString(value.FamilyName))
            {
                throw new InputValidationError("The \"familyName\" parameter must be a string.");
            }
        }

        private static bool IsString(object value) => value is string;
    }

}