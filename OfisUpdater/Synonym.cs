namespace OfisUpdater
{
    using System.Collections.Generic;
    using System.Globalization;

    public class Synonym : LanguageWordList
    {
        public Synonym(CultureInfo language, IReadOnlyList<string> words)
            : base(language, words)
        {
        }
    }
}
