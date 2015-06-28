namespace OfisUpdater
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public abstract class LanguageWordList : LanguageEntry
    {
        private readonly IReadOnlyList<string> words;

        public LanguageWordList(CultureInfo language, IReadOnlyList<string> words)
            : base(language)
        {
            if (words == null)
            {
                throw new ArgumentNullException("words");
            }

            this.words = words;
        }

        public IReadOnlyList<string> Words
        {
            get
            {
                return this.words;
            }
        }
    }
}
