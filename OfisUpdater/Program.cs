namespace OfisUpdater
{
    using OffLangParser;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = new FileStream(@"labels_old.txt", FileMode.Open, FileAccess.Read, FileShare.None, 1, true))
            {
                var f = new LinkedLangFileParser(
                    new LangFileParser(
                        new StopWordsParser(),
                        new SynonymsParser(),
                        new TranslationSetParser(
                            new TranslationParser(),
                            new LinkedDataParser(new List<PrefixOnlyParser<LinkedData>>(0)))))
                    .Parse(stream, Encoding.UTF8);
                var euOrganic = f.TranslationSets.Single(ts => ts.Translations.Any(t => t.Language.Name == "en" && t.Words.Any(w => t.Language.CompareInfo.Compare("EU Organic", w, CompareOptions.IgnoreCase) == 0)));

                var euOrganicChildren = f.TranslationSets.Where(ts => ts.Parents.Any(p => p == euOrganic));
                var words = euOrganicChildren.Select(t => t.Translations[0].Words[0]);

                var ofis = new OfisParser().Parse(@"ofis.csv");
                var @new = ofis.Where(x => !words.Any(w => w.Equals(x.Code, StringComparison.OrdinalIgnoreCase)));

                var codes = @new.Select(x => x.Code).Union(words);

                using (var @out = new StreamWriter(@"labels_new.txt"))
                {
                    foreach (var code in codes.OrderBy(c => c))
                    {
                        @out.WriteLine("< en:EU Organic");
                        @out.Write("en:");
                        @out.WriteLine(code);
                        @out.WriteLine();
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
