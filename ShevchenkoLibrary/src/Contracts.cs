namespace Shevchenko
{
    using System;
    using Shevchenko.AnthroponymDeclension;
    using Shevchenko.Language;

    /// <summary>
    /// Вхідні параметри для відмінювання антропонімів.
    /// </summary>
    /// <example>
    /// <code>
    /// new DeclensionInput
    /// {
    ///     Gender = GrammaticalGender.Masculine,
    ///     GivenName = "Тарас",
    ///     PatronymicName = "Григорович",
    ///     FamilyName = "Шевченко"
    /// };
    /// </code>
    /// </example>
    public class DeclensionInput
    {
        public GrammaticalGender Gender { get; set; }
        public string GivenName { get; set; }
        public string PatronymicName { get; set; }
        public string FamilyName { get; set; }

        public static explicit operator Anthroponym(DeclensionInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return new Anthroponym
            {
                GivenName = input.GivenName,
                PatronymicName = input.PatronymicName,
                FamilyName = input.FamilyName
            };
        }
    }

    /// <summary>
    /// Результат відмінювання антропонімів.
    /// </summary>
    /// <example>
    /// <code>
    /// new DeclensionOutput
    /// {
    ///     GivenName = "Тарасе",
    ///     PatronymicName = "Григоровичу",
    ///     FamilyName = "Шевченку"
    /// };
    /// </code>
    /// </example>
    public class DeclensionOutput
    {
        public string GivenName { get; set; }
        public string PatronymicName { get; set; }
        public string FamilyName { get; set; }

        public static explicit operator Anthroponym(DeclensionOutput output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            return new Anthroponym()
            {
                GivenName = output.GivenName,
                PatronymicName = output.PatronymicName,
                FamilyName = output.FamilyName
            };
        }
    }

    /// <summary>
    /// Вхідні параметри для визначення граматичного роду.
    /// </summary>
    /// <example>
    /// <code>
    /// new GenderDetectionInput
    /// {
    ///     GivenName = "Тарас",
    ///     PatronymicName = "Григорович",
    ///     FamilyName = "Шевченко"
    /// };
    /// </code>
    /// </example>
    public class GenderDetectionInput
    {
        public string GivenName { get; set; }
        public string PatronymicName { get; set; }
        public string FamilyName { get; set; }

        public static explicit operator Anthroponym(GenderDetectionInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return new Anthroponym
            {
                GivenName = input.GivenName,
                PatronymicName = input.PatronymicName,
                FamilyName = input.FamilyName
            };
        }
    }

    /// <summary>
    /// Результат визначення граматичного роду.
    /// </summary>
    /// <example>
    /// <code>
    /// GrammaticalGender.Masculine
    /// </code>
    /// </example>
    public enum GenderDetectionOutput
    {
        Masculine = GrammaticalGender.Masculine,
        Feminine = GrammaticalGender.Feminine,
        Undefined = 0 // Represents `null` from the original TypeScript code.
    }
}
