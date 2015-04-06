namespace OfisUpdater
{
    using System;
    using System.Globalization;

    public abstract class LanguageEntry
    {
        private readonly CultureInfo language;

        protected LanguageEntry(CultureInfo language)
        {
            if (language == null)
            {
                throw new ArgumentNullException("language");
            }

            this.language = language;
        }

        public CultureInfo Language
        {
            get
            {
                return this.language;
            }
        }
    }
}
