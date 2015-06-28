namespace OfisUpdater
{
    using System;
    using System.Collections.Generic;

    public class LangFile
    {
        private readonly IReadOnlyList<Stopwords> stopwords;

        private readonly IReadOnlyList<Synonym> synonyms;

        private readonly IReadOnlyList<TranslationSet> translationSets;

        public LangFile(IReadOnlyList<Stopwords> stopwords, IReadOnlyList<Synonym> synonyms, IReadOnlyList<TranslationSet> translationSets)
        {
            if (stopwords == null)
            {
                throw new ArgumentNullException("stopwords");
            }

            if (synonyms == null)
            {
                throw new ArgumentNullException("synonyms");
            }

            if (translationSets == null)
            {
                throw new ArgumentNullException("translationSets");
            }

            this.stopwords = stopwords;
            this.synonyms = synonyms;
            this.translationSets = translationSets;
        }

        public IReadOnlyList<Stopwords> Stopwords
        {
            get
            {
                return this.stopwords;
            }
        }

        public IReadOnlyList<Synonym> Synonyms
        {
            get
            {
                return this.synonyms;
            }
        }

        public IReadOnlyList<TranslationSet> TranslationSets
        {
            get
            {
                return this.translationSets;
            }
        }
    }
}
