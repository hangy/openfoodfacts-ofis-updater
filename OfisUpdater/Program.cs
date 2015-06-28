namespace OfisUpdater
{
    using System;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var f = new LinkedLangFileParser(new LangFileParser(new StopwordsParser(), new SynonymsParser(), new TranslationSetParser(new TranslationParser()))).Parse(@"labels_old.txt");
            var euOrganic = f.TranslationSets.Single(ts => ts.Translations.Any(t => t.Language.TwoLetterISOLanguageName == "en" && t.Words.Any(w => string.Compare("EU Organic", w, true, t.Language) == 0)));

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
