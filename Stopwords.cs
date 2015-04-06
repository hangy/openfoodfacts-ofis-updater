namespace OfisUpdater
{
    using System.Collections.Generic;
    using System.Globalization;

    public class Stopwords : LanguageWordList
    {
        public Stopwords(CultureInfo language, IReadOnlyList<string> words)
            : base(language, words)
        {
        }
    }
}
