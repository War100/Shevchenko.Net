using System.Reflection;

namespace Shevchenko.AnthroponymDeclension.FamilyNameClassifier
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Shevchenko.Language;

    public class FamilyNameClassifier
    {
        private readonly string _csvFilePath = "Shevchenko.src.Resources.training.csv";
        private readonly Dictionary<string, FamilyNameClass> _wordClasses;

        /// <summary>
        /// Ініціалізує новий екземпляр <see cref="FamilyNameClassifier"/> із вказаним CSV-файлом.
        /// </summary>
        public FamilyNameClassifier()
        {
            _wordClasses = LoadFromEmbeddedCsv(_csvFilePath);
        }

        /// <summary>
        /// Класифікує слово за частиною мови.
        /// </summary>
        /// <param name="word">Слово для класифікації.</param>
        /// <returns>Класифікована частина мови.</returns>
        public FamilyNameClass Classify(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Слово не може бути порожнім або null.", nameof(word));

            // Перевіряємо в словнику
            if (_wordClasses.TryGetValue(word.ToLowerInvariant(), out var wordClass))
            {
                return wordClass;
            }

            // Евристика для визначення
            return ApplyHeuristics(word);
        }

        /// <summary>
        /// Applies heuristic rules to identify parts of speech.
        /// </summary>
        /// <param name="word">A word for analysis.</param>
        /// <returns>A part of speech defined by rules.</returns>
        private FamilyNameClass ApplyHeuristics(string word)
        {
            var nounEndingsLong = new List<string>
            { 
                "уя"   
            };
            
            // Перевірка на типові закінчення прикметників
            var adjectiveEndings = new List<string>
            {
                "ий", "ій", "нй", "ова", "ове", "ава", "а", "я", "є", "єй", "нний", "йна", "ський", "ська", "цький",
                "цька", "ста" , "ша", "ше"
            };

            // Перевірка на типові закінчення іменників
            var nounEndings = new List<string>
            {
                "ко", "енко", "чук", "ів", "ов", "ев", "юк", "ук", "ак", "як"
            };
            
            // Перевірка на іменники закінчення яких пересікається з прикметниками
            if (nounEndingsLong.Any(ending => word.EndsWith(ending)))
            {
                return new FamilyNameClass { WordClass = WordClass.Noun };
            }

            // Перевірка на прикметники
            if (adjectiveEndings.Any(ending => word.EndsWith(ending)))
            {
                return new FamilyNameClass { WordClass = WordClass.Adjective };
            }

            // Перевірка на іменники
            if (nounEndings.Any(ending => word.EndsWith(ending)))
            {
                return new FamilyNameClass { WordClass = WordClass.Noun };
            }

            // Якщо жодна з умов не підходить, повертаємо "unknown"
            return new FamilyNameClass { WordClass = WordClass.Unknown };
        }

        /// <summary>
        /// Завантажує дані із вбудованого CSV-файлу у словник.
        /// </summary>
        /// <returns>Словник із даними про слова та їх класи.</returns>
        private Dictionary<string, FamilyNameClass> LoadFromEmbeddedCsv(string csvResource)
        {
            var dictionary = new Dictionary<string, FamilyNameClass>();

            // Отримуємо поточну збірку
            var assembly = typeof(FamilyNameClassifier).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(csvResource))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Ресурс '{csvResource}' не знайдено.");
                }

                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            var word = parts[0].Trim().ToLowerInvariant();
                            var wordClassString = parts[1].Trim().ToLowerInvariant();

                            WordClass wordClass;
                            switch (wordClassString)
                            {
                                case "noun":
                                    wordClass = WordClass.Noun;
                                    break;
                                case "adjective":
                                    wordClass = WordClass.Adjective;
                                    break;
                                default:
                                    wordClass = WordClass.Unknown;
                                    break;
                            }

                            dictionary[word] = new FamilyNameClass { WordClass = wordClass };
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}
