namespace OfisUpdater
{
    using System;
    using System.Collections.Generic;

    public class TranslationSet
    {
        private readonly IReadOnlyList<TranslationSet> parents;

        private readonly IReadOnlyList<Translation> translations;

        private readonly string wikidata;

        public TranslationSet(IReadOnlyList<TranslationSet> parents, IReadOnlyList<Translation> translations, string wikidata)
        {
            if (parents == null)
            {
                throw new ArgumentNullException("parents");
            }

            if (translations == null)
            {
                throw new ArgumentNullException("translations");
            }

            this.parents = parents;
            this.translations = translations;
            this.wikidata = wikidata;
        }

        public IReadOnlyList<TranslationSet> Parents
        {
            get
            {
                return this.parents;
            }
        }

        public IReadOnlyList<Translation> Translations
        {
            get
            {
                return this.translations;
            }
        }

        public string Wikidata
        {
            get
            {
                return this.wikidata;
            }
        }
    }
}
