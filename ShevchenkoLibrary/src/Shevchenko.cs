namespace Shevchenko
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Shevchenko.Language;
    using Shevchenko.GenderDetection;
    using Shevchenko.WordDeclension;
    using Shevchenko.AnthroponymDeclension;
    
    public class AnthroponymInflection
    {
        private static IEnumerable<DeclensionRule> Rules()
        {
            var rulesDataPath = "Shevchenko.src.Resources.declension-rules.json";
            var jsonData = ResourceReader.ReadEmbeddedResource(rulesDataPath);
            var rules = JsonConvert.DeserializeObject<List<DeclensionRule>>(jsonData);

            if (rules != null) return rules;
            throw new InvalidOperationException();
        }
        
        private static AnthroponymInflector CreateInflector()
        {
            var wordInflector = new WordInflector(Rules());
            var familyNameClassifier = new Shevchenko.AnthroponymDeclension.FamilyNameClassifier.FamilyNameClassifier();
            var givenNameInflector = new GivenNameInflector(wordInflector);
            var patronymicNameInflector = new PatronymicNameInflector(wordInflector);
            var familyNameInflector = new FamilyNameInflector(wordInflector, familyNameClassifier);
            AnthroponymInflector inflector = new AnthroponymInflector(
                givenNameInflector,
                patronymicNameInflector,
                familyNameInflector
            );
            return inflector;
        }
        
        private static AnthroponymInflector _anthroponymInflector = CreateInflector();
        
        /// <summary>
        /// Inflects an anthroponym in nominative grammatical case.
        /// </summary>
        /// <example>
        /// var anthroponym = await AnthroponymInflector.InNominativeAsync(new Anthroponym
        /// {
        ///     Gender = GrammaticalGender.Masculine,
        ///     GivenName = "Тарас",
        ///     PatronymicName = "Григорович",
        ///     FamilyName = "Шевченко"
        /// });
        /// </example>
        public static async Task<Anthroponym> InNominativeAsync(AnthroponymDeclension.Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Nominative);
        }

        /// <summary>
        /// Inflects an anthroponym in genitive grammatical case.
        /// </summary>
        public static async Task<Anthroponym> InGenitiveAsync(Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Genitive);
        }

        /// <summary>
        /// Inflects an anthroponym in dative grammatical case.
        /// </summary>
        public static async Task<Anthroponym> InDativeAsync(Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Dative);
        }

        /// <summary>
        /// Inflects an anthroponym in accusative grammatical case.
        /// </summary>
        public static async Task<Anthroponym> InAccusativeAsync(Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Accusative);
        }

        /// <summary>
        /// Inflects an anthroponym in ablative grammatical case.
        /// </summary>
        public static async Task<Anthroponym> InAblativeAsync(Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Ablative);
        }

        /// <summary>
        /// Inflects an anthroponym in locative grammatical case.
        /// </summary>
        public static async Task<Anthroponym> InLocativeAsync(Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Locative);
        }

        /// <summary>
        /// Inflects an anthroponym in vocative grammatical case.
        /// </summary>
        public static async Task<Anthroponym> InVocativeAsync(Anthroponym input)
        {
            InputValidation.ValidateDeclensionInput(input);
            return await _anthroponymInflector.InflectAsync(input, input.Gender, GrammaticalCase.Vocative);
        }

        /// <summary>
        /// Detects the grammatical gender of an anthroponym.
        /// </summary>
        public static async Task<GrammaticalGender?> DetectGenderAsync(Anthroponym input)
        {
            InputValidation.ValidateGenderDetectionInput(input);
            return await GenderDetector.DetectGender(input);
        }
    }
}