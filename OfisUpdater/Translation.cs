namespace OfisUpdater
{
    using System.Collections.Generic;
    using System.Globalization;

    public class Translation : LanguageWordList
    {
        public Translation(CultureInfo language, IReadOnlyList<string> words)
            : base(language, words)
        {
        }
    }
}
