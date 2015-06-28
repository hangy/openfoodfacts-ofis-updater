namespace OfisUpdater
{
    using System.Collections.Generic;
    using System.Globalization;

    public class SynonymsParser : LanguageWordListParser<Synonym>
    {
        public SynonymsParser()
            : base("synonyms")
        {
        }

        protected override Synonym BuildResult(CultureInfo language, IReadOnlyList<string> words)
        {
            return new Synonym(language, words);
        }
    }
}
