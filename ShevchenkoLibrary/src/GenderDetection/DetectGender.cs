namespace Shevchenko.GenderDetection
{
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Shevchenko.AnthroponymDeclension;
    using Shevchenko.Language;

    public class GenderDetector
    {
        private static readonly Regex MasculinePatronymicPattern = new Regex(@"[иі]ч$", RegexOptions.Compiled);
        private static readonly Regex FemininePatronymicPattern = new Regex(@"на$", RegexOptions.Compiled);
        private static readonly Regex ApostropheVariationPattern = new Regex(@"[`""]", RegexOptions.Compiled);

        private static readonly Dictionary<string, GrammaticalGender> GivenNamesGenders
            = LoadGivenNamesGenders("Shevchenko.src.Resources.givenNamesGenders.json");

        /// <summary>
        /// Detects the grammatical gender of the anthroponym using
        /// patronymic name endings and the dictionary of known given names.
        /// </summary>
        /// <param name="anthroponym">The anthroponym to analyze.</param>
        /// <returns>
        /// The grammatical gender of the anthroponym, or <c>null</c> if the gender cannot be determined.
        /// </returns>
        public static async Task<GrammaticalGender?> DetectGender(Anthroponym anthroponym)
        {
            await Task.Yield();
            
            if (!string.IsNullOrEmpty(anthroponym.PatronymicName))
            {
                var patronymicName = anthroponym.PatronymicName.ToLowerInvariant();

                if (MasculinePatronymicPattern.IsMatch(patronymicName))
                {
                    return GrammaticalGender.Masculine;
                }
                else if (FemininePatronymicPattern.IsMatch(patronymicName))
                {
                    return GrammaticalGender.Feminine;
                }
            }

            if (!string.IsNullOrEmpty(anthroponym.GivenName))
            {
                var givenName = ApostropheVariationPattern.Replace(anthroponym.GivenName, "'").ToLowerInvariant();

                if (GivenNamesGenders.TryGetValue(givenName, out var gender))
                {
                    return gender;
                }
            }

            return null;
        }
        
        private static Dictionary<string, GrammaticalGender> LoadGivenNamesGenders(string filePath)
        {
            var json = ResourceReader.ReadEmbeddedResource(filePath);

            // Deserialization of JSON into dictionary
            var rawData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            if (rawData == null)
            {
                throw new InvalidOperationException("Failed to deserialize given names data.");
            }

            var genders = new Dictionary<string, GrammaticalGender>(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in rawData)
            {
                if (Enum.TryParse<GrammaticalGender>(entry.Value, true, out var gender))
                {
                    genders[entry.Key] = gender;
                }
                else
                {
                    throw new InvalidOperationException($"Unknown gender value: {entry.Value}");
                }
            }

            return genders;
        }
    }
}