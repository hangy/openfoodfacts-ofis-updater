namespace OfisUpdater
{
    using OfisUpdater.Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TranslationSetParser : IMultiLineParser<TranslationSet>
    {
        private readonly ISingleLineParser<Translation> translationParser;

        public TranslationSetParser(ISingleLineParser<Translation> translationParser)
        {
            if (translationParser == null)
            {
                throw new ArgumentNullException("translationParser");
            }

            this.translationParser = translationParser;
        }

        public bool TryParse(IReadOnlyList<string> lines, out TranslationSet result)
        {
            if (lines == null)
            {
                throw new ArgumentNullException("lines");
            }

            const string wikidataPrefix = "wikidata:en:";
            var parents = lines.Where(l => l.StartsWith(Settings.Default.ParentIndicator));
            var values = lines.Where(l => !l.StartsWith(Settings.Default.ParentIndicator) && !l.StartsWith(wikidataPrefix));
            var wikidata = lines.Where(l => !l.StartsWith(Settings.Default.ParentIndicator) && l.StartsWith(wikidataPrefix)).SingleOrDefault();

            var parentSets = new List<TranslationSet>();
            foreach (var line in parents)
            {
                var trimmedLine = line.Substring(Settings.Default.ParentIndicator.Length, line.Length - Settings.Default.ParentIndicator.Length).Trim();

                var parentSetTranslations = new List<Translation>();
                Translation translation;
                if (this.translationParser.TryParse(trimmedLine, out translation))
                {
                    parentSetTranslations.Add(translation);
                }

                parentSets.Add(new TranslationSet(new List<TranslationSet>(0), parentSetTranslations, null));
            }

            var translations = new List<Translation>();
            foreach (var line in values)
            {
                Translation translation;
                if (this.translationParser.TryParse(line, out translation))
                {
                    translations.Add(translation);
                }
            }

            result = new TranslationSet(parentSets, translations, wikidata);
            return true;
        }
    }
}
