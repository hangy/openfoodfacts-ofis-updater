namespace OfisUpdater
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class LangFileParser
    {
        private readonly ISingleLineParser<Stopwords> stopwordsParser;

        private readonly ISingleLineParser<Synonym> synonymsParser;

        private readonly IMultiLineParser<TranslationSet> translationSetParser;

        public LangFileParser(
            ISingleLineParser<Stopwords> stopwordsParser,
            ISingleLineParser<Synonym> synonymsParser,
            IMultiLineParser<TranslationSet> translationSetParser)
        {
            if (stopwordsParser == null)
            {
                throw new ArgumentNullException("stopwordsParser");
            }

            if (synonymsParser == null)
            {
                throw new ArgumentNullException("synonymsParser");
            }

            if (translationSetParser == null)
            {
                throw new ArgumentNullException("translationSetParser");
            }

            this.stopwordsParser = stopwordsParser;
            this.synonymsParser = synonymsParser;
            this.translationSetParser = translationSetParser;
        }

        public LangFile Parse(string fileName)
        {
            var stopwordList = new List<Stopwords>();
            var synonymList = new List<Synonym>();
            var translationSetList = new List<TranslationSet>();

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, true))
            {
                List<string> lines = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Stopwords stopwords;
                    if (this.stopwordsParser.TryParse(line, out stopwords))
                    {
                        stopwordList.Add(stopwords);
                        continue;
                    }

                    Synonym synonym;
                    if (this.synonymsParser.TryParse(line, out synonym))
                    {
                        synonymList.Add(synonym);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line) && lines.Count > 0)
                    {
                        TranslationSet translationSet;
                        if (this.translationSetParser.TryParse(lines, out translationSet))
                        {
                            translationSetList.Add(translationSet);
                            lines.Clear();
                            continue;
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(line))
                    {
                        lines.Add(line);
                    }
                }
            }

            return new LangFile(stopwordList, synonymList, translationSetList);
        }
    }
}
