namespace OfisUpdater
{
    using OfisUpdater.Properties;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public abstract class LanguageWordListParser<TResult> : PrefixOnlyParser<TResult>
    {
        protected LanguageWordListParser(string prefix)
            : base(prefix)
        {
        }

        protected override bool TryParseWithoutPrefix(string lineWithoutPrefix, out TResult result)
        {
            result = default(TResult);

            var split = lineWithoutPrefix.Split(Settings.Default.LanguageSeparator);
            if (split.Length != 2)
            {
                return false;
            }

            var language = Culture.FromISOName(split[0].Trim());
            if (language == null)
            {
                return false;
            }

            var words = split[1].Trim().Split(Settings.Default.TermSeparator).Select(w => w.Trim()).ToList();
            result = this.BuildResult(language, words);
            return true;
        }

        protected abstract TResult BuildResult(CultureInfo language, IReadOnlyList<string> words);
    }
}
