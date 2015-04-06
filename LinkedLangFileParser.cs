namespace OfisUpdater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LinkedLangFileParser
    {
        private readonly LangFileParser langFileParser;

        public LinkedLangFileParser(LangFileParser langFileParser)
        {
            if (langFileParser == null)
            {
                throw new ArgumentNullException("langFileParser");
            }

            this.langFileParser = langFileParser;
        }

        public LangFile Parse(string fileName)
        {
            var unlinkedFile = this.langFileParser.Parse(fileName);

            var resultSet = new List<TranslationSet>();

            // Assume that the top-down order has been kept, so that every child appears after it's parent.
            foreach (var set in unlinkedFile.TranslationSets)
            {
                if (set.Parents.Count == 0)
                {
                    resultSet.Add(set);
                }
                else
                {
                    var realParents = new List<TranslationSet>();

                    foreach (var parent in set.Parents)
                    {
                        foreach (var parentTranslation in parent.Translations)
                        {
                            foreach (var parentTranslationWord in parentTranslation.Words)
                            {
                                realParents.AddRange(resultSet.Where(ts => ts.Translations.Any(t => t.Language.Equals(parentTranslation.Language) && t.Words.Any(w => string.Compare(parentTranslationWord, w, true, parentTranslation.Language) == 0))));
                            }
                        }
                    }

                    resultSet.Add(new TranslationSet(realParents.Distinct().ToList(), set.Translations, set.Wikidata));
                }
            }

            return new LangFile(unlinkedFile.Stopwords, unlinkedFile.Synonyms, resultSet);
        }
    }
}
