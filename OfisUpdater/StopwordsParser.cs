namespace OfisUpdater
{
    using System.Collections.Generic;
    using System.Globalization;

    public class StopwordsParser : LanguageWordListParser<Stopwords>
    {
        public StopwordsParser()
            : base("stopwords")
        {
        }

        protected override Stopwords BuildResult(CultureInfo language, IReadOnlyList<string> words)
        {
            return new Stopwords(language, words);
        }
    }
}
