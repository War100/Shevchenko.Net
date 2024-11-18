namespace Shevchenko.AnthroponymDeclension.FamilyNameClassifier
{
    using System;
    using Shevchenko.Language;
    public class FamilyNameClassTransformer
    {

        /// <summary>
        /// Кодує об'єкт FamilyNameClass у масив байтів.
        /// </summary>
        /// <param name="familyNameClass">Об'єкт FamilyNameClass для кодування.</param>
        /// <returns>Закодований масив байтів.</returns>
        public byte[] Encode(FamilyNameClass familyNameClass)
        {
            var values = new byte[1];

            switch (familyNameClass.WordClass)
            {
                case WordClass.Noun:
                    values[0] = 1;
                    break;
                case WordClass.Adjective:
                    values[0] = 0;
                    break;
                default:
                    throw new ArgumentException($"Invalid word class: \"{familyNameClass.WordClass}\".");
            }

            return values;
        }

        /// <summary>
        /// Декодує масив байтів у об'єкт FamilyNameClass.
        /// </summary>
        /// <param name="values">Масив байтів для декодування.</param>
        /// <returns>Об'єкт FamilyNameClass.</returns>
        public FamilyNameClass Decode(Array values)
        {
            if (values.Length != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Invalid vector length.");
            }

            var value = Convert.ToSingle(values.GetValue(0));

            if (value < 0 || value > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Invalid vector value.");
            }

            var wordClass = value >= 0.5f ? WordClass.Noun : WordClass.Adjective;

            return new FamilyNameClass { WordClass = wordClass };
        }    
    }
}